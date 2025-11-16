using System.ComponentModel.DataAnnotations;

namespace Employee_Accounting_Service.Models
{
    public class CompanyDto
    {
        [Required(ErrorMessage = "Укажите имя компании")]
        [MinLength(3, ErrorMessage = "Имя слишком короткое")]
        [MaxLength(100, ErrorMessage = "Имя компании слишком длинное")]
        public string? Name { get; set; }
    }
}
