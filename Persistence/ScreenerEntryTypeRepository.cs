using System.Collections.Generic;
using System.Linq;
using trading.Models;
using trading.Persistence;
using Trading.Persistence.Interfaces;

namespace Trading.Persistence
{
    public class ScreenerEntryTypeRepository : Repository<ScreenerEntryType>, IScreenerEntryTypeRepository
    {
        public ScreenerEntryTypeRepository(TradingDbContext context) : base(context)
        {

        }

        public IEnumerable<ScreenerEntryType> GetAllForScreenerType(ScreenerType st)
        {
            return context.ScreenerEntryMappings.Where(x => x.ScreenerType.Id == st.Id).Select(t => t.ScreenerEntryType).Distinct().ToList();
        }
    }
}
