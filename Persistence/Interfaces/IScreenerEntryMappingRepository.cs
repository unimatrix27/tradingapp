using System.Collections.Generic;
using trading.Models;

namespace Trading.Persistence.Interfaces
{
    public interface IScreenerEntryMappingRepository : IRepository<ScreenerEntryMapping>
    {
        IEnumerable<ScreenerEntryMapping> GetAllForScreenerType(int id);
    }
}
