using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetMicroserviceTemplate.Application.UseCases;
using NetMicroserviceTemplate.Domain.Extensions;
using System.Reflection;

namespace NetMicroserviceTemplate.Application.Extensions;

public static class ApplicationServicesExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        => services.AddUseCases(configuration);

    private static IServiceCollection AddUseCases(this IServiceCollection services, IConfiguration configuration)
        => services.AddImplementationsOf<IUseCaseBase>();
}
