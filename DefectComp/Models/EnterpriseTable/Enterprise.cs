using NonFactors.Mvc.Lookup;
using System.ComponentModel.DataAnnotations;

namespace DefectComp.Models.EnterpriseTable
{
    public class Enterprise
    {
        [Key]
        public int EnterpriseID { get; set; }

        [LookupColumn]
        [Display(Name = "Наименование предприятия")]
        [Required(ErrorMessage = "Пожалуйста, введите наименование предприятия!")]
        public string EnterpriseDesc { get; set; }
    }
}


