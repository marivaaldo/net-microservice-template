
namespace NetMicroserviceTemplate.Application.UseCases.Customers;

public interface IGetCustomersUseCase : IUseCaseWithResponse<IEnumerable<Customer>> { }

internal sealed class GetCustomersUseCase(ICustomerRepository customerRepository) : IGetCustomersUseCase
{
    private readonly ICustomerRepository _customerRepository = customerRepository;

    public async Task<IEnumerable<Customer>> Execute(CancellationToken cancellationToken = default)
    {
        return await _customerRepository.FindAllAsync(cancellationToken);
    }
}

