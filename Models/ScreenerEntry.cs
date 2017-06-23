   using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using EntityFrameworkCore.Triggers;
    
    namespace trading.Models
{
 
    public partial class ScreenerEntry  : BaseTable
    {
        public CellColor Fg { get; set; } = CellColor.Undefined;
        public CellColor Bg { get; set; } = CellColor.Undefined;
        public int? value { get; set; }
        public int ScreenerEntryTypeId { get; set; }
        public int ScreenerLineId { get; set; }
        public virtual ScreenerEntryType ScreenerEntryType { get; set; }
        public virtual ScreenerLine ScreenerLine { get; set; }
    }
}
