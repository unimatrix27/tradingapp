using System;
using System.Globalization;
using System.IO;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using trading.Persistence;
using System.Linq;
using System.Net;
using trading.Models;
using Microsoft.Extensions.Logging;
using ImageMagick;

using Trading.Persistence.Interfaces;

namespace trading.Background
{
    public class ScreenerDownloader
    {
        private IUnitOfWork uow;


        public ScreenerDownloader(IUnitOfWork uow) { 
            this.uow = uow;
        }

        public void Download(IJobCancellationToken cancellationToken)
        {
            bool didFindAny = false;

            foreach (var screenerType in uow.ScreenerTypes.getToDownload())
            {
                if (screenerType.TimeInterval.Duration != TimeSpan.FromDays(1))
                {
                    screenerType.LastResult = "Only Daily Screeners are implemented so far";
                    //_log.LogError("Only Daily Screeners are implemented so far");
                    break;
                }

                DateTimeOffset? dateLookingFor = 
                    screenerType?.BrokerInstrumentScreenerTypes.FirstOrDefault()?.BrokerInstrument.BrokerSymbol.Exchange
                    .LastCloseTime;
                if (dateLookingFor == null)
                {
                    screenerType.LastResult = "Keine Member / Exchange Close Time eingetragen";
                    break;
                }
                if (screenerType.Screeners.FirstOrDefault(s => s.TimeStamp.Date == dateLookingFor?.Date) != null)
                {
                    screenerType.LastResult = "Aktuellster Screener ist schon in DB";
                    continue;
                }
                var urlDate = screenerType.UseToday ? DateTimeOffset.Now : dateLookingFor;
                var url = screenerType.URL.Replace("YYYY", urlDate?.Year.ToString("D"))
                    .Replace("/M/", "/" + urlDate?.Month.ToString("D") + "/")
                    .Replace("DATE",urlDate?.Date.ToString("ddMM"));
                var filename = screenerType.Path + "temp\\" + screenerType.Name + ".gif";
                var file = new System.IO.FileInfo(filename);
                //_log.LogDebug(url + " => " + filename);
                MagickImage image;
                try
                {
                    file.Directory.Create();
                    using (var webclient = new WebClient()) webclient.DownloadFile(url, filename);
                    image = new MagickImage(file);
                }
                catch (Exception ex)
                {
                    //_log.LogError(ex, "Error on Downloading Screener");
                    screenerType.LastResult = ex.ToString();
                    break;
                }
                var signature = image.Signature;
                if (screenerType.LastHash == signature)
                {
                    screenerType.LastCheck = DateTime.Now;
                    continue;
                }
                screenerType.LastHash = signature;
                // if (VerifyColorSignature(screenerType, image) == false) break;

                string destFileName = screenerType.Path + dateLookingFor?.Year.ToString("0000") + "\\" +
                                      dateLookingFor?.Month.ToString("00") + "\\" +
                                      dateLookingFor?.Day.ToString("00") + "_" + screenerType.Name + ".gif";
                try
                {
                    var destFile = new FileInfo(destFileName);
                    destFile.Directory.Create();
                    if (File.Exists(destFileName)) File.Delete(destFileName);
                    file.MoveTo(destFileName);
                }
                catch (Exception ex)
                {
                    //_log.LogError(ex, "Error on saving screener in destfile");
                    screenerType.LastResult = ex.ToString();
                    break;
                }
                if (!uow.Screeners.ImageExists(destFileName))
                {
                    var newScreener = new Screener();
                    newScreener.ImageFile = destFileName;
                    newScreener.TimeStamp = (DateTimeOffset)dateLookingFor;
                    newScreener.ImageHash = signature;
                    newScreener.ScreenerType = screenerType;
                    uow.Screeners.Add(newScreener);
                    image.Dispose();
                    didFindAny = true;
                    screenerType.LastCheck = DateTime.Now;
                }
                
            }
            uow.Complete();
            if (didFindAny) BackgroundJob.Enqueue<ScreenerParseScheduler>(x => x.Schedule(JobCancellationToken.Null));

        }
    }
}

