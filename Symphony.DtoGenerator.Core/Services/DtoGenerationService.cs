using System.Linq;
using System.Reflection;
using Deloitte.Symphony.DtoGeneration.Core.Helpers.Utilities;
using Deloitte.Symphony.DtoGeneration.Core.Interfaces;
using Deloitte.Symphony.DtoGeneration.Core.Models;

namespace Deloitte.Symphony.DtoGeneration.Core.Services
{
    /// <summary>   An entity template service.</summary>
    public class DtoGenerationService : IDtoGenerationService
    {
        private readonly IResultDtoService _resultDtoService;

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Constructor.</summary>
        /// <param name="resultDtoService"> The result dto service. </param>
        ///-------------------------------------------------------------------------------------------------
        public DtoGenerationService(IResultDtoService resultDtoService)
        {
            _resultDtoService = resultDtoService;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Enumerates generate template in this collection.</summary>
        /// <param name="assembly">         The DLL. </param>
        /// <param name="jsonConfigDto">    . </param>
        /// <returns>   An list of Entity Dto Results.</returns>
        ///-------------------------------------------------------------------------------------------------
        public GenResult Generate(Assembly assembly, JsonConfigDto jsonConfigDto)
        {
            var result = new GenResult
            {
                NamespacePrefix = jsonConfigDto.DtoNamespacePrefix,
                NamespaceSuffix = jsonConfigDto.DtoNamespaceSuffix,
                DtoPrefix = jsonConfigDto.DtoClassPrefix,
                DtoSuffix = jsonConfigDto.DtoClassSuffix,
                DllOutputDirectory = jsonConfigDto.DllOutputDirectory
            };

            //Read in entities from assembly, exclude anything that isnt a root aggregate
            var types = assembly.GetTypes().ToList();

            //for each type
            types.ForEach(type =>
            {
                if (type.IsClassExcluded(result.GenResultDtos, jsonConfigDto)) return;

                //add to collection
                result.GenResultDtos.Add(_resultDtoService.CreateResultDto(jsonConfigDto, type));
            });
            
            return result;
        }
    }
}