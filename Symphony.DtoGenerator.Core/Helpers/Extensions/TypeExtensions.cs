using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Deloitte.Symphony.DtoGeneration.Core.Helpers.Extensions
{
    public static class TypeExtensions
    {
        /// <summary>
        /// Determine whether a type is simple (String, Decimal, DateTime, etc) 
        /// or complex (i.e. custom class with public properties and methods).
        /// </summary>
        /// <see cref="http://stackoverflow.com/questions/2442534/how-to-test-if-type-is-primitive"/>
        public static bool IsSimpleType(
            this Type type)
        {
            return
                type.IsValueType ||
                type.IsPrimitive ||
                new[] {
                    typeof(string),
                    typeof(decimal),
                    typeof(DateTime),
                    typeof(DateTimeOffset),
                    typeof(TimeSpan),
                    typeof(Guid)
                }.Contains(type) ||
                Convert.GetTypeCode(type) != TypeCode.Object;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the primitives in this collection.</summary>
        /// <param name="array">    The array to act on. </param>
        /// <returns>An enumerator that allows foreach to be used to process the primitives in this
        ///     collection.</returns>
        ///-------------------------------------------------------------------------------------------------
        public static IEnumerable<PropertyInfo> GetPrimitivesTypes(this PropertyInfo[] array)
        {
            return array.Where(propInfo => propInfo.PropertyType.IsSimpleType()).ToList();
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the complex types in this collection.</summary>
        /// <param name="array">    The array to act on. </param>
        /// <returns>An enumerator that allows foreach to be used to process the complex types in this
        ///     collection.</returns>
        ///-------------------------------------------------------------------------------------------------
        public static IEnumerable<PropertyInfo> GetComplexTypes(this PropertyInfo[] array)
        {
            return array.Where(propInfo => !propInfo.PropertyType.IsSimpleType()).ToList();
        }
    }
}