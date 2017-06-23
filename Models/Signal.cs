using System.Collections.Generic;
using System.Linq;

namespace trading.Models
{
    public partial class Signal : BaseTable
    {
        public SignalType SignalType { get; set; }
        public TradeDirection TradeDirection { get; set; }
        public int BrokerInstrumentId { get; set; }
        public int TimeIntervalId { get; set; }
        public virtual BrokerInstrument BrokerInstrument { get; set; }
        public virtual TimeInterval TimeInterval{ get; set; }
        public virtual ICollection<SignalStep> SignalSteps { get; set; }
        public virtual ICollection<Trade> Trades { get; set; }

        public decimal? Entry {get{
                        if (SignalSteps.Count == 0) return null ;
            return SignalSteps?.OrderByDescending(x => x.Created).FirstOrDefault().Entry;
        }}
        public decimal? Stop {get{
                        if (SignalSteps.Count == 0) return null ;
            return SignalSteps?.OrderByDescending(x => x.Created).FirstOrDefault().Sl;
        }}
        public decimal? Profit {get{
                        if (SignalSteps.Count == 0) return null ;
            return SignalSteps?.OrderByDescending(x => x.Created).FirstOrDefault().Tp;
        }}


        public Signal()
        {
            SignalSteps = new HashSet<SignalStep>();
            Trades = new HashSet<Trade>();
        }
    }
}
