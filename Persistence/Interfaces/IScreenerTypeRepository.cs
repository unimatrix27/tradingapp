using System.Collections.Generic;
using trading.Models;

namespace Trading.Persistence.Interfaces
{
    public interface IScreenerTypeRepository : IRepository<ScreenerType>
    {
        IEnumerable<ScreenerType> getToDownload();
    }
}
