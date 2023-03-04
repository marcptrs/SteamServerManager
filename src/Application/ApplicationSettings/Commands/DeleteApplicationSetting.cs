using Ardalis.GuardClauses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SteamServerManager.Application.Common.Services.Data;

namespace SteamServerManager.Application.ApplicationSettings.Commands;

public record DeleteApplicationSettingCommand(int Id) : IRequest;

public class DeleteApplicationSettingCommandHandler : AsyncRequestHandler<DeleteApplicationSettingCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteApplicationSettingCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    protected override async Task Handle(DeleteApplicationSettingCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.ApplicationSettings
            .Where(l => l.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.ApplicationSettings.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}