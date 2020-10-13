using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DefectComp.Models.SCsTable;
using DefectComp.Models.GoodsTable;
using DefectComp.Models;
using DefectComp.Models.EnterpriseTable;
using DefectComp.Models.StatusesTable;
using DefectComp.Models.CompensationTypesTable;
using DefectComp.Models.DepartmentsTable;
using DefectComp.Models.NotesTable;
using DefectComp.Models.CommonLogTable;

namespace DefectComp.Models.OrdersTable
{
    public interface IOrderRepository
    {
        IQueryable<Order> Orders { get; }
        IQueryable<SC> SCs { get; }
        IQueryable<Goods> Goods { get; }
        IQueryable<Enterprise> Enterprises { get; }
        IQueryable<Status> Statuses { get; }
        IQueryable<CompensationType> CompensationTypes { get; }
        IQueryable<Department> Departments { get; }
        IQueryable<Note> Notes { get; }
        IQueryable<CommonLog> CommonLogs { get; }
        ApplicationDBContext Context { get; }
        void Save(Order order);
        Order Delete(int ID);
    }
}
