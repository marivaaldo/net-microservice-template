using MediatR;
using Microsoft.EntityFrameworkCore;
using NetMicroserviceTemplate.Application.Customers.Commands.RegisterCustomer;
using NetMicroserviceTemplate.Application.Exceptions;
using NetMicroserviceTemplate.Domain.Contracts.Repositories;
using NetMicroserviceTemplate.Domain.Events;
using NetMicroserviceTemplate.Infrastructure.Data.Context;
using NetMicroserviceTemplate.Tests.Mocks;

namespace NetMicroserviceTemplate.Tests.UnitTests.Application.Customers;

public class RegisterCustomerCommandHandlerTests
{
    private readonly Mock<IMediator> _mediator;
    private readonly Mock<ApplicationContext> _applicationContextMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<CustomerRepositoryFake> _customerRepositoryMock;
    private readonly RegisterCustomerCommandHandler _registerCustomerCommandHandler;

    public RegisterCustomerCommandHandlerTests()
    {
        _mediator = new Mock<IMediator>();
        _applicationContextMock = new Mock<ApplicationContext>(new DbContextOptions<ApplicationContext>(), (IDomainEventDispatcher)null);
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _customerRepositoryMock = new Mock<CustomerRepositoryFake>(_applicationContextMock.Object);
        _customerRepositoryMock.Setup(r => r.UnitOfWork).Returns(_unitOfWorkMock.Object);
        _registerCustomerCommandHandler = new RegisterCustomerCommandHandler(_customerRepositoryMock.Object, _mediator.Object);
    }

    [Fact]
    public async Task Handle_ExistingEmail_ThrowsBusinessException()
    {
        // Arrange
        var command = new RegisterCustomerCommand
        {
            FullName = "John Doe",
            Age = 30,
            Email = "john.doe@example.com",
            Address = Mocks.AddressFake.New("USA")
        };
        _customerRepositoryMock.Setup(r => r.FindByEmail(command.Email)).Returns([Mocks.CustomerFake.New(command.FullName, command.Age, command.Email, command.Address)]);

        // Act and Assert
        await Assert.ThrowsAsync<BusinessException>(() => _registerCustomerCommandHandler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_UnitOfWorkRollback_ThrowsException()
    {
        // Arrange
        var command = new RegisterCustomerCommand
        {
            FullName = "John Doe",
            Age = 30,
            Email = "john.doe@example.com",
            Address = Mocks.AddressFake.New("USA")
        };
        _customerRepositoryMock.Setup(r => r.FindByEmail(command.Email)).Returns((CustomerFake[])null);
        _customerRepositoryMock.Setup(r => r.UnitOfWork.CommitTransactionAsync(CancellationToken.None)).ThrowsAsync(new Exception());

        // Act and Assert
        await Assert.ThrowsAsync<Exception>(() => _registerCustomerCommandHandler.Handle(command, CancellationToken.None));
    }
}