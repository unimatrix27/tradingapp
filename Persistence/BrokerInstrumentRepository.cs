using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Query.Internal;
using trading.Models;
using trading.Persistence;
using Trading.Persistence.Interfaces;

namespace Trading.Persistence
{
    public class BrokerInstrumentRepository : Repository<BrokerInstrument>, IBrokerInstrumentRepository
    {
        public BrokerInstrumentRepository(TradingDbContext context) : base(context)
        {

        }

        public PriceEntry GetLastPriceEntry()
        {
            throw new NotImplementedException();
        }


        public IEnumerable<BrokerInstrument> GetAllWithNames()
        {
            return context.BrokerInstruments
                .Include(i => i.BrokerSymbol)
                .Include(i => i.BrokerSymbol.Exchange)
                .Include(i => i.BrokerSymbol.Exchange.Broker)
                .Include(i => i.InstrumentType)
                .Include(i => i.BrokerSymbol.InstrumentName)
                .Include(i => i.BrokerInstrumentScreenerTypes)
                .Include(i => i.BrokerSymbol.Exchange.Currency)
                .Include(i => i.PriceEntries);
        }

        public IEnumerable<BrokerInstrument> GetAllForBroker(string brokerName)
        {
            return this.GetAllWithNames().Where(y => y.BrokerSymbol.Exchange.Broker.ShortName == brokerName);
        }
    }
}
