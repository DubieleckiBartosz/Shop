using Application.DtoModels.OrderLineModels;
using Application.DtoModels.OrderModels;
using Application.Service.Interfaces;
using Application.Wrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using WebAPI.RequestModels;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [SwaggerOperation(Summary = "Receiving active orders")]
        [HttpGet]
        public async Task<IActionResult> GetActiveOrders()
        {
            var result = await _orderService.GetActiveOrdersAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return Ok(new ServiceResponse<IEnumerable<OrderDto>>(result));
        }

        [SwaggerOperation(Summary = "Receiving orders between dates")]
        [HttpPost("GetOrders")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetByDate([FromBody] OrderRequest order)
        {
            return Ok(await _orderService.GetOrdersByDateAsync(order.minDate, order.maxDate));
        }

        [SwaggerOperation(Summary = "Create new order")]
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto orderDto)
        {
            var order = await _orderService.CreateOrderAsync(orderDto, User.FindFirstValue(ClaimTypes.NameIdentifier));
            return Ok(new ServiceResponse<OrderDto>(order) { Message = "You have three days to delete" });
        }

        [SwaggerOperation(Summary = "Change of order")]
        [HttpPut("[action]/{orderId}")]
        public async Task<IActionResult> UpdateOrder([FromRoute]int orderId,[FromBody] UpdateOrderLineDto updateOrderlineDto)
        {
            return Ok(await _orderService.UpdateOrderAsync(orderId, User.FindFirstValue(ClaimTypes.NameIdentifier), updateOrderlineDto));
        }

        [SwaggerOperation(Summary = "Delete order")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder([FromRoute]int id)
        {
            var result= await _orderService.DeleteAsync(id,User.FindFirstValue(ClaimTypes.NameIdentifier));
            return Ok(result);
        }
    }
}
