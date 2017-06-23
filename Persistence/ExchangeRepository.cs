using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using trading.Models;
using trading.Persistence;
using Trading.Persistence.Interfaces;

namespace Trading.Persistence
{
    public class ExchangeRepository : Repository<Exchange>, IExchangeRepository
    {
        public ExchangeRepository(TradingDbContext context) : base(context)
        {

        }

        public IEnumerable<Exchange> GetAllWithCurrency()
        {
            return context.Exchanges.Include(x => x.Currency);
        }
    }
}
