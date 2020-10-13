using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DefectComp.Models.EnterpriseTable
{
    public class EFEnterpriseRepository : IEnterpriseRepository
    {
        private ApplicationDBContext context;
        public EFEnterpriseRepository(ApplicationDBContext ctx) => context = ctx;
        public ApplicationDBContext Context => context;

        public IEnumerable<Enterprise> Enterprises => context.Enterprises;

        // удалить запись
        public Enterprise Delete(int enterpriseID)
        {
            Enterprise dbEntry = context.Enterprises.FirstOrDefault(p => p.EnterpriseID == enterpriseID);
            if (dbEntry != null)
            {
                context.Enterprises.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        // сохранить запись
        public void Save(Enterprise enterprise)
        {
            if (enterprise.EnterpriseID == 0)
            {
                context.Enterprises.Add(enterprise);
            }
            else
            {
                Enterprise dbEntry = context.Enterprises.FirstOrDefault(p => p.EnterpriseID == enterprise.EnterpriseID);
                if (dbEntry != null)
                {
                    dbEntry.EnterpriseID = enterprise.EnterpriseID;
                    dbEntry.EnterpriseDesc = enterprise.EnterpriseDesc;
                }
            }
            context.SaveChanges();
        }

    }
}



