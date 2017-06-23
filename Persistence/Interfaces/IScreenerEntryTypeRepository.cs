using System.Collections.Generic;
using trading.Models;

namespace Trading.Persistence.Interfaces
{
    public interface IScreenerEntryTypeRepository : IRepository<ScreenerEntryType>
    {
        IEnumerable<ScreenerEntryType> GetAllForScreenerType(ScreenerType st);  
    }
}
