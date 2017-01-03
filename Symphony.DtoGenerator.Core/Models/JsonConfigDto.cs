using System.Collections.Generic;

namespace Deloitte.Symphony.DtoGeneration.Core.Models
{
    /// <summary>   A symphony configurationService.</summary>
    public class JsonConfigDto
    {
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the exclusions.</summary>
        /// <value> The exclusions.</value>
        ///-------------------------------------------------------------------------------------------------
        public List<ExcludedDto> Exclusions { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the namespace prefix.</summary>
        /// <value> The namespace prefix.</value>
        ///-------------------------------------------------------------------------------------------------
        public string DtoNamespacePrefix { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the namespace suffix.</summary>
        /// <value> The namespace suffix.</value>
        ///-------------------------------------------------------------------------------------------------
        public string DtoNamespaceSuffix { get; set; }
        
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the dto prefix.</summary>
        /// <value> The dto prefix.</value>
        ///-------------------------------------------------------------------------------------------------
        public string DtoClassPrefix { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the dto suffix.</summary>
        /// <value> The dto suffix.</value>
        ///-------------------------------------------------------------------------------------------------
        public string DtoClassSuffix { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the full pathname of the DLL input file.</summary>
        /// <value> The full pathname of the DLL input file.</value>
        ///-------------------------------------------------------------------------------------------------
        public string DllInputPath { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the pathname of the DLL output directory.</summary>
        /// <value> The pathname of the DLL output directory.</value>
        ///-------------------------------------------------------------------------------------------------
        public string DllOutputDirectory { get; set; }

    }

    /// <summary>   An excluded class dto.</summary>
    public class ExcludedDto
    {
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the name of the class.</summary>
        /// <value> The name of the class.</value>
        ///-------------------------------------------------------------------------------------------------
        public string ClassFullName { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the remove derived classes.</summary>
        /// <value> The remove derived classes.</value>
        ///-------------------------------------------------------------------------------------------------
        public bool? IncludeDerivedClasses { get; set; }
        
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the properties.</summary>
        /// <value> The properties.</value>
        ///-------------------------------------------------------------------------------------------------
        public List<string> PropertyNames { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets a list of names of the fields.</summary>
        /// <value> A list of names of the fields.</value>
        ///-------------------------------------------------------------------------------------------------
        public List<string> FieldNames { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets a list of names of the methods.</summary>
        /// <value> A list of names of the methods.</value>
        ///-------------------------------------------------------------------------------------------------
        public List<string> MethodNames { get; set; }
    }
}