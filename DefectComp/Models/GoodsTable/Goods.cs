using System.ComponentModel.DataAnnotations;
using DefectComp.Models.OrdersTable;
using System.Linq;
using NonFactors.Mvc.Lookup;

namespace DefectComp.Models.GoodsTable
{
    public class Goods
    {
        [Key]
        [LookupColumn]
        [Display(Name = "Артикул")]
        public int GoodsID { get; set; }
        [LookupColumn]
        [Display(Name = "Наименование товара")]
        public string GoodsName { get; set; }

        //public virtual IQueryable<Order> Orders { get; set; }
    }
}

