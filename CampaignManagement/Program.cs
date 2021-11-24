
using System;
using CampaignManagement.Operation;
using CampaignManagement.Utilitiy;

namespace Assignment
{
    class Program
    {
        static void Main(string[] args)
        {
            new ScenarioHandler(new CampaignOperation(), new OrderOperation(), new ProductOperation()).ReadScenario();

            Console.ReadLine();
        }

    }
}
