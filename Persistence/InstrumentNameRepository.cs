using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using trading.Models;
using trading.Persistence;
using Trading.Persistence.Interfaces;

namespace Trading.Persistence
{
    public class InstrumentNameRepository : Repository<InstrumentName>, IInstrumentNameRepository
    {
        public InstrumentNameRepository(TradingDbContext context) : base(context)
        {

        }

        public PriceEntry GetLastPriceEntry(string instrumentName, TimeInterval ti = null)
        {
            if (ti == null)
            {
                ti = Queryable.First<TimeInterval>(context.TimeIntervals, t => t.Name == "D1");
                if (ti == null) return null;
            }

            var bTI = Queryable.Where<BrokerTimeInterval>(context.BrokerTimeIntervals, x => x.TimeInterval == ti).OrderBy(x => x.dataPriority).Include(x => x.Broker).ThenInclude(y => y.Exchanges).ToList();
            if (bTI == null) return null;

            var instrument = Queryable.Where<InstrumentName>(context.InstrumentNames, i => i.Name == instrumentName)
                .Include(x => x.BrokerSymbols)
                .ThenInclude(y => y.Exchange).FirstOrDefault();
            if (instrument == null) return null;

            BrokerSymbol bSymbol = null;
            foreach (var b in bTI)
            {
                foreach (var sym in instrument.BrokerSymbols)
                {
                    if (b.Broker.Exchanges.Contains(sym.Exchange)) bSymbol = sym;
                }
            }
            if (bSymbol == null) return null;

            var firstEntry = EntityFrameworkQueryableExtensions.Include<PriceEntry, BrokerInstrument>(context.PriceEntries, x => x.BrokerInstrument)
                .ThenInclude(y => y.BrokerSymbol)
                .OrderByDescending(p => p.TimeStamp)
                .FirstOrDefault(x => x.BrokerInstrument.BrokerSymbol == bSymbol);

            return firstEntry;
        }

        public IEnumerable<InstrumentName> GetFull()
        {
            return  EntityFrameworkQueryableExtensions.Include<InstrumentName, ICollection<BrokerSymbol>>(context.InstrumentNames, i => i.BrokerSymbols)
                .ThenInclude(bs => bs.BrokerInstruments)
                .ThenInclude(bi => bi.BrokerSymbol.Exchange)
                .Include(i => i.BrokerSymbols)
                .ThenInclude(bs => bs.BrokerInstruments)
                .ThenInclude(bi => bi.InstrumentType);
        }
    }
}