using trading.Models;

namespace Trading.Persistence.Interfaces
{
    public interface IIndicatorEntryRepository : IRepository<IndicatorEntry>
    {
        void AddOrUpdate(IndicatorEntry newEntry);

    }
}
