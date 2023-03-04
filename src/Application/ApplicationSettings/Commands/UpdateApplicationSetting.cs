using Ardalis.GuardClauses;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SteamServerManager.Application.Common.Services.Data;
using SteamServerManager.WebUI.Shared.ApplicationSetting;

namespace SteamServerManager.Application.ApplicationSettings.Commands;

public record UpdateApplicationSettingCommand(UpdateApplicationSettingRequest ApplicationSetting) : IRequest;

public class UpdateApplicationSettingCommandValidator : AbstractValidator<UpdateApplicationSettingCommand>
{
    public UpdateApplicationSettingCommandValidator()
    {
        RuleFor(r => r.ApplicationSetting)
            .SetValidator(new UpdateApplicationSettingRequestValidator());
    }
}

public class UpdateApplicationSettingCommandHandler : AsyncRequestHandler<UpdateApplicationSettingCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateApplicationSettingCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    
    protected override async Task Handle(UpdateApplicationSettingCommand request, 
        CancellationToken cancellationToken)
    {
        var entity = await _context.ApplicationSettings.FirstOrDefaultAsync(
            i => i.Id == request.ApplicationSetting.Id, 
            cancellationToken: cancellationToken);

        Guard.Against.NotFound(request.ApplicationSetting.Id, entity);

        entity.SteamCmdPath = request.ApplicationSetting.SteamCmdPath;
        entity.DefaultServerPath = request.ApplicationSetting.DefaultServerPath;

        await _context.SaveChangesAsync(cancellationToken);
    }
}

