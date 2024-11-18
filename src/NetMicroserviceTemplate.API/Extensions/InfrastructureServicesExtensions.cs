using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using NetMicroserviceTemplate.Domain.Contracts.Repositories;
using NetMicroserviceTemplate.Domain.Entities;
using NetMicroserviceTemplate.Infrastructure.Data.Context;
using NetMicroserviceTemplate.Infrastructure.Data.Repositories;

namespace NetMicroserviceTemplate.API.Extensions;

public static class InfrastructureServicesExtensions
{
    public static IServiceCollection AddInfrastructureDataServices(this IServiceCollection services)
        => services
        .AddContext()
        .AddRepositories();

    private static IServiceCollection AddContext(this IServiceCollection services)
        => services
        .AddDbContext<IApplicationContext, ApplicationContext>(options =>
        {
            options
                .UseInMemoryDatabase("NetMicroserviceTemplate")
                .ConfigureWarnings(warnings => warnings.Ignore(InMemoryEventId.TransactionIgnoredWarning));
        });

    private static IServiceCollection AddRepositories(this IServiceCollection services)
        => services
            .AddEntityRepository<ICustomerRepository, UnitOfWorkRepository<Customer>, ApplicationContext>();
}
