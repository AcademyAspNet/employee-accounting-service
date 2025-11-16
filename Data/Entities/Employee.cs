using System.ComponentModel.DataAnnotations;

namespace Employee_Accounting_Service.Data.Entities
{
    public class Employee
    {
        public int Id { get; set; }

        [MaxLength(150)]
        public required string FirstName { get; set; }

        [MaxLength(175)]
        public required string LastName { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int DepartmentId { get; set; }

        public Department? Department { get; set; }
    }
}
