using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace DefectComp.Models.ViewModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Пожалуйста, введите имя пользователя!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите пароль!")]
        [UIHint("password")]
        public string Pwd { get; set; }

        public string ReturnUrl { get; set; } = "/";
    }
}
