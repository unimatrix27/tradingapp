   using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using EntityFrameworkCore.Triggers;
    
    namespace trading.Models
{
 
    public partial class ScreenerEntryType  : BaseTable
    {
        

        [Required]
        [StringLength(40)]
        public string Name { get; set; }


        public virtual ICollection<ScreenerEntryMapping> ScreenerEntryMappings { get; set; }

        public ScreenerEntryType()
        {
            ScreenerEntryMappings = new HashSet<ScreenerEntryMapping>();
        }
    }
}
