using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DefectComp.Models.SCsTable;
using DefectComp.Models.GoodsTable;
using DefectComp.Models.EnterpriseTable;
using DefectComp.Models.StatusesTable;
using DefectComp.Models.CompensationTypesTable;
using DefectComp.Models.DepartmentsTable;
using DefectComp.Models.NotesTable;
using DefectComp.Models.CommonLogTable;

namespace DefectComp.Models.OrdersTable
{
    public class EFOrderRepository : IOrderRepository
    {
        private ApplicationDBContext context;
        public EFOrderRepository(ApplicationDBContext ctx) => context = ctx;
        public ApplicationDBContext Context => context;

        public IQueryable<Order> Orders => context.Orders;
        public IQueryable<SC> SCs => context.SCs;
        public IQueryable<Goods> Goods => context.Goods;
        public IQueryable<Enterprise> Enterprises => context.Enterprises;
        public IQueryable<Status> Statuses => context.Statuses;
        public IQueryable<CompensationType> CompensationTypes => context.CompensationTypes;
        public IQueryable<Department> Departments => context.Departments;
        public IQueryable<Note> Notes => context.Notes;
        public IQueryable<CommonLog> CommonLogs => context.CommonLogs;

        // удалить запись
        public Order Delete(int ID)
        {
            Order dbEntry = context.Orders.FirstOrDefault(p => p.ID == ID);
            if (dbEntry != null)
            {
                context.Orders.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        // сохранить запись
        public void Save(Order order)
        {
            if (order.ID == 0)
            {
                context.Orders.Add(order);
            }
            else
            {
                Order dbEntry = context.Orders.FirstOrDefault(p => p.ID == order.ID);
                if (dbEntry != null)
                {
                    dbEntry.ID = order.ID;
                    dbEntry.Number = order.Number;
                    dbEntry.DocDate = order.DocDate;
                    dbEntry.RefNumber = order.RefNumber;
                    dbEntry.RefDate = order.RefDate;
                    dbEntry.CustomerDesc = order.CustomerDesc;
                    dbEntry.GoodsID = order.GoodsID;
                    dbEntry.Model = order.Model;
                    dbEntry.SN = order.SN;
                    dbEntry.SellerID = order.SellerID;
                    dbEntry.StatusID = order.StatusID;
                    dbEntry.CompTypeID = order.CompTypeID;
                    dbEntry.DepartmentID = order.DepartmentID;
                    dbEntry.FlagClosed = order.FlagClosed;
                    dbEntry.ClosedDate = order.ClosedDate;
                    dbEntry.ProviderID = order.ProviderID;
                    dbEntry.SFNumber = order.SFNumber;
                    dbEntry.SFDate = order.SFDate;
                    dbEntry.GoodsCost = order.GoodsCost;
                    dbEntry.SC_ID = order.SC_ID;
                    dbEntry.ProduceDate = order.ProduceDate;
                    dbEntry.ProviderSaleDate = order.ProviderSaleDate;
                    dbEntry.NumInvoiceOPT = order.NumInvoiceOPT;
                    dbEntry.DateInvoiceOPT = order.DateInvoiceOPT;
                }
            }
            context.SaveChanges();
        }
    }
}

