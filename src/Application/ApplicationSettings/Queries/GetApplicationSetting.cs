using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SteamServerManager.Application.Common.Services.Data;
using SteamServerManager.WebUI.Shared.ApplicationSetting;

namespace SteamServerManager.Application.ApplicationSettings.Queries;

public record GetApplicationSettingQuery(int Id) : IRequest<ApplicationSettingVm>;

public class GetApplicationSettingQueryHandler : IRequestHandler<GetApplicationSettingQuery, 
    ApplicationSettingVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetApplicationSettingQueryHandler(
        IApplicationDbContext context,
        IMapper mapper
        )
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<ApplicationSettingVm> Handle(GetApplicationSettingQuery request, 
        CancellationToken cancellationToken)
    {
        return new ApplicationSettingVm
        {
            ApplicationSetting = await _context.ApplicationSettings
                .ProjectTo<ApplicationSettingDto>(_mapper.ConfigurationProvider)
                .SingleAsync(a => a.Id == request.Id, cancellationToken)
        };
    }
}