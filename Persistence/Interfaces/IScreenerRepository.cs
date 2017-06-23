using trading.Models;

namespace Trading.Persistence.Interfaces
{
    public interface IScreenerRepository : IRepository<Screener>
    {
        bool ImageExists(string destFileName);
        Screener GetFull(int id);
    }
}
