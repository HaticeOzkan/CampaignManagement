using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities.Request
{
    [Serializable]
    public class CreateCampaignRequest
    {
        public string CampaignCode { get; set; }
        public string ProductCode { get; set; }
        public int Duration { get; set; }
        public double PriceManipulationLimit { get; set; }
        public long TargetSalesCount { get; set; }
    }
}
