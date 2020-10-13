using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DefectComp.Models.StatusesTable
{
    public class EFStatusesRepository : IStatusesRepository
    {
        public ApplicationDBContext context;
        public EFStatusesRepository(ApplicationDBContext ctx) => context = ctx;

        public ApplicationDBContext Context => context;

        public IEnumerable<Status> Statuses => context.Statuses;

        // удалить запись
        public Status Delete(int statusID)
        {
            Status dbEntry = context.Statuses.FirstOrDefault(p => p.StatusID == statusID);
            if (dbEntry != null)
            {
                context.Statuses.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public void Save(Status status)
        {
            if (status.StatusID == 0)
            {
                context.Statuses.Add(status);
            }
            else
            {
                Status dbEntry = context.Statuses.FirstOrDefault(p => p.StatusID == status.StatusID);
                if (dbEntry != null)
                {
                    dbEntry.StatusID = status.StatusID;
                    dbEntry.StatusDesc = status.StatusDesc;
                }
            }
            context.SaveChanges();
        }
    }
}
