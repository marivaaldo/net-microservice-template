using NetMicroserviceTemplate.Application.Events.Customers.CustomerRegistered;

namespace NetMicroserviceTemplate.Application.Customers.Commands.RegisterCustomer;

internal class RegisterCustomerCommandHandler(ICustomerRepository customerRepository, IMediator mediator) : IRequestHandler<RegisterCustomerCommand, Guid>
{
    private readonly ICustomerRepository _customerRepository = customerRepository;
    private readonly IMediator _mediator = mediator;

    public async Task<Guid> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
    {
        var existingCustomer = _customerRepository.FindByEmail(request.Email);

        if (existingCustomer != null && existingCustomer.Any())
            throw new BusinessException("E-mail registered by another customer");

        var customer = new Customer(request.FullName, request.Age, request.Email, request.Address);

        try
        {
            await _customerRepository.UnitOfWork.BeginTransactionAsync(cancellationToken);
            await _customerRepository.InsertAsync(customer, cancellationToken);
            await _customerRepository.UnitOfWork.CompleteAsync(cancellationToken);
            await _customerRepository.UnitOfWork.CommitTransactionAsync(cancellationToken);
            await _mediator.Publish(new CustomerRegisteredIntegrationEvent(customer.Id), cancellationToken);
        }
        catch
        {
            await _customerRepository.UnitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }

        return customer.Id;
    }
}
