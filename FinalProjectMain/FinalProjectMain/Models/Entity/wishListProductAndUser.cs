using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalProjectMain.Models.Entity
{
    public class wishListProductAndUser : BaseEntity
    {
        public int? UserId { get; set; }
        public virtual User User { get; set; }
        public int ProductsId { get; set; }
        public virtual Product Products { get; set; }
    }
}