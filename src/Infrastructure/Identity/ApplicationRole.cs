using Microsoft.AspNetCore.Identity;
using SteamServerManager.WebUI.Shared.Authorization;

namespace SteamServerManager.Infrastructure.Identity;

public class ApplicationRole : IdentityRole
{
	public Permissions Permissions { get; set; }
}
