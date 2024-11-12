
using Microsoft.AspNetCore.Mvc;
using NetMicroserviceTemplate.Application.UseCases.Customers;

namespace NetMicroserviceTemplate.API.Endpoints
{
    public sealed class CustomerEndpoints : IEndpointsMapping
    {
        public string Prefix => "customer";
        public string DisplayName => "Customers";

        public void Map(RouteGroupBuilder group)
        {
            group.MapGet("/", GetCustomers);
            group.MapPost("/", RegisterCustomer);
        }

        private async Task<IResult> GetCustomers(
            [FromServices] IGetCustomersUseCase getCustomers,
            CancellationToken cancellationToken = default)
            => Results.Ok(await getCustomers.Execute(cancellationToken));

        private async Task<IResult> RegisterCustomer(
            [FromBody] CreateCustomerRequest request,
            [FromServices] IRegisterCustomerUseCase registerCustomer,
            CancellationToken cancellationToken = default)
        {
            var id = await registerCustomer.Execute(request);
            return Results.Ok(id);
        }
    }
}
