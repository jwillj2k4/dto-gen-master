using System.Reflection;
using Deloitte.Symphony.DtoGeneration.Core.Models;

namespace Deloitte.Symphony.DtoGeneration.Core.Interfaces
{
    /// <summary>   Interface for assembly loader.</summary>
    public interface IAssemblyLoaderService
    {
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Loads an assembly.</summary>
        /// <param name="configJsonConfigDto">  The configuration JSON configuration dto. </param>
        /// <returns>   The assembly.</returns>
        ///-------------------------------------------------------------------------------------------------
        Assembly LoadAssembly(JsonConfigDto configJsonConfigDto);
    }
}