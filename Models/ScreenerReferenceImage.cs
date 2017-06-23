   using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using EntityFrameworkCore.Triggers;
    
    namespace trading.Models
{
    public enum CellColor
    {
        Undefined = 0,
        Red = 1,
        Green = 2,
        Black = 3,
        White = 4,
        DarkGreen = 5,
        Yellow = 6
    }

    public partial class ScreenerReferenceImage  : BaseTable
    {
        
        public int? BrokerInstrumentId { get; set; }
        public int ScreenerTypeId { get;  set; }
        public string ImageSignature { get; set; }

        public virtual BrokerInstrument BrokerInstrument { get; set; }
        
        public virtual ScreenerType ScreenerType { get; set; }

        public CellColor CellColor { get; set; }
        public bool Unused { get; set; } = false;

    }
}
