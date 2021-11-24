using System.Net;

namespace CampaignManagement.Interface
{
    public interface IHttpWebRequestFactory
    {
        HttpWebRequest Create(string uri);
    }
}
