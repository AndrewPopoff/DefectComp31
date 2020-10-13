using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefectComp.Models.EnterpriseTable
{
    public class EnterpriseViewModel
    {
        public IQueryable<Enterprise> Enterprises { get; set; }
        public int Page { get; set; }
        public string Sort { get; set; }
        public int CurrentID { get; set; }
    }
}
