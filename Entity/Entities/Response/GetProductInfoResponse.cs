using Entity.Entities.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities.Response
{
    [Serializable]
    public class GetProductInfoResponse : BaseResponse
    {
        public decimal Price { get; set; }
        public long Stock { get; set; }
    }
}
