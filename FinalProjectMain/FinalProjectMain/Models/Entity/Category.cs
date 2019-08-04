using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProjectMain.Models.Entity
{
    public class Category : BaseEntity
    {
        public string CategoryName { get; set; }
        public bool? withSubCtegory { get; set; }
    }
}