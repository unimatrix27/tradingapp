using trading.Models;
using trading.Persistence;
using Trading.Persistence.Interfaces;

namespace Trading.Persistence
{
    public class InstrumentTypeRepository : Repository<InstrumentType>, IInstrumentTypeRepository
    {
        public InstrumentTypeRepository(TradingDbContext context) : base(context)
        {

        }
    }
}
