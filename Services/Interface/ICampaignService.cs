using Entity.Entities.BaseModel;
using Entity.Entities.Request;
using Entity.Entities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface ICampaignService
    {
        BaseResponse CleanCampaignOnSystem();
        BaseResponse CreateCampaign(CreateCampaignRequest createProductRequest);
        GetCampaignInfoResponse GetCampaignInfo(string campaignCode);
        IncreaseTimeResponse IncreaseTime(int hour);
    }
}
