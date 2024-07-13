
using Application;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace OnlineStore.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(int userId, int productId)
        {
            try
            {
                await _orderService.CreateOrder(userId, productId);
                return Ok();
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
