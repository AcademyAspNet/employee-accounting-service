using System.ComponentModel.DataAnnotations;

namespace Employee_Accounting_Service.Data.Entities
{
    public class Company
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public required string Name { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public IList<Department> Departments { get; set; } = [];
    }
}
