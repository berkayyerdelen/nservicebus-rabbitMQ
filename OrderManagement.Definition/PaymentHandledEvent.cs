using NServiceBus;

namespace OrderManagement.Definition;

public class PaymentHandledEvent: IMessage
{
    public Guid ProductReferenceId { get; set; }
}