
namespace NetMicroserviceTemplate.Application.Customers.Commands.RegisterCustomer;

internal class RegisterCustomerCommandHandler(ICustomerRepository customerRepository) : IRequestHandler<RegisterCustomerCommand, Guid>
{
    private readonly ICustomerRepository _customerRepository = customerRepository;

    public async Task<Guid> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
    {
        var existingCustomer = await _customerRepository.FindByEmailAsync(request.Email);

        if (existingCustomer != null)
            throw new BusinessException("E-mail registered by another customer");

        var customer = new Customer(request.FullName, request.Age, request.Email, request.Address);

        try
        {
            await _customerRepository.UnitOfWork.BeginTransactionAsync(cancellationToken);
            await _customerRepository.AddAsync(customer, cancellationToken);
            await _customerRepository.UnitOfWork.CommitTransactionAsync(cancellationToken);
        }
        catch
        {
            await _customerRepository.UnitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }

        return customer.Id;
    }
}
