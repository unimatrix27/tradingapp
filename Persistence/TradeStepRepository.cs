using trading.Models;
using trading.Persistence;
using Trading.Persistence.Interfaces;

namespace Trading.Persistence
{
    public class TradeStepRepository : Repository<TradeStep>, ITradeStepRepository
    {
        public TradeStepRepository(TradingDbContext context) : base(context) { }
    }
}
