using System.Linq;
using trading.Models;
using trading.Persistence;
using Trading.Persistence.Interfaces;

namespace Trading.Persistence
{
    public class IndicatorEntryRepository : Repository<IndicatorEntry>, IIndicatorEntryRepository
    {
        public IndicatorEntryRepository(TradingDbContext context) : base(context)
        {

        }

        public void AddOrUpdate(IndicatorEntry newEntry)
        {
            var existing = context.IndicatorEntries.FirstOrDefault(
                x => x.PriceEntryId == newEntry.PriceEntryId && x.Type == newEntry.Type);
            if (existing != null)
            {
                existing.Data = newEntry.Data;
            }
            else
            {
                context.IndicatorEntries.Add(newEntry);
            }
        }
    }
}
