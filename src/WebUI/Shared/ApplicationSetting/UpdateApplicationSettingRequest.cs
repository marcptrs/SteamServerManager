using FluentValidation;

namespace SteamServerManager.WebUI.Shared.ApplicationSetting;

public class UpdateApplicationSettingRequest
{
    public int Id { get; set; }
    public string? SteamCmdPath { get; set; }
    public string? DefaultServerPath { get; set; }
}

public class UpdateApplicationSettingRequestValidator : 
    AbstractValidator<UpdateApplicationSettingRequest>
{
    public UpdateApplicationSettingRequestValidator()
    {
        RuleFor(v => v.SteamCmdPath)
            .NotEmpty();

        RuleFor(v => v.DefaultServerPath)
            .NotEmpty();
    }
}