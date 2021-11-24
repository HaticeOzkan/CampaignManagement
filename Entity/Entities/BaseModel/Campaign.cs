using Entity.Entities.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities.BaseModel
{
    public class Campaign
    {
        public decimal CurrentProductPrice { get; set; }
        public decimal InitialProductPrice { get; set; }
        public decimal Turnover { get; set; }
        public string ProductCode { get; set; }
        public long TotalProductStock { get; set; }
        public string CampaignCode { get; set; }
        public int Duration { get; set; }
        public long TargetSalesCount { get; set; }
        public double PriceManipulationLimit { get; set; }
        public double RateTobeIncreasedPerHour
        {
            get
            {
                return PriceManipulationLimit / Duration;
            }
        }
        public long CurrentStock
        {
            get
            {
                return TotalProductStock - TotalOrderQuantitiy;
            }
        }
        public long TotalOrderQuantitiy { get; set; }
        public int CurrentHour { get; set; } = default(int);
        public bool IsFinished { get; set; } = true;
        private Campaign() { }

        private static Campaign Instance;
        private static object lockObject = new object();
        public static Campaign ActiveInstance
        {
            get
            {
                if (Instance == null)
                {
                    lock (lockObject)
                    {
                        if (Instance == null)
                        {
                            Instance = new Campaign();
                        }
                    }
                }
                return Instance;
            }
        }
    }
}
