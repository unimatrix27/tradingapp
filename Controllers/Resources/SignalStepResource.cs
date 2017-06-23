using System;
using trading.Models;

namespace trading.Controllers.Resources
{
    public class SignalStepResource
    {
        public int Id { get; set; }
        public SignalStepType SignalStepType { get; set; }
        public int SignalId { get; set; }
        public virtual SignalResource Signal { get; set; }
        public int PriceEntryId { get; set; }
        public virtual PriceEntryResource PriceEntry { get; set; }
        public EntryType? EntryType { get; set; }
        public decimal? Entry { get; set; }
        public decimal? Sl { get; set; }
        public decimal? Tp { get; set; }
         public StopLossType? SlType { get; set; }
        public ExitType? ExitType { get; set; }
        public string Reason { get; set; }
        public DateTimeOffset Created {get;set;}
        public DateTimeOffset Updated {get;set;}
    }
}