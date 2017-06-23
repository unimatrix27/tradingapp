using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using trading.Models;
using trading.Persistence;
using Trading.Persistence.Interfaces;
using Trading.Persistence.Transfer;

namespace Trading.Persistence
{


    public class PriceEntryRepository : Repository<PriceEntry>, IPriceEntryRepository
    {
        private readonly ILogger<UnitOfWork> logger;

        public PriceEntryRepository(TradingDbContext context, ILogger<UnitOfWork> logger) : base(context)
        {
            this.logger = logger;
        }

        public IEnumerable<PriceEntry> GetAllForBrokerInstrument(int id)
        {
            return Queryable.Where<PriceEntry>(context.PriceEntries, x => x.BrokerInstrumentId == id);
        }

        public IEnumerable<PriceEntry> GetForBrokerInstrument(int id, TimeInterval ti)
        {
            return Queryable.Where<PriceEntry>(context.PriceEntries, x => x.BrokerInstrumentId == id && x.TimeInterval == ti);
        }

        public PriceEntry GetForBrokerInstrument(int id, TimeInterval ti, DateTimeOffset timeStamp)
        {
            return Queryable.FirstOrDefault<PriceEntry>(context.PriceEntries, x => x.BrokerInstrumentId == id && x.TimeInterval == ti &&
                                                            x.TimeStamp == timeStamp);
        }

        public void AddPrice(PriceEntry newPrice)
        {
            this.Add(newPrice);
            this.UpdateIndicatorData(newPrice);

        }

        public void UpdateIndicatorData(PriceEntry newPrice)
        {
            LoggerExtensions.LogInformation(logger, "PriceEntry Repo Updating Indicator Data for "+newPrice.BrokerInstrumentId + ", " + newPrice.TimeStamp);
            //var oldIndicatorData = Queryable.Where<IndicatorEntry>(context.IndicatorEntries, x => x.PriceEntry == newPrice);
            //if(oldIndicatorData !=null) context.IndicatorEntries.RemoveRange(oldIndicatorData);

            var prices = Queryable.Where<PriceEntry>(context.PriceEntries, x => x.BrokerInstrumentId == newPrice.BrokerInstrumentId
                            && x.TimeIntervalId == newPrice.TimeIntervalId
                            && x.TimeStamp <= newPrice.TimeStamp)
                .OrderByDescending(t => t.TimeStamp)
                .Take(78).ToList();
            if (prices.Count() < 52)
            {
                LoggerExtensions.LogInformation(logger, "PriceEntry Repo  " + newPrice.BrokerInstrumentId + "not enough history "+ prices.Count());
                return;
            }
            var prices_at = prices.Take(52);
            var prices_orig = prices;


            // Calculate all Indicators , should possibly be moved somewhere else

            // Span A, SpanB nach AT

            var highs = new List<decimal>(prices_at.Select(y => y.High));
            var lows = new List<decimal>(prices_at.Select(y => y.Low));

            var temp9 = (highs.GetRange(0, 9).Max() + lows.GetRange(0, 9).Min()) / 2;
            var temp26 = (highs.GetRange(0, 26).Max() + lows.GetRange(0, 26).Min()) / 2;
            var spanb = (highs.GetRange(0, 52).Max() + lows.GetRange(0, 52).Min()) / 2;
            var spana = (temp9 + temp26) / 2;
            var entrySpanA = new IndicatorEntry{Data=spana,PriceEntryId = newPrice.Id,Type=IndicatorDataType.Wolke_SpanA,IsDirty = false};
            var entrySpanB = new IndicatorEntry { Data = spanb, PriceEntryId = newPrice.Id, Type = IndicatorDataType.Wolke_SpanB, IsDirty = false };
            prices_orig.RemoveRange(0, 26);
            if (prices_orig.Count() >= 52)
            {
                var highs_orig = new List<decimal>(prices_orig.Select(y => y.High));
                var lows_orig = new List<decimal>(prices_orig.Select(y => y.Low));

                var temp9_orig = (highs_orig.GetRange(0, 9).Max() + lows_orig.GetRange(0, 9).Min()) / 2;
                var temp26_orig = (highs_orig.GetRange(0, 26).Max() + lows_orig.GetRange(0, 26).Min()) / 2;
                var spanb_orig = (highs_orig.GetRange(0, 52).Max() + lows_orig.GetRange(0, 52).Min()) / 2;
                var spana_orig = (temp9_orig + temp26_orig) / 2;

                var entrySpanAorig = new IndicatorEntry
                {
                    Data = spana_orig,
                    PriceEntryId = newPrice.Id,
                    Type = IndicatorDataType.OrigWolke_SpanA,
                    IsDirty = false
                };
                var entrySpanBorig = new IndicatorEntry
                {
                    Data = spanb_orig,
                    PriceEntryId = newPrice.Id,
                    Type = IndicatorDataType.OrigWolke_SpanB,
                    IsDirty = false
                };
                AddOrUpdateIndicator(entrySpanAorig);
                AddOrUpdateIndicator(entrySpanBorig);
                LoggerExtensions.LogInformation(logger, "PriceEntry Repo  " + newPrice.BrokerInstrumentId + "orig Wolke added");
            }

            LoggerExtensions.LogInformation(logger, "PriceEntry Repo  " + newPrice.BrokerInstrumentId + "Wolke added");
            AddOrUpdateIndicator(entrySpanA);
            AddOrUpdateIndicator(entrySpanB);


        }

        public void AddOrUpdateIndicator(IndicatorEntry newEntry)
        {
            var existing = context.IndicatorEntries.FirstOrDefault(
                x => x.PriceEntryId == newEntry.PriceEntryId && x.Type == newEntry.Type);
            if (existing != null)
            {
                existing.Data = newEntry.Data;
            }
            else
            {
                context.IndicatorEntries.Add(newEntry);
            }
        }



        
    }
}