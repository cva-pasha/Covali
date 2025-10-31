using Covali.EventSourcing.Events;
using Xunit;

namespace Covali.EventSourcing.Tests.Unit;

public class EventMetadataTests
{
    [Fact]
    public void EventMetadata_CanBeExtendedWithPartialClass()
    {
        // Arrange & Act
        // EventMetadata is empty by design, allowing developers to extend it
        // This test verifies that EventMetadata can be instantiated
        var metadata = new EventMetadata();

        // Assert
        Assert.NotNull(metadata);
    }

    [Fact]
    public void EventWithMetadata_CanBeInherited()
    {
        // Arrange
        var testEvent = new TestEvent
        {
            TestData = "Sample"
        };

        // Act
        var metadata = testEvent.GetMetadata();

        // Assert
        Assert.NotNull(metadata);
    }

    [Fact]
    public void EventWithMetadata_IsAssignableToIEvent()
    {
        // Arrange
        var testEvent = new TestEvent { TestData = "Sample" };

        // Act & Assert
        Assert.IsAssignableFrom<IEvent>(testEvent);
    }

    private sealed class TestEvent : EventWithMetadata
    {
        public required string TestData { get; init; }

        public override EventMetadata GetMetadata() => new();
    }
}
