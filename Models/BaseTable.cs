using System;
using EntityFrameworkCore.Triggers;
using trading.Models;

namespace trading.Models
{


    public abstract partial class BaseTable
    {
        public int Id { get; set; }
        public DateTimeOffset? Created { get;  set; }


        public DateTimeOffset? Updated { get;  set; }

         static BaseTable()
         {
             Triggers<BaseTable>.Inserting += entry => entry.Entity.Created = entry.Entity.Updated = DateTimeOffset.Now;
             Triggers<BaseTable>.Updating += entry => entry.Entity.Updated = DateTimeOffset.Now;
         }


    }
}
