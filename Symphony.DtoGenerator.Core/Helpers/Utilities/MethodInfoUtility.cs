using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Deloitte.Symphony.DtoGeneration.Core.Constants;
using Deloitte.Symphony.DtoGeneration.Core.Helpers.Extensions;
using Deloitte.Symphony.DtoGeneration.Core.Models;

namespace Deloitte.Symphony.DtoGeneration.Core.Helpers.Utilities
{

    /// <summary>   A method information utility.</summary>
    public static class MethodInfoUtility
    {

        ///-------------------------------------------------------------------------------------------------
        /// <summary>A Type extension method that gets included primitive method return values.</summary>
        /// <param name="type">             The type to act on. </param>
        /// <param name="jsonConfigDto">    The JSON configuration dto. </param>
        /// <returns>   The included primitive method return values.</returns>
        ///-------------------------------------------------------------------------------------------------
        public static List<MethodInfo> GetIncludedPrimitiveMethodReturnValues(this Type type, JsonConfigDto jsonConfigDto)
        {
            //get all primitives
            return type.GetAllMethods().GetPrimitiveMethodReturnTypes().GetIncludedMethods(jsonConfigDto, type).ToList();
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>A Type extension method that gets included complex method return values.</summary>
        /// <param name="type">             The type to act on. </param>
        /// <param name="jsonConfigDto">    The JSON configuration dto. </param>
        /// <returns>   The included complex method return values.</returns>
        ///-------------------------------------------------------------------------------------------------
        public static List<MethodInfo> GetIncludedComplexMethodReturnValues(this Type type, JsonConfigDto jsonConfigDto)
        {
            return type.GetAllMethods().GetComplexMethodReturnTypes().GetIncludedMethods(jsonConfigDto, type).ToList();
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Enumerates create and get method property infos in this collection.</summary>
        /// <param name="list"> The list to act on. </param>
        /// <returns>An enumerator that allows foreach to be used to process create and get method
        ///     property infos in this collection.</returns>
        ///-------------------------------------------------------------------------------------------------
        public static IEnumerable<PropertyInfo> CreateAndGetMethodProperties(this IEnumerable<MethodInfo> list)
        {
            return list.Select(z => new DerivedPropertyInfo(z.Name, z.ReturnType));
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets all methods in this collection. Dont go to system.object</summary>
        /// <param name="type"> The type to act on. </param>
        /// <returns>An enumerator that allows foreach to be used to process all methods in this
        ///     collection.</returns>
        ///-------------------------------------------------------------------------------------------------
        private static IEnumerable<MethodInfo> GetAllMethods(this Type type)
        {
            return  type == null || type == typeof(object) || type.IsEnum
                ? Enumerable.Empty<MethodInfo>()
                : type.GetMethods(BindingFlagConstant.BindFlags).Union(GetAllMethods(type.BaseType));
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the primitive method return types in this collection.</summary>
        /// <param name="list"> The list to act on. </param>
        /// <returns>An enumerator that allows foreach to be used to process the primitive method return
        ///     types in this collection.</returns>
        ///-------------------------------------------------------------------------------------------------
        private static IEnumerable<MethodInfo> GetPrimitiveMethodReturnTypes(this IEnumerable<MethodInfo> list)
        {
            return list.Where(info => info.ReturnType.IsSimpleType()).ToList();
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the complex method return types in this collection.</summary>
        /// <param name="list"> The list to act on. </param>
        /// <returns>An enumerator that allows foreach to be used to process the complex method return
        ///     types in this collection.</returns>
        ///-------------------------------------------------------------------------------------------------
        private static IEnumerable<MethodInfo> GetComplexMethodReturnTypes(this IEnumerable<MethodInfo> list)
        {
            return list.Where(info => !info.ReturnType.IsSimpleType()).ToList();
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the included methods in this collection.</summary>
        /// <param name="list">             The list to act on. </param>
        /// <param name="jsonConfigDto">    The JSON configuration dto. </param>
        /// <param name="type">             The type to act on. </param>
        /// <returns>An enumerator that allows foreach to be used to process the included methods in this
        ///     collection.</returns>
        ///-------------------------------------------------------------------------------------------------
        private static IEnumerable<MethodInfo> GetIncludedMethods(this IEnumerable<MethodInfo> list, JsonConfigDto jsonConfigDto, Type type)
        {
            return list
                .Where(
                    info =>
                            //ensure the property is not on the exclusion list for this type
                            !info.Name.IsMethodExcluded(jsonConfigDto, type) &&
                            info.Name.IsValidField() &&
                            info.ReturnType != typeof(void))
                .ToList();
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>A string extension method that query if 'methodName' is method excluded.</summary>
        /// <param name="methodName">       The methodName to act on. </param>
        /// <param name="jsonConfigDto">    The JSON configuration dto. </param>
        /// <param name="classType">        Type of the class. </param>
        /// <returns>   True if method excluded, false if not.</returns>
        ///-------------------------------------------------------------------------------------------------
        private static bool IsMethodExcluded(this string methodName, JsonConfigDto jsonConfigDto, Type classType)
        {
            //if there are no exclusions, return false
            if (jsonConfigDto.Exclusions.None().GetValueOrDefault(true)) return false;

            if (classType.FullName.IsMethodExplicitExclusion(jsonConfigDto.Exclusions, methodName)) return true;

            //if its base class is in the exclusion list, and property name matches, and set to remove derived, return true
            return classType.IsMethodImplicitExclusion(jsonConfigDto.Exclusions, methodName, false);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>A string extension method that query if 'className' is method explicit exclusion.</summary>
        /// <param name="className">    The className to act on. </param>
        /// <param name="exclusions">   The exclusions. </param>
        /// <param name="methodName">   The methodName to act on. </param>
        /// <returns>   True if method explicit exclusion, false if not.</returns>
        ///-------------------------------------------------------------------------------------------------
        private static bool IsMethodExplicitExclusion(this string className, IEnumerable<ExcludedDto> exclusions, string methodName)
        {
            return exclusions.Any(z =>

                    //where class name matches current
                    z.ClassFullName.Equals(className, StringComparison.OrdinalIgnoreCase) &&
                    //where properties match current    
                    z.MethodNames.Any(
                        n => string.Equals(n, methodName, StringComparison.OrdinalIgnoreCase)));
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>A Type extension method that query if this object is method implicit exclusion.</summary>
        /// <param name="type">             The type to act on. </param>
        /// <param name="exclusions">       The exclusions. </param>
        /// <param name="methodName">       The methodName to act on. </param>
        /// <param name="shouldExclude">    True if should exclude. </param>
        /// <returns>   True if method implicit exclusion, false if not.</returns>
        ///-------------------------------------------------------------------------------------------------
        private static bool IsMethodImplicitExclusion(this Type type, ICollection<ExcludedDto> exclusions, string methodName, bool shouldExclude)
        {
            //recurse to system.object
            if (type.BaseType != null)
                shouldExclude = type.BaseType.IsMethodImplicitExclusion(exclusions, methodName, shouldExclude);

            //return if not valid or if there are any base exclusions with excluded dervied equals true 
            return shouldExclude || exclusions.Any(
                       z =>
                           z.ClassFullName.Equals(type.FullName, StringComparison.OrdinalIgnoreCase) &&
                           z.IncludeDerivedClasses.GetValueOrDefault(true) &&
                           //where properties match current    
                           z.MethodNames != null && z.MethodNames.Any(
                               name => string.Equals(name, methodName, StringComparison.OrdinalIgnoreCase)));
        }
    }
}