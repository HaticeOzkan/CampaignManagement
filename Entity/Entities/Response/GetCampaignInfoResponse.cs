using Entity.Entities.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities.Response
{
    [Serializable]
    public class GetCampaignInfoResponse : BaseResponse
    {
        public bool IsFinished { get; set; }
        public long TargetSalesCount { get; set; }
        public long TotalSales { get; set; }
        public decimal Turnover { get; set; }
        public string AvarageItemPrice { get; set; }
    }
}
