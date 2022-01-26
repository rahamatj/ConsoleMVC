
using Microsoft.Extensions.Configuration;

namespace ConsoleMVC.Configuration
{
    public class AppConfiguration : IAppConfiguration
    {
        IConfiguration _configuration;
        string _jsonFile;

        public AppConfiguration(IConfigurationBuilder configurationBuilder, string jsonFile)
        {
            _jsonFile = jsonFile;
            _configuration = configurationBuilder
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile(_jsonFile, optional: true, reloadOnChange: true)
                                .Build();
        }

        T GetSectionObject<T>()
        {
            var section = typeof(T).Name;
            var sectionObj = _configuration.GetSection(section).Get<T>();

            if (sectionObj == null)
                throw new Exception($"{section} not found in {_jsonFile}.");

            return sectionObj;
        }

        public T Get<T>() where T : ISection
        {
            try
            {
                return GetSectionObject<T>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return default(T);
            }
        }
    }
}
