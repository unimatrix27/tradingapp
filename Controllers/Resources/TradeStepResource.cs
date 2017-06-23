using System;
using trading.Models;

namespace trading.Controllers.Resources
{
    public class TradeStepResource
    {
        public int TradeId { get; set; }
        public TradeStepType Type { get; set; }
        public TradeStepResult Result { get; set; } = TradeStepResult.Pending;
        public TradeStepErrorReason? Reason { get; set; }
        public int? Size { get; set; }
        public decimal? EntryLevel { get; set; }
        public decimal? StopLevel { get; set; }
        public decimal? ProfitLevel { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset Updated { get; set; }
    }
}