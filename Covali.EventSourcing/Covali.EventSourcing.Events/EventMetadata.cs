namespace Covali.EventSourcing.Events;

/// <summary>
/// Event metadata holder. Use partial classes to extend with additional metadata properties.
/// This class provides core metadata properties that are universally applicable to all events.
/// Modules can extend this class with their own partial class definitions to add domain-specific metadata.
/// </summary>
/// <remarks>
/// <para>
/// <strong>Extensibility Pattern:</strong>
/// </para>
/// <code>
/// // In your module (e.g., Notifications module):
/// namespace Covali.EventSourcing.Events;
///
/// public partial class EventMetadata
/// {
///     public string? NotificationCategory { get; init; }
///     public HashSet&lt;string&gt;? DefaultChannels { get; init; }
/// }
/// </code>
/// <para>
/// This pattern allows any module to extend metadata without modifying the core EventSourcing package.
/// </para>
/// </remarks>
/// <example>
/// <code>
/// var metadata = new EventMetadata
/// {
///     EventCode = "Identity.UserRegistered",
///     DisplayName = "User Registered",
///     Description = "A new user has registered on the platform",
///     Category = "Security",
///     PlaceholderKeys = ["UserId", "Email", "FirstName"]
/// };
/// </code>
/// </example>
public partial class EventMetadata
{
    /// <summary>
    /// Unique event code identifier used for event tracking, routing, and integration.
    /// </summary>
    /// <remarks>
    /// <para>
    /// <strong>Recommended Format:</strong> "{ModuleName}.{EventName}"
    /// </para>
    /// <para>
    /// <strong>Examples:</strong>
    /// <list type="bullet">
    ///   <item><description>Identity.UserRegistered</description></item>
    ///   <item><description>Payment.TransactionCompleted</description></item>
    ///   <item><description>Inventory.StockLevelChanged</description></item>
    /// </list>
    /// </para>
    /// <para>
    /// This code should be unique across your system and remain stable over time,
    /// as external systems may depend on it for integration and routing.
    /// </para>
    /// </remarks>
    public required string EventCode { get; init; }

    /// <summary>
    /// Human-readable display name for UI, reporting, and administrative tools.
    /// </summary>
    /// <remarks>
    /// This name is used in dashboards, logs, and admin interfaces.
    /// It should be concise (2-5 words) and clearly describe the event.
    /// </remarks>
    /// <example>
    /// <code>
    /// DisplayName = "User Registered"
    /// DisplayName = "Payment Processed"
    /// DisplayName = "Order Shipped"
    /// </code>
    /// </example>
    public required string DisplayName { get; init; }

    /// <summary>
    /// Detailed description of what this event represents and when it occurs.
    /// </summary>
    /// <remarks>
    /// Provide context about:
    /// <list type="bullet">
    ///   <item><description>What action triggered this event</description></item>
    ///   <item><description>What state changed in the system</description></item>
    ///   <item><description>Any important business rules or conditions</description></item>
    /// </list>
    /// </remarks>
    /// <example>
    /// <code>
    /// Description = "Triggered when a new user successfully completes registration and email verification"
    /// </code>
    /// </example>
    public required string Description { get; init; }

    /// <summary>
    /// Event category for grouping, filtering, and organizational purposes.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Use domain-specific categories that make sense for your application.
    /// Categories help organize events in logs, dashboards, and administrative tools.
    /// </para>
    /// <para>
    /// <strong>Common Categories:</strong>
    /// <list type="bullet">
    ///   <item><description>Security - Authentication, authorization, access control</description></item>
    ///   <item><description>Transaction - Financial operations, payments</description></item>
    ///   <item><description>Inventory - Stock management, product updates</description></item>
    ///   <item><description>User - User lifecycle events</description></item>
    ///   <item><description>System - System-level operations, maintenance</description></item>
    /// </list>
    /// </para>
    /// <para>
    /// Modules can map these string categories to their own enums as needed.
    /// </para>
    /// </remarks>
    public required string Category { get; init; }

    /// <summary>
    /// Available data fields that can be extracted from this event for templating,
    /// logging, analytics, or external system integration.
    /// </summary>
    /// <remarks>
    /// <para>
    /// List property names from the event that contain useful data for downstream processing.
    /// These keys can be used for:
    /// <list type="bullet">
    ///   <item><description>Template rendering (e.g., email notifications)</description></item>
    ///   <item><description>Log formatting and structured logging</description></item>
    ///   <item><description>Analytics dimensions</description></item>
    ///   <item><description>External system data mapping</description></item>
    /// </list>
    /// </para>
    /// <para>
    /// <strong>Best Practice:</strong> Only include properties that external systems
    /// or cross-cutting concerns need to access. Don't expose internal implementation details.
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// PlaceholderKeys = ["UserId", "Email", "FirstName", "RegisteredAt"]
    /// </code>
    /// </example>
    public required HashSet<string> PlaceholderKeys { get; init; }
}
