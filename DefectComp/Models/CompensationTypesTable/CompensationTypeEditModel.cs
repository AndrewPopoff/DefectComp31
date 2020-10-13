using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefectComp.Models.CompensationTypesTable
{
    public class CompensationTypeEditModel
    {
        public CompensationType CompensationType { get; set; }
        public int Page { get; set; }
        public string Sort { get; set; }
        public string ReturnUrl { get; set; }
        public int CurrentID { get; set; }
    }
}

