namespace SteamServerManager.Application.Common.Services.Identity;

public interface ICurrentUser
{
    string? UserId { get; }
}