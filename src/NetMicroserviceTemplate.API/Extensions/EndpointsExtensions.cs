using NetMicroserviceTemplate.API.Endpoints;
using System.Reflection;

namespace NetMicroserviceTemplate.API.Extensions;

internal static class EndpointsExtensions
{
    public static WebApplication MapAllEndpoints(this WebApplication app, string basePrefix = "app/")
    {
        var endpointsMappingsTypes = Assembly.GetExecutingAssembly().GetTypes()
            .Where(type => typeof(IEndpointsMapping).IsAssignableFrom(type) && !type.IsAbstract && type.IsSealed && !type.IsInterface)
            .ToArray();

        foreach (var type in endpointsMappingsTypes)
        {
            var mapMethod = type.GetMethod(nameof(IEndpointsMapping.Map));
            var instance = Activator.CreateInstance(type) as IEndpointsMapping;

            var routeGroup = app
                .MapGroup($"{basePrefix}{instance.Prefix}")
                .WithDisplayName(instance.DisplayName);

            mapMethod?.Invoke(instance, [routeGroup]);
        }

        return app;
    }
}
