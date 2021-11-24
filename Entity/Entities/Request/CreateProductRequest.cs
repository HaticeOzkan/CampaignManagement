using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities.Request
{
    [Serializable]
    public class CreateProductRequest
    {
        public string ProductCode { get; set; }
        public decimal Price { get; set; }
        public long Stock { get; set; }
    }
}
