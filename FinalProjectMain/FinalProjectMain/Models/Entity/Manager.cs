using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalProjectMain.Models.Entity
{
    public class Manager : BaseEntity
    {
        [Required(ErrorMessage ="Zəhmət olmasa ad daxil edin!!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Zəhmət olmasa soyad daxil edin!!")]
        public string Surname { get; set; }
        [EmailAddress,Required(ErrorMessage = "Zəhmət olmasa ad daxil edin!!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Zəhmət olmasa ələqə nömrəsi daxil edin!!")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Zəhmət olmasa şifrə daxil edin!!")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Zəhmət olmasa vəzifə seçin!!")]
        public int? ManagerStatusId { get; set; }
        public virtual ManagerStatus ManagerStatus{ get; set; }
    }
}