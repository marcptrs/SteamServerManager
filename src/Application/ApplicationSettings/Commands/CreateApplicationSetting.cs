using FluentValidation;
using MediatR;
using SteamServerManager.Application.Common.Services.Data;
using SteamServerManager.Domain.Entities;
using SteamServerManager.WebUI.Shared.ApplicationSetting;

namespace SteamServerManager.Application.ApplicationSettings.Commands;

public record CreateApplicationSettingCommand(CreateApplicationSettingRequest ApplicationSetting) : IRequest<int>;

public class CreateApplicationSettingCommandValidator : AbstractValidator<CreateApplicationSettingCommand>
{
    public CreateApplicationSettingCommandValidator()
    {
        RuleFor(r => r.ApplicationSetting).SetValidator(
            new CreateApplicationSettingRequestValidator());
    }
}

public class CreateApplicationSettingCommandHandler : IRequestHandler<CreateApplicationSettingCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateApplicationSettingCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<int> Handle(CreateApplicationSettingCommand request, CancellationToken cancellationToken)
    {
        var entity = new ApplicationSetting
        {
            SteamCmdPath = request.ApplicationSetting.SteamCmdPath,
            DefaultServerPath = request.ApplicationSetting.DefaultServerPath
        };

        _context.ApplicationSettings.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}