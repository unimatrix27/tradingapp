namespace trading.Models
{
    public partial class StopLossRule : BaseTable
    {
        public StopLossType SlType { get; set; }
        public StopLossRuleType SlRuleType { get; set; }
        public RuleRelation RuleRelation { get; set; }
        public decimal? Value { get; set; }
        public RuleRelation NewRuleRelation { get; set; }
        public decimal? NewValue { get; set; }

    }
}