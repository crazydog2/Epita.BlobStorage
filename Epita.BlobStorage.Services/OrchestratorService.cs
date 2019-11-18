using System.Threading.Tasks;
using Epita.BlobStorage.Services.Contracts;

namespace Epita.BlobStorage.Services
{
    public class OrchestratorService : IOrchestratorService
    {

        public async Task<bool> NotifyAsync<T>(string userId, string entityId)
        {
            await Task.Delay(100).ConfigureAwait(false);

            return true;
        }
    }
}