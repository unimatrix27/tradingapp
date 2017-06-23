   using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using EntityFrameworkCore.Triggers;
    
    namespace trading.Models
{

    public enum IndicatorDataType
    {
        Wolke_SpanA,
        Wolke_SpanB,
        OrigWolke_SpanA,
        OrigWolke_SpanB
    }

    public partial class IndicatorEntry  : BaseTable
    {
        public int PriceEntryId { get; set; }

        [Required]
        public bool IsDirty { get; set; } = false;

        public IndicatorDataType Type { get; set; }
        public decimal Data{ get; set; }

    public virtual PriceEntry PriceEntry { get; set; }

    }
}
