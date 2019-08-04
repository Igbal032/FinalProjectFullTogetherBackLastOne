using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProjectMain.Models.Entity
{
    public class Product : BaseEntity
    {

        public string Name { get; set; }
        public string About { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPrice { get; set; }
        public bool isWaitListForAdmin { get; set; }
        public bool isSold { get; set; }
        public int LikeCount { get; set; }
        public int starLevel { get; set; }
        public string Comment { get; set; }
        public string SubCategory { get; set; }
        public int colorId { get; set; }
        public virtual Color color { get; set; }
        public decimal SizeORCount { get; set; }
        public virtual Size size { get; set; }
        public bool isConfirm { get; set; }
        public int ConfirmId { get; set; }
        public DateTime? sharedDate { get; set; }
        public string imgPath { get; set; }
        public int ProducerId { get; set; }
        public virtual ProducerCompany Producer { get; set; }
        public int? SharedId { get; set; }
        public virtual Manager Shared { get; set; }

    }
}