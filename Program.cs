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

            builder.Services.AddControllers();

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

            app.UseCors("AllowAll");
            app.MapControllers();

            app.Run();
        }
    }
}
