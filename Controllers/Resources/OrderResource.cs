using System;
using trading.Models;

namespace trading.Controllers.Resources
{
    public class OrderResource 
    {
        public int Id { get; set; }
        public int TradeId { get; set; }
        public virtual TradeResource Trade { get; set; }
        public string BrokerOrderId { get; set; }
        public OrderFunction Function { get; set; }
        public OrderType Type { get; set; }
        public OrderState State { get; set; }
        public int Size { get; set; }
        public decimal? LimitLevel { get; set; }
        public decimal? StopLevel { get; set; }
        public decimal? ExecutedLevel { get; set; }
        public decimal? Costs { get; set; }
        public TradeDirection Direction { get; set; }
        public DateTimeOffset? ValidAfter { get; set; }


    }
}