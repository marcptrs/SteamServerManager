using Microsoft.Extensions.Logging;
using SteamServerManager.Application.ApplicationSettings.Commands;
using SteamServerManager.Application.Common.Behaviours;
using SteamServerManager.Application.Common.Services.Identity;
using SteamServerManager.WebUI.Shared.ApplicationSetting;

namespace SteamServerManager.Application.UnitTests.Common.Behaviours;

public class RequestLoggerTests
{
    private Mock<ILogger<CreateApplicationSettingCommand>> _logger = null!;
    private Mock<ICurrentUser> _currentUser = null!;
    private Mock<IIdentityService> _identityService = null!;
    
    [SetUp]
    public void Setup()
    {
        _logger = new Mock<ILogger<CreateApplicationSettingCommand>>();
        _currentUser = new Mock<ICurrentUser>();
        _identityService = new Mock<IIdentityService>();
    }
    
    [Test]
    public async Task ShouldCallGetUserNameAsyncOnceIfAuthenticated()
    {
        _currentUser.Setup(x => x.UserId).Returns(Guid.NewGuid().ToString());

        var requestLogger = new LoggingBehaviour<CreateApplicationSettingCommand>(
            _logger.Object, _currentUser.Object, _identityService.Object);

        await requestLogger.Process(
            new CreateApplicationSettingCommand(new CreateApplicationSettingRequest
            {
                SteamCmdPath = "/opt/steam/steamcmd", 
                DefaultServerPath = "/opt/steam/server"
            }), new CancellationToken());

        _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Once);
    }

    [Test]
    public async Task ShouldNotCallGetUserNameAsyncOnceIfUnauthenticated()
    {
        var requestLogger = new LoggingBehaviour<CreateApplicationSettingCommand>(
            _logger.Object, _currentUser.Object, _identityService.Object);

        await requestLogger.Process(
            new CreateApplicationSettingCommand(new CreateApplicationSettingRequest
            {
                SteamCmdPath = "/opt/steam/steamcmd", 
                DefaultServerPath = "/opt/steam/server"
            }), new CancellationToken());
        
        _identityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Never);
    }
}