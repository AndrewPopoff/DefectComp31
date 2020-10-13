using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DefectComp.Models.Users;


namespace DefectComp.Models
{
    public static class IdentitySeedData
    {
        private const string adminUser = "Admin";
        private const string adminPwd = "Sct ,eltn666"; // "Все будет666"

        public static async void EnsurePopulated(IApplicationBuilder app)
        {
            UserManager<AppUser> userManager = app.ApplicationServices.GetRequiredService<UserManager<AppUser>>();
            if(userManager.Users.Count() == 0)
            {
                AppUser user = new AppUser("Admin");
                await userManager.CreateAsync(user, adminPwd);
            }
        }
    }
}


