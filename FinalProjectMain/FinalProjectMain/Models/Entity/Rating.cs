using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProjectMain.Models.Entity
{
    public class Rating : BaseEntity
    {
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int starCount { get; set; }
        public int currentStar { get; set; }
        public int totalStarLevel { get; set; }
    }
}