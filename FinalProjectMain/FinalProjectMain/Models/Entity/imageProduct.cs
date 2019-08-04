using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProjectMain.Models.Entity
{
    public class imageProduct : BaseEntity
    {
        public string imgPath { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}