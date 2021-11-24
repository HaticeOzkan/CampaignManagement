
namespace CampaignManagement.Utilitiy
{
    public class MessageHandler
    {
        public static readonly string GeneralErrorMessage = "An error occurred while processing your transaction. Please try again later";
        public static readonly string IncorrectCommandErrorMessage = "The command entered is incorrect. Please try again by checking the information";

        public static string GetGeneralErrorMessage()
        {
            return GeneralErrorMessage;
        }

        public static string GetIncorrectCommandErrorMessage()
        {
            return IncorrectCommandErrorMessage;
        }

        public static string GetBaseUrl()
        {
            return System.Configuration.ConfigurationSettings.AppSettings["WEPAPI_HOST"];
        }
    }
}
