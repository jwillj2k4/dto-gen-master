using System.IO;
using System.Reflection;
using Deloitte.Symphony.DtoGeneration.Core.Interfaces;
using Deloitte.Symphony.DtoGeneration.Core.Models;

namespace Deloitte.Symphony.DtoGeneration.Core.Services
{
    /// <summary>   An assembly loader.</summary>
    public class AssemblyLoaderService : IAssemblyLoaderService
    {
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Loads an assembly.</summary>
        /// <param name="configJsonConfigDto">  The configuration JSON configuration dto. </param>
        /// <returns>   The assembly.</returns>
        ///-------------------------------------------------------------------------------------------------
        public Assembly LoadAssembly(JsonConfigDto configJsonConfigDto)
        {
            var tmpFile = new FileInfo(configJsonConfigDto.DllInputPath);
            return Assembly.Load(File.ReadAllBytes(tmpFile.FullName));
        }
    }
}