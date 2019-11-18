using System.Threading.Tasks;

namespace Epita.BlobStorage.Services.Contracts
{
    public interface IOrchestratorService
    {
        Task<bool> NotifyAsync<T>(string userId, string entityId);
    }
}