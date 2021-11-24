using Entity.Entities.BaseModel;
using Entity.Entities.Request;
using Services.Service;
using System.ComponentModel;
using Xunit;

namespace CampaignManagementTest.WepApiTest.ServiceTest
{
    public class OrderServiceTest
    {
        public OrderService Service;
        public static Campaign CampaignOnSystem;

        public OrderServiceTest()
        {
            Service = new OrderService();
            CampaignOnSystem = Campaign.ActiveInstance;
        }

        [Fact]
        [Category("Order")]
        public void CreateOrder_ShouldReturnError_WhenOrderIsNull()
        {
            var result = Service.CreateOrder(null);
            Assert.NotNull(result);
            var exception = Assert.IsType<BaseResponse>(result);
            var actual = exception.IsError;
            const bool expected = true;
            Assert.Equal(expected, actual);
        }

        [Fact]
        [Category("Order")]
        public void CreateOrder_ShouldReturnError_WhenProductCodeIsInvalid()
        {
            CampaignOnSystem.ProductCode = "X7L";
            CampaignOnSystem.TotalProductStock = 20;

            CreateOrderRequest request = new CreateOrderRequest
            {
                ProductCode = "X5T",
                Quantity = 20
            };

            var result = Service.CreateOrder(request);
            Assert.NotNull(result);
            var exception = Assert.IsType<BaseResponse>(result);
            var actual = exception.IsError;
            const bool expected = true;
            Assert.Equal(expected, actual);
        }

        [Fact]
        [Category("Order")]
        public void CreateOrder_ShouldReturnError_WhenInsufficientStock()
        {
            CampaignOnSystem.ProductCode = "X7L";
            CampaignOnSystem.TotalProductStock = 20;

            CreateOrderRequest request = new CreateOrderRequest
            {
                ProductCode = "X7L",
                Quantity = 50
            };

            var result = Service.CreateOrder(request);
            Assert.NotNull(result);
            var exception = Assert.IsType<BaseResponse>(result);
            var actual = exception.IsError;
            const bool expected = true;
            Assert.Equal(expected, actual);
        }

        [Fact]
        [Category("Order")]
        public void CreateOrder_ShouldReturnError_WhenQuantityLessThenZero()
        {
            CampaignOnSystem.ProductCode = "X7L";
            CampaignOnSystem.TotalProductStock = 20;

            CreateOrderRequest request = new CreateOrderRequest
            {
                ProductCode = "X7L",
                Quantity = -2
            };

            var result = Service.CreateOrder(request);
            Assert.NotNull(result);
            var exception = Assert.IsType<BaseResponse>(result);
            var actual = exception.IsError;
            const bool expected = true;
            Assert.Equal(expected, actual);
        }
    }
}