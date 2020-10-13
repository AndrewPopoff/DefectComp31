using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DefectComp.Models.CommonLogTable
{
    public class EFCommonLogRepository : ICommonLogRepository
    {
        private ApplicationDBContext context;
        public ApplicationDBContext Context => context;

        public EFCommonLogRepository(ApplicationDBContext ctx) => context = ctx;

        public IEnumerable<CommonLog> CommonLogs => context.CommonLogs;

        // сохранить запись - здесь только добавление
        public void Save(CommonLog commonLog)
        {
            if (commonLog.RecordID == 0)
            {
                context.CommonLogs.Add(commonLog);
            }
            context.SaveChanges();
        }
    }
}


