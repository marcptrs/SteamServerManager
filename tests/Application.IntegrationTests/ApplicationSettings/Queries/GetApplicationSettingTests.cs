using SteamServerManager.Application.ApplicationSettings.Commands;
using SteamServerManager.Application.ApplicationSettings.Queries;
using SteamServerManager.WebUI.Shared.ApplicationSetting;

namespace SteamServerManager.Application.IntegrationTests.ApplicationSettings.Queries;

using static Testing;

[TestFixture]
public class GetApplicationSettingTests : BaseTestFixture
{
    [Test]
    public async Task ShouldReturnOneApplicationSetting()
    {
        await RunAsDefaultUserAsync();

        var id = await SendAsync(new CreateApplicationSettingCommand(
            new CreateApplicationSettingRequest 
            { 
                SteamCmdPath = "/opt/steam/steamcmd", 
                DefaultServerPath = "/opt/steam/servers" 
            }));

        var query = new GetApplicationSettingQuery(id);

        var result = await SendAsync(query);

        result.Should().NotBeNull();
    }
}