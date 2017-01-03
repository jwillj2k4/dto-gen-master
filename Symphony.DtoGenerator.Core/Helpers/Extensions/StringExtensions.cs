using System;

namespace Deloitte.Symphony.DtoGeneration.Core.Helpers.Extensions
{
    /// <summary>   A string extensions.</summary>
    public static class StringExtensions
    {
        ///-------------------------------------------------------------------------------------------------
        /// <summary>A string extension method that query if this object contains the given string1.</summary>
        /// <param name="string1">  The string1 to act on. </param>
        /// <param name="string2">  The second string. </param>
        /// <param name="comp">     (Optional) The component. </param>
        /// <returns>   True if the object is in this collection, false if not.</returns>
        ///-------------------------------------------------------------------------------------------------
        public static bool Contains(this string string1, string string2, StringComparison comp = StringComparison.Ordinal)
        {
            return string1.IndexOf(string2, comp) >= 0;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   A string extension method that query if 'string1' is valid field.</summary>
        /// <param name="string1">  The string1 to act on. </param>
        /// <returns>   True if valid field, false if not.</returns>
        ///-------------------------------------------------------------------------------------------------
        public static bool IsValidField(this string string1)
        {
            return !(string1.Contains("k__BackingField", StringComparison.OrdinalIgnoreCase) || string1.StartsWith("get_", StringComparison.OrdinalIgnoreCase) || string1.StartsWith("set_", StringComparison.OrdinalIgnoreCase));
        }
    }
}