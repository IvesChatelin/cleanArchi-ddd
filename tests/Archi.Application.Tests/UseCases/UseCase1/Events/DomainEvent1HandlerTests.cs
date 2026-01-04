using Archi.Domain.Abstractions;
using Archi.Domain.DomainEvents;

namespace Archi.Application.Tests.UseCases.UseCase1.Events;

public class Fixture {
}

public class DomainEvent1HandlerTests: IClassFixture<Fixture>
{
    private readonly IDomainEventDispatcher _dispatcher;

    public DomainEvent1HandlerTests(Fixture fixture)
    {
    }

    [Fact]
    public async Task HandlerAsync_Event_Task()
    {
        DomainEvent1 evt = new (Guid.NewGuid());

        await _dispatcher.DispatchAsync(evt);
    }
}
