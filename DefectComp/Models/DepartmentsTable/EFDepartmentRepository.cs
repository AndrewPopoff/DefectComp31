using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace DefectComp.Models.DepartmentsTable
{
    public class EFDepartmentRepository : IDepartmentRepository
    {
        private ApplicationDBContext context;
        public EFDepartmentRepository(ApplicationDBContext ctx) => context = ctx;
        public ApplicationDBContext Context => context;

        public IEnumerable<Department> Departments => context.Departments;

        // удалить запись
        public Department Delete(int departmentID)
        {
            Department dbEntry = context.Departments.FirstOrDefault(p => p.DepartmentID == departmentID);
            if (dbEntry != null)
            {
                context.Departments.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        // сохранить запись
        public void Save(Department department)
        {
            if (department.DepartmentID == 0)
            {
                context.Departments.Add(department);
            }
            else
            {
                Department dbEntry = context.Departments.FirstOrDefault(p => p.DepartmentID == department.DepartmentID);
                if (dbEntry != null)
                {
                    dbEntry.DepartmentID = department.DepartmentID;
                    dbEntry.DepartmentDesc = department.DepartmentDesc;
                    dbEntry.DepColor = department.DepColor;
                    dbEntry.DepStyle = department.DepStyle;
                }
            }
            context.SaveChanges();
        }

    }
}





