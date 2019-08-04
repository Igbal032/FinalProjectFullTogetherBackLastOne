using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProjectMain.Models.Entity
{
    public class additionalInformation : BaseEntity
    {
        public int Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string Whatsapp { get; set; }
    }
}