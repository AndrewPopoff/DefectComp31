using System;
using System.Linq;
using DefectComp.Models.NotesTable;

namespace DefectComp.Models.OrdersTable
{
    public class OrderViewModel
    {
        public IQueryable<Order> Orders { get; set; }
        public int Page { get; set; }
        public string Sort { get; set; }
        public int CurrentID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int FlagClosed { get; set; }
    }
}
