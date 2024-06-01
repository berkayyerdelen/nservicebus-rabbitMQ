using OrderManagement.Domain;

namespace OrderManagement.Infrastructure;

public class OrderRepository : OrderRepository.IOrderRepository
{
    private Dictionary<Guid, Order> _orders;

    public OrderRepository()
    {
        _orders = new Dictionary<Guid, Order>();
    }

    public Task AddOrder(Order order)
    {
        _orders.Add(order.OrderId, order);

        return Task.CompletedTask;
    }

    public Task<Order> GetOrderById(Guid orderId)
    {
        Order order;

        if (!_orders.TryGetValue(orderId, out order))
        {
            order = new Order();
        }

        return Task.FromResult(order);
    }

    public interface IOrderRepository
    {
        Task<Order> GetOrderById(Guid orderId);
        Task AddOrder(Order order);
    }
}