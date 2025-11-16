using Employee_Accounting_Service.Data;
using Employee_Accounting_Service.Data.Entities;
using Employee_Accounting_Service.Models;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Accounting_Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ApplicationDbContext>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            var app = builder.Build();

            app.MapGet("/api/v1/companies", ([FromServices] ApplicationDbContext database) =>
            {
                return database.Companies.ToList();
            });

            app.MapPost("/api/v1/companies", (
                [FromServices] ApplicationDbContext database,
                [FromBody] CompanyDto companyDto
            ) =>
            {
                string companyName = companyDto.Name?.Trim() ?? "";

                if (string.IsNullOrWhiteSpace(companyName))
                    return Results.BadRequest("Company name must not be null or empty!");

                Company? foundCompany = database.Companies.FirstOrDefault(
                    c => c.Name == companyName
                );

                if (foundCompany != null)
                    return Results.BadRequest("Company with specified name already exists!");

                Company company = new Company()
                {
                    Name = companyName
                };

                database.Companies.Add(company);
                database.SaveChanges();

                return Results.Ok(company);
            });

            app.UseCors("AllowAll");

            app.Run();
        }
    }
}
