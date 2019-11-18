using System.Collections.Generic;
using System.Threading.Tasks;
using Epita.BlobStorage.Model;

namespace Epita.BlobStorage.Services.Contracts
{
    public interface IAlbumService
    {
        /// <summary>
        /// Create an album
        /// </summary>
        /// <param name="userId">The owner of the album</param>
        /// <param name="name">The name of the album</param>
        /// <returns>The id of the newly created album</returns>
        Task<string> CreateAsync(string userId, string name);

        /// <summary>
        /// Get an album by its id
        /// </summary>
        /// <param name="userId">The owner of the album</param>
        /// <param name="albumId">The album Id</param>
        /// <returns>The Album</returns>
        Task<Album> GetByIdAsync(string userId, string albumId);

        /// <summary>
        /// Delete the photo of the user
        /// </summary>
        /// <param name="userId">The owner of the photo</param>
        /// <param name="albumId">The album Id</param>
        /// <returns>true if success, false otherwise</returns>
        Task<bool> DeleteByIdAsync(string userId, string albumId);

        /// <summary>
        /// Get all the album for a user
        /// </summary>
        /// <param name="userId">The owner of the albums</param>
        /// <returns>The list of all the user's album</returns>
        Task<IEnumerable<Album>> GetAsync(string userId);
    }
}