using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefectComp.Models.CommonLogTable
{
    public class CommonLogViewModel
    {
        public IQueryable<CommonLog> CommonLogs { get; set; }
    }
}
