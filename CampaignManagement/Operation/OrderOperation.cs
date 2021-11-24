using CampaignManagement.Interface;
using CampaignManagement.Utilitiy;
using Entity.Entities.BaseModel;
using Entity.Entities.Request;
using System;

namespace CampaignManagement.Operation
{
    public class OrderOperation: IOrderOperation
    {
        public static readonly string PostMethod = "POST";
        public ApiCaller ApiCaller { get; set; }

        public OrderOperation()
        {
            ApiCaller = new ApiCaller();
        }

        public string CreateOrder(string[] command)
        {
            try
            {
                string resultMessage = string.Empty;

                CreateOrderCommandValidation(command);

                CreateOrderRequest order = CreateOrderRequestMap(command);

                var result = ApiCaller.Call<BaseResponse>(PostMethod, MessageHandler.GetBaseUrl() + "api/order/create-order", order);

                if (!result.IsError)
                {
                    resultMessage = string.Format("Order created; product {0}, quantity {1}", order.ProductCode, order.Quantity);

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

        private static void CreateOrderCommandValidation(string[] command)
        {
            const int createOrderCommandLength = 3;

            if (command.Length != createOrderCommandLength || !long.TryParse(command[2], out long z))
            {
                throw new Exception(MessageHandler.GetIncorrectCommandErrorMessage());
            }
        }

        private static CreateOrderRequest CreateOrderRequestMap(string[] command)
        {
            return new CreateOrderRequest
            {
                ProductCode = command[1],
                Quantity = Convert.ToInt64(command[2])
            };
        }
    }
}
