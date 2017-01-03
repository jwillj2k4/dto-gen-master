using System;
using System.Collections.Generic;
using System.Reflection;

namespace Deloitte.Symphony.DtoGeneration.Core.Models
{
    /// <summary>   A result class. </summary>
    public class GenResultDto
    {
        public GenResultDto()
        {
            Dependencies = new List<GenResultDto>();
            Primitives = new List<PropertyInfo>();
        }
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the namespace. </summary>
        ///
        /// <value> The namespace. </value>
        ///-------------------------------------------------------------------------------------------------
        public string Namespace { get; set; }
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the type of the base.</summary>
        /// <value> The type of the base.</value>
        ///-------------------------------------------------------------------------------------------------
        public Type BaseType { get; set; }

        public string ComplexTypeClassName { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the result properties. </summary>
        ///
        /// <value> The result properties. </value>
        ///-------------------------------------------------------------------------------------------------
        public IList<GenResultDto> Dependencies { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the primitives.</summary>
        /// todo rename this since we will not need the dependencies
        /// <value> The primitives.</value>
        ///-------------------------------------------------------------------------------------------------
        public IList<PropertyInfo> Primitives { get; set; }



    } 
}