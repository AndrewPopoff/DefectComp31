using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefectComp.Models.Users
{
    public class AppUserEditModel
    {
        public AppUser AppUser { get; set; }
        public int Page { get; set; }
        public string Sort { get; set; }
        public string ReturnUrl { get; set; }
        public string CurrentID { get; set; }
    }
}
