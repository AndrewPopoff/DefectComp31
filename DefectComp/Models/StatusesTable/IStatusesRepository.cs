using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefectComp.Models.StatusesTable
{
    public interface IStatusesRepository
    {
        IEnumerable<Status> Statuses { get; }
        void Save(Status status);
        Status Delete(int statusID);
    }
}
