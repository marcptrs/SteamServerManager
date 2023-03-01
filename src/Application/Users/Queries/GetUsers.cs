using MediatR;
using SteamServerManager.Application.Common.Services.Identity;
using SteamServerManager.WebUI.Shared.AccessControl;

namespace SteamServerManager.Application.Users.Queries;

public record GetUsersQuery() : IRequest<UsersVm>;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, UsersVm>
{
    private readonly IIdentityService _identityService;

    public GetUsersQueryHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<UsersVm> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var result = new UsersVm
        {
            Users = await _identityService.GetUsersAsync(cancellationToken)
        };

        return result;
    }
}