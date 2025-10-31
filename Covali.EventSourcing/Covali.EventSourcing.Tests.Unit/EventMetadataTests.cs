using Covali.EventSourcing.Events;
using Xunit;

namespace Covali.EventSourcing.Tests.Unit;

public class EventMetadataTests
{
    [Fact]
    public void EventMetadata_CanBeCreatedWithRequiredProperties()
    {
        // Arrange & Act
        var metadata = new EventMetadata
        {
            EventCode = "Test.EventOccurred",
            DisplayName = "Test Event",
            Description = "A test event for unit testing",
            PlaceholderKeys = ["Key1", "Key2"]
        };

        // Assert
        Assert.Equal("Test.EventOccurred", metadata.EventCode);
        Assert.Equal("Test Event", metadata.DisplayName);
        Assert.Equal("A test event for unit testing", metadata.Description);
        Assert.Contains("Key1", metadata.PlaceholderKeys);
        Assert.Contains("Key2", metadata.PlaceholderKeys);
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
        Assert.Equal("Test.EventOccurred", metadata.EventCode);
        Assert.Equal("Test Event Occurred", metadata.DisplayName);
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

        public override EventMetadata GetMetadata() => new()
        {
            EventCode = "Test.EventOccurred",
            DisplayName = "Test Event Occurred",
            Description = "Test event for unit testing",
            PlaceholderKeys = ["TestData"]
        };
    }
}
