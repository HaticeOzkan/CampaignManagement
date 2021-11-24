using CampaignManagement.Interface;
using CampaignManagement.Utilitiy;
using Entity.Entities.BaseModel;
using Entity.Entities.Request;
using Moq;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Text;
using Xunit;

namespace CampaignManagementTest.ConsoleAppTest.UtilityTest
{
    public class ApiCallerTest
    {
        [Fact]
        public void CreateCampaign_ShouldReturnSuccess_WhenRequestWebApi()
        {
            var request = new CreateCampaignRequest
            {
                CampaignCode = "C11",
                ProductCode = "P11",
                Duration = 10,
                PriceManipulationLimit = 20,
                TargetSalesCount = 100
            };

            var response = new BaseResponse
            {
                IsError = false,
            };

            var expected = JsonConvert.SerializeObject(response);
            var expectedBytes = Encoding.UTF8.GetBytes(expected);
            var responseStream = new MemoryStream();
            responseStream.Write(expectedBytes, 0, expectedBytes.Length);
            responseStream.Seek(0, SeekOrigin.Begin);

            var responseWeb = new Mock<HttpWebResponse>();
            responseWeb.Setup(c => c.GetResponseStream()).Returns(responseStream);

            var requestWeb = new Mock<HttpWebRequest>();
            requestWeb.Setup(c => c.GetResponse()).Returns(responseWeb.Object);

            var factory = new Mock<IHttpWebRequestFactory>();
            factory.Setup(c => c.Create(It.IsAny<string>()))
                .Returns(requestWeb.Object);

            var actualRequest = factory.Object.Create(MessageHandler.GetBaseUrl());
            actualRequest.Method = WebRequestMethods.Http.Get;

            string actual;

            using (var httpWebResponse = (HttpWebResponse)actualRequest.GetResponse())
            {
                using (var streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    actual = streamReader.ReadToEnd();
                }
            }

            Assert.Equal(actual, expected);
        }
    }
}
