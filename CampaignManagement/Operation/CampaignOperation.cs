using CampaignManagement.Interface;
using CampaignManagement.Utilitiy;
using Entity.Entities.BaseModel;
using Entity.Entities.Request;
using Entity.Entities.Response;
using System;
using System.Text;

namespace CampaignManagement.Operation
{
    public class CampaignOperation: ICampaignOperation
    {
        public static readonly string GetMethod = "GET";
        public static readonly string PostMethod = "POST";
        public ApiCaller ApiCaller { get; set; }

        public CampaignOperation()
        {
            ApiCaller = new ApiCaller();
        }

        public string CreateCampaign(string[] command)
        {
            try
            {
                string resultMessage = string.Empty;

                CreateCampaignCommandValidation(command);

                CreateCampaignRequest campaign = CreateCampaignRequestMap(command);

                var result = ApiCaller.Call<BaseResponse>(PostMethod, MessageHandler.GetBaseUrl() + "api/campaign/create-campaign", campaign);

                if (!result.IsError)
                {
                    resultMessage = string.Format("Campaign created; name {0}, product {1}, duration {2}, limit {3}, target sales count {4}",
                        campaign.CampaignCode, campaign.ProductCode, campaign.Duration, campaign.PriceManipulationLimit, campaign.TargetSalesCount);

                    return resultMessage;
                }
                else
                {
                    resultMessage = result.ErrorCode > 0 ? result.ErrorMessage : MessageHandler.GetGeneralErrorMessage();

                    throw new Exception(resultMessage);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public void CleanSystemScenario()
        {
            try
            {
                var result = ApiCaller.Call<BaseResponse>(GetMethod, MessageHandler.GetBaseUrl() + "api/campaign/clean-campaign-system", null);

                if (result.IsError)
                {
                    throw new Exception(MessageHandler.GetGeneralErrorMessage());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string IncreaseTime(string[] command)
        {
            try
            {
                string resultMessage = string.Empty;

                IncreaseCommandValidation(command);
                string hour = command[1];

                var result = ApiCaller.Call<IncreaseTimeResponse>(GetMethod, string.Format(MessageHandler.GetBaseUrl() + "api/campaign/increase-time/{0}", hour), null);

                if (!result.IsError)
                {
                    resultMessage = result.CurrentTime == 0 ? "Campaign has not been started yet." : string.Format("Time is {0}", result.CurrentTime);

                    return resultMessage;
                }
                else
                {
                    resultMessage = result.ErrorCode > 0 ? result.ErrorMessage : MessageHandler.GetGeneralErrorMessage();

                    throw new Exception(resultMessage);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string GetCampaignInfo(string[] command)
        {
            try
            {
                StringBuilder resultMessage = new StringBuilder();

                GetCampaignInfoCommandValidation(command);
                string campaignCode = command[1];

                var result = ApiCaller.Call<GetCampaignInfoResponse>(GetMethod, string.Format(MessageHandler.GetBaseUrl() + "api/campaign/campaign-info/{0}", campaignCode), null);

                if (!result.IsError)
                {
                    resultMessage.Append(string.Format("Campaign {0} info; ", campaignCode));
                    resultMessage.Append(result.IsFinished ? "Status Inactive, " : "Status Active, ");
                    resultMessage.Append(string.Format("Target Sales {0}, Total Sales {1}, Turnover {2}, Average Item Price {3}", result.TargetSalesCount, result.TotalSales, result.Turnover, result.AvarageItemPrice));

                    return resultMessage.ToString();
                }
                else
                {
                    resultMessage.Append(result.ErrorCode > 0 ? result.ErrorMessage : MessageHandler.GetGeneralErrorMessage());

                    throw new Exception(resultMessage.ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static void CreateCampaignCommandValidation(string[] command)
        {
            const int createCampaignCommandLength = 6;

            if (command.Length != createCampaignCommandLength || !int.TryParse(command[3], out int x) || !double.TryParse(command[4], out double y) || !long.TryParse(command[5], out long z))
            {
                throw new Exception(MessageHandler.GetIncorrectCommandErrorMessage());
            }
        }

        private static void IncreaseCommandValidation(string[] command)
        {
            const int increaseTimeCommandLength = 2;

            if (command.Length != increaseTimeCommandLength || !int.TryParse(command[1], out int x))
            {
                throw new Exception(MessageHandler.GetIncorrectCommandErrorMessage());
            }

        }

        private static void GetCampaignInfoCommandValidation(string[] command)
        {
            const int getCampaignInfoCommandLength = 2;

            if (command.Length != getCampaignInfoCommandLength)
            {
                throw new Exception(MessageHandler.GetIncorrectCommandErrorMessage());
            }
        }

        private static CreateCampaignRequest CreateCampaignRequestMap(string[] command)
        {
            return new CreateCampaignRequest
            {
                CampaignCode = command[1],
                ProductCode = command[2],
                Duration = Convert.ToInt32(command[3]),
                PriceManipulationLimit = Convert.ToInt32(command[4]),
                TargetSalesCount = Convert.ToInt64(command[5])
            };
        }
    }
}
