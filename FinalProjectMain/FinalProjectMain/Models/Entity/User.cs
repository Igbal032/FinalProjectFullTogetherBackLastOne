using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalProjectMain.Models.Entity
{
    public class User : BaseEntity
    {
        [Required(ErrorMessage = "Adınızı Daxil Edin")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Soyadınızı Daxil Edin")]
        public string UserSurname { get; set; }
        public string UserAge { get; set; }
        [Required(ErrorMessage = "Cinsinizi Seçin")]
        public int GenderId { get; set; }
        public virtual Gender Gender { get; set; }
        [Required(ErrorMessage = "Emailinizi Daxil Edin")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Şifrənizi Daxil Edin")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Doğum tarixini daxil edin")]
        [DataType(DataType.DateTime)]
        public DateTime BirthDate { get; set; }
        public bool? Notification { get; set; }
        public bool isBlock { get; set; }
    }
}