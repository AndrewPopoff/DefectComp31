using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DefectComp.Models.GoodsTable
{
    public class EFGoodsRepository : IGoodsRepository
    {
        public ApplicationDBContext context;
        public EFGoodsRepository(ApplicationDBContext ctx) => context = ctx;

        public ApplicationDBContext Context => context;

        public IQueryable<Goods> Goods => context.Goods;
    }
}


