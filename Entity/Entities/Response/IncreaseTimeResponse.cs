using Entity.Entities.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities.Response
{
    [Serializable]
    public class IncreaseTimeResponse:BaseResponse
    {
        public int CurrentTime { get; set; }
    }
}
