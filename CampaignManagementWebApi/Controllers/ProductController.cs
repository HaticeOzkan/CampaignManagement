using Entity.Entities.BaseModel;
using Entity.Entities.Request;
using Entity.Entities.Response;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;

namespace CampaignManagementWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("product-info/{productCode}")]
        public IActionResult GetProductInfo(string productCode)
        {
            GetProductInfoResponse result = _productService.GetProductInfo(productCode);

            return new JsonResult(result) { ContentType = "application/json" };

        }

        [HttpPost("create-product")]
        public IActionResult CreateProduct([FromBody] CreateProductRequest product)
        {
            BaseResponse result = _productService.CreateProduct(product);

            return new JsonResult(result) { ContentType = "application/json" };
        }
    }
}