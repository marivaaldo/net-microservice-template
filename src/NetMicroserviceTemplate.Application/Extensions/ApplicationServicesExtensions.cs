using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NetMicroserviceTemplate.Application.Extensions;

public static class ApplicationServicesExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        => services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationServicesExtensions).Assembly));
}
