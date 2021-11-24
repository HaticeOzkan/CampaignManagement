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
    public class ProductService : IProductService
    {
        public static Campaign CampaignOnSystem;
        public static readonly string InvalidEnteredValuesErrorMessage = "Entered values ​​are not valid";
        public ProductService()
        {
            CampaignOnSystem = Campaign.ActiveInstance;
        }

        public BaseResponse CreateProduct(CreateProductRequest product)
        {
            BaseResponse response = new BaseResponse();

            try
            {
                if (product == null || product.Price <= 0 || product.Stock <= 0 || product.ProductCode == null)
                {
                    throw new BusinessException(InvalidEnteredValuesErrorMessage, 2);
                }
                else
                {
                    CampaignOnSystem.CurrentProductPrice = product.Price;
                    CampaignOnSystem.InitialProductPrice = product.Price;
                    CampaignOnSystem.ProductCode = product.ProductCode;
                    CampaignOnSystem.TotalProductStock = product.Stock;
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

        public GetProductInfoResponse GetProductInfo(string productCode)
        {
            GetProductInfoResponse response = new GetProductInfoResponse();

            try
            {
                if (string.IsNullOrEmpty(productCode) || productCode != CampaignOnSystem.ProductCode)
                {
                    throw new BusinessException(InvalidEnteredValuesErrorMessage, 2);
                }
                else
                {
                    response.Price =Math.Round(CampaignOnSystem.CurrentProductPrice,2);
                    response.Stock = CampaignOnSystem.TotalProductStock;
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
    }
}
