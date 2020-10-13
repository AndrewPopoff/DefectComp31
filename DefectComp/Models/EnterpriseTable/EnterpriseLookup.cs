using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NonFactors.Mvc.Lookup;

namespace DefectComp.Models.EnterpriseTable
{
    public class EnterpriseLookup : ALookup<Enterprise>
    {
        private ApplicationDBContext context;

        public EnterpriseLookup(ApplicationDBContext ctx)
        {
            context = ctx;
            GetId = (p) => p.EnterpriseID.ToString();
            GetLabel = (model) => model.EnterpriseDesc;
        }

        public override IQueryable<Enterprise> GetModels()
        {
            return context.Set<Enterprise>();
        }

    }
}
