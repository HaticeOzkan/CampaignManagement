using Entity.Entities.BaseModel;
using Entity.Entities.Request;
using Entity.Entities.Response;
using Services.Service;
using System.ComponentModel;
using Xunit;

namespace CampaignManagementTest.WepApiTest.ServiceTest
{
    public class CampaignServiceTest
    {
        public CampaignService Service;
        public static Campaign CampaignOnSystem;

        public CampaignServiceTest()
        {
            Service = new CampaignService();
            CampaignOnSystem = Campaign.ActiveInstance;
        }

        [Fact]
        [Category("Campaign")]
        public void CreateCampaign_ShouldReturnError_WhenProductCodeIsInvalid()
        {
            CampaignOnSystem.ProductCode = "JK9";

            CreateCampaignRequest request = new CreateCampaignRequest
            {
                CampaignCode = "KL5",
                ProductCode = "K5T",
                Duration = 12,
                PriceManipulationLimit = 20,
                TargetSalesCount = 50
            };

            var result = Service.CreateCampaign(request);
            Assert.NotNull(result);
            var exception = Assert.IsType<BaseResponse>(result);
            var actual = exception.IsError;
            const bool expected = true;
            Assert.Equal(expected, actual);
        }

        [Fact]
        [Category("Campaign")]
        public void CreateCampaign_ShouldReturnError_WhenCampaignIsNotFinished()
        {
            CampaignOnSystem.CampaignCode = "KL9";

            CreateCampaignRequest request = new CreateCampaignRequest
            {
                CampaignCode = "KL5",
                ProductCode = "K5T",
                Duration = 12,
                PriceManipulationLimit = 20,
                TargetSalesCount = 50
            };

            var result = Service.CreateCampaign(request);
            Assert.NotNull(result);
            var exception = Assert.IsType<BaseResponse>(result);
            var actual = exception.IsError;
            const bool expected = true;
            Assert.Equal(expected, actual);
        }

        [Fact]
        [Category("Campaign")]
        public void CreateCampaign_ShouldReturnError_WhenTargetSalesCountLessThenZore()
        {
            CreateCampaignRequest request = new CreateCampaignRequest
            {
                CampaignCode = "KL5",
                ProductCode = "K5T",
                Duration = 12,
                PriceManipulationLimit = 20,
                TargetSalesCount = -2
            };

            var result = Service.CreateCampaign(request);
            Assert.NotNull(result);
            var exception = Assert.IsType<BaseResponse>(result);
            var actual = exception.IsError;
            const bool expected = true;
            Assert.Equal(expected, actual);
        }

        [Fact]
        [Category("Campaign")]
        public void CreateCampaign_ShouldSuccess_WhenCampaignIsValid()
        {
            CampaignOnSystem.CampaignCode = null;
            CampaignOnSystem.TargetSalesCount = 0;
            CampaignOnSystem.TotalOrderQuantitiy = 0;
            CampaignOnSystem.CurrentHour = 0;
            CampaignOnSystem.InitialProductPrice = 0;
            CampaignOnSystem.Duration = 10;
            CampaignOnSystem.PriceManipulationLimit = 0;
            CampaignOnSystem.ProductCode = "K5T";

            CreateCampaignRequest request = new CreateCampaignRequest
            {
                CampaignCode = "KL5",
                ProductCode = "K5T",
                Duration = 12,
                PriceManipulationLimit = 20,
                TargetSalesCount = 50
            };

            var result = Service.CreateCampaign(request);
            Assert.NotNull(result);
            var exception = Assert.IsType<BaseResponse>(result);
            var actual = exception.IsError;
            const bool expected = false;
            Assert.Equal(expected, actual);
        }

        [Fact]
        [Category("Campaign")]
        public void GetCampaignInfo_ShouldReturnError_WhenCampaignCodeIsInvalid()
        {
            CampaignOnSystem.CampaignCode = "K3T";
            var result = Service.GetCampaignInfo("K2E");
            Assert.NotNull(result);
            var exception = Assert.IsType<GetCampaignInfoResponse>(result);
            var actual = exception.IsError;
            const bool expected = true;
            Assert.Equal(expected, actual);
        }

        [Fact]
        [Category("Campaign")]
        public void IncreaseTime_ShouldSuccess_WhenCampaignIsValid()
        {
            CampaignOnSystem.CampaignCode = "C1";
            CampaignOnSystem.TargetSalesCount = 100;
            CampaignOnSystem.TotalOrderQuantitiy = 0;
            CampaignOnSystem.CurrentHour = 0;
            CampaignOnSystem.InitialProductPrice = 100;
            CampaignOnSystem.Duration = 10;
            CampaignOnSystem.PriceManipulationLimit = 20;

            var result = Service.IncreaseTime(3);
            Assert.NotNull(result);
            var exception = Assert.IsType<IncreaseTimeResponse>(result);
            var actual = exception.IsError;
            const bool expected = false;
            Assert.Equal(expected, actual);
        }
    }
}
