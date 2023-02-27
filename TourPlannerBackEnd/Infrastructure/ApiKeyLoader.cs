namespace TourPlannerBackEnd.Infrastructure
{
    public class ApiKeyLoader
    {
        // enter your api key location here
        private readonly string[] ApiKeyPaths = new string[] {
            @"C:\Studium\MapQuestApiKey.txt",
            @"D:\Studium\Sommersemester 2023\SWEN2\Data\MapQuestApiKey.txt"
        };

        public string Load()
        {
            string key;
            foreach(string path in ApiKeyPaths)
            {
                key = TryLoadApiKeyFrom(path);
                if(string.IsNullOrEmpty(key) == false)
                {
                    return key;
                }
            }

            throw new Exception("The Api key was not found!");
        }

        private string TryLoadApiKeyFrom(string path)
        {
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    return reader.ReadToEnd();
                }
            }
            catch
            {
                // not found or other exception
                return null;
            }
        }
    }
}
