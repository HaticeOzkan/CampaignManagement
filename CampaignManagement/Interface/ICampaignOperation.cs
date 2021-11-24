
namespace CampaignManagement.Interface
{
    public interface ICampaignOperation
    {
        string CreateCampaign(string[] command);
        string IncreaseTime(string[] command);
        string GetCampaignInfo(string[] command);
        void CleanSystemScenario();
    }
}
