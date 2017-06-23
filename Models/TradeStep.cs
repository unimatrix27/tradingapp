namespace trading.Models
{
    public partial class TradeStep : BaseTable
    {
        public int TradeId { get; set; }
        public virtual Trade Trade { get; set; }
        public TradeStepType Type { get; set; }
        public TradeStepResult Result { get; set; } = TradeStepResult.Pending;
        public TradeStepErrorReason? Reason { get; set; }
        public int? Size { get; set; }
        public decimal? EntryLevel { get; set; }
        public decimal? StopLevel { get; set; }
        public decimal? ProfitLevel { get; set; }
       

    }
}
