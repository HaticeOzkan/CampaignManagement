using Entity.Entities.BaseModel;
using Entity.Entities.Request;
using Entity.Entities.Response;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;

namespace CampaignManagementWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignController : ControllerBase
    {
        private readonly ICampaignService _campaignService;

        public CampaignController(ICampaignService campaignService)
        {
            _campaignService = campaignService;
        }

        [HttpGet("campaign-info/{campaignCode}")]
        public IActionResult GetCampaignInfo(string campaignCode)
        {
            GetCampaignInfoResponse result = _campaignService.GetCampaignInfo(campaignCode);

            return new JsonResult(result){ContentType = "application/json"};
        }

        [HttpPost("create-campaign")]
        public IActionResult CreateCampaign([FromBody] CreateCampaignRequest campaign)
        {
            BaseResponse result = _campaignService.CreateCampaign(campaign);

            return new JsonResult(result) { ContentType = "application/json"};
        }

        [HttpGet("increase-time/{hour}")]
        public IActionResult IncreaseTime(int hour)
        {
            IncreaseTimeResponse result = _campaignService.IncreaseTime(hour);

            return new JsonResult(result) { ContentType = "application/json"};
        }

        [HttpGet("clean-campaign-system")]
        public IActionResult CleanCampaignOnSystem()
        {
            BaseResponse result = _campaignService.CleanCampaignOnSystem();

            return new JsonResult(result) { ContentType = "application/json" };
        }
    }
}