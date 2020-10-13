using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefectComp.Models.SCsTable
{
    public interface ISCRepository
    {
        IEnumerable<SC> SCs { get; }
        void Save(SC sc);
        SC Delete(int SC_ID);
    }
}
