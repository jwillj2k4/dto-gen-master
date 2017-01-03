using Deloitte.Symphony.DtoGeneration.Core.Models;

namespace Deloitte.Symphony.DtoGeneration.Core.Interfaces
{
    /// <summary>   Interface for configuration service.</summary>
    public interface IConfigurationService
    {
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets configuration file.</summary>
        /// <returns>   The configuration file.</returns>
        ///-------------------------------------------------------------------------------------------------
        JsonConfigDto GetConfigFile();

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Removes non aggregate class exclusions</summary>
        /// <param name="config">   The configuration. </param>
        /// <returns>   A JsonConfigDto.</returns>
        ///-------------------------------------------------------------------------------------------------
        JsonConfigDto RemoveNonAggregatesFromConfig(JsonConfigDto config);
    }
}
