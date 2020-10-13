using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefectComp.Models.CompensationTypesTable
{
    public interface ICompensationTypeRepository
    {
        IEnumerable<CompensationType> CompensationTypes { get; }
        void Save(CompensationType ct);
        CompensationType Delete(int typeID);
    }
}
