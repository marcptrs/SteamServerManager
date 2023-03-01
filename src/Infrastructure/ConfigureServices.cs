using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SteamServerManager.Application.Common.Services.Data;
using SteamServerManager.Application.Common.Services.Identity;
using SteamServerManager.Infrastructure.Data;
using SteamServerManager.Infrastructure.Data.Interceptors;
using SteamServerManager.Infrastructure.Identity;

namespace SteamServerManager.Infrastructure;

public static class ConfigureServices
{
	public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
	{
		var connectionString = configuration.GetConnectionString("DefaultConnection") ??
		                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

		services.AddDbContext<ApplicationDbContext>(options =>
			options.UseSqlite(connectionString));
		
		services.AddScoped<ApplicationDbContextInitialiser>();

		services.AddScoped<IApplicationDbContext>(sp =>
			sp.GetRequiredService<ApplicationDbContext>());

		services.AddScoped<AuditableEntitySaveChangesInterceptor>();

		services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
			.AddRoles<ApplicationRole>()
			.AddEntityFrameworkStores<ApplicationDbContext>()
			.AddClaimsPrincipalFactory<ApplicationUserClaimsPrincipalFactory>();

		services.AddIdentityServer()
			.AddApiAuthorization<ApplicationUser, ApplicationDbContext>(options =>
			{
				options.IdentityResources["openid"].UserClaims.Add("role");
				options.ApiResources.Single().UserClaims.Add("role");
				options.IdentityResources["openid"].UserClaims.Add("permissions");
				options.ApiResources.Single().UserClaims.Add("permissions");
			});

		JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("role");

		services.AddScoped<IIdentityService, IdentityService>();

		return services;
	}
}