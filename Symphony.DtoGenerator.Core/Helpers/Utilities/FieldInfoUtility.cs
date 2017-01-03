using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Deloitte.Symphony.DtoGeneration.Core.Constants;
using Deloitte.Symphony.DtoGeneration.Core.Helpers.Extensions;
using Deloitte.Symphony.DtoGeneration.Core.Models;

namespace Deloitte.Symphony.DtoGeneration.Core.Helpers.Utilities
{
    /// <summary>   A field information utility.</summary>
    public static class FieldInfoUtility
    {
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   A Type extension method that gets included primitive fields.</summary>
        /// <param name="type">             The type to act on. </param>
        /// <param name="jsonConfigDto">    The JSON configuration dto. </param>
        /// <returns>   The included primitive fields.</returns>
        ///-------------------------------------------------------------------------------------------------
        public static List<FieldInfo> GetIncludedPrimitiveFields(this Type type, JsonConfigDto jsonConfigDto)
        {
            //get all primitives
            return type.GetAllFields().GetPrimitiveFieldTypes().GetIncludedFields(jsonConfigDto, type).ToList();
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   A Type extension method that gets included complex fields.</summary>
        /// <param name="type">             The type to act on. </param>
        /// <param name="jsonConfigDto">    The JSON configuration dto. </param>
        /// <returns>   The included complex fields.</returns>
        ///-------------------------------------------------------------------------------------------------
        public static List<FieldInfo> GetIncludedComplexFields(this Type type, JsonConfigDto jsonConfigDto)
        {
            //get all complex properties that are not on the exlcusions list
            return type.GetAllFields().GetComplexFieldTypes().GetIncludedFields(jsonConfigDto, type).ToList();
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Enumerates create field property information in this collection.</summary>
        /// <param name="list"> The list to act on. </param>
        /// <returns>An enumerator that allows foreach to be used to process create field property
        ///     information in this collection.</returns>
        ///-------------------------------------------------------------------------------------------------
        public static IEnumerable<PropertyInfo> CreateAndGetFieldProperties(this IEnumerable<FieldInfo> list)
        {
            return list.Select(z => new DerivedPropertyInfo(z.Name, z.FieldType));
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets all fields in this collection.</summary>
        /// <param name="type"> The type to act on. </param>
        /// <returns>An enumerator that allows foreach to be used to process all fields in this collection.</returns>
        ///-------------------------------------------------------------------------------------------------
        private static IEnumerable<FieldInfo> GetAllFields(this Type type)
        {
            return type == null ||  type == typeof(object) || type.IsEnum
                ? Enumerable.Empty<FieldInfo>()
                : type.GetFields(BindingFlagConstant.BindFlags).Union(GetAllFields(type.BaseType));
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the primitive field types in this collection.</summary>
        /// <param name="list"> The list to act on. </param>
        /// <returns>An enumerator that allows foreach to be used to process the primitive field types in
        ///     this collection.</returns>
        ///-------------------------------------------------------------------------------------------------
        private static IEnumerable<FieldInfo> GetPrimitiveFieldTypes(this IEnumerable<FieldInfo> list)
        {
            return list.Where(info => info.FieldType.IsSimpleType()).ToList();
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the complex field types in this collection.</summary>
        /// <param name="list"> The list to act on. </param>
        /// <returns>An enumerator that allows foreach to be used to process the complex field types in
        ///     this collection.</returns>
        ///-------------------------------------------------------------------------------------------------
        private static IEnumerable<FieldInfo> GetComplexFieldTypes(this IEnumerable<FieldInfo> list)
        {
            return list.Where(fieldInfo => !fieldInfo.FieldType.IsSimpleType()).ToList();
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the included fields in this collection.</summary>
        /// <param name="list">             The list to act on. </param>
        /// <param name="jsonConfigDto">    The JSON configuration dto. </param>
        /// <param name="type">             The type to act on. </param>
        /// <returns>An enumerator that allows foreach to be used to process the included fields in this
        ///     collection.</returns>
        ///-------------------------------------------------------------------------------------------------
        private static IEnumerable<FieldInfo> GetIncludedFields(this IEnumerable<FieldInfo> list, JsonConfigDto jsonConfigDto, Type type)
        {
            return list
                .Where(
                    info =>
                            //ensure the property is not on the exclusion list for this type
                            !info.Name.IsFieldExcluded(jsonConfigDto, type) &&
                            info.Name.IsValidField())

                .ToList();
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   A string extension method that query if 'fieldName' is field excluded.</summary>
        /// <param name="fieldName">        Name of the field. </param>
        /// <param name="jsonConfigDto">    The JSON configuration dto. </param>
        /// <param name="classType">        Type of the class. </param>
        /// <returns>   True if field excluded, false if not.</returns>
        ///-------------------------------------------------------------------------------------------------
        private static bool IsFieldExcluded(this string fieldName, JsonConfigDto jsonConfigDto, Type classType)
        {
            //if there are no exclusions, return false
            if (jsonConfigDto.Exclusions.None().GetValueOrDefault(true)) return false;

            if (classType.FullName.IsFieldExplicitFieldExclusion(jsonConfigDto.Exclusions, fieldName)) return true;

            //if its base class is in the exclusion list, and property name matches, and set to remove derived, return true
            return classType.IsFieldImplicitFieldExclusion(jsonConfigDto.Exclusions, fieldName, false);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>A string extension method that query if 'className' is field explicit field
        ///     exclusion.</summary>
        /// <param name="className">    The className to act on. </param>
        /// <param name="exclusions">   The exclusions. </param>
        /// <param name="fieldName">    Name of the field. </param>
        /// <returns>   True if field explicit field exclusion, false if not.</returns>
        ///-------------------------------------------------------------------------------------------------
        private static bool IsFieldExplicitFieldExclusion(this string className, IEnumerable<ExcludedDto> exclusions, string fieldName)
        {
            return exclusions.Any(z =>

                    //where class name matches current
                    z.ClassFullName.Equals(className, StringComparison.OrdinalIgnoreCase) &&
                    //where properties match current    
                    z.FieldNames.Any(
                        n => string.Equals(n, fieldName, StringComparison.OrdinalIgnoreCase)));
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>A Type extension method that query if this object is field implicit field
        ///     exclusion.</summary>
        /// <param name="type">             The type to act on. </param>
        /// <param name="exclusions">       The exclusions. </param>
        /// <param name="fieldName">        Name of the field. </param>
        /// <param name="shouldExclude">    True if should exclude. </param>
        /// <returns>   True if field implicit field exclusion, false if not.</returns>
        ///-------------------------------------------------------------------------------------------------
        private static bool IsFieldImplicitFieldExclusion(this Type type, ICollection<ExcludedDto> exclusions, string fieldName, bool shouldExclude)
        {
            //recurse to system.object
            if (type.BaseType != null)
                shouldExclude = type.BaseType.IsFieldImplicitFieldExclusion(exclusions, fieldName, shouldExclude);

            //return if not valid or if there are any base exclusions with excluded dervied equals true 
            return shouldExclude || exclusions.Any(
                       z =>
                           z.ClassFullName.Equals(type.FullName, StringComparison.OrdinalIgnoreCase) &&
                           z.IncludeDerivedClasses.GetValueOrDefault(true) &&
                           //where properties match current    
                           z.FieldNames != null && z.FieldNames.Any(
                               name => string.Equals(name, fieldName, StringComparison.OrdinalIgnoreCase)));
        }
    }
}