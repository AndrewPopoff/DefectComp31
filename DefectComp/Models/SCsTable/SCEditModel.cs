using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefectComp.Models.SCsTable
{
    public class SCEditModel
    {
        public SC SC { get; set; }
        public int Page { get; set; }
        public string Sort { get; set; }
        public string ReturnUrl { get; set; }
        public int CurrentID { get; set; }
    }
}

