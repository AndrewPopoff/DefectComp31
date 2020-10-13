using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefectComp.Models.CompensationTypesTable
{
    public class CompensationTypeViewModel
    {
        public IQueryable<CompensationType> CompensationTypes { get; set; }
        public int Page { get; set; }
        public string Sort { get; set; }
        public int CurrentID { get; set; }
    }
}
