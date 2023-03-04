using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SteamServerManager.Application.Common.Services.Identity;
using SteamServerManager.Infrastructure.Data;

namespace SteamServerManager.Application.IntegrationTests;

using static Testing;

internal class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(configurationBuilder =>
        {
            var integrationConfig = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            configurationBuilder.AddConfiguration(integrationConfig);
        });
        
        builder.ConfigureServices((b, services) =>
        {
            services
                .Remove<ICurrentUser>()
                .AddTransient(_ => Mock.Of<ICurrentUser>(s =>
                    s.UserId == GetCurrentUserId()));
            
            services
                .Remove<DbContextOptions<ApplicationDbContext>>()
                .AddDbContext<ApplicationDbContext>((_, options) =>
                    options.UseSqlite(b.Configuration.GetConnectionString("DefaultConnection"),
                        bb => bb.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        });
    }
}