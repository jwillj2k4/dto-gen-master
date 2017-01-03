using Deloitte.Symphony.DtoGeneration.Core.Models;

namespace Deloitte.Symphony.DtoGeneration.Core.Interfaces
{
    public interface IGenerationInvokerService
    {
        GenResult Invoke();
    }
}