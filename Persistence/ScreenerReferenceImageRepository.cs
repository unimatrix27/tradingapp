using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using trading.Models;
using trading.Persistence;
using Trading.Persistence.Interfaces;

namespace Trading.Persistence
{
    public class ScreenerReferenceImageRepository : Repository<ScreenerReferenceImage>, IScreenerReferenceImageRepository
    {
        public ScreenerReferenceImageRepository(TradingDbContext context) : base(context)
        {

        }

        public IEnumerable<ScreenerReferenceImage> GetForTypeAndColor(int id, CellColor color)
        {
            return context.ScreenerReferenceImages.Include(x => x.ScreenerType).Where(x => x.ScreenerTypeId == id && x.CellColor == color).ToList();
        }
    }
}
