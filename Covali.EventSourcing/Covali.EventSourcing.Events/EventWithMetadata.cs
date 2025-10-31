namespace Covali.EventSourcing.Events;

/// <summary>
/// Base class for events that provide rich metadata for cross-cutting concerns.
/// </summary>
/// <remarks>
/// <para>
/// <strong>Important:</strong> You must extend <see cref="EventMetadata"/> with a partial class
/// before using this class. The core <c>EventMetadata</c> class is intentionally empty.
/// </para>
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
/// Handlers for <c>EventWithMetadata</c> can be registered globally to handle all events
/// that inherit from this base class:
/// </para>
/// <code>
/// public class NotificationEventHandler : IEventHandler&lt;EventWithMetadata&gt;
/// {
///     public async Task HandleAsync(EventWithMetadata evt, CancellationToken ct)
///     {
///         var metadata = evt.GetMetadata();
///         // Process notification using metadata
///     }
/// }
/// </code>
/// <para>
/// This handler will be automatically called for ALL events inheriting from <c>EventWithMetadata</c>,
/// alongside any specific handlers for the concrete event type.
/// </para>
/// </remarks>
/// <example>
/// First, extend EventMetadata with your own structure:
/// <code>
/// namespace Covali.EventSourcing.Events;
///
/// public partial class EventMetadata
/// {
///     public required string EventCode { get; init; }
///     public required string DisplayName { get; init; }
///     public required string Description { get; init; }
///     public required HashSet&lt;string&gt; PlaceholderKeys { get; init; }
/// }
/// </code>
/// Then create events using your metadata:
/// <code>
/// public sealed record UserRegisteredEvent : EventWithMetadata
/// {
///     // Event data
///     public required string UserId { get; init; }
///     public required string Email { get; init; }
///     public required string FirstName { get; init; }
///
///     // Metadata implementation (uses your partial class definition)
///     public override EventMetadata GetMetadata() => new()
///     {
///         EventCode = "Identity.UserRegistered",
///         DisplayName = "User Registered",
///         Description = "A new user has registered on the platform",
///         PlaceholderKeys = ["UserId", "Email", "FirstName"]
///     };
/// }
/// </code>
/// </example>
public abstract class EventWithMetadata : IEvent
{
    /// <summary>
    /// Gets the metadata for this event.
    /// </summary>
    /// <returns>Event metadata containing properties defined in your partial class extension.</returns>
    /// <remarks>
    /// <para>
    /// Override this method to provide event-specific metadata. The metadata structure
    /// is determined by your partial class definition of <see cref="EventMetadata"/>.
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
    public abstract EventMetadata GetMetadata();
}
