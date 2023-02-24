using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlannerBackEnd.Infrastructure
{
    public class ApiKeyLoader
    {
        private const string ApiKeyPath = @"C:\Studium\MapQuestApiKey.txt";
        public string Load()
        {
            using (StreamReader reader = new StreamReader(ApiKeyPath))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
