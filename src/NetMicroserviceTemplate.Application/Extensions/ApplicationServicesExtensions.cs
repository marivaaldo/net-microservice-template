using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetMicroserviceTemplate.Application.UseCases;
using System.Reflection;

namespace NetMicroserviceTemplate.Application.Extensions;

public static class ApplicationServicesExtensions
{
    private static readonly Type[] _baseUseCaseTypes = [typeof(IUseCaseBase), typeof(IUseCase), typeof(IUseCaseWithRequest<>), typeof(IUseCaseWithResponse<>), typeof(IUseCase<,>)];

    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var useCases = assembly.GetTypes()
            .Where(t => t.IsClass && t.IsSealed && typeof(IUseCaseBase).IsAssignableFrom(t))
            .ToList();

        foreach (var useCase in useCases)
        {
            var useCaseInterface = useCase.GetInterfaces().First();
            services.AddScoped(useCaseInterface, useCase);
        }

        return services;
    }
}
