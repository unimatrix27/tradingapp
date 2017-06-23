using System.Collections.Generic;
using trading.Models;

namespace Trading.Persistence.Interfaces
{
    public interface ISignalRepository : IRepository<Signal>
    {
        IEnumerable<Signal> GetOpen();
    }
}
