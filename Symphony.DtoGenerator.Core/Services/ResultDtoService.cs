using System;
using System.Linq;
using Deloitte.Symphony.DtoGeneration.Core.Helpers.Utilities;
using Deloitte.Symphony.DtoGeneration.Core.Interfaces;
using Deloitte.Symphony.DtoGeneration.Core.Models;

namespace Deloitte.Symphony.DtoGeneration.Core.Services
{
    public class ResultDtoService : IResultDtoService
    {
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Creates result dto.</summary>
        /// <param name="jsonConfigDto">    The JSON configuration dto. </param>
        /// <param name="type">             The type. </param>
        /// <returns>   The new result dto.</returns>
        ///-------------------------------------------------------------------------------------------------
        public GenResultDto CreateResultDto(JsonConfigDto jsonConfigDto, Type type)
        {
            var result = CreateBaseDto(type);

            AddAdditionalInformation(result, jsonConfigDto, type);

            return result;
        }

        /// -------------------------------------------------------------------------------------------------
        ///  <summary>   Creates base dto.</summary>
        /// <param name="type">             The type. </param>
        ///  <returns>   The new base dto.</returns>
        /// -------------------------------------------------------------------------------------------------
        private static GenResultDto CreateBaseDto(Type type)
        {
            var result = new GenResultDto
            {
                BaseType = type,
                Namespace = type.Namespace
         
            };

            return result;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Adds an additional information.</summary>
        /// <param name="result">           The result. </param>
        /// <param name="jsonConfigDto">    The JSON configuration dto. </param>
        /// <param name="type">             The type. </param>
        ///-------------------------------------------------------------------------------------------------
        private static void AddAdditionalInformation(GenResultDto result, JsonConfigDto jsonConfigDto, Type type)
        {
            AddProperties(result, jsonConfigDto, type);

            AddFields(result, jsonConfigDto, type);

            AddMethods(result, jsonConfigDto, type);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Adds the properties.</summary>
        /// <param name="result">           The result. </param>
        /// <param name="jsonConfigDto">    The JSON configuration dto. </param>
        /// <param name="type">             The type. </param>
        ///-------------------------------------------------------------------------------------------------
        private static void AddProperties(GenResultDto result, JsonConfigDto jsonConfigDto, Type type)
        {
            //add the primitive and complex properties to the primitives list
            result.Primitives =
                result.Primitives.Union(type.GetIncludedPrimitiveProperties(jsonConfigDto))
                .Union(type.GetIncludedComplexProperties(jsonConfigDto)).ToList();
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Adds the fields.</summary>
        /// <param name="result">           The result. </param>
        /// <param name="jsonConfigDto">    The JSON configuration dto. </param>
        /// <param name="type">             The type. </param>
        ///-------------------------------------------------------------------------------------------------
        private static void AddFields(GenResultDto result, JsonConfigDto jsonConfigDto, Type type)
        {
            //add fields to primitives list
            result.Primitives = result.Primitives.Union(type.GetIncludedPrimitiveFields(jsonConfigDto).CreateAndGetFieldProperties())
                .Union(type.GetIncludedComplexFields(jsonConfigDto).CreateAndGetFieldProperties()).ToList();
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Adds the methods.</summary>
        /// <param name="result">           The result. </param>
        /// <param name="jsonConfigDto">    The JSON configuration dto. </param>
        /// <param name="type">             The type. </param>
        ///-------------------------------------------------------------------------------------------------
        private static void AddMethods(GenResultDto result, JsonConfigDto jsonConfigDto, Type type)
        {
            //add methods to primitives list
            result.Primitives =
                result.Primitives.Union(
                    type.GetIncludedPrimitiveMethodReturnValues(jsonConfigDto).CreateAndGetMethodProperties())
            .Union(type.GetIncludedComplexMethodReturnValues(jsonConfigDto).CreateAndGetMethodProperties()).ToList();
        }
    }
}