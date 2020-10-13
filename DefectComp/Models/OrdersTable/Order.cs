using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DefectComp.Models.GoodsTable;
using DefectComp.Models.EnterpriseTable;
using DefectComp.Models.StatusesTable;
using DefectComp.Models.CompensationTypesTable;
using DefectComp.Models.DepartmentsTable;
using DefectComp.Models.SCsTable;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DefectComp.Models.OrdersTable
{
    public class Order
    {
        [Key]
        public int ID { get; set; }
        public int ? Number { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите дату документа!")]
        public DateTime ? DocDate { get; set; }
        public string RefNumber { get; set; }
        public DateTime ? RefDate { get; set; }
        public string CustomerDesc { get; set; }

        public int ? GoodsID { get; set; }
        public Goods Goods { get; set; }

        public string Model { get; set; }
        public string SN { get; set; }

        [ForeignKey("Seller")]
        public int ? SellerID { get; set; }
        public Enterprise Seller { get; set; }

        public int ? StatusID { get; set; }
        public Status Status { get; set; }

        [ForeignKey("CompensationType")]
        public int ? CompTypeID { get; set; }
        public CompensationType CompensationType { get; set; }

        public int ? DepartmentID { get; set; }
        public Department Department { get; set; }

        public byte FlagClosed { get; set; }
        public DateTime ? ClosedDate { get; set; }

        [ForeignKey("Provider")]
        public int? ProviderID { get; set; }
        public Enterprise Provider { get; set; }

        public string SFNumber { get; set; }
        public DateTime ? SFDate { get; set; }
        public decimal ? GoodsCost { get; set; }

        [ForeignKey("SC")]
        public int? SC_ID { get; set; }
        public SC SC { get; set; }

        public DateTime ? ProduceDate { get; set; }
        public DateTime ? ProviderSaleDate { get; set; }
        public string NumInvoiceOPT { get; set; }
        public DateTime ? DateInvoiceOPT { get; set; }

        [NotMapped]
        public string PeriodDays { get; set; }

        [NotMapped]
        public bool FlagClosedBool
        {
            get { return FlagClosed > 0; }
            set { FlagClosed = Convert.ToByte(value); }
        }

        [NotMapped]
        public IEnumerable<SelectListItem> itemsSC { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem> itemsStatus { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem> itemsCompType { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem> itemsDep { get; set; }
    }
}
