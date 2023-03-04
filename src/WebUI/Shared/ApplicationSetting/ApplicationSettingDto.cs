namespace SteamServerManager.WebUI.Shared.ApplicationSetting;

public class ApplicationSettingDto
{
    public int Id { get; set; }
    public string? SteamCmdPath { get; set; }
    public string? DefaultServerPath { get; set; }
}