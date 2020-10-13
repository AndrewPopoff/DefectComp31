using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace DefectComp.Models.Users
{
    public class AppUser : IdentityUser
    {
        public AppUser(string userName) : base(userName) { }
    }
}
