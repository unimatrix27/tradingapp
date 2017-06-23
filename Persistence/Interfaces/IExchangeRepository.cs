using System.Collections.Generic;
using trading.Models;

namespace Trading.Persistence.Interfaces
{
    public interface IExchangeRepository : IRepository<Exchange>
    {
        IEnumerable<Exchange> GetAllWithCurrency();
    }
}
