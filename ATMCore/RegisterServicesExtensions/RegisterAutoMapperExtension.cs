using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace ATMCore.RegisterServicesExtensions;

/// <summary>
/// Class for AutoMapper globale.
/// </summary>
public static class RegisterAutoMapperExtension
{
    public static IServiceCollection RegisterAutoMapper<AutoMapperProfile>(this IServiceCollection services)
        where AutoMapperProfile : Profile, new()
    {
        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile<AutoMapperProfile>();
        });

        IMapper mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);

        return services;
    }
}
