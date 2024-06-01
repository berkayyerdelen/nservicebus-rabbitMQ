using NServiceBus;

namespace OrderManagement.Definition;

public class OrderCreatedEvent : IMessage
{
    public Guid UniqueId { get; set; }
}