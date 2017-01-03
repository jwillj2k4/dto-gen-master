using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Deloitte.Symphony.DtoGeneration.Tests
{
    [TestClass]
    public class JsonConfigurationTests
    {
        [Ignore]
        [TestMethod]
        public void JsonConfigSchema_Should_MatchJsonConfigDto()
        {
            //todo, ensure the modified json configuration can be deserialized into to GenerationConfigDto
            //todo ensure reading in json file results to a hydrated object
            // JsonConfigDto result;
            //
            // var obj = new JsonConfigDto
            // {
            //    DllPath = "fake dll path",
            //    DtoPrefix = "fake prefix",
            //    DtoSuffix = "fake suffix",
            //    Exclusions = new List<ExcludedClassDto>
            //    {
            //        new ExcludedClassDto()
            //        {
            //exclude entire class
            //            ClassName = "fake class 2"
            //        },
            //        new ExcludedClassDto()
            //        {
            //exclude properties
            //            ClassName = "fake class 1",
            //            Properties = new List<string>() {"a", "b", "c"}
            //        }
            //    }
            // };
            //
            //
            //
            // string output = JsonConvert.SerializeObject(obj);
        }

        [Ignore]
        [TestMethod]
        public void JsonConfigRequiredProperties_Should_BePopulated()
        {
        }

        [Ignore]
        [TestMethod]
        public void JsonConfig_OptionalProperties_Should_BeOptional()
        {
        }

    }
}
