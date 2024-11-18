using NetMicroserviceTemplate.Domain.Contracts.Repositories;
using NetMicroserviceTemplate.Domain.Events.Customers.CustomerRegistered;

namespace NetMicroserviceTemplate.Tests.UnitTests.DomainEvents.Customers
{
    public class CustomerRegisteredEventHandlerTests
    {
        [Fact]
        public async Task HandleAsync_ChangesCustomerAddressAndUpdateCustomer()
        {
            // Arrange
            var customerRepositoryMock = new Mock<ICustomerRepository>();
            var customer = new Customer("John Doe", 30, "john.doe@example.com", new Address("USA", "State", "City", "Neighborhood", "Street", "Number", "Complement", "PostalCode"));
            customerRepositoryMock.Setup(r => r.FindOneAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(customer);

            var customerRegisteredEvent = new CustomerRegisteredEvent(customer.Id);
            var customerRegisteredEventHandler = new CustomerRegisteredEventHandler(customerRepositoryMock.Object);

            // Act
            await customerRegisteredEventHandler.HandleAsync(customerRegisteredEvent, CancellationToken.None);

            // Assert
            customerRepositoryMock.Verify(r => r.FindOneAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
            customerRepositoryMock.Verify(r => r.SaveAsync(It.IsAny<Customer>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task HandleAsync_ThrowsException_WhenCustomerRepositoryThrowsException()
        {
            // Arrange
            var customerRepositoryMock = new Mock<ICustomerRepository>();
            customerRepositoryMock.Setup(r => r.FindOneAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .Throws<Exception>();

            var customerRegisteredEvent = new CustomerRegisteredEvent(Guid.NewGuid());
            var customerRegisteredEventHandler = new CustomerRegisteredEventHandler(customerRepositoryMock.Object);

            // Act and Assert
            await Assert.ThrowsAsync<Exception>(() => customerRegisteredEventHandler.HandleAsync(customerRegisteredEvent, CancellationToken.None));
        }
    }
}