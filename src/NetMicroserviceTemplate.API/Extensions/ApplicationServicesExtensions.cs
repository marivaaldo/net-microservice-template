using NetMicroserviceTemplate.Application.Events;

namespace NetMicroserviceTemplate.API.Extensions;

public static class ApplicationServicesExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        => services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(IIntegrationEvent).Assembly));
}
