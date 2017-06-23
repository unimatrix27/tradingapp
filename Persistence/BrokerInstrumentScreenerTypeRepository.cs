using trading.Models;
using trading.Persistence;
using Trading.Persistence.Interfaces;

namespace Trading.Persistence
{
    public class BrokerInstrumentScreenerTypeRepository : Repository<BrokerInstrumentScreenerType>, IBrokerInstrumentScreenerTypeRepository
    {
        public BrokerInstrumentScreenerTypeRepository(TradingDbContext context) : base(context)
        {

        }
    }
}
