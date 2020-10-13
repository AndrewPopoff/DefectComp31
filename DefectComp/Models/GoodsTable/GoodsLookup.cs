using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NonFactors.Mvc.Lookup;

namespace DefectComp.Models.GoodsTable
{
    public class GoodsLookup : ALookup<Goods>
    {
        private ApplicationDBContext context;

        public GoodsLookup(ApplicationDBContext ctx)
        {
            context = ctx;
            GetId = (p) => p.GoodsID.ToString();
            GetLabel = (model) => model.GoodsName;
        }

        public override IQueryable<Goods> GetModels()
        {
            return context.Set<Goods>();
        }
    }
}
