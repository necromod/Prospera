using Microsoft.EntityFrameworkCore;
using Prospera.Data;
using Prospera.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Prospera.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

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
            var azureDbName = "prosperamodel"; 
            var azureDbUser = "ProsperaModel";
            var azureDbPassword = "Prospera2023@";
            var connectionString = $"Server={azureDbServer};Database={azureDbName};User Id={azureDbUser};Password={azureDbPassword};Trusted_Connection=False;Encrypt=True;";

            builder.Services.AddDbContext<ProsperaContext>(options =>
                options.UseSqlServer(connectionString));

            // Configure o ProsperaContext
            builder.Services.AddDbContext<ProsperaContext>(options =>
                options.UseSqlServer(connectionString));

            // Adicione o serviço do UsuarioController
            builder.Services.AddScoped<UsuarioController>();
            // Adicione o serviço do LoginController
            builder.Services.AddScoped<LoginController>();

            builder.Services.AddScoped<TerceirosViewModel, TerceirosViewModelInterface>();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped<SessaoInterface, Sessao>();
            builder.Services.AddSession(o =>
            {
                o.IdleTimeout = TimeSpan.FromMinutes(5);
                o.Cookie.HttpOnly = true;
                o.Cookie.IsEssential =true;
            });

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
            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            // Adicione um filtro personalizado de autorização aqui
            var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().WithMetadata(new CustomAuthorizeFilter(policy));
            });

            app.MapControllerRoute(
            name: "exibicao2",
            pattern: "Exibicao2",
            defaults: new { controller = "Home", action = "CarregarExibicao2" }
            );

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.Run();


        }
    }
}
