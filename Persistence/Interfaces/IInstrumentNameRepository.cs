using System.Collections.Generic;
using trading.Models;

namespace Trading.Persistence.Interfaces
{
    public interface IInstrumentNameRepository : IRepository<InstrumentName>
    {
        PriceEntry GetLastPriceEntry(string instrument, TimeInterval ti = null);
        IEnumerable<InstrumentName> GetFull();

    }
}
