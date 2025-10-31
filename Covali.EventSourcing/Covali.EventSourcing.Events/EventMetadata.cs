namespace Covali.EventSourcing.Events;

/// <summary>
/// Empty metadata holder for events. Use partial classes to extend with metadata properties.
/// </summary>
/// <remarks>
/// <para>
/// <strong>Extensibility Pattern:</strong>
/// </para>
/// <para>
/// This class is intentionally empty to provide maximum flexibility. Each module can define
/// its own metadata structure by extending this class with a partial class definition.
/// </para>
/// <code>
/// // In your module (e.g., Notifications module):
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
/// <para>
/// Or use enums, different types, or any structure that fits your needs:
/// </para>
/// <code>
/// // Alternative approach with enum-based event codes:
/// namespace Covali.EventSourcing.Events;
///
/// public partial class EventMetadata
/// {
///     public required EventCode Code { get; init; }
///     public required NotificationPriority Priority { get; init; }
///     public required HashSet&lt;NotificationChannel&gt; Channels { get; init; }
/// }
/// </code>
/// <para>
/// This pattern allows any module to define metadata without being restricted by the core package.
/// </para>
/// </remarks>
public partial class EventMetadata
{
}
