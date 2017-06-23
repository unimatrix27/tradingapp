using trading.Models;
using trading.Persistence;
using Trading.Persistence.Interfaces;

namespace Trading.Persistence
{
    public class ScreenerEntryRepository : Repository<ScreenerEntry>, IScreenerEntryRepository
    {
        public ScreenerEntryRepository(TradingDbContext context) : base(context)
        {

        }
    }
}
