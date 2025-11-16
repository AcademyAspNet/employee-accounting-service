using System.ComponentModel.DataAnnotations;

namespace Employee_Accounting_Service.Data.Entities
{
    public class Department
    {
        public int Id { get; set; }

        [MaxLength(150)]
        public required string Name { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int CompanyId { get; set; }

        public Company? Company { get; set; }

        public IList<Employee> Employees { get; set; } = [];
    }
}
