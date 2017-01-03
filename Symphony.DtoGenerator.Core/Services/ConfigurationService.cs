using System.IO;
using System.Linq;
using Deloitte.Symphony.DtoGeneration.Core.Helpers.Dto;
using Deloitte.Symphony.DtoGeneration.Core.Interfaces;
using Deloitte.Symphony.DtoGeneration.Core.Models;
using Newtonsoft.Json;

namespace Deloitte.Symphony.DtoGeneration.Core.Services
{
    /// <summary>   A configuration service.</summary>
    public class ConfigurationService : IConfigurationService
    {
        /// <summary>   Full pathname of the configuration file.</summary>
        private readonly string _configPath;

        /// <summary>   Default constructor.</summary>
        public ConfigurationService() : this(GetDefaultConfigPath()) { }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Constructor.</summary>
        /// <param name="configPath">   Full pathname of the configuration file. </param>
        ///-------------------------------------------------------------------------------------------------

        public ConfigurationService(string configPath)
        {
            _configPath = configPath;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Creates a object based on the Json config file.</summary>
        /// <returns>   The configurationService file.</returns>
        ///-------------------------------------------------------------------------------------------------
        public JsonConfigDto GetConfigFile()
        {
            var configFile = new FileInfo(_configPath);

            var result = JsonConvert.DeserializeObject<JsonConfigDto>(File.ReadAllText(configFile.FullName));

            return result;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Removes the bad entries described by config.</summary>
        /// <param name="config">   The configuration. </param>
        /// <returns>   A JsonConfigDto.</returns>
        ///-------------------------------------------------------------------------------------------------
        public JsonConfigDto RemoveNonAggregatesFromConfig(JsonConfigDto config)
        {
            var result = config;
            var excludeList = config.Exclusions.Where(z =>
            {
                //if it is not a root aggregate and attempting to remove the class, do not include
                var toExclude = !z.IsRootAggregate() && z.HasNoCollections();

                return toExclude;
            });

            result.Exclusions = config.Exclusions.Except(excludeList).ToList();

            return result;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets default configurationService path.</summary>
        /// <returns>   The default path using the project root.</returns>
        ///-------------------------------------------------------------------------------------------------
        private static string GetDefaultConfigPath()
        {

            return new FileInfo(Path.GetFullPath("Symphony.DtoGen.Config.json")).FullName;

            //var currentDir = Environment.CurrentDirectory;

            //return new FileInfo(currentDir + @"\Symphony.DtoGen.Config.json").FullName;
        }
    }
}