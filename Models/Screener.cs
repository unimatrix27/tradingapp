   using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using EntityFrameworkCore.Triggers;
    
    namespace trading.Models
{
 
    public partial class Screener  : BaseTable
    {

        public int? PrevId { get; set; }
        public int? NextId { get; set; }
        public DateTimeOffset TimeStamp { get; set; }

        public string ImageFile { get; set; }
        public bool IsProcessed { get; set; } = false;
        public string ImageHash { get; set; }
        public bool ParseError { get; set; } = false;
        public string ParseErrorString { get; set; }
        public int ScreenerTypeId { get; set; }
        public virtual ScreenerType ScreenerType { get; set; }

        public virtual ICollection<ScreenerLine> ScreenerLines { get; set; }
         public Screener()
        {
            ScreenerLines = new HashSet<ScreenerLine>();
        }
    }
}
