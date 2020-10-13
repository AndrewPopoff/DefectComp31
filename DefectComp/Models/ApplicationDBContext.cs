using Microsoft.EntityFrameworkCore;
using DefectComp.Models.GoodsTable;
using DefectComp.Models.StatusesTable;
using DefectComp.Models.EnterpriseTable;
using DefectComp.Models.CompensationTypesTable;
using DefectComp.Models.SCsTable;
using DefectComp.Models.DepartmentsTable;
using DefectComp.Models.OrdersTable;
using DefectComp.Models.NotesTable;
using DefectComp.Models.CommonLogTable;

namespace DefectComp.Models
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }
        public DbSet<Goods> Goods { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Enterprise> Enterprises { get; set; }
        public DbSet<CompensationType> CompensationTypes { get; set; }
        public DbSet<SC> SCs { get; set; }
        public DbSet<Department> Departments {get; set;}
        public DbSet<Order> Orders { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<CommonLog> CommonLogs { get; set; }
    }
}
