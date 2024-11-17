using Application.Contracts;
using Application.Dtos.Orders;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class OrderController : BaseApiController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateDto orderCreateDto) =>
            HandleResult(await _orderService.CreateOrder(orderCreateDto));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(Guid id) =>
            HandleResult(await _orderService.GetOrder(id));

        [HttpGet]
        public async Task<IActionResult> GetAllOrders() =>
            HandleResult(await _orderService.GetAllOrders());

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder([FromBody] OrderUpdateDto orderUpdateDto, Guid id) =>
            HandleResult(await _orderService.UpdateOrder(orderUpdateDto, id));

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateOrderStatus([FromBody] OrderStatus newStatus, Guid id) =>
            HandleResult(await _orderService.UpdateOrderStatus(newStatus, id));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id) =>
            HandleResult(await _orderService.DeleteOrder(id));
    }
}
