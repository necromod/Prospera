using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Prospera.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Prospera
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Adicione a configuração do arquivo appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .Build();

            // Configurar a string de conexão para o Azure SQL com as informações fornecidas
            var azureDbServer = "prosperamodel.database.windows.net";
            var azureDbName = "prosperamodel"; // Substitua pelo nome do seu banco de dados
            var azureDbUser = "ProsperaModel";
            var azureDbPassword = "Prospera2023@";
            var connectionString = $"Server={azureDbServer};Database={azureDbName};User Id={azureDbUser};Password={azureDbPassword};Trusted_Connection=False;Encrypt=True;";

            builder.Services.AddDbContext<ProsperaContext>(options =>
                options.UseSqlServer(connectionString));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
