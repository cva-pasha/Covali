using Covali.EventSourcing.Commands;

namespace Covali.EventSourcing.Tests.Unit.Commands.Stubs;

internal class InternalCommandHandler : ICommandHandler<SampleCommand>
{
    public Task HandleAsync(
        SampleCommand command,
        CancellationToken ct = default
    )
    {
        return Task.CompletedTask;
    }
}