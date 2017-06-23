using System;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using trading.Persistence;
using MyYahooFinance.NET;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.Service;
using Microsoft.Extensions.Logging;
using trading.Models;
using Trading.Persistence.Interfaces;

namespace trading.Background
{
    public interface IYahooDownloader{
        Task<int> Download(IJobCancellationToken cancellationToken);

    }
    public class YahooDownloader
    {
        private IUnitOfWork uow;
        private YahooFinanceClient _yahooFinance;
        private ILogger<YahooDownloader> logger;

        public YahooDownloader(IUnitOfWork uow, ILogger<YahooDownloader> logger)
        {
            this.logger = logger;
            logger.LogInformation("YahooDownloader created");
            this.uow =uow;
            _yahooFinance = new YahooFinanceClient();
            
        }

        public void Download(IJobCancellationToken cancellationToken){
            var ti =  uow.TimeIntervals.GetAll().FirstOrDefault(x => x.Duration == TimeSpan.FromDays(1));

            var result = uow.BrokerInstruments.GetAllForBroker("YA");//.Where(x => x.Id>54 && x.Id <58);

            logger.LogInformation("YahooDownloader will download "+result.Count()+" BrokerInstruments");
            foreach (var element in result)
            {
                logger.LogInformation("YahooDownloader start " + element.BrokerSymbol.Name);
                List<YahooHistoricalPriceData> yahooPriceHistory= null; 
                try
                {
                    yahooPriceHistory =
                        _yahooFinance.GetDailyHistoricalPriceData(element.BrokerSymbol.Name,
                            endDate: DateTime.Today + TimeSpan.FromDays(1),
                            startDate: DateTime.Today - TimeSpan.FromDays(3));

                    yahooPriceHistory.Sort((x, y) => x.Date.CompareTo(y.Date));

                    foreach (var price in yahooPriceHistory)
                    {
                        if (price.Volume > 0)
                        {
                            var priceDate = element.BrokerSymbol.Exchange.CloseTimeOnDate(price.Date);
                            var priceEntry = uow.PriceEntries.GetForBrokerInstrument(element.Id, ti,priceDate);
                            if (priceEntry == null)
                            {
                                priceEntry = new PriceEntry();
                                logger.LogInformation("YahooDownloader " + element.BrokerSymbol.Name +
                                                      " new Price for " + price.Date);
                                uow.PriceEntries.Add(priceEntry);

                            }
                            else
                            {
                                logger.LogInformation("YahooDownloader " + element.BrokerSymbol.Name + " updated Price for " + price.Date);
                            }
                            priceEntry.BrokerInstrument = element;
                            priceEntry.IsFinished = true;
                            priceEntry.Open = price.Open;
                            priceEntry.High = price.High;
                            priceEntry.Low = price.Low;
                            priceEntry.Close = price.Close;
                            priceEntry.Volume = price.Volume;
                            priceEntry.TimeInterval = ti;
                            priceEntry.TimeStamp = priceDate;
                            uow.PriceEntries.UpdateIndicatorData(priceEntry);
                        }
                    }
                    
                }
                catch (Exception e)
                {
                    logger.LogInformation("YahooDownloader Exception " + e.ToString());
                }

                var currentPriceEntry = uow.PriceEntries.GetForBrokerInstrument(element.Id, ti).FirstOrDefault(x => x.IsFinished == false);

                if (yahooPriceHistory?.FirstOrDefault()?.Date != DateTime.Now.Date
                    && element.BrokerSymbol.Exchange.IsOpen
                    )
                {
                    YahooRealTimeData rt = _yahooFinance.GetRealTimeData(element.BrokerSymbol.Name);
                    if (rt != null && rt.LastTradeTime.TimeOfDay < DateTime.Now.TimeOfDay)
                    {
                        DateTime compare = rt.LastTradeTime.Date;
                       
                        if (currentPriceEntry == null)
                        {
                            currentPriceEntry = new PriceEntry();
                            uow.PriceEntries.AddPrice(currentPriceEntry);
                        }
                        //var realTradeTime = rt.LastTradeTime + element.Exchange.TimeZoneOffset;

                        currentPriceEntry.BrokerInstrument = element;
                        currentPriceEntry.IsFinished = false;
                        currentPriceEntry.Open = rt.Open;
                        currentPriceEntry.High = rt.High;
                        currentPriceEntry.Low = rt.Low;
                        currentPriceEntry.Close = rt.Last;
                        currentPriceEntry.Volume = rt.Volume;
                        currentPriceEntry.TimeInterval = ti;
                        currentPriceEntry.TimeStamp = element.BrokerSymbol.Exchange.getLocalTime(rt.LastTradeTime);
                        uow.PriceEntries.UpdateIndicatorData(currentPriceEntry);
                    }
                }
                else
                {
                    if (currentPriceEntry != null) uow.PriceEntries.Remove(currentPriceEntry);
                }


            }

           uow.Complete();

            BackgroundJob.Enqueue<SignalProcessor>(x => x.ProcessAllSignals(JobCancellationToken.Null));
        }

       
    }

    public class SignalProcessor
    {
        private IUnitOfWork uow;
        private ILogger<SignalProcessor> logger;

        public SignalProcessor(IUnitOfWork uow, ILogger<SignalProcessor> logger)
        {
            this.logger = logger;
            logger.LogInformation("YahooDownloader created");
            this.uow = uow;
        }

        public void ProcessAllSignals(IJobCancellationToken cancellationToken)
        {
            this.uow.ProcessSignals();
            this.uow.Complete();
        }
    }

}

