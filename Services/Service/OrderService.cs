using Entity.Entities.BaseModel;
using Entity.Entities.Request;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class OrderService : IOrderService
    {
        public static Campaign CampaignOnSystem;
        public static readonly string InvalidEnteredValuesErrorMessage = "Entered values ​​are not valid";
        public static readonly string TargetNumberOfSalesReachedErrorMessage = "Target number of sales reached";
        public static readonly string InsufficientstockErrorMessage = "Insufficient stock";

        public OrderService()
        {
            CampaignOnSystem = Campaign.ActiveInstance;
        }

        public BaseResponse CreateOrder(CreateOrderRequest order)
        {
            BaseResponse response = new BaseResponse();

            try
            {
               if(order.Quantity <= CampaignOnSystem.CurrentStock)
                {
                    if (CampaignOnSystem.CampaignCode!=null && (CampaignOnSystem.TotalOrderQuantitiy == CampaignOnSystem.TargetSalesCount))
                    {
                        CampaignOnSystem.IsFinished = true;
                        CampaignOnSystem.CurrentProductPrice = CampaignOnSystem.InitialProductPrice;

                        throw new BusinessException(TargetNumberOfSalesReachedErrorMessage, 1);
                    }
                    else if (order == null || order.Quantity <= 0 || order.ProductCode != CampaignOnSystem.ProductCode)
                    {
                        throw new BusinessException(InvalidEnteredValuesErrorMessage, 2);
                    }
                    else
                    {
                        if (CampaignOnSystem.CampaignCode == null || CampaignOnSystem.IsFinished)
                        {
                            CampaignOnSystem.TotalProductStock -= order.Quantity;
                        }
                        else
                        {
                            CampaignOnSystem.TotalOrderQuantitiy += order.Quantity;
                            CampaignOnSystem.TotalProductStock -= order.Quantity;
                            CampaignOnSystem.Turnover += order.Quantity * CampaignOnSystem.CurrentProductPrice;
                        }
                    }
                }
                else
                {
                   throw new BusinessException(InsufficientstockErrorMessage, 1);
                }
            }
            catch(BusinessException ex)
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

    }
}
