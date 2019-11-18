using System.Collections.Generic;
using System.Threading.Tasks;
using Epita.BlobStorage.Model;
using Epita.BlobStorage.Services.Contracts;

namespace Epita.BlobStorage.Services
{
    public class AlbumService : IAlbumService
    {
        public Task<string> CreateAsync(string userId, string name)
        {
            throw new System.NotImplementedException();
        }

        public Task<Album> GetByIdAsync(string userId, string albumId)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteByIdAsync(string userId, string albumId)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Album>> GetAsync(string userId)
        {
            throw new System.NotImplementedException();
        }
    }
}