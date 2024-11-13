using NetMicroserviceTemplate.Application.Customers.Commands.RegisterCustomer;
using NetMicroserviceTemplate.Application.Customers.Queries.GetCustomers;

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
            [FromServices] IMediator mediator,
            CancellationToken cancellationToken = default)
            => Results.Ok(await mediator.Send(new GetCustomersQuery(), cancellationToken));

        private async Task<IResult> RegisterCustomer(
            [FromBody] RegisterCustomerCommand command,
            [FromServices] IMediator mediator,
            CancellationToken cancellationToken = default)
        {
            var id = await mediator.Send(command, cancellationToken);
            return Results.Ok(id);
        }
    }
}
