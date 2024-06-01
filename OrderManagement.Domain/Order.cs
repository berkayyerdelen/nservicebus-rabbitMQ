namespace OrderManagement.Domain;

public class Order
{
    public Guid OrderId { get; set; }
    public decimal Price { get; set; }
    public OrderStatus Status { get; set; }
}
public enum OrderStatus
{
    OrderCreated,
    PaymentPending,
    Processing,
    OrderCompleted
}