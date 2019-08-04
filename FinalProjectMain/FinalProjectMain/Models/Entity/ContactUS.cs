using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalProjectMain.Models.Entity
{
    public class ContactUS : BaseEntity
    {
        [Required(ErrorMessage = "Mövzunu daxil Edin")]
        public string Subject { get; set; }
        [EmailAddress, Required(ErrorMessage = "Email daxil Edin")]
        public string Email { get; set; }
        public string filePath { get; set; }
        [Required(ErrorMessage = "Mövzunu daxil Edin")]
        public string Message { get; set; }
        public string AnsweredMessage { get; set; }
        public DateTime answeredDate { get; set; }
        public bool isRead { get; set; }
        public bool isAnswered { get; set; }

    }
}