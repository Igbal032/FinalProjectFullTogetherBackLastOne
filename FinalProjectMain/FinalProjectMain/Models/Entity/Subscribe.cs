using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProjectMain.Models.Entity
{
    public class Subscribe : BaseEntity
    {
        public string Email { get; set; }
        public bool isConfirm  { get; set; }
        public DateTime? confirmDate { get; set; }
    }
}