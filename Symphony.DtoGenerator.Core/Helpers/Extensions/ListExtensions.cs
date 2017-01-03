using System.Collections.Generic;
using System.Linq;

namespace Deloitte.Symphony.DtoGeneration.Core.Helpers.Extensions
{
    /// <summary>   List extensions.</summary>
    public static class ListExtensions
    {
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   An IEnumerable&lt;T&gt; extension method that nones the given list.</summary>
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        /// <param name="list"> The list to act on. </param>
        /// <returns>   True if it succeeds, false if it fails.</returns>
        ///-------------------------------------------------------------------------------------------------
        public static bool? None<T>(this IEnumerable<T> list)
        {
            return !list?.Any();
        }
    }
}