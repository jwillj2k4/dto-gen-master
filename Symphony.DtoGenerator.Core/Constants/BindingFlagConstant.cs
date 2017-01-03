using System.Reflection;

namespace Deloitte.Symphony.DtoGeneration.Core.Constants
{
    public static class BindingFlagConstant
    {
        /// <summary>   The bind flags.</summary>
        public const BindingFlags BindFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly;
    }
}