using System;
using System.Collections.Generic;
using trading.Models;
using Trading.Persistence.Transfer;

namespace Trading.Persistence.Interfaces
{
    public interface IPriceEntryRepository : IRepository<PriceEntry>
    {
        IEnumerable<PriceEntry> GetAllForBrokerInstrument(int id);
        IEnumerable<PriceEntry> GetForBrokerInstrument(int id, TimeInterval ti);
        PriceEntry GetForBrokerInstrument(int id, TimeInterval ti, DateTimeOffset timeStamp);
        void AddPrice(PriceEntry newPrice);
        void UpdateIndicatorData(PriceEntry newPrice);
        



    }
}
