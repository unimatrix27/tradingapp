using trading.Models;
using trading.Persistence;
using Trading.Persistence.Interfaces;

namespace Trading.Persistence
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(TradingDbContext context) : base(context) { }
    }
}
