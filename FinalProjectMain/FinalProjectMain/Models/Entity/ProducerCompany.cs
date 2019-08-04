using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProjectMain.Models.Entity
{
    public class ProducerCompany : BaseEntity
    {
        public string ProducerName { get; set; }
        public virtual ICollection<Product> Products  { get; set; }

    }
}