using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DefectComp.Models.SCsTable
{
    public class EFSCRepository : ISCRepository
    {
        private ApplicationDBContext context;
        public EFSCRepository(ApplicationDBContext ctx) => context = ctx;
        public ApplicationDBContext Context => context;

        public IEnumerable<SC> SCs => context.SCs;

        // удалить запись
        public SC Delete(int SC_ID)
        {
            SC dbEntry = context.SCs.FirstOrDefault(p => p.SC_ID == SC_ID);
            if (dbEntry != null)
            {
                context.SCs.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        // сохранить запись
        public void Save(SC sc)
        {
            if (sc.SC_ID == 0)
            {
                context.SCs.Add(sc);
            }
            else
            {
                SC dbEntry = context.SCs.FirstOrDefault(p => p.SC_ID == sc.SC_ID);
                if (dbEntry != null)
                {
                    dbEntry.SC_ID = sc.SC_ID;
                    dbEntry.SCDesc = sc.SCDesc;
                }
            }
            context.SaveChanges();
        }
    }
}
