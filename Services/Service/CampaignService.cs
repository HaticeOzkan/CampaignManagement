using Entity.Entities.BaseModel;
using Entity.Entities.Request;
using Entity.Entities.Response;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class CampaignService : ICampaignService
    {
        public static Campaign CampaignOnSystem;
        public static readonly string InvalidEnteredValuesErrorMessage = "Entered values ​​are not valid";
        public static readonly string ActiveCampaignInTheSystemErrorMessage = "There is an active campaign in the system.";
        public static readonly string CampaignExpiredErrorMessage = "Campaign has expired";

        public CampaignService()
        {
            CampaignOnSystem = Campaign.ActiveInstance;
        }

        public BaseResponse CreateCampaign(CreateCampaignRequest campaign)
        {
            BaseResponse response = new BaseResponse();

            try
            {
                if (campaign == null || campaign.CampaignCode == null || campaign.ProductCode != CampaignOnSystem.ProductCode || campaign.Duration < 1 || campaign.PriceManipulationLimit <= 0 || campaign.TargetSalesCount <= 0)
                {
                    throw new BusinessException(InvalidEnteredValuesErrorMessage, 2);
                }
                else if (!CampaignOnSystem.IsFinished)
                {
                    throw new BusinessException(ActiveCampaignInTheSystemErrorMessage, 1);
                }
                else
                {
                    CampaignOnSystem.CampaignCode = campaign.CampaignCode;
                    CampaignOnSystem.Duration = campaign.Duration;
                    CampaignOnSystem.PriceManipulationLimit = Math.Round(campaign.PriceManipulationLimit);
                    CampaignOnSystem.TargetSalesCount = campaign.TargetSalesCount;
                    CampaignOnSystem.IsFinished = false;
                }
            }
            catch (BusinessException ex)
            {
                response.IsError = true;
                response.ErrorMessage = ex.Message;
                response.ErrorCode = ex.ErrorCode;
            }
            catch (Exception ex)
            {
                response.IsError = true;
                response.ErrorMessage = ex.Message;
            }

            return response;
        }

        public GetCampaignInfoResponse GetCampaignInfo(string campaignCode)
        {
            GetCampaignInfoResponse response = new GetCampaignInfoResponse();

            try
            {
                if (campaignCode == null || campaignCode != CampaignOnSystem.CampaignCode)
                {
                    throw new BusinessException(InvalidEnteredValuesErrorMessage, 2);
                }
                else
                {
                    response.IsFinished = CampaignOnSystem.IsFinished;
                    response.TargetSalesCount = CampaignOnSystem.TargetSalesCount;
                    response.TotalSales = CampaignOnSystem.TotalOrderQuantitiy;
                    response.Turnover = Math.Round(CampaignOnSystem.Turnover, 2);
                    response.AvarageItemPrice = CampaignOnSystem.Turnover == 0 ? "-" : Math.Round(CampaignOnSystem.Turnover / CampaignOnSystem.TotalOrderQuantitiy, 2).ToString();
                }
            }
            catch (BusinessException ex)
            {
                response.IsError = true;
                response.ErrorMessage = ex.Message;
                response.ErrorCode = ex.ErrorCode;
            }
            catch (Exception ex)
            {
                response.IsError = true;
                response.ErrorMessage = ex.Message;
            }

            return response;
        }

        public IncreaseTimeResponse IncreaseTime(int hour)
        {
            IncreaseTimeResponse response = new IncreaseTimeResponse();

            try
            {
                if (CampaignOnSystem.CampaignCode != null && !CampaignOnSystem.IsFinished)
                {
                    if (hour <= 0)
                    {
                        throw new BusinessException(InvalidEnteredValuesErrorMessage, 2);

                    }
                    else if (CampaignOnSystem.CurrentHour + hour > CampaignOnSystem.Duration)
                    {
                        CampaignOnSystem.CurrentProductPrice = CampaignOnSystem.InitialProductPrice;
                        CampaignOnSystem.CurrentHour = CampaignOnSystem.CurrentHour + hour;
                        response.CurrentTime = CampaignOnSystem.CurrentHour;
                        CampaignOnSystem.IsFinished = true;

                        throw new BusinessException(string.Format(CampaignExpiredErrorMessage + ", Time is {0}", CampaignOnSystem.CurrentHour), 1);
                    }
                    else
                    {
                        CampaignOnSystem.CurrentHour = CampaignOnSystem.CurrentHour + hour;
                        double rateToBeApplied = CampaignOnSystem.RateTobeIncreasedPerHour * CampaignOnSystem.CurrentHour;
                        CampaignOnSystem.CurrentProductPrice = CampaignOnSystem.InitialProductPrice - (CampaignOnSystem.InitialProductPrice * (decimal)(rateToBeApplied / 100));
                        response.CurrentTime = CampaignOnSystem.CurrentHour;
                    }
                }
            }
            catch (BusinessException ex)
            {
                response.IsError = true;
                response.ErrorMessage = ex.Message;
                response.ErrorCode = 1;
            }
            catch (Exception ex)
            {
                response.IsError = true;
                response.ErrorMessage = ex.Message;
            }

            return response;
        }

        public BaseResponse CleanCampaignOnSystem()
        {
            BaseResponse response = new BaseResponse();

            try
            {
                CampaignOnSystem.CampaignCode = default(string);
                CampaignOnSystem.CurrentHour = default(int);
                CampaignOnSystem.CurrentProductPrice = default(decimal);
                CampaignOnSystem.Duration = default(int);
                CampaignOnSystem.InitialProductPrice= default(decimal);
                CampaignOnSystem.IsFinished = true;
                CampaignOnSystem.PriceManipulationLimit = default(double);
                CampaignOnSystem.ProductCode = default(string);
                CampaignOnSystem.TargetSalesCount= default(long);
                CampaignOnSystem.TotalOrderQuantitiy = default(long);
                CampaignOnSystem.TotalProductStock= default(long);
                CampaignOnSystem.Turnover= default(decimal);
            }
            catch (Exception ex)
            {
                response.IsError = true;
                response.ErrorMessage = ex.Message;
            }

            return response;
        }
    }
}
