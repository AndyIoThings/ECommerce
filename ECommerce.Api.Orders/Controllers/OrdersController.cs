using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ECommerce.Api.Orders.Interfaces;


namespace ECommerce.Api.Orders.Controllers
{
    namespace ECommerce.Api.Orders.Controllers
    {
        [ApiController]
        [Route("api/orders")]
        public class OrdersController : ControllerBase
        {
            private readonly IOrdersProvider ordersProvider;
            public OrdersController(IOrdersProvider ordersProvider)
            {
                this.ordersProvider = ordersProvider;
            }
            [HttpGet("{customerId}")]
            public async Task<IActionResult> GetOrdersAsync(int customerId)
            {
                var result = await ordersProvider.GetOrdersAsync(customerId);
                if (result.IsSuccess)
                {
                    return Ok(result.Orders);
                }
                return NotFound();
            }
        }
    }
}
