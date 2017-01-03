using System;
using Deloitte.Symphony.DtoGeneration.Core.Interfaces;
using Deloitte.Symphony.DtoGeneration.Core.Models;

namespace Deloitte.Symphony.DtoGeneration.Core.Services
{
    /// <summary>   A dto generation service.</summary>
    public class GenerationInvokerService : IGenerationInvokerService, IDisposable
    {
        /// <summary>   The configuration service.</summary>
        private readonly IConfigurationService _configurationService;
        /// <summary>   The assembly service.</summary>
        private readonly IAssemblyLoaderService _assemblyService;
        /// <summary>   The entity template service.</summary>
        private readonly IDtoGenerationService _dtoGenerationService; 

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Constructor.</summary>
        /// <param name="configurationService">  The configuration service. </param>
        /// <param name="assemblyService">              The assembly service. </param>
        /// <param name="dtoGenerationService">        The entity template service. </param>
        ///-------------------------------------------------------------------------------------------------
        public GenerationInvokerService(IConfigurationService configurationService, IAssemblyLoaderService assemblyService, IDtoGenerationService dtoGenerationService)
        {
            _configurationService = configurationService;
            _assemblyService = assemblyService;
            _dtoGenerationService = dtoGenerationService;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Enumerates transform in this collection.</summary>
        /// <returns>An enumerator that allows foreach to be used to process transform in this collection.</returns>
        ///-------------------------------------------------------------------------------------------------
        public GenResult Invoke()
        {
            //read in json file
            var config = _configurationService.GetConfigFile();

            //remove non aggregate class exclusions
            config = _configurationService.RemoveNonAggregatesFromConfig(config);

            //read in dll
            var dll = _assemblyService.LoadAssembly(config);

            //return template
            var result = _dtoGenerationService.Generate(dll, config);

            return result;
        }


        ///-------------------------------------------------------------------------------------------------
        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting
        ///     unmanaged resources.</summary>
        ///-------------------------------------------------------------------------------------------------
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Releases the unmanaged resources used by the
        ///     Symphony.DtoGenerator.Core.Services.GenerationInvokerService and optionally releases the
        ///     managed resources.</summary>
        /// <param name="disposing">    True to release both managed and unmanaged resources; false to
        ///                             release only unmanaged resources. </param>
        ///-------------------------------------------------------------------------------------------------
        protected virtual void Dispose(bool disposing)
        {
            if (disposing) {}
        }
    }
}