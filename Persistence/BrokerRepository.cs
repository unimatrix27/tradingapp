using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using trading.Models;
using trading.Persistence;
using Trading.Persistence.Interfaces;

namespace Trading.Persistence
{
    public class BrokerRepository : Repository<Broker>, IBrokerRepository
    {
        public BrokerRepository(TradingDbContext context) : base(context)
        {

        }

        public IEnumerable<Broker> GetAllWithReferenceData()
        {
            return EntityFrameworkQueryableExtensions.Include<Broker, ICollection<Exchange>>(context.Brokers, x => x.Exchanges).ThenInclude(y => y.Currency).ToList();
        }
    }
}