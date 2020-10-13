using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DefectComp.Models.CompensationTypesTable
{
    public class EFCompensationTypeRepository : ICompensationTypeRepository
    {
        private ApplicationDBContext context;
        public EFCompensationTypeRepository(ApplicationDBContext ctx) => context = ctx;
        public ApplicationDBContext Context => context;
        public IEnumerable<CompensationType> CompensationTypes => context.CompensationTypes;

        // удалить запись
        public CompensationType Delete(int typeID)
        {
            CompensationType dbEntry = context.CompensationTypes.FirstOrDefault(p => p.TypeID == typeID);
            if (dbEntry != null)
            {
                context.CompensationTypes.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        // сохранить запись
        public void Save(CompensationType ct)
        {
            if (ct.TypeID == 0)
            {
                context.CompensationTypes.Add(ct);
            }
            else
            {
                CompensationType dbEntry = context.CompensationTypes.FirstOrDefault(p => p.TypeID == ct.TypeID);
                if (dbEntry != null)
                {
                    dbEntry.TypeID = ct.TypeID;
                    dbEntry.TypeDesc = ct.TypeDesc;
                }
            }
            context.SaveChanges();
        }
    }
}




    
