using Entity.Entities.BaseModel;
using Entity.Entities.Request;
using Entity.Entities.Response;
using Services.Service;
using System.ComponentModel;
using Xunit;

namespace CampaignManagementTest.WepApiTest.ServiceTest
{
    public class ProductServiceTest
    {
        public ProductService Service;
        public static Campaign CampaignOnSystem;

        public ProductServiceTest()
        {
            Service = new ProductService();
            CampaignOnSystem = Campaign.ActiveInstance;
        }

        [Fact]
        [Category("Product")]
        public void CreateProduct_ShouldReturnError_WhenPriceLessThenZero()
        {
            CreateProductRequest request = new CreateProductRequest
            {
                ProductCode = "XR5",
                Price = -1,
                Stock = 20
            };

            var result = Service.CreateProduct(request);
            Assert.NotNull(result);
            var exception = Assert.IsType<BaseResponse>(result);
            var actual = exception.IsError;
            const bool expected = true;
            Assert.Equal(expected, actual);
        }

        [Fact]
        [Category("Product")]
        public void CreateProduct_ShouldReturnError_WhenStockLessThenZero()
        {
            CreateProductRequest request = new CreateProductRequest
            {
                ProductCode = "XR5",
                Price = 20,
                Stock = -2
            };

            var result = Service.CreateProduct(null);
            Assert.NotNull(result);
            var exception = Assert.IsType<BaseResponse>(result);
            var actual = exception.IsError;
            const bool expected = true;
            Assert.Equal(expected, actual);
        }

        [Fact]
        [Category("Product")]
        public void CreateProduct_ShouldSuccess_WhenProductIsValid()
        {
            CreateProductRequest request = new CreateProductRequest
            {
                ProductCode = "XR5",
                Price = 20,
                Stock = 10
            };

            var result = Service.CreateProduct(request);
            Assert.NotNull(result);
            var exception = Assert.IsType<BaseResponse>(result);
            var actual = exception.IsError;
            const bool expected = false;
            Assert.Equal(expected, actual);
        }

        [Fact]
        [Category("Product")]
        public void GetProductInfo_ShouldReturnError_WhenProductCodeIsNull()
        {
            var result = Service.GetProductInfo(null);
            Assert.NotNull(result);
            var exception = Assert.IsType<GetProductInfoResponse>(result);
            var actual = exception.IsError;
            const bool expected = true;
            Assert.Equal(expected, actual);
        }

        [Fact]
        [Category("Product")]
        public void GetProductInfo_ShouldReturnError_WhenProductCodeInvalid()
        {
            CampaignOnSystem.ProductCode = "XA2";
            var result = Service.GetProductInfo("XA4");
            Assert.NotNull(result);
            var exception = Assert.IsType<GetProductInfoResponse>(result);
            var actual = exception.IsError;
            const bool expected = true;
            Assert.Equal(expected, actual);
        }

        [Fact]
        [Category("Product")]
        public void GetProductInfo_ShouldSuccess_WhenProductCodeIsValid()
        {
            CampaignOnSystem.ProductCode = "XA2";
            var result = Service.GetProductInfo("XA2");
            Assert.NotNull(result);
            var exception = Assert.IsType<GetProductInfoResponse>(result);
            var actual = exception.IsError;
            const bool expected = false;
            Assert.Equal(expected, actual);
        }
    }
}
