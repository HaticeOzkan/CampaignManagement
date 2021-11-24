using CampaignManagement.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CampaignManagement.Utilitiy
{
    public class ScenarioHandler
    {
        private readonly ICampaignOperation _campaignOperation;
        private readonly IOrderOperation _orderOperation;
        private readonly IProductOperation _productOperation;

        public ScenarioHandler(ICampaignOperation campaignOperation, IOrderOperation orderOperation, IProductOperation productOperation)
        {
            _campaignOperation = campaignOperation;
            _orderOperation = orderOperation;
            _productOperation = productOperation;
        }

        public void ReadScenario()
        {
            string command = string.Empty;
            string resultMessage = string.Empty;

            try
            {
                string name = GetScenarioName();

                using (var reader = new StreamReader(String.Format(@"Scenarios\{0}.txt", name)))
                {
                    while (!reader.EndOfStream)
                    {
                        command = reader.ReadLine();

                        resultMessage = ExecuteCommand(command);

                        Console.WriteLine(resultMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                resultMessage = ex.Message.ToString();
                Console.WriteLine(resultMessage);
            }
            finally
            {
                Console.WriteLine(Environment.NewLine);
                RunFinallyProcesses();
            }
        }

        private void RunFinallyProcesses()
        {
            try
            {
                _campaignOperation.CleanSystemScenario();
                ReadScenario();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public string ExecuteCommand(string command)
        {
            try
            {
                string[] seperatedCommand = command.Trim().Split(null);

                switch (seperatedCommand[0])
                {
                    case "create_product":
                        return _productOperation.CreateProduct(seperatedCommand);
                    case "get_product_info":
                        return _productOperation.GetProductInfo(seperatedCommand);
                    case "create_order":
                        return _orderOperation.CreateOrder(seperatedCommand);
                    case "create_campaign":
                        return _campaignOperation.CreateCampaign(seperatedCommand);
                    case "get_campaign_info":
                        return _campaignOperation.GetCampaignInfo(seperatedCommand);
                    case "increase_time":
                        return _campaignOperation.IncreaseTime(seperatedCommand);
                    default:
                        throw new Exception("InvalidCommand");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        public static string GetScenarioName()
        {

            string text = GetStartText();
            List<string> scenarioNameList = GetScenarioNameList();
            Console.WriteLine(text.ToString());

            string fileName = Console.ReadLine().ToLower();
            bool control = !scenarioNameList.Contains(fileName);

            while (control)
            {
                Console.WriteLine("Scenario is not found. Please enter the scenario that is in the list.");
                fileName = Console.ReadLine().ToLower();
                control = !scenarioNameList.Contains(fileName);
            }

            return fileName;
        }

        private static string GetStartText()
        {
            List<string> scenarioNameList = GetScenarioNameList();
            StringBuilder text = new StringBuilder();
            text.Append("Which scenario would you like to run? Please enter scenario name.");
            text.AppendLine();

            foreach (var scenarioName in scenarioNameList)
            {
                text.Append(scenarioName + " ");
            }

            return text.ToString();
        }

        private static List<string> GetScenarioNameList()
        {
            char delimiterCharSlash = '\\';
            char delimiterCharPoint = '.';
            List<string> files = Directory.GetFiles(@"Scenarios").ToList();
            List<string> scenarioList = new List<string>();
            string[] seperatedFileName;

            foreach (var file in files)
            {
                seperatedFileName = file.Split(delimiterCharSlash);
                seperatedFileName = seperatedFileName[1].Split(delimiterCharPoint);

                scenarioList.Add(seperatedFileName[0].ToLower());
            }

            return scenarioList;
        }
    }
}
