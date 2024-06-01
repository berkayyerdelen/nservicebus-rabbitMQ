using Microsoft.AspNetCore.Mvc;
using OrderManagement.Definition;

namespace OrderManagement.Controllers;

[ApiController]
[Route("orders")]
public class OrdersController: ControllerBase
{
    private readonly IMessageSession _messageSession;

    public OrdersController(IMessageSession messageSession)
    {
        _messageSession = messageSession;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder()
    {
        await _messageSession.Send(new OrderCreatedEvent()
        {
            UniqueId = new Guid("0928a430-58ea-4f9b-bf70-852e1ae49e8c")
        });
        return Ok();
    }

    [HttpPost("payment")]
    public async Task<IActionResult> CreatePayment()
    {
        await _messageSession.Send(new PaymentHandledEvent()
        {
            ProductReferenceId = new Guid("0928a430-58ea-4f9b-bf70-852e1ae49e8c")
        });
        return Ok();
    }
}