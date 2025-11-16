using Employee_Accounting_Service.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Employee_Accounting_Service.Data
{
    public class ApplicationDbContext : DbContext
    {
        private const string CONNECTION_STRING_NAME = "Default";

        public DbSet<Company> Companies { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }

        private readonly string _connectionString;

        public ApplicationDbContext(IConfiguration configuration)
        {
            string? connectionString = configuration.GetConnectionString(CONNECTION_STRING_NAME);

            if (string.IsNullOrEmpty(connectionString))
                throw new MissingFieldException($"Failed to get connection string with '{CONNECTION_STRING_NAME}' name");

            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (optionsBuilder.IsConfigured)
                return;

            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Company>()
                        .HasIndex(c => c.Name)
                        .IsUnique();

            modelBuilder.Entity<Company>()
                        .Property(c => c.CreatedAt)
                        .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<Department>()
                        .HasIndex(d => d.Name)
                        .IsUnique();

            modelBuilder.Entity<Department>()
                        .Property(d => d.CreatedAt)
                        .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<Employee>()
                        .Property(e => e.CreatedAt)
                        .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
