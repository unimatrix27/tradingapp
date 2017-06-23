using trading.Models;
using trading.Persistence;
using Trading.Persistence.Interfaces;

namespace Trading.Persistence
{
    public class BrokerSymbolRepository : Repository<BrokerSymbol>, IBrokerSymbolRepository
    {
        public BrokerSymbolRepository(TradingDbContext context) : base(context)
        {

        }
    }
}