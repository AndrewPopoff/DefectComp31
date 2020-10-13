using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefectComp.Models.DepartmentsTable
{
    public class DepartmentViewModel
    {
        public IQueryable<Department> Departments { get; set; }
        public int Page { get; set; }
        public string Sort { get; set; }
        public int CurrentID { get; set; }
    }
}
