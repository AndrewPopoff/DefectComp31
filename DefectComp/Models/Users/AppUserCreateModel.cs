using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DefectComp.Models.Users
{
    public class AppUserCreateModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите имя пользователя!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите пароль!")]
        public string Pwd { get; set; }
        public int Page { get; set; }
        public string Sort { get; set; }
        public string ReturnUrl { get; set; }
        public string CurrentID { get; set; }
    }
}

