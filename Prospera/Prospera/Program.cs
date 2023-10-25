using Microsoft.EntityFrameworkCore;
using Prospera.Data;
using Prospera.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

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

            /*// Adicione a configuração de autorização aqui
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AcessoComum", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireRole("Comum");
                });

                options.AddPolicy("AcessoAdmin", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireRole("Admin");
                });
            });

            builder.Services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();

                options.Filters.Add(new CustomAuthorizeFilter(policy));
            });*/


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
                pattern: "{controller=Usuario}/{action=login}/{id?}");

            // Adicione um filtro personalizado de autorização aqui
            var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().WithMetadata(new CustomAuthorizeFilter(policy));
            });

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.Run();


        }
    }
}
