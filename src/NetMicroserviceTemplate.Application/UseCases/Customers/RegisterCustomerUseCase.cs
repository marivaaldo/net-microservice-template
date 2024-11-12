﻿namespace NetMicroserviceTemplate.Application.UseCases.Customers;

public record CreateCustomerRequest(string FullName, int Age, string Email, Address Address);

public interface IRegisterCustomerUseCase : IUseCase<CreateCustomerRequest, Guid> { }

internal sealed class RegisterCustomerUseCase(ICustomerRepository customerRepository) : IRegisterCustomerUseCase
{
    private readonly ICustomerRepository _customerRepository = customerRepository;

    public async Task<Guid> Execute(CreateCustomerRequest request, CancellationToken cancellationToken = default)
    {
        var existingCustomer = await _customerRepository.FindByEmailAsync(request.Email);

        if (existingCustomer != null)
            throw new BusinessException("E-mail registered by another customer");

        var customer = new Customer(request.FullName, request.Age, request.Email, request.Address);

        await _customerRepository.AddAsync(customer, cancellationToken);

        return customer.Id;
    }
}