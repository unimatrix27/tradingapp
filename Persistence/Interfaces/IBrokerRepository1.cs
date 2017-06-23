using System.Collections.Generic;
using trading.Models;

namespace Trading.Persistence.Interfaces
{
    public interface IBrokerRepository : IRepository<Broker>
    {
        IEnumerable<Broker> GetAllWithReferenceData();
    }
}
