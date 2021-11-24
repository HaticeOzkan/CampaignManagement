using CampaignManagement.Utilitiy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignManagement.Interface
{
    public interface IApiCaller
    {
        T Caller<T>(string methodType, string uri, object postData);
    }
}
