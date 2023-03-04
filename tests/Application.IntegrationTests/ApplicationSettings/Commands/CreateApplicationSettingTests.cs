using SteamServerManager.Application.ApplicationSettings.Commands;
using SteamServerManager.Application.Common.Exceptions;
using SteamServerManager.Domain.Entities;
using SteamServerManager.WebUI.Shared.ApplicationSetting;

namespace SteamServerManager.Application.IntegrationTests.ApplicationSettings.Commands;

using static Testing;

[TestFixture]
public class CreateApplicationSettingTests
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CreateApplicationSettingCommand(
            new CreateApplicationSettingRequest());

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreateApplicationSetting()
    {
        var userId = await RunAsDefaultUserAsync();

        var request = new CreateApplicationSettingRequest
        {
            SteamCmdPath = "/opt/steam/steamcmd",
            DefaultServerPath = "/opt/steam/servers"
        };

        var command = new CreateApplicationSettingCommand(request);

        var id = await SendAsync(command);

        var applicationSetting = await FindAsync<ApplicationSetting>(id);

        applicationSetting.Should().NotBeNull();
        applicationSetting!.SteamCmdPath.Should().Be(request.SteamCmdPath);
        applicationSetting.DefaultServerPath.Should().Be(request.DefaultServerPath);
        applicationSetting.CreatedBy.Should().Be(userId);
        applicationSetting.CreatedUtc.Should().
            BeCloseTo(DateTime.UtcNow, TimeSpan.FromMilliseconds(10000));
    }
}