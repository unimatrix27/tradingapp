   using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using EntityFrameworkCore.Triggers;
    
    namespace trading.Models
{
 
    public partial class ScreenerEntryMapping  : BaseTable
    {
        public int position { get; set; }
        public bool active { get; set; }
        public int ScreenerTypeId { get; set; }
        public int ScreenerEntryTypeId { get; set; }
        public virtual ScreenerType ScreenerType { get; set; }
        public virtual ScreenerEntryType ScreenerEntryType { get; set; }
    }
}
