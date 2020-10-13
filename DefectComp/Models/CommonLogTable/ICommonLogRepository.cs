using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefectComp.Models.CommonLogTable
{
    public interface ICommonLogRepository
    {
        IEnumerable<CommonLog> CommonLogs { get; }
        void Save(CommonLog commonLog);
    }
}
