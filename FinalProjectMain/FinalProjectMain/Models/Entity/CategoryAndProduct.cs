using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProjectMain.Models.Entity
{
    public class CategoryAndProduct : BaseEntity
    {
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }


    }
}