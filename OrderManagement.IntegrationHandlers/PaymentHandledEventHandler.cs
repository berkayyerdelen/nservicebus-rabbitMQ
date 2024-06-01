using OrderManagement.Definition;

namespace OrderManagement.IntegrationHandlers;

public class PaymentHandledEventHandler: IHandleMessages<PaymentHandledEvent>
{
    
    public Task Handle(PaymentHandledEvent message, IMessageHandlerContext context)
    {
        Console.WriteLine(message.ProductReferenceId);
        return Task.CompletedTask;
    }
}