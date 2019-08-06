using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProjectMain.Models.Entity
{
    public class mainCarousel : BaseEntity
    {
        public string imgPath { get; set; }
        public string title { get; set; }
        public DateTime? changedDate { get; set; }
    }
}