using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefectComp.Models.DepartmentsTable
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> Departments { get; }
        void Save(Department department);
        Department Delete(int departmentID);
    }
}
