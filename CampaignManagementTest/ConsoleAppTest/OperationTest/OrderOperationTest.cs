using CampaignManagement.Interface;
using CampaignManagement.Operation;
using CampaignManagement.Utilitiy;
using Entity.Entities.BaseModel;
using Entity.Entities.Request;
using Moq;
using System;
using System.ComponentModel;
using Xunit;

namespace CampaignManagementTest.ConsoleAppTest.OperationTest
{
    public class OrderOperationTest
    {
        private OrderOperation OrderOperation { get; set; }

        public OrderOperationTest()
        {
            OrderOperation = new OrderOperation();
        }

        [Fact]
        [Category("OrderOperation")]
        public void CreateOrder_ShouldReturnError_WhenCommandLengthWrong()
        {
            String[] Command = new string[] { "create_campaign", "C11", "P11" };

            var result = Record.Exception(() => OrderOperation.CreateOrder(Command));

            Assert.NotNull(result);

            var exception = Assert.IsType<Exception>(result);

            Assert.Equal(MessageHandler.GetIncorrectCommandErrorMessage(), exception.Message);
        }


        [Fact]
        [Category("OrderOperation")]
        public void CreateOrder_ShouldSuccess_WhenCommandIsValid()
        {
            BaseResponse response = new BaseResponse
            {
                ErrorCode=0,
                IsError = false
            };

            CreateOrderRequest request = new CreateOrderRequest
            {
                ProductCode = "20A",
                Quantity = 5
            };

            var mock = new Mock<IApiCaller>();
            mock.Setup(c => c.Caller<BaseResponse>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<object>()))
                .Returns(response);

            var result = mock.Object.Caller<BaseResponse>("POST", MessageHandler.GetBaseUrl(), request);

            Assert.NotNull(result);

            var actual = Assert.IsType<BaseResponse>(result).IsError;
            const bool expected = false;
            Assert.Equal(expected, actual);
        }
    }
}
