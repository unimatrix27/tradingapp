using trading.Models;
using trading.Persistence;
using Trading.Persistence.Interfaces;

namespace Trading.Persistence
{
    public class ScreenerLineRepository : Repository<ScreenerLine>, IScreenerLineRepository
    {
        public ScreenerLineRepository(TradingDbContext context) : base(context)
        {

        }
    }
}
