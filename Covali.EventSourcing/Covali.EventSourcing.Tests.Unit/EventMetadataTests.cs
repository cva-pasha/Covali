using Covali.EventSourcing.Events;

namespace Covali.EventSourcing.Tests.Unit;

public class EventMetadataTests
{
    [Fact]
    public void IEventMetadata_CanBeImplemented()
    {
        // Arrange & Act
        // IEventMetadata is a marker interface allowing developers to define custom metadata
        // This test verifies that a custom metadata class can be instantiated
        var metadata = new TestEventMetadata
        {
            TestProperty = "Test"
        };

        // Assert
        Assert.NotNull(metadata);
        Assert.Equal(expected: "Test", metadata.TestProperty);
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
        Assert.Equal(expected: "Sample", metadata.TestProperty);
    }

    [Fact]
    public void EventWithMetadata_IsAssignableToIEvent()
    {
        // Arrange
        var testEvent = new TestEvent { TestData = "Sample" };

        // Act & Assert
        Assert.IsAssignableFrom<IEvent>(testEvent);
    }

    [Fact]
    public void EventWithMetadata_SupportsGenericMetadataType()
    {
        // Arrange
        var testEvent = new TestEvent { TestData = "Sample" };

        // Act
        var metadata = testEvent.GetMetadata();

        // Assert
        Assert.IsType<TestEventMetadata>(metadata);
        Assert.IsAssignableFrom<IEventMetadata>(metadata);
    }

    private record TestEventMetadata : IEventMetadata
    {
        public required string TestProperty { get; init; }
    }

    private sealed class TestEvent : EventWithMetadata<TestEventMetadata>
    {
        public required string TestData { get; init; }

        public override TestEventMetadata GetMetadata()
        {
            return new TestEventMetadata
            {
                TestProperty = TestData
            };
        }
    }
}