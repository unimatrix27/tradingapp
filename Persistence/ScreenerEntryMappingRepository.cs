using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using trading.Models;
using trading.Persistence;
using Trading.Persistence.Interfaces;

namespace Trading.Persistence
{
    public class ScreenerEntryMappingRepository : Repository<ScreenerEntryMapping>, IScreenerEntryMappingRepository
    {
        public ScreenerEntryMappingRepository(TradingDbContext context) : base(context)
        {

        }

        public IEnumerable<ScreenerEntryMapping> GetAllForScreenerType(int id)
        {
            return context.ScreenerEntryMappings.Where(x => x.ScreenerType.Id == id).Include(x => x.ScreenerEntryType);
        }
    }
}
