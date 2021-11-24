using CampaignManagement.Interface;
using CampaignManagement.Utilitiy;
using Entity.Entities.BaseModel;
using Entity.Entities.Request;
using Entity.Entities.Response;
using System;

namespace CampaignManagement.Operation
{
    public class ProductOperation: IProductOperation
    {
        public static readonly string GetMethod = "GET";
        public static readonly string PostMethod = "POST";
        public ApiCaller ApiCaller { get; set; }

        public ProductOperation()
        {
            ApiCaller = new ApiCaller();
        }

        public string CreateProduct(string[] command)
        {
            try
            {
                string resultMessage = string.Empty;

                CreateProductCommandValidation(command);
                CreateProductRequest product = CreateProductRequestMap(command);

                var result = ApiCaller.Call<BaseResponse>(PostMethod, MessageHandler.GetBaseUrl() + "api/product/create-product", product);

                if (!result.IsError)
                {
                    resultMessage = string.Format("Product created; code {0}, price {1}, stock {2}", product.ProductCode, product.Price, product.Stock);

                    return resultMessage;
                }
                else
                {
                    resultMessage= result.ErrorCode > 0 ? result.ErrorMessage : MessageHandler.GetGeneralErrorMessage();

                    throw new Exception(resultMessage);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
           
        }

        public string GetProductInfo(string[] command)
        {
            try
            {
                string resultMessage = string.Empty;
                GetProductInfoCommandValidation(command);
                string productCode = command[1];

                var result = ApiCaller.Call<GetProductInfoResponse>(GetMethod, string.Format(MessageHandler.GetBaseUrl() + "api/product/product-info/{0}", productCode), null);

                if (!result.IsError)
                {
                    resultMessage = string.Format("Product {0} info; price {1}, stock {2}", productCode, result.Price, result.Stock);

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

        private static void CreateProductCommandValidation(string[] command)
        {
            const int createProductCommandLength = 4;

            if (command.Length != createProductCommandLength || !decimal.TryParse(command[2], out decimal x) || !long.TryParse(command[3], out long y))
            {
                throw new Exception(MessageHandler.GetIncorrectCommandErrorMessage());
            }
        }

        private static void GetProductInfoCommandValidation(string[] command)
        {
            const int getProductInfoCommandLength = 2;

            if (command.Length != getProductInfoCommandLength)
            {
                throw new Exception(MessageHandler.GetIncorrectCommandErrorMessage());
            }
        }

        private static CreateProductRequest CreateProductRequestMap(string[] command)
        {
            return new CreateProductRequest
            {
                ProductCode = command[1],
                Price = Convert.ToDecimal(command[2]),
                Stock = Convert.ToInt64(command[3])
            };
        }
    }
}
