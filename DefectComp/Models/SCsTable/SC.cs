using System.ComponentModel.DataAnnotations;

namespace DefectComp.Models.SCsTable
{
    public class SC
    {
        [Key]
        public int SC_ID { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите наименование сервисного центра!")]
        public string SCDesc { get; set; }
    }
}


