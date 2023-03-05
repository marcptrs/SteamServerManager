using SteamServerManager.Application.ApplicationSettings.Commands;
using SteamServerManager.Application.Common.Exceptions;
using SteamServerManager.Domain.Entities;
using SteamServerManager.WebUI.Shared.ApplicationSetting;

namespace SteamServerManager.Application.IntegrationTests.ApplicationSettings.Commands;

using static Testing;

[TestFixture]
public class UpdateApplicationSettingTests : BaseTestFixture
{
	[Test]
	public async Task ShouldRequireMinimumFields()
    {
        await RunAsDefaultUserAsync();
        
        var command = new UpdateApplicationSettingCommand(
            new UpdateApplicationSettingRequest());

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldUpdateApplicationSetting()
    {
        var userId = await RunAsDefaultUserAsync();

        var id = await SendAsync(new CreateApplicationSettingCommand(
            new CreateApplicationSettingRequest
            {
                SteamCmdPath = "/opt/steam/steamcmd",
                DefaultServerPath = "/opt/steam/servers"
            }));

        var command = new UpdateApplicationSettingCommand(new UpdateApplicationSettingRequest
        {
            Id = id,
            SteamCmdPath = "/opt/steam/steamcmd2/",
            DefaultServerPath = "/opt/steam/servers2"
        });

        await SendAsync(command);

        var applicationSetting = await FindAsync<ApplicationSetting>(id);

        applicationSetting.Should().NotBeNull();
        applicationSetting!.SteamCmdPath.Should().
            Be(command.ApplicationSetting.SteamCmdPath);
        applicationSetting.DefaultServerPath.Should().
            Be(command.ApplicationSetting.DefaultServerPath);
        applicationSetting.LastModifiedBy.Should().NotBeNull();
        applicationSetting.LastModifiedBy.Should().Be(userId);
        applicationSetting.LastModifiedUtc.Should().
            BeCloseTo(DateTime.UtcNow, TimeSpan.FromMicroseconds(10000));

    }
}

