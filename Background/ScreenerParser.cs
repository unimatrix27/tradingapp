using System;
using System.IO;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using trading.Persistence;
using System.Linq;
using System.Net;
using trading.Models;
using Microsoft.Extensions.Logging;
using ImageMagick;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Trading.Persistence;
using Trading.Persistence.Interfaces;

namespace trading.Background
{
    public class ScreenerParser
    {
        private IUnitOfWork uow;
        private ILogger<YahooDownloader> logger;

        public ScreenerParser(IUnitOfWork uow, ILogger<YahooDownloader> logger) { 
           this.uow = uow;
            this.logger = logger;
        }

        public enum Direction
        {
            right,
            left,
            up,
            down,
            none
        }

        public static int[] FindLineInColor(MagickImage img, MagickColor[] colors = null, int startx=0, int starty=0, int length = 5,
            Direction lookDirection = Direction.down, Direction MoveDirection=Direction.down, bool negative = false, int allowedErrors = 0)
        {
            MagickImage clone = null;
            if (colors == null)
            {
                colors = new MagickColor[1];
                colors[0] = new MagickColor("white");
            }
            bool match = negative;
            bool error = false;
            int errorcount = 0;
            while (error == false && match == negative && starty <= img.Height && startx <= img.Width)
            {
                //if (clone != null) clone.Dispose();
                clone = img.Clone();
                match = true;
                try
                {
                    switch (lookDirection)
                    {
                        case Direction.right:
                            clone.Crop(startx, starty, length, 1);
                            break;
                        case Direction.left:
                            clone.Crop(startx - length, starty, length, 1);
                            break;
                        case Direction.up:
                            clone.Crop(startx, starty - length, 1, length);
                            break;
                        case Direction.down:
                            clone.Crop(startx, starty, 1, length);
                            break;
                    }
                    using (var pixels = clone.GetPixels())
                    {
                        foreach (var pix in pixels)
                        {
                            var col = pix.ToColor();
                            errorcount = 0;
                            foreach (var refcol in colors)
                            {
                                if (col.R == refcol.R && col.B == refcol.B && col.G == refcol.G)
                                {
                                    errorcount++;
                                    break;
                                }
                            }
                            if (errorcount <= allowedErrors)
                            {
                                
                                match = false;
                                break;
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    //Console.WriteLine(ex);
                    match = false;
                    error = true;
                }
                if (match == negative)
                {
                    switch (MoveDirection)
                    {
                        case Direction.right:
                            startx++;
                            break;
                        case Direction.left:
                            startx--;
                            break;
                        case Direction.up:
                            starty--;
                            break;
                        case Direction.down:
                            starty++;
                            break;
                        case Direction.none:
                            return null;
                    }
                }
            }

            // ist genug platz zum schauen
            if (match == negative || startx > img.Width  || starty > img.Height)
                return null;
            return new[] { startx, starty };
        }
        
        public void ParseScreener(IJobCancellationToken cancellationToken, int screenerId)
        {
            int totalLinesMatched = 0;
            List<int> lines = new List<int>();
            SortedDictionary<int, int> columns = new SortedDictionary<int, int>();
            SortedDictionary<int, int> newcolumns = new SortedDictionary<int, int>();

            KeyValuePair<int, int>[] columnlist;

            int counter = 0;
            MagickImage image = null;
            MagickImage smallimage = null;


            var result = new[] { 0, 0 };
            int topwhite = 0;
            int leftstart = 0;
            int height = 0;
            Collection<double> errors = new Collection<double>();

            var screener = uow.Screeners.GetFull(screenerId);
            if (screener == null) return;
            string logCat = "ScreenerParser " + screenerId + " ";
            logger.LogInformation(logCat + "starting, removing "+screener.ScreenerLines.Count()+" lines");
            // bestehende Entries und Lines löschen, falls der Screener schonmal eingelesen wurde
            uow.ScreenerLines.RemoveRange(screener.ScreenerLines);
            uow.Complete();

            // Müll aufräumen falls vorhanden
            lines.Clear();
            columns.Clear();
            newcolumns.Clear();
            if (image != null) image.Dispose();

            // Inits
            result[0] = 0;
            result[1] = 0;

            MagickColor[] whiteyellow = new MagickColor[2];
            whiteyellow[0] = new MagickColor(255,255,0);
            whiteyellow[1] = new MagickColor(255, 255, 255);


            // Image Datei einlesen
            //Console.WriteLine(screener.ImageFile);
            image = new MagickImage(screener.ImageFile);

            // find the first line in the picture which is not fully white (in most cases this line does not exist, but in some it does)
            // but first skip the first few columsn of grey if they exist


            smallimage = image.Clone();
            smallimage.Crop(0, 0, 300, 80);

            int firstLines = 0;
            result = null;
            while (result == null || result?[0] > 200)
            {
                result = FindLineInColor(smallimage, startx: 20, starty: firstLines,
                        lookDirection: Direction.right, MoveDirection: Direction.right, length: 95,
                        negative: false);
                firstLines++;
            }
            firstLines--;
            if (firstLines > 19)
            {
                screener.ParseError = true;
                screener.ParseErrorString = "Keine weisse Linine gefunden";
                uow.Complete();
                throw new Exception(screener.ParseErrorString);
            }

            try
            {
                result = FindLineInColor(smallimage, starty: firstLines, lookDirection: Direction.right,
                    MoveDirection: Direction.right, length: 15, negative: false);

                result = FindLineInColor(smallimage, starty:result[1],startx: result[0], lookDirection: Direction.right,
                    length: 15, negative: true);

                //find the first white row below the top grey border
                result = FindLineInColor(smallimage, startx: 15, starty: result[1],
                    lookDirection: Direction.right, length: 15);
                topwhite = result[1];
                result = FindLineInColor(smallimage, startx: result[0], starty: result[1], negative: true,
                    MoveDirection: Direction.left);
                result[0]++;
                //Console.WriteLine("Linksweiss: {0}", result[0]);

                leftstart = result[0];
                logger.LogInformation(logCat + "topwhite " + topwhite + " leftstart "+leftstart);
                result = FindLineInColor(image, startx: result[0], starty: result[1], length: 1,
                    negative: true, colors: whiteyellow);
                //Console.WriteLine("x: {0}, y: {1}", result[0], result[1]);
                height = result[1];
                lines.Add(height + 1);
                result = FindLineInColor(image, startx: result[0], starty: result[1] + 1, length: 1,
                    negative: true,colors:whiteyellow);
                //Console.WriteLine("x: {0}, y: {1}", result[0], result[1]);
                height = result[1] - height - 1;
               
                lines.Add(result[1] + 1);
                //Console.WriteLine("hoehe {0}", height);
                if (height < 5)
                {
                    lines.Clear();
                    result = FindLineInColor(smallimage, startx: leftstart+10, starty: topwhite, lookDirection: Direction.down, MoveDirection:Direction.right ,negative: false,length:25);
                    result = FindLineInColor(smallimage, startx: result[0], starty: topwhite, lookDirection: Direction.down, MoveDirection: Direction.right, negative: true, length: 5);
                    result[0]--;
                    result = FindLineInColor(image, startx: result[0], starty: result[1], length: 1,
                    negative: true);
                    height = result[1];
                    lines.Add(height + 1);
                    result = FindLineInColor(image, startx: result[0], starty: result[1] + 1, length: 1,
                        negative: true, colors: whiteyellow);
                    //Console.WriteLine("x: {0}, y: {1}", result[0], result[1]);
                    height = result[1] - height - 1;
                    lines.Add(result[1] + 1);

                }
                if (height < 10)
                {
                    screener.ParseError = true;
                    screener.ParseErrorString = "height kleiner 10";
                    uow.Complete();
                    return;
                }
                logger.LogInformation(logCat + "height " + height);
                smallimage.Dispose();
                smallimage = image.Clone();
                smallimage.Crop(result[0], 1, 1, image.Height);
                smallimage.RePage();
                while (result[1] + height < image.Height)
                {
                    result = FindLineInColor(smallimage, starty: result[1] + 1, length: 1, negative: true, colors: whiteyellow);
                    var comp = FindLineInColor(smallimage, starty: result[1] + 1,colors: whiteyellow);
                    if (comp==null || comp[1] - result[1]  < (result[1]+2*height > image.Height ? 5 : 2) )
                    {
                        lines.Add(result[1] + 2);

                    }
                }
                lines.Remove(lines.Last());
                lines.Add(lines.Min() - height);
                logger.LogInformation(logCat + "lines " + lines.Count());
                result[1] = lines.Min() - 2;
                result[0] = leftstart;
                smallimage.Dispose();
                smallimage = image.Clone();
            }
            catch (Exception e)
            {
                //Console.WriteLine(e);
                //Console.WriteLine("Zeilen finden fehlgeschlagen");
                screener.ParseError = true;
                screener.ParseErrorString = "Zeilen finden fehlgeschlagen   " + e.ToString();
                uow.Complete();
                return;
            }

            // oberen Teil des Images nach den Spaltenlinien durchsuchen


            try
            {
                smallimage.Crop(0, 0, image.Width, result[1]);

                columns.Add(leftstart, 0);
                int[] result2 = new int[2];
                result2[0] = 0;
                while (result != null)
                {
                    result = FindLineInColor(img: smallimage, starty: result[1], startx: result2[0] ==  0 ? columns.Keys.Max() + 1 : result[0],
                        MoveDirection: Direction.right, lookDirection: Direction.up, length: 2, negative: true,allowedErrors:0);
                    result2 = FindLineInColor(img: smallimage, starty: result[1], startx: result[0] - 1, lookDirection: Direction.up, MoveDirection:Direction.left, length: 13);
                    if (result2[0] != result[0] - 1 && columns.Count > 2)
                        continue;
                    result2[0] = 0;
                    columns[columns.Keys.Max()] = result[0] - 1 - columns.Keys.Max();
                    columns.Add(result[0] + 1, 0);
                    if (smallimage.Width - result[0] < 10) break;
                }
                logger.LogInformation(logCat + "columns " + columns.Count());
                MagickImage outImage = image.Clone();
                lines.ForEach(x => outImage.Draw(new DrawableLine(leftstart, x - 1, image.Width, x - 1)));

                foreach (var c in columns)
                {
                    if (FindLineInColor(img: smallimage, startx: c.Key - 1, starty: topwhite,
                            MoveDirection: Direction.none, length: 2) != null)
                    {
                        newcolumns.Add(c.Key + 1, c.Value - 1);
                        newcolumns[newcolumns.Keys.Where(x => x < c.Key).Max()]++;
                    }
                    else
                    {
                        newcolumns.Add(c.Key, c.Value);
                    }
                }
                columns = newcolumns;
                columns.Remove(columns.Keys.Last());
   
                columnlist = new KeyValuePair<int, int>[columns.Count];
                counter = 0;
                var mappings = uow.ScreenerEntryMappings.GetAllForScreenerType(screener.ScreenerTypeId);
                foreach (var c in columns)
                {

                    var xx = c.Key;
                    outImage.Draw(new DrawableLine(xx - 1, lines[0], xx - 1, image.Height));
                    outImage.Draw(new Drawables().FontPointSize(12).FillColor(MagickColors.Blue).Text(xx - 1, lines[0], counter.ToString()));

                    var spaltenname = mappings.FirstOrDefault(x => x.position == counter)?.ScreenerEntryType.Name;
                    if(spaltenname!=null) outImage.Draw(new Drawables().FontPointSize(11).FillColor(MagickColors.Blue).Text(xx - 1, lines[1], spaltenname));
                    columnlist[counter++] = c;

                }
                FileInfo saveFile = new FileInfo(screener.ImageFile.Replace("data", "data2"));
                saveFile.Directory.Create();
                outImage.Write(saveFile);
            }


            catch (Exception e)
            {
                //Console.WriteLine("Spalten finden fehlgeschlagen");
                //Console.WriteLine(e);
                screener.ParseError = true;
                screener.ParseErrorString = "Spalten finden fehlgeschlagen  " + e.ToString();
                uow.Complete();
                return;
            }

            // macht instrumentnames with reference pictures
            counter = 0;
			MagickImage lineImage = null;
			MagickImage refImage = null;
			MagickImage dithImage = null;
            foreach (var l in lines)
            {
                counter++;
                if (lineImage != null) lineImage.Dispose();
				lineImage = image.Clone();
                lineImage.Crop(0, l, image.Width, height);
                lineImage.RePage();
                var nameColumn = screener.ScreenerType.NameColumn;
                if (refImage != null) refImage.Dispose();
				refImage = lineImage.Clone();
                refImage.Crop(columnlist[nameColumn].Key, 0, columnlist[nameColumn].Value, height);
                refImage.RePage();
                refImage.Trim();
                refImage.RePage();


                // get foreground color from name cell
                if(dithImage !=null) dithImage.Dispose();
				dithImage = refImage.Clone();
                dithImage.OrderedDither("3x3");
                var hist = dithImage.Histogram();
                var prominentcount = hist.Values.OrderByDescending(x => x).ToArray()[1];
                var pc = hist.FirstOrDefault(x => x.Value == prominentcount).Key;
                CellColor currentCellColor = CellColor.Undefined;
                if (pc.R == 255 && pc.G == 255 && pc.B == 0) currentCellColor = CellColor.Yellow;
                if (pc.R == 0 && pc.G == 255 && pc.B == 0) currentCellColor = CellColor.Green;
                if (pc.R == 255 && pc.G == 0 && pc.B == 0) currentCellColor = CellColor.Red;


                var referenceImages =
                    uow.ScreenerReferenceImages.GetForTypeAndColor(screener.ScreenerTypeId, currentCellColor);
                    
                bool foundMatch = false;
                bool matchUnreferenced = false;
                if (referenceImages != null)
                {
                    MagickImage compareImage = null;
					foreach (var referenceImage in referenceImages)
                    {
                        if (foundMatch == true) break;
                        FileInfo compareFile =
                            new FileInfo(screener.ScreenerType.Path + "\\ref\\" + referenceImage.ImageSignature + ".gif");

                            if(compareImage != null) compareImage.Dispose();
							compareImage = new MagickImage(compareFile);
                            var isEqual = compareImage.Compare(refImage);
                            if (isEqual.NormalizedMeanError != 0) errors.Add(isEqual.NormalizedMeanError);
                            if (isEqual.NormalizedMeanError < 0.01)
                            {
                                if (referenceImage.BrokerInstrumentId == null)
                                {
                                    matchUnreferenced = true;
                                }
                                else  // wir haben den InstrumentName gefunden!
                                {
                                    totalLinesMatched++;
                                    logger.LogInformation(logCat + "found: " + referenceImage.BrokerInstrumentId);
                                     foundMatch = true;
                                    ScreenerLine newLine = new ScreenerLine();
                                var existingLines = uow.ScreenerLines.GetAll().Where(x => x.BrokerInstrument == referenceImage.BrokerInstrument && x.Screener == screener).ToList();
                                if ( existingLines.Count >0)
                                {
                                    screener.ParseError = true;
                                    screener.ParseErrorString = "Doppelt:  " + l + " - " + counter + " - " + referenceImage.Id;
                                    logger.LogInformation(logCat + screener.ParseErrorString);
                                    FileInfo errorFile =
                                        new FileInfo(screener.ScreenerType.Path + "\\referror\\" + referenceImage.Id + "_"+l+".gif");
                                    errorFile.Directory.Create();
                                    refImage.Write(errorFile);
                                    uow.Complete();
                                        return;
                                }
                                    uow.ScreenerLines.Add(newLine);
                                    newLine.Screener = screener;
                                    newLine.BrokerInstrument = referenceImage.BrokerInstrument;
                                    var tempbs = uow.BrokerInstruments.GetAllWithNames().FirstOrDefault(x => x.Id == referenceImage.BrokerInstrumentId);
                                    var entryMappings = screener.ScreenerType.ScreenerEntryMappings.ToList();
                                    foreach (var entryMapping in entryMappings)
                                    {
                                        CellColor bgColor;
                                        ScreenerEntry newEntry = new ScreenerEntry();
                                        uow.ScreenerEntries.Add(newEntry);
                                        newEntry.ScreenerLine = newLine;
                                        newEntry.ScreenerEntryType = entryMapping.ScreenerEntryType;
                                        if(refImage!=null) refImage.Dispose();
										refImage = lineImage.Clone();
                                        refImage.Crop(columnlist[entryMapping.position].Key, 0, columnlist[entryMapping.position].Value, 2);
                                        refImage.RePage();
                                        refImage.OrderedDither("2x2");
                                        hist = refImage.Histogram();
                                        prominentcount = hist.Values.OrderByDescending(x => x).ToArray()[0];
                                        pc = hist.FirstOrDefault(x => x.Value == prominentcount).Key;
                                        bgColor = CellColor.Undefined;
                                        if (pc.R == 255 && pc.G == 255 && pc.B == 0) bgColor = CellColor.Yellow;
                                        if (pc.R == 0 && pc.G == 255 && pc.B == 0) bgColor = CellColor.Green;
                                        if (pc.R == 255 && pc.G == 0 && pc.B == 0) bgColor = CellColor.Red;
                                        if (pc.R == 0 && pc.G == 0 && pc.B == 0) bgColor = CellColor.Black;

                                        if (entryMapping.ScreenerEntryType.Name == "status")
                                        {
                                            newEntry.Fg = currentCellColor;
                                        }
                                        else
                                        {
                                            newEntry.Bg = bgColor;
                                        }
                                        logger.LogInformation(logCat + "parsed entry "+entryMapping.ScreenerEntryType.Name+" BG: "+bgColor);
                                    }
                                    break;
                            }
                            }
                            compareImage.Dispose();
                        }
                    }
                    if (foundMatch == false && matchUnreferenced == false)
                    {
                        ScreenerReferenceImage newReferenceImage = new ScreenerReferenceImage();
                        logger.LogInformation(logCat + "Neues Ref Image in Zeile " + counter);
                        newReferenceImage.ScreenerType = screener.ScreenerType;
                        newReferenceImage.CellColor = currentCellColor;
                        newReferenceImage.ImageSignature = refImage.Signature;
                        uow.ScreenerReferenceImages.Add(newReferenceImage);
                        FileInfo compareFile =
                            new FileInfo(screener.ScreenerType.Path + "\\ref\\" + newReferenceImage.ImageSignature + ".gif");
                        compareFile.Directory.Create();
                        refImage.Write(compareFile);
                        refImage.Dispose();

                    }
                    refImage.Dispose();
                    lineImage.Dispose();
                    dithImage.Dispose();
                    //refImage.Write(image.FileName.Replace(".gif",l+".gif"));
                }

                //Console.WriteLine("Total lines matched: {0} out of {1} in DB, out of {2} in Screenerimage", totalLinesMatched, screener.ScreenerType.BrokerInstruments.Count, lines.Count);
                if (totalLinesMatched == screener.ScreenerType.BrokerInstrumentScreenerTypes.Count)
                {
                    screener.IsProcessed = true;
                screener.ParseError = false;
                screener.ParseErrorString = totalLinesMatched + " out of " + screener.ScreenerType.BrokerInstrumentScreenerTypes.Count + " in DB, out of " + lines.Count + " in Screenerimage, height: " + height;
                Console.WriteLine("Screener {0} from {1} set to processed", screener.ScreenerType.Name, screener.TimeStamp);
                }
                if (errors.Count != 0) Console.WriteLine("Min Error {0}", errors.Min());
                image.Dispose();
                errors.Clear();


            if (totalLinesMatched != screener.ScreenerType.BrokerInstrumentScreenerTypes.Count)
            {
                screener.ParseError = false;
                screener.ParseErrorString = totalLinesMatched + " out of " + screener.ScreenerType.BrokerInstrumentScreenerTypes.Count + " in DB, out of " + lines.Count + " in Screenerimage, height: " + height;
            }

            uow.Complete();


        }
          
    }
}

