using System.Runtime.Serialization;
using AutoMapper;
using SteamServerManager.Application.Common.Services.Data;
using SteamServerManager.Domain.Entities;
using SteamServerManager.WebUI.Shared.ApplicationSetting;

namespace SteamServerManager.Application.UnitTests.Common.Mappings;

public class MappingTests
{
    private readonly IConfigurationProvider _configuration;
    private readonly IMapper _mapper;

    public MappingTests()
    {
        _configuration = new MapperConfiguration(config =>
            config.AddMaps(typesFromAssembliesContainingMappingDefinitions: typeof(IApplicationDbContext)));

        _mapper = _configuration.CreateMapper();
    }

    [Test]
    public void ShouldHaveValidConfiguration()
    {
        _configuration.AssertConfigurationIsValid();
    }

    [Test]
    [TestCase(typeof(ApplicationSetting), typeof(ApplicationSettingDto))]
    public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
    {
        var instance = GetInstanceOf(source);

        _mapper.Map(instance, source, destination);
    }

    private object GetInstanceOf(Type type)
    {
        return type.GetConstructor(Type.EmptyTypes) != null ? Activator.CreateInstance(type)! :
            // Type without parameterless constructor
            FormatterServices.GetUninitializedObject(type);
    }
}