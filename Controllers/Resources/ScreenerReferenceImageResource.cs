   using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using EntityFrameworkCore.Triggers;
using trading.Controllers.Resources;

namespace trading.Models
{


    public partial class ScreenerReferenceImageResource  : BaseTable
    {
        
        public int? BrokerInstrumentId { get; set; }
        public int ScreenerTypeId { get;  set; }
        public string ImageSignature { get; set; }

        public virtual BrokerInstrumentResource BrokerInstrument { get; set; }
        
        public virtual ScreenerTypeResource ScreenerType { get; set; }

        public CellColor CellColor { get; set; }
        public string CellColorName {get{
            switch (CellColor){
                case CellColor.Undefined:
                    return "undefined";
                case CellColor.Red:
                    return "red";
                case CellColor.Green:
                    return "green";
                case CellColor.Black:
                    return "black";
                case CellColor.White:
                    return "white";
                case CellColor.DarkGreen:
                    return "darkgreen";
                case CellColor.Yellow:
                    return "yellow";
                default:
                    return "error";
            }
        }}
        public bool Unused { get; set; }
        public int UnreferencedImages { get; set; }



    }
}
