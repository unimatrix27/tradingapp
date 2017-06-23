namespace trading.Models
{
    public partial class SignalStep : BaseTable
    {
        public SignalStepType SignalStepType { get; set; }
        public int SignalId { get; set; }
        public virtual Signal Signal { get; set; }
        public int PriceEntryId { get; set; }
        public virtual PriceEntry PriceEntry { get; set; }
        public EntryType? EntryType { get; set; }
        public decimal? Entry { get; set; }
        public decimal? Sl { get; set; }
        public decimal? Tp { get; set; }
        public StopLossType? SlType { get; set; }
        public ExitType? ExitType { get; set; }
        public string Reason { get; set; }
    }
}