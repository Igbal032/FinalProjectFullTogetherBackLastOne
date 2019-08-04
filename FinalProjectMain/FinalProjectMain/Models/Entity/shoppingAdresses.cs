using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProjectMain.Models.Entity
{
    public class shoppingAdresses : BaseEntity
    {
        public string Name { get; set; }
        public string About { get; set; }
        public string Adress { get; set; }
        public string imgPath { get; set; }
    }
}