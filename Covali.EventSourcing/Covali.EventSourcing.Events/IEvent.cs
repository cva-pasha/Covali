namespace Covali.EventSourcing.Events;

/// <summary>
/// Marker interface for all domain events.
/// Events represent something that has happened in the system.
/// </summary>
/// <remarks>
/// <para>
/// Events are immutable records of facts that have occurred. They should:
/// <list type="bullet">
///   <item><description>Be named in past tense (e.g., UserRegistered, OrderShipped)</description></item>
///   <item><description>Contain all data needed to understand what happened</description></item>
///   <item><description>Be immutable (use records or init-only properties)</description></item>
///   <item><description>Not contain business logic</description></item>
/// </list>
/// </para>
/// <para>
/// For events that need metadata for cross-cutting concerns, inherit from
/// <c>EventWithMetadata&lt;TMetadata&gt;</c> instead of implementing this interface directly.
/// </para>
/// </remarks>
public interface IEvent;