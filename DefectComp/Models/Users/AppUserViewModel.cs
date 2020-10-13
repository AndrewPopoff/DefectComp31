using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefectComp.Models.Users
{
    public class AppUserViewModel
    {
        public IQueryable<AppUser> AppUsers { get; set; }
        public int Page { get; set; }
        public string Sort { get; set; }
        public string CurrentID { get; set; }
    }
}


