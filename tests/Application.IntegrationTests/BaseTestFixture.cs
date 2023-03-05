using Microsoft.Extensions.DependencyInjection;
using SteamServerManager.Infrastructure.Data;

namespace SteamServerManager.Application.IntegrationTests;

using static Testing;

[TestFixture]
public class BaseTestFixture
{
    [SetUp]
    public async Task TestSetUp()
    {
        // Reset SQLite database on each test
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
    }
}