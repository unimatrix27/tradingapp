using System;
using System.Collections.Generic;
using System.Linq;
using trading.Models;

namespace trading.Controllers.Resources
{
    public class SignalResource
    {
        public int Id { get; set; }
        public SignalType SignalType { get; set; }
        public TradeDirection TradeDirection { get; set; }
        public int BrokerInstrumentId { get; set; }
        public int TimeIntervalId { get; set; }
        public virtual InstrumentNameAllResource Instrument { get; set; }
        public virtual TimeInterval TimeInterval { get; set; }
        public virtual ICollection<SignalStepResource> SignalSteps { get; set; }
        public virtual ICollection<TradeResource> Trades { get; set; }
        public decimal? Entry { get; set; }
        public decimal? Stop { get; set; }
        public decimal? Profit { get; set; }

        public DateTimeOffset? LastTime {get{
                        if (SignalSteps.Count == 0) return null ;
            return SignalSteps?.OrderByDescending(x => x.Created).FirstOrDefault().PriceEntry.TimeStamp;
        }}
        public SignalResource()
        {
            SignalSteps = new HashSet<SignalStepResource>();
            Trades = new HashSet<TradeResource>();
        }
    }
}