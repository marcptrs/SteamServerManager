using FluentValidation;

namespace SteamServerManager.WebUI.Shared.ApplicationSetting;

public class CreateApplicationSettingRequest
{
    public string? SteamCmdPath { get; set; }
    public string? DefaultServerPath { get; set; }
}

public class CreateApplicationSettingRequestValidator : AbstractValidator<CreateApplicationSettingRequest>
{
    public CreateApplicationSettingRequestValidator()
    {
        RuleFor(v => v.SteamCmdPath)
            .NotEmpty();

        RuleFor(v => v.DefaultServerPath)
            .NotEmpty();
    }
}