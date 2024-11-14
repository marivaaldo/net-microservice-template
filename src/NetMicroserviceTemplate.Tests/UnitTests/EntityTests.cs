using NetMicroserviceTemplate.Domain.Events;

namespace NetMicroserviceTemplate.Tests.UnitTests.Domain.Entities;

public class EntityTests
{
    [Fact]
    public void AddDomainEvent_AddsEventToDomainEventsList()
    {
        // Arrange
        var entity = new FakeEntity();
        var domainEventMock = new Mock<IDomainEvent>();

        // Act
        entity.AddFakeDomainEvent(domainEventMock.Object);

        // Assert
        Assert.Single(entity._domainEvents);
    }

    [Fact]
    public void AddDomainEvent_DoesNotAddDuplicateEvents()
    {
        // Arrange
        var entity = new FakeEntity();
        var domainEventMock = new Mock<IDomainEvent>();
        entity.AddFakeDomainEvent(domainEventMock.Object);

        // Act
        entity.AddFakeDomainEvent(domainEventMock.Object);

        // Assert
        Assert.Single(entity._domainEvents);
    }

    [Fact]
    public void ConsumeDomainEvents_ReturnsAllDomainEvents()
    {
        // Arrange
        var entity = new FakeEntity();
        var domainEventMock1 = new Mock<IDomainEvent>();
        var domainEventMock2 = new Mock<IDomainEvent>();
        domainEventMock1.Setup(x => x.Id).Returns(Guid.NewGuid());
        domainEventMock2.Setup(x => x.Id).Returns(Guid.NewGuid());
        entity.AddFakeDomainEvent(domainEventMock1.Object);
        entity.AddFakeDomainEvent(domainEventMock2.Object);

        // Act
        var consumedEvents = entity.ConsumeDomainEvents();

        // Assert
        Assert.Equal(2, consumedEvents.Count);
        Assert.Contains(domainEventMock1.Object, consumedEvents);
        Assert.Contains(domainEventMock2.Object, consumedEvents);
    }

    [Fact]
    public void ConsumeDomainEvents_ClearsDomainEventsList()
    {
        // Arrange
        var entity = new FakeEntity();
        var domainEventMock1 = new Mock<IDomainEvent>();
        var domainEventMock2 = new Mock<IDomainEvent>();
        domainEventMock1.Setup(x => x.Id).Returns(Guid.NewGuid());
        domainEventMock2.Setup(x => x.Id).Returns(Guid.NewGuid());
        entity.AddFakeDomainEvent(domainEventMock1.Object);
        entity.AddFakeDomainEvent(domainEventMock2.Object);

        // Act
        entity.ConsumeDomainEvents();

        // Assert
        Assert.Empty(entity._domainEvents);
    }
}

public class FakeEntity : Entity
{
    public new List<IDomainEvent> _domainEvents => base._domainEvents;
    public void AddFakeDomainEvent(IDomainEvent domainEvent) => base.AddDomainEvent(domainEvent);
}
