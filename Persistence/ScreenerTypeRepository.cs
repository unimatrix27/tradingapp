using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using trading.Models;
using trading.Persistence;
using Trading.Persistence.Interfaces;

namespace Trading.Persistence
{
    public class ScreenerTypeRepository : Repository<ScreenerType>, IScreenerTypeRepository
    {
        public ScreenerTypeRepository(TradingDbContext context) : base(context)
        {

        }

        public IEnumerable<ScreenerType> getToDownload()
        {
            return context.ScreenerTypes
                .Where(x => (DateTime.Now - x.LastCheck) > x.UpdateFrequency)
                .Include(st => st.TimeInterval)
                .Include(st => st.BrokerInstrumentScreenerTypes)
                .ThenInclude(bst => bst.BrokerInstrument)
                .ThenInclude(bi => bi.BrokerSymbol)
                .ThenInclude(bs => bs.Exchange);
        }


    }
}
