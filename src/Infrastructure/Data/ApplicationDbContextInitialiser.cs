using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SteamServerManager.Infrastructure.Identity;
using SteamServerManager.WebUI.Shared.Authorization;

namespace SteamServerManager.Infrastructure.Data;

public class ApplicationDbContextInitialiser
{
        private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;

    private const string AdministratorsRole = "Administrators";
    private const string AccountsRole = "Accounts";
    private const string OperationsRole = "Operations";

    private const string DefaultPassword = "Password123!";

    public ApplicationDbContextInitialiser(
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitialiseAsync()
    {
        await InitialiseWithMigrationsAsync();
    }

    private async Task InitialiseWithDropCreateAsync()
    {
        await _context.Database.EnsureDeletedAsync();
        await _context.Database.EnsureCreatedAsync();
    }

    private async Task InitialiseWithMigrationsAsync()
    {
        await _context.Database.EnsureCreatedAsync();
    }

    public async Task SeedAsync()
    {
        await SeedIdentityAsync();
        //await SeedDataAsync();
    }

    private async Task SeedIdentityAsync()
    {
        // Create roles
        await _roleManager.CreateAsync(
            new ApplicationRole
            {
                Name = AdministratorsRole,
                NormalizedName = AdministratorsRole.ToUpper(),
                Permissions = Permissions.All
            });

        await _roleManager.CreateAsync(
            new ApplicationRole
            {
                Name = AccountsRole,
                NormalizedName = AccountsRole.ToUpper(),
                Permissions =
                    Permissions.ViewUsers
            });

        await _roleManager.CreateAsync(
            new ApplicationRole
            {
                Name = OperationsRole,
                NormalizedName = OperationsRole.ToUpper(),
                Permissions =
                    Permissions.ViewUsers |
                    Permissions.GameServer
            });

        // Ensure admin role has all permissions
        var adminRole = await _roleManager.FindByNameAsync(AdministratorsRole);
        adminRole!.Permissions = Permissions.All;
        await _roleManager.UpdateAsync(adminRole);

        // Create default admin user
        var adminUserName = "admin@localhost";
        var adminUser = new ApplicationUser { UserName = adminUserName, Email = adminUserName };

        await _userManager.CreateAsync(adminUser, DefaultPassword);

        adminUser = await _userManager.FindByNameAsync(adminUserName);

        await _userManager.AddToRoleAsync(adminUser!, AdministratorsRole);

        // Create default auditor user
        var auditorUserName = "auditor@localhost";
        var auditorUser = new ApplicationUser { UserName = auditorUserName, Email = auditorUserName };
        await _userManager.CreateAsync(auditorUser, DefaultPassword);

        await _context.SaveChangesAsync();
    }

    // private async Task SeedDataAsync()
    // {
    //
    //     await _context.SaveChangesAsync();
    // }
}