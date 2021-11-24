using Entity.Entities.BaseModel;
using Entity.Entities.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IOrderService
    {
        BaseResponse CreateOrder(CreateOrderRequest createProductRequest);
    }
}
