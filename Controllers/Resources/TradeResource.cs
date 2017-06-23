using System;
using System.Collections.Generic;
using System.Linq;
using trading.Models;

namespace trading.Controllers.Resources
{
    public class TradeResource{
        public int Id {get;set;}   
        public int SignalId { get; set; }
        public SignalResource Signal { get; set; }
        public SignalState SignalState {get;set;}
        public decimal? Size {get;set;}
        public decimal? Entry{get;set;}
        public decimal? Stop {get;set;}
        public decimal? Profit {get;set;}

        public TradeStepType? Type {get;set;}
        public decimal? LastClose {get{
                        if (TradeSteps.Count == 0) return null ;
            return Signal?.Instrument.lastPriceEntry.Close;
        }}


        public decimal? PnL { get{
         if (TradeSteps.Count == 0) return null ;
            var lStep = TradeSteps.OrderByDescending(x => x.Created).FirstOrDefault(x => x.Type != TradeStepType.Hide);
            if(  lStep.Type == TradeStepType.Filled)
                return Signal.TradeDirection == TradeDirection.Long ? (LastClose - Entry) * Size : (Entry - LastClose) * Size;
            if(  lStep.Type == TradeStepType.Closed)
                return Signal.TradeDirection == TradeDirection.Long ? (Profit - Entry) * Size : (Entry - Profit) * Size;
            return null;

        } }
        public decimal? atRisk { get{
                        if (TradeSteps.Count == 0) return null ;
            var lStep = TradeSteps.OrderByDescending(x => x.Created).FirstOrDefault(x => x.Type != TradeStepType.Hide);
            if(  lStep.Type == TradeStepType.Filled)
                return Signal.TradeDirection == TradeDirection.Long ? (Entry - Stop) * Size : (Stop - Entry) * Size;
            return null;
        } }

        public decimal? PercentPnL{ get{
            if (PnL == null) return null;
            var entryStep = TradeSteps.OrderByDescending(x => x.Created).FirstOrDefault(x => x.Type == TradeStepType.Placed);
            if(entryStep == null) return null;
            var entry = (decimal) entryStep.EntryLevel;
            var stop = (decimal)entryStep.StopLevel;
            var tp = (decimal)entryStep.ProfitLevel;
            Decimal res;
            var r = (Math.Abs(entry - stop));
            var tpr = (Math.Abs(tp - entry));
            if (PnL <0 ){
                res = 1 - (Math.Abs( (Decimal) LastClose - stop)/r);
            }else{
                res = 1 - (Math.Abs(  tp - (Decimal) LastClose)/tpr);
            }
            return res;
        }}
        public virtual ICollection<TradeStepResource> TradeSteps { get; set; }
        public virtual ICollection<OrderResource> Orders { get; set; }

        public TradeResource()
        {
            TradeSteps = new HashSet<TradeStepResource>();
            Orders = new HashSet<OrderResource>();
        }
    }
}