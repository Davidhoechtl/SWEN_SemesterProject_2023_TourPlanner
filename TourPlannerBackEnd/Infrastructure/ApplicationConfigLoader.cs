using Newtonsoft.Json;
using System.Reflection;
using TourPlanner.DataTransferObjects.Models;

namespace TourPlannerBackEnd.Infrastructure
{
    public class ApplicationConfigLoader
    {
        public ApplicationConfiguration Load()
        {
            ApplicationConfiguration config;
            string appConfigFileLocation = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "AppConfiguration.json");
            if(File.Exists(appConfigFileLocation))
            {
                using(StreamReader reader = new StreamReader(appConfigFileLocation))
                {
                    string json = reader.ReadToEnd();
                    config = JsonConvert.DeserializeObject<ApplicationConfiguration>(json);
                    return config;
                }
            }
            else
            {
                throw new FileNotFoundException(appConfigFileLocation);
            }
        }
    }
}
