using MediatR;
using NetMicroserviceTemplate.Application.Customers.Commands.RegisterCustomer;
using NetMicroserviceTemplate.Application.Exceptions;
using NetMicroserviceTemplate.Domain.Contracts.Repositories;
using NetMicroserviceTemplate.Infrastructure.Data.Context;
using NetMicroserviceTemplate.Infrastructure.Data.Repositories;

namespace NetMicroserviceTemplate.Tests.UnitTests.Application.Customers;

public class RegisterCustomerCommandHandlerTests
{
    private readonly Mock<IMediator> _mediator;
    private readonly Mock<IApplicationContext> _applicationContextMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<CustomerRepository> _customerRepositoryMock;
    private readonly RegisterCustomerCommandHandler _registerCustomerCommandHandler;

    public RegisterCustomerCommandHandlerTests()
    {
        _mediator = new Mock<IMediator>();
        _applicationContextMock = new Mock<IApplicationContext>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _customerRepositoryMock = new Mock<CustomerRepository>(_applicationContextMock.Object);
        _customerRepositoryMock.Setup(r => r.UnitOfWork).Returns(_unitOfWorkMock.Object);
        _registerCustomerCommandHandler = new RegisterCustomerCommandHandler(_customerRepositoryMock.Object, _mediator.Object);
    }

    [Fact]
    public async Task Handle_ValidCommand_ReturnsCustomerGuid()
    {
        // Arrange
        var command = new RegisterCustomerCommand
        {
            FullName = "John Doe",
            Age = 30,
            Email = "john.doe@example.com",
            Address = Mocks.Address.New("USA")
        };

        // Act
        var result = await _registerCustomerCommandHandler.Handle(command, CancellationToken.None);

        // Assert
        _customerRepositoryMock.Verify(r => r.FindByEmailAsync(command.Email), Times.Once);
        _customerRepositoryMock.Verify(r => r.InsertAync(It.IsAny<Customer>(), CancellationToken.None), Times.Once);
        _customerRepositoryMock.Verify(r => r.UnitOfWork.CommitTransactionAsync(CancellationToken.None), Times.Once);
        _customerRepositoryMock.Verify(r => r.UnitOfWork.RollbackTransactionAsync(CancellationToken.None), Times.Never);
        Assert.NotNull(result);
        Assert.IsType<Guid>(result);
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
            Address = Mocks.Address.New("USA")
        };
        _customerRepositoryMock.Setup(r => r.FindByEmailAsync(command.Email)).ReturnsAsync(Mocks.Customer.New(command.FullName, command.Age, command.Email, command.Address));

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
            Address = Mocks.Address.New("USA")
        };
        _customerRepositoryMock.Setup(r => r.FindByEmailAsync(command.Email)).ReturnsAsync((Customer)null);
        _customerRepositoryMock.Setup(r => r.UnitOfWork.CommitTransactionAsync(CancellationToken.None)).ThrowsAsync(new Exception());

        // Act and Assert
        await Assert.ThrowsAsync<Exception>(() => _registerCustomerCommandHandler.Handle(command, CancellationToken.None));
    }
}