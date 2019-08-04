using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProjectMain.Models.Entity
{
    public class OrderHistory : BaseEntity
    {
        public int productId { get; set; }
        public virtual Product product { get; set; }
        public int userId { get; set; }
        public virtual User user { get; set; }
    }
}