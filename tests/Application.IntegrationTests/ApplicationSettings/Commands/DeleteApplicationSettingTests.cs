using Ardalis.GuardClauses;
using SteamServerManager.Application.ApplicationSettings.Commands;
using SteamServerManager.Domain.Entities;
using SteamServerManager.WebUI.Shared.ApplicationSetting;

namespace SteamServerManager.Application.IntegrationTests.ApplicationSettings.Commands;

using static Testing;

[TestFixture]
public class DeleteApplicationSettingTests
{
    [Test]
    public async Task ShouldRequireValidApplicationSettingId()
    {
        var command = new DeleteApplicationSettingCommand(0);

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }
    
    [Test]
    public async Task ShouldDeleteApplicationSetting()
    {
        var id = await SendAsync(new CreateApplicationSettingCommand(
            new CreateApplicationSettingRequest
            {
                SteamCmdPath = "/opt/steam/steamcmd",
                DefaultServerPath = "/opt/steam/servers"
            }));

        await SendAsync(new DeleteApplicationSettingCommand(id));

        var applicationSetting = await FindAsync<ApplicationSetting>(id);

        applicationSetting.Should().BeNull();
    }
}