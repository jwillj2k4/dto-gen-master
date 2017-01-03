using System;
using Deloitte.Symphony.DtoGeneration.Core.Models;

namespace Deloitte.Symphony.DtoGeneration.Core.Interfaces
{
    /// <summary>   Interface for result dto service.</summary>
    public interface IResultDtoService
    {
        /// -------------------------------------------------------------------------------------------------
        ///  <summary>   Creates result dto.</summary>
        /// <param name="jsonConfigDto">    The JSON configuration dto. </param>
        /// <param name="type">             The type. </param>
        /// <returns>   The new result dto.</returns>
        /// -------------------------------------------------------------------------------------------------
        GenResultDto CreateResultDto(JsonConfigDto jsonConfigDto, Type type);
    }
}