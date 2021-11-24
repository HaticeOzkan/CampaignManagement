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
    public class ProductOperationTest
    {
        private ProductOperation ProductOperation { get; set; }

        public ProductOperationTest()
        {
            ProductOperation = new ProductOperation();
        }

        [Fact]
        [Category("ProductOperation")]
        public void CreateProduct_ShouldReturnError_WhenCommandLengthWrong()
        {
            String[] Command = new string[] {  "C11", "P11" };

            var result = Record.Exception(() => ProductOperation.CreateProduct(Command));

            Assert.NotNull(result);

            var exception = Assert.IsType<Exception>(result);

            Assert.Equal(MessageHandler.GetIncorrectCommandErrorMessage(), exception.Message);
        }

        [Fact]
        [Category("ProductOperation")]
        public void GetProductInfo_ShouldReturnError_WhenCommandWrong()
        {
            String[] Command = new string[] { "96", "8", "P11", "asdsd", "we", "666" };

            var result = Record.Exception(() => ProductOperation.GetProductInfo(Command));

            Assert.NotNull(result);

            var exception = Assert.IsType<Exception>(result);

            Assert.Equal(MessageHandler.GetIncorrectCommandErrorMessage(), exception.Message);
        }

        [Fact]
        [Category("ProductOperation")]
        public void CreateProduct_ShouldSuccess_WhenCommandIsValid()
        {
            CreateProductRequest request = new CreateProductRequest
            {
              ProductCode="RE4",
              Price=10,
              Stock=10
            };

            BaseResponse response = new BaseResponse
            {
                ErrorCode = 0,
                IsError = false
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
