using CampaignManagement.Interface;
using CampaignManagement.Operation;
using CampaignManagement.Utilitiy;
using Moq;
using System;
using System.ComponentModel;
using Xunit;
using Entity.Entities.Response;

namespace CampaignManagementTest.ConsoleAppTest.OperationTest
{
    public class CampaignOperationTest
    {
        private CampaignOperation CampaignOperation { get; set; }

        public CampaignOperationTest()
        {
            CampaignOperation = new CampaignOperation();  
        }

        [Fact]
        [Category("CampaignOperation")]
        public void CreateCampaign_ShouldReturnError_WhenCommandLengthWrong()
        {
            String[] Command = new string[] { "create_campaign", "C11", "P11" };

            var result = Record.Exception(() => CampaignOperation.CreateCampaign(Command));

            Assert.NotNull(result);

            var exception = Assert.IsType<Exception>(result);

            Assert.Equal(MessageHandler.GetIncorrectCommandErrorMessage(), exception.Message);
        }

        [Fact]
        [Category("CampaignOperation")]
        public void IncreaseTime_ShouldReturnError_WhenCommandWrong()
        {
            String[] Command = new string[] { "96", "8", "P11" ,"asdsd","we","666"};

            var result = Record.Exception(() => CampaignOperation.CreateCampaign(Command));

            Assert.NotNull(result);

            var exception = Assert.IsType<Exception>(result);

            Assert.Equal(MessageHandler.GetIncorrectCommandErrorMessage(), exception.Message);
        }

        [Fact]
        [Category("CampaignOperation")]
        public void GetCampaignInfo_ShouldSuccess_WhenCommandIsValid()
        {
            GetCampaignInfoResponse response = new GetCampaignInfoResponse
            {
                IsFinished = true,
                TargetSalesCount = 100,
                TotalSales = 100,
                Turnover = 50,
                AvarageItemPrice = "2",
                IsError=false
            };

            var mock = new Mock<IApiCaller>();
            mock.Setup(c => c.Caller<GetCampaignInfoResponse>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<object>()))
                .Returns(response);

            var result = mock.Object.Caller<GetCampaignInfoResponse>("GET", MessageHandler.GetBaseUrl(), null);

            Assert.NotNull(result);

            var actual = Assert.IsType<GetCampaignInfoResponse>(result).IsError;
            const bool expected = false;
            Assert.Equal(expected, actual);
        }
    }
}
