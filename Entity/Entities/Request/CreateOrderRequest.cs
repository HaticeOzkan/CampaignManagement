using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities.Request
{
    [Serializable]
    public class CreateOrderRequest
    {
        public string ProductCode { get; set; }
        public long Quantity { get; set; }
    }
}
