using Microsoft.EntityFrameworkCore;
using Prospera.Data;
using Prospera.Helpers;
using Microsoft.AspNetCore.Authorization;
using Prospera.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;

namespace Prospera
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Get connection string from configuration (appsettings.json, environment, or user-secrets)
            var connectionString = builder.Configuration.GetConnectionString("ProsperaContext");

            // If connection string uses Azure AD Authentication keyword, set appropriate token provider
            if (!string.IsNullOrWhiteSpace(connectionString) && connectionString.Contains("Authentication=\"Active Directory Default\"", StringComparison.OrdinalIgnoreCase))
            {
                // Use Default Azure Credential to obtain access token at runtime when running in Azure
                builder.Services.AddSingleton<TokenAcquisitionService>();

                builder.Services.AddDbContext<ProsperaContext>((serviceProvider, options) =>
                {
                    var tokenService = serviceProvider.GetRequiredService<TokenAcquisitionService>();
                    var sqlConnection = new SqlConnection(connectionString);
                    options.UseSqlServer(sqlConnection, sqlOptions =>
                    {
                        // additional SQL Server options if needed
                    }).EnableSensitiveDataLogging();

                    // Acquire access token and register an interceptor to set it per connection open
                    options.AddInterceptors(new SqlAccessTokenInterceptor(tokenService));
                });
            }
            else
            {
                // Register DbContext once using the resolved connection string
                builder.Services.AddDbContext<ProsperaContext>(options =>
                    options.UseSqlServer(connectionString));
            }

            // Dependency injection and services
            builder.Services.AddScoped<UsuarioController>();
            builder.Services.AddScoped<LoginController>();

            builder.Services.AddScoped<TerceirosViewModel, TerceirosViewModelInterface>();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped<SessaoInterface, Sessao>();

            builder.Services.AddSession(o =>
            {
                o.IdleTimeout = TimeSpan.FromMinutes(5);
                o.Cookie.HttpOnly = true;
                o.Cookie.IsEssential = true;
            });

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

                endpoints.MapControllerRoute(
                    name: "BuscarDespesas",
                    pattern: "Contas/BuscarDespesas/{id?}",
                    defaults: new { controller = "Contas", action = "BuscarDespesas" }
                );
            });

            app.MapControllerRoute(
                name: "exibicao2",
                pattern: "Exibicao2",
                defaults: new { controller = "Home", action = "CarregarExibicao2" }
            );

            app.Run();
        }
    }
}
