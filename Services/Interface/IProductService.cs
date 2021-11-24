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
    public interface IProductService
    {
        BaseResponse CreateProduct(CreateProductRequest createProductRequest);
        GetProductInfoResponse GetProductInfo(string productCode);
    }
}
