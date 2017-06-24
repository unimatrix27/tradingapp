using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EntityFrameworkCore.Triggers;

namespace trading.Models
{
    public enum SignalType
    {
        Icer,
        uNL,
        uxIcer
    }

    public enum TradeDirection
     {
        Long =1,
        Short =-1
    }

    public enum SignalStepType
    {
        New = 1,
        Change = 0,
        Cancel = -1
    }

    public enum EntryType
    {
        StopLimit = 1,
        Limit = 2
    }

    public enum StopLossType
    {
        Icer1 = 1
    }

    public enum ExitType
    {
        Icer1 = 1
    }

    public enum StopLossRuleType
    {
        Price = 1
    }

    public enum RuleRelation
    {
        R = 1
    }

    public enum TradeStepType
    {
        Prepped = 1,
        PrepChange = 2,
        PrepCancel = 3,
        Placed= 10,
        PlacedOld = 11,
        Filled = 20,
        Cancel =-1,
        Closed = 30,
        TGS = 33,
        Hide=99
    }

    public enum TradeStepResult
    {
        Pending = 0,
        Ok = 1,
        Error = -1,
    }
    public enum TradeStepErrorReason{
        NoMargin = 1,
        NoConnect =2,
        Unknown = -1
    }

    public enum OrderFunction
    {
        Entry = 1,
        TP = 2,
        SL = -1,
        Time = 0
    }

    public enum OrderType
    {
        Market,
        Stop,
        StopLimit,
        Limit
    }

    public enum OrderState
    {
        Prepared,
        Submitted,
        Rejected,
        Filled,
        Canceled
    }
      public enum SignalState{
            Ok = 0,
            Changed = 1,
            Cancelled = -1
        }
 

    public partial class PriceEntry  : BaseTable
    {
        [Required]
        public Decimal Open { get; set; }

        [Required]
        public Decimal High { get; set; }

        [Required]
        public Decimal Low { get; set; }

        [Required]
        public Decimal Close { get; set; }


        public long? Volume { get; set; }

        [Required]
        public DateTimeOffset TimeStamp { get; set; }


        [Required]
        public bool IsFinished { get; set; } = false;
         [Required]
         public int TimeIntervalId { get; set; }
         [Required]
        public int BrokerInstrumentId { get; set; }

        public virtual TimeInterval TimeInterval { get; set; }

        public virtual BrokerInstrument BrokerInstrument { get; set; }
        public virtual ICollection<IndicatorEntry> IndicatorEntries { get; set; }
        public PriceEntry()
        {
            IndicatorEntries = new HashSet<IndicatorEntry>();
        }
    }
}
