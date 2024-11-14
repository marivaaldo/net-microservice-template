using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using NetMicroserviceTemplate.Infrastructure.Data.Repositories;

namespace NetMicroserviceTemplate.Infrastructure.Data.Extensions;

public static class DataServicesExtensions
{
    public static IServiceCollection AddInfrastructureDataServices(this IServiceCollection services)
        => services
        .AddContext()
        .AddRepositories();

    private static IServiceCollection AddContext(this IServiceCollection services)
        => services
        .AddDbContext<ApplicationContext>(options =>
        {
            options
                .UseInMemoryDatabase("NetMicroserviceTemplate")
                .ConfigureWarnings(warnings => warnings.Ignore(InMemoryEventId.TransactionIgnoredWarning));
        });

    private static IServiceCollection AddRepositories(this IServiceCollection services)
        => services
        .AddScoped<ICustomerRepository, CustomerRepository>()
        .AddScoped(typeof(IEntityRepository<>), typeof(EntityRepository<>))
        .AddScoped(typeof(IEntityRepository<,>), typeof(EntityRepository<,>));
}
