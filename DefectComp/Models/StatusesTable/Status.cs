using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DefectComp.Models.StatusesTable
{
    public class Status
    {
        [Key]
        public int StatusID { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите наименование статуса!")]
        public string StatusDesc { get; set; }
    }
}
