using AutoMapper;
using SteamServerManager.Domain.Entities;
using SteamServerManager.WebUI.Shared.ApplicationSetting;

namespace SteamServerManager.Application.ApplicationSettings;

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<ApplicationSetting, ApplicationSettingDto>();
    }
}