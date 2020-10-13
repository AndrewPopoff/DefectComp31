using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefectComp.Models.EnterpriseTable
{
    public interface IEnterpriseRepository
    {
        IEnumerable<Enterprise> Enterprises { get; }
        void Save(Enterprise enterprise);
        Enterprise Delete(int enterpriseID);
    }
}
