using System.Reflection;
using Deloitte.Symphony.DtoGeneration.Core.Models;

namespace Deloitte.Symphony.DtoGeneration.Core.Interfaces
{
    /// <summary>   Interface for entity template service.</summary>
    public interface IDtoGenerationService
    {
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Enumerates generate in this collection.</summary>
        /// <param name="dll">              The DLL. </param>
        /// <param name="jsonConfigDto">    The JSON configuration dto. </param>
        /// <returns>An enumerator that allows foreach to be used to process generate in this collection.</returns>
        ///-------------------------------------------------------------------------------------------------
        GenResult Generate(Assembly dll, JsonConfigDto jsonConfigDto);
    }
}