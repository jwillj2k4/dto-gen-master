using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Deloitte.Symphony.DtoGeneration.Core.Constants;
using Deloitte.Symphony.DtoGeneration.Core.Helpers.Extensions;
using Deloitte.Symphony.DtoGeneration.Core.Models;

namespace Deloitte.Symphony.DtoGeneration.Core.Helpers.Utilities
{
    /// <summary>   A property information utility.</summary>
    public static class PropertyInfoUtility
    {
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   A Type extension method that gets included primitive properties.</summary>
        /// <param name="type">             The type to act on. </param>
        /// <param name="jsonConfigDto">    The JSON configuration dto. </param>
        /// <returns>   The included primitive properties.</returns>
        ///-------------------------------------------------------------------------------------------------
        public static List<PropertyInfo> GetIncludedPrimitiveProperties(this Type type, JsonConfigDto jsonConfigDto)
        {
            //get all primitives
            return type.GetAllProperties().GetPrimitivePropertyTypes().GetIncludedProperties(jsonConfigDto, type).ToList();
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   A Type extension method that gets included complex properties.</summary>
        /// <param name="type">             The type to act on. </param>
        /// <param name="jsonConfigDto">    The JSON configuration dto. </param>
        /// <returns>   The included complex properties.</returns>
        ///-------------------------------------------------------------------------------------------------
        public static List<PropertyInfo> GetIncludedComplexProperties(this Type type, JsonConfigDto jsonConfigDto)
        {
            //get all complex properties that are not on the exlcusions list
            return type.GetAllProperties().GetComplexPropertyTypes().GetIncludedProperties(jsonConfigDto, type).ToList();
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets all properties in this collection.</summary>
        /// <param name="type"> The type to act on. </param>
        /// <returns>An enumerator that allows foreach to be used to process all properties in this
        ///     collection.</returns>
        ///-------------------------------------------------------------------------------------------------
        private static IEnumerable<PropertyInfo> GetAllProperties(this Type type)
        {
            //get all properties recursively up the tree until system.object
            return type == null || type == typeof(object) || type.IsEnum
                ? Enumerable.Empty<PropertyInfo>()
                : type.GetProperties(BindingFlagConstant.BindFlags).Union(GetAllProperties(type.BaseType));
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the primitive property types in this collection.</summary>
        /// <param name="list"> The list to act on. </param>
        /// <returns>An enumerator that allows foreach to be used to process the primitive property types
        ///     in this collection.</returns>
        ///-------------------------------------------------------------------------------------------------
        private static IEnumerable<PropertyInfo> GetPrimitivePropertyTypes(this IEnumerable<PropertyInfo> list)
        {
            return list.Where(info => info.PropertyType.IsSimpleType()).ToList();
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the complex property types in this collection.</summary>
        /// <param name="list"> The list to act on. </param>
        /// <returns>An enumerator that allows foreach to be used to process the complex property types in
        ///     this collection.</returns>
        ///-------------------------------------------------------------------------------------------------
        private static IEnumerable<PropertyInfo> GetComplexPropertyTypes(this IEnumerable<PropertyInfo> list)
        {
            return list.Where(propInfo => !propInfo.PropertyType.IsSimpleType()).ToList();
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the included properties in this collection.</summary>
        /// <param name="list">             The list to act on. </param>
        /// <param name="jsonConfigDto">    The JSON configuration dto. </param>
        /// <param name="type">             The type to act on. </param>
        /// <returns>An enumerator that allows foreach to be used to process the included properties in
        ///     this collection.</returns>
        ///------------------------------------
        /// -------------------------------------------------------------
        private static IEnumerable<PropertyInfo> GetIncludedProperties(this IEnumerable<PropertyInfo> list, JsonConfigDto jsonConfigDto, Type type)
        {
            return list
                .Where(
                    info =>
                            //ensure the property is not on the exclusion list for this type
                            !info.Name.IsPropertyExcluded(jsonConfigDto, type) &&
                            info.Name.IsValidField())

                .ToList();
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>A string extension method that query if 'propertyName' is property excluded.</summary>
        /// <param name="propertyName">     Name of the property. </param>
        /// <param name="jsonConfigDto">    The JSON configuration dto. </param>
        /// <param name="classType">        Type of the class. </param>
        /// <returns>   True if property excluded, false if not.</returns>
        ///-------------------------------------------------------------------------------------------------
        private static bool IsPropertyExcluded(this string propertyName, JsonConfigDto jsonConfigDto, Type classType)
        {
            //if there are no exclusions, return false
            if (jsonConfigDto.Exclusions.None().GetValueOrDefault(true)) return false;

            if (classType.FullName.IsPropertyExplicitExclusion(jsonConfigDto.Exclusions, propertyName)) return true;

            //if its base class is in the exclusion list, and property name matches, and set to remove derived, return true
            return classType.IsPropertyImplicitExclusion(jsonConfigDto.Exclusions, propertyName, false);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>A string extension method that query if 'className' is property explicit
        ///     exclusion.</summary>
        /// <param name="className">    The className to act on. </param>
        /// <param name="exclusions">   The exclusions. </param>
        /// <param name="propertyName"> Name of the property. </param>
        /// <returns>   True if property explicit exclusion, false if not.</returns>
        ///-------------------------------------------------------------------------------------------------
        private static bool IsPropertyExplicitExclusion(this string className, IEnumerable<ExcludedDto> exclusions, string propertyName)
        {
            return exclusions.Any(z =>

                //where class name matches current
                    z.ClassFullName.Equals(className, StringComparison.OrdinalIgnoreCase) &&
                    //where properties match current    
                    z.PropertyNames.Any(
                        n => string.Equals(n, propertyName, StringComparison.OrdinalIgnoreCase)));
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>A Type extension method that query if this object is property implicit exclusion.</summary>
        /// <param name="type">             The type to act on. </param>
        /// <param name="exclusions">       The exclusions. </param>
        /// <param name="propertyName">     Name of the property. </param>
        /// <param name="shouldExclude">    True if should exclude. </param>
        /// <returns>   True if property implicit exclusion, false if not.</returns>
        ///-------------------------------------------------------------------------------------------------
        private static bool IsPropertyImplicitExclusion(this Type type, ICollection<ExcludedDto> exclusions, string propertyName, bool shouldExclude)
        {
            //recurse to system.object
            if (type.BaseType != null)
                shouldExclude = type.BaseType.IsPropertyImplicitExclusion(exclusions, propertyName, shouldExclude);

            //return if not valid or if there are any base exclusions with excluded dervied equals true 
            return shouldExclude || exclusions.Any(
                       z =>
                           z.ClassFullName.Equals(type.FullName, StringComparison.OrdinalIgnoreCase) &&
                           z.IncludeDerivedClasses.GetValueOrDefault(true) &&
                           //where properties match current    
                           z.PropertyNames != null && z.PropertyNames.Any(
                               name => String.Equals(name, propertyName, StringComparison.OrdinalIgnoreCase)));
        }

    }
}