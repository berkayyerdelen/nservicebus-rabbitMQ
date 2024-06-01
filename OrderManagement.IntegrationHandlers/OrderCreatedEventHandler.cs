using OrderManagement.Definition;

namespace OrderManagement.IntegrationHandlers;

public class OrderCreatedEventHandler: IHandleMessages<OrderCreatedEvent>
{
    public Task Handle(OrderCreatedEvent message, IMessageHandlerContext context)
    {
        Console.WriteLine(message.UniqueId);
        return Task.CompletedTask;

    }
}