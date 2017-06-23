using System.Collections.Generic;
using trading.Models;

namespace Trading.Persistence.Interfaces
{
    public interface IScreenerReferenceImageRepository : IRepository<ScreenerReferenceImage>
    {
        IEnumerable<ScreenerReferenceImage> GetForTypeAndColor(int id, CellColor color);
    }
}
