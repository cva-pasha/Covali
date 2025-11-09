namespace Covali.EventSourcing.Events;

/// <summary>
/// Marker interface for event metadata.
/// Implement this interface to define custom metadata structures for your events.
/// </summary>
/// <remarks>
/// <para>
/// This interface is intentionally empty to provide maximum flexibility.
/// Applications define their own metadata structure by creating a class or record
/// that implements this interface.
/// </para>
/// <para>
/// <strong>Example Implementation:</strong>
/// </para>
/// <code>
/// public record EventMetadata : IEventMetadata
/// {
///     public required string EventCode { get; init; }
///     public required string DisplayName { get; init; }
///     public required string Description { get; init; }
///     public required HashSet&lt;string&gt; PlaceholderKeys { get; init; }
///
///     // Optional: Add module-specific properties
///     public string? NotificationCategory { get; init; }
///     public int? Priority { get; init; }
/// }
/// </code>
/// <para>
/// <strong>Usage with EventWithMetadata:</strong>
/// </para>
/// <code>
/// public sealed class UserRegisteredEvent : EventWithMetadata&lt;EventMetadata&gt;
/// {
///     public required string UserId { get; init; }
///     public required string Email { get; init; }
///
///     public override EventMetadata GetMetadata() => new()
///     {
///         EventCode = "Identity.UserRegistered",
///         DisplayName = "User Registered",
///         Description = "New user registered",
///         PlaceholderKeys = ["UserId", "Email"]
///     };
/// }
/// </code>
/// </remarks>
public interface IEventMetadata
{
}