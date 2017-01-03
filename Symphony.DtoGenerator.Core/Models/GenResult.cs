using System.Collections.Generic;

namespace Deloitte.Symphony.DtoGeneration.Core.Models
{
    public class GenResult
    {
        /// <summary>
        /// Gets a collection of the GenResultDto class
        /// </summary>
        public List<GenResultDto> GenResultDtos { get; set; }
 

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the namespace prefix.</summary>
        /// <value> The namespace prefix.</value>
        ///-------------------------------------------------------------------------------------------------
        public string NamespacePrefix { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the namespace suffix.</summary>
        /// <value> The namespace suffix.</value>
        ///-------------------------------------------------------------------------------------------------
        public string NamespaceSuffix { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the dto prefix.</summary>
        /// 
        /// <value> The dto prefix.</value>
        ///-------------------------------------------------------------------------------------------------
        public string DtoPrefix { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the dto suffix.</summary>
        /// 
        /// <value> The dto suffix.</value>
        ///-------------------------------------------------------------------------------------------------
        public string DtoSuffix { get; set; }

        public string DllOutputDirectory { get; set; }

        public GenResult()
        {
            GenResultDtos = new List<GenResultDto>();
        }

    }
}
