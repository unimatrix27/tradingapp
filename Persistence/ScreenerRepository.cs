using Microsoft.EntityFrameworkCore;
using System.Linq;
using trading.Models;
using trading.Persistence;
using Trading.Persistence.Interfaces;

namespace Trading.Persistence
{
    public class ScreenerRepository : Repository<Screener>, IScreenerRepository
    {
        public ScreenerRepository(TradingDbContext context) : base(context)
        {

        }

        public bool ImageExists(string destFileName)
        {
            return context.Screeners.FirstOrDefault(x => x.ImageFile == destFileName) != null;
        }

        public Screener GetFull(int id)
        {
            return context.Screeners.Where((s => s.Id == id)).Include(s => s.ScreenerType)
                .Include(s => s.ScreenerLines)
                .ThenInclude(x => x.ScreenerEntries)
                .ThenInclude(x => x.ScreenerEntryType)
                .Include(s => s.ScreenerType)
                .ThenInclude(st => st.BrokerInstrumentScreenerTypes)
                .ThenInclude(stt => stt.BrokerInstrument)
                .Include(s => s.ScreenerType)
                .ThenInclude(st => st.ScreenerReferenceImages)
                .FirstOrDefault();
        }
    }
}
