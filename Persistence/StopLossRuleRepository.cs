using trading.Models;
using trading.Persistence;
using Trading.Persistence.Interfaces;

namespace Trading.Persistence
{
    public class StopLossRuleRepository : Repository<StopLossRule>, IStopLossRuleRepository
    {
        public StopLossRuleRepository(TradingDbContext context) : base(context) { }
    }
}
