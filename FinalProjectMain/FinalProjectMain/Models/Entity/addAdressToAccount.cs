using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalProjectMain.Models.Entity
{
    public class addAdressToAccount : BaseEntity
    {
        public string UserSecondName { get; set; }
        [Required(ErrorMessage = "Adınızı Daxil Edin")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Soyadınızı Daxil Edin")]
        public string UserSurname { get; set; }
        public int CountryId { get; set; }
        public virtual Countries Countries { get; set; }
        public string City { get; set; }
        [Required(ErrorMessage = "Adresinizi Daxil Edin")]
        public string Adress { get; set; }
        public string Adress2 { get; set; }
        public string Phone { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}