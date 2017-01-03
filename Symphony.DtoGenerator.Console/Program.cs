using Deloitte.Symphony.DtoGeneration.Core.Services;

namespace Deloitte.Symphony.DtoGeneration.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //poor mans dependency injection for now
            var invokerService = new GenerationInvokerService(new ConfigurationService(), new AssemblyLoaderService(),
                new DtoGenerationService(new ResultDtoService()));

            var resultDto = invokerService.Invoke();
        }
    }
}
