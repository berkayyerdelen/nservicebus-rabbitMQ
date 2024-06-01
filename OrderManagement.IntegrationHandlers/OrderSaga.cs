using OrderManagement.Definition;

namespace OrderManagement.IntegrationHandlers;

public class OrderSaga: ContainSagaData
{
    public Guid OrderId { get; set; }
}

public class OrderSageStateHandler: Saga<OrderSaga>, IAmStartedByMessages<OrderCreatedEvent>, IHandleMessages<PaymentHandledEvent>
{
    protected override void ConfigureHowToFindSaga(SagaPropertyMapper<OrderSaga> mapper)
    {
        mapper.MapSaga(saga => saga.OrderId).ToMessage<OrderCreatedEvent>(msg => msg.UniqueId)
            .ToMessage<PaymentHandledEvent>(y => y.ProductReferenceId);
    }

    public Task Handle(OrderCreatedEvent message, IMessageHandlerContext context)
    {
        Console.WriteLine($"Internal event handled: {message.UniqueId}");
        return Task.CompletedTask;
    }

    public Task Handle(PaymentHandledEvent message, IMessageHandlerContext context)
    {
        Console.WriteLine($"Payment event handled: {message.ProductReferenceId}");
        MarkAsComplete();
        return Task.CompletedTask;
    }
}