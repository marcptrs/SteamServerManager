using SteamServerManager.Domain.Common;

namespace SteamServerManager.Domain.Entities;

public class ApplicationSetting : BaseAuditableEntity
{
    public string? SteamCmdPath { get; set; }
    public string? DefaultServerPath { get; set; }
}