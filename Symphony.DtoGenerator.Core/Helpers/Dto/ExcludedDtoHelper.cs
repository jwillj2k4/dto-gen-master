using System;
using Deloitte.Symphony.DtoGeneration.Core.Helpers.Extensions;
using Deloitte.Symphony.DtoGeneration.Core.Models;

namespace Deloitte.Symphony.DtoGeneration.Core.Helpers.Dto
{
    public static class ExcludedDtoHelper
    {
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Query if 'excludedDto' has no collections.</summary>
        /// <param name="excludedDto">  The excluded dto. </param>
        /// <returns>   True if no collections, false if not.</returns>
        ///-------------------------------------------------------------------------------------------------
        public static bool HasNoCollections(this ExcludedDto excludedDto)
        {
            return excludedDto.MethodNames.None().GetValueOrDefault(true) &&
                   excludedDto.PropertyNames.None().GetValueOrDefault(true) &&
                   excludedDto.FieldNames.None().GetValueOrDefault(true);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   An ExcludedDto extension method that query if 'dto' is root aggregate.</summary>
        /// <param name="dto">  The dto to act on. </param>
        /// <returns>   True if root aggregate, false if not.</returns>
        ///-------------------------------------------------------------------------------------------------
        public static bool IsRootAggregate(this ExcludedDto dto)
        {
            return dto.ClassFullName.Contains("rootaggregate", StringComparison.CurrentCultureIgnoreCase);
        }
    }
}