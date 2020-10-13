using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DefectComp.Models.CompensationTypesTable
{
    public class CompensationType
    {
        [Key]
        public int TypeID { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите наименование типа компенсации!")]
        public string TypeDesc { get; set; }
    }
}
