namespace trading.Models
{
    public partial class ExitRule : BaseTable
    {
        public ExitType ExitType { get; set; }
        public RuleRelation RuleRelation { get; set; }
        public decimal? Value { get; set; }
        public decimal? Amount { get; set; }

    }
}
