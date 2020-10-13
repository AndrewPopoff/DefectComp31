using System.ComponentModel.DataAnnotations;

namespace DefectComp.Models.DepartmentsTable
{
    public class Department
    {
        [Key]
        public int DepartmentID { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите наименование отдела!")]
        public string DepartmentDesc { get; set; }

        public string DepColor { get; set; }

        public string DepStyle { get; set; }
    }
}
