using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProjectMain.Models.Entity
{
    public class SubCategory : BaseEntity
    {
        public string SubCategoryName { get; set; }
        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}