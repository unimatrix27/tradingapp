using System.Collections.Generic;
using System.Linq;

namespace trading.Models
{

    public partial class Trade : BaseTable
    {
        public int SignalId { get; set; }
        public Signal Signal { get; set; }
        public SignalState SignalState {get;set;} = SignalState.Ok;
        public virtual ICollection<TradeStep> TradeSteps { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        
        public decimal? Size {get{
            if (TradeSteps.Count == 0) return null ;
            return TradeSteps?.OrderByDescending(x => x.Created).FirstOrDefault().Size;
        }}
        public decimal? Entry {get{
                        if (TradeSteps.Count == 0) return null ;
            return TradeSteps?.OrderByDescending(x => x.Created).FirstOrDefault().EntryLevel;
        }}
        public decimal? Stop {get{
                        if (TradeSteps.Count == 0) return null ;
            return TradeSteps?.OrderByDescending(x => x.Created).FirstOrDefault().StopLevel;
        }}
        public decimal? Profit {get{
                        if (TradeSteps.Count == 0) return null ;
            return TradeSteps?.OrderByDescending(x => x.Created).FirstOrDefault().ProfitLevel;
        }}

        public TradeStepType? Type {get{
            if (TradeSteps.Count == 0) return null ;
                return TradeSteps?.OrderByDescending(x => x.Created).FirstOrDefault().Type;
            }}

        public Trade()
        {
            TradeSteps = new HashSet<TradeStep>();
            Orders = new HashSet<Order>();
        }

    }
}
