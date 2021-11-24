using Entity.Entities.BaseModel;
using Entity.Entities.Request;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;

namespace CampaignManagementWebApi.Controllers
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

        [HttpPost("create-order")]
        public IActionResult CreateOrder([FromBody] CreateOrderRequest order)
        {
            BaseResponse result = _orderService.CreateOrder(order);

            return new JsonResult(result) { ContentType = "application/json"};
        }
    }
}