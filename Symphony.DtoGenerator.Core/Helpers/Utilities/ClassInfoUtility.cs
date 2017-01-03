using System;
using System.Collections.Generic;
using System.Linq;
using Deloitte.Symphony.DtoGeneration.Core.Helpers.Dto;
using Deloitte.Symphony.DtoGeneration.Core.Helpers.Extensions;
using Deloitte.Symphony.DtoGeneration.Core.Models;

namespace Deloitte.Symphony.DtoGeneration.Core.Helpers.Utilities
{
    /// <summary>   The class information utility.</summary>
    public static class ClassInfoUtility
    {
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   A Type extension method that query if 'type' is class excluded.</summary>
        /// <param name="type">             The type to act on. </param>
        /// <param name="results">          The results. </param>
        /// <param name="jsonConfigDto">    The JSON configuration dto. </param>
        /// <returns>   True if class excluded, false if not.</returns>
        ///-------------------------------------------------------------------------------------------------
        public static bool IsClassExcluded(this Type type, ICollection<GenResultDto> results, JsonConfigDto jsonConfigDto)
        {
            //if there are no exclusions, return false
            if (jsonConfigDto.Exclusions.None().GetValueOrDefault(true)) return false;

            //Not doing this, a class may be a partial
            //if the collection contains the type, return true
            //if (results.Any(z => z.BaseType == type)) return true;

            //if it is explicitly in the exclusion list, return true
            if (type.FullName.IsClassExplicitExclusion(jsonConfigDto.Exclusions)) return true;

            //if its base class is in the exclusion list, and set to remove derived, return true
            return type.IsImplicitClassExclusion(jsonConfigDto.Exclusions, false);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>A Type extension method that query if 'type' is implicit class exclusion.</summary>
        /// <param name="type">             The type to act on. </param>
        /// <param name="exclusions">       The exclusions. </param>
        /// <param name="shouldExclude">    True if should exclude. </param>
        /// <returns>   True if implicit class exclusion, false if not.</returns>
        ///-------------------------------------------------------------------------------------------------
        private static bool IsImplicitClassExclusion(this Type type, ICollection<ExcludedDto> exclusions, bool shouldExclude)
        {
            //recurse to system.object
            if (type.BaseType != null)
                shouldExclude = type.BaseType.IsImplicitClassExclusion(exclusions, shouldExclude);

            //return if not valid or if there are any base exclusions with excluded dervied equals true 
            return shouldExclude || exclusions.Any(
                       z =>
                           z.ClassFullName.Equals(type.FullName, StringComparison.OrdinalIgnoreCase) &&
                           z.IncludeDerivedClasses.GetValueOrDefault(true) &&
                           z.HasNoCollections());
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>A string extension method that query if 'className' is class explicit exclusion.</summary>
        /// <param name="className">    The className to act on. </param>
        /// <param name="exclusions">   The exclusions. </param>
        /// <returns>   True if class explicit exclusion, false if not.</returns>
        ///-------------------------------------------------------------------------------------------------
        private static bool IsClassExplicitExclusion(this string className, IEnumerable<ExcludedDto> exclusions)
        {
            return exclusions.Any(
                z =>
                    z.ClassFullName.Equals(className, StringComparison.OrdinalIgnoreCase) && z.HasNoCollections());
        }
    }
}