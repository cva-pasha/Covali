namespace Covali.EventSourcing.Events;

/// <summary>
/// Base class for events that provide rich metadata for cross-cutting concerns.
/// </summary>
/// <typeparam name="TMetadata">
/// The metadata type implementing <see cref="IEventMetadata"/>.
/// Define your own metadata structure by creating a class or record that implements IEventMetadata.
/// </typeparam>
/// <remarks>
/// <para>
/// <strong>When to Use:</strong>
/// </para>
/// <list type="bullet">
///   <item><description>Events that trigger notifications</description></item>
///   <item><description>Events tracked in analytics</description></item>
///   <item><description>Events logged to external systems</description></item>
///   <item><description>Events that require structured metadata</description></item>
/// </list>
/// <para>
/// <strong>When NOT to Use:</strong>
/// </para>
/// <list type="bullet">
///   <item><description>Simple internal events with no external integrations</description></item>
///   <item><description>High-frequency events where metadata overhead is unacceptable</description></item>
/// </list>
/// <para>
/// <strong>Handler Registration:</strong>
/// </para>
/// <para>
/// Handlers for <c>EventWithMetadata&lt;TMetadata&gt;</c> can be registered globally to handle all events
/// that inherit from this base class with a specific metadata type:
/// </para>
/// <code>
/// public class NotificationEventHandler : IEventHandler&lt;EventWithMetadata&lt;EventMetadata&gt;&gt;
/// {
///     public async Task HandleAsync(EventWithMetadata&lt;EventMetadata&gt; evt, CancellationToken ct)
///     {
///         var metadata = evt.GetMetadata();
///         // Process notification using metadata
///     }
/// }
/// </code>
/// <para>
/// This handler will be automatically called for ALL events inheriting from
/// <c>EventWithMetadata&lt;EventMetadata&gt;</c>, alongside any specific handlers for the concrete event type.
/// </para>
/// </remarks>
/// <example>
/// First, define your metadata structure:
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
/// Then create events using your metadata:
/// <code>
/// public sealed class UserRegisteredEvent : EventWithMetadata&lt;EventMetadata&gt;
/// {
///     // Event data
///     public required string UserId { get; init; }
///     public required string Email { get; init; }
///     public required string FirstName { get; init; }
///
///     // Metadata implementation
///     public override EventMetadata GetMetadata() => new()
///     {
///         EventCode = "Identity.UserRegistered",
///         DisplayName = "User Registered",
///         Description = "A new user has registered on the platform",
///         PlaceholderKeys = ["UserId", "Email", "FirstName"],
///         NotificationCategory = "Security",
///         Priority = 2
///     };
/// }
/// </code>
/// </example>
public abstract class EventWithMetadata<TMetadata> : IEvent
    where TMetadata : IEventMetadata
{
    /// <summary>
    /// Gets the metadata for this event.
    /// </summary>
    /// <returns>Event metadata of type <typeparamref name="TMetadata"/>.</returns>
    /// <remarks>
    /// <para>
    /// Override this method to provide event-specific metadata. The metadata structure
    /// is determined by your implementation of <see cref="IEventMetadata"/>.
    /// The metadata is used by:
    /// <list type="bullet">
    ///   <item><description>Event handlers to determine how to process the event</description></item>
    ///   <item><description>Notification systems to route and render messages</description></item>
    ///   <item><description>Analytics systems to categorize and track events</description></item>
    ///   <item><description>Logging systems to structure log entries</description></item>
    ///   <item><description>Administrative tools to display event information</description></item>
    /// </list>
    /// </para>
    /// <para>
    /// <strong>Performance Note:</strong> This method may be called multiple times during event processing.
    /// Consider caching the result if metadata construction is expensive.
    /// </para>
    /// </remarks>
    public abstract TMetadata GetMetadata();
}
