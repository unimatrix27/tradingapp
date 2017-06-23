using trading.Models;
using trading.Persistence;
using Trading.Persistence.Interfaces;

namespace Trading.Persistence
{
    public class ExitRuleRepository : Repository<ExitRule>, IExitRuleRepository
    {
        public ExitRuleRepository(TradingDbContext context) : base(context) { }
    }
}
