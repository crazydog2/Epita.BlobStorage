using System.Collections.Generic;
using System.Threading.Tasks;
using Epita.BlobStorage.Model;

namespace Epita.BlobStorage.Logic.Contracts
{
    public interface IAlbumLogic
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

        /// <summary>
        /// Add an existing uploaded photo to an existing album
        /// </summary>
        /// <param name="userId">The owner of the album and the photo</param>
        /// <param name="albumId">The album Id</param>
        /// <param name="photoId">The photo Id</param>
        /// <returns>true if success, false otherwise</returns>
        Task<bool> AddPhotoToAlbumByAsync(string userId, string albumId, string photoId);

        /// <summary>
        /// Get all the photo that exists in the album of the user
        /// </summary>
        /// <param name="userId">The owner of the album</param>
        /// <param name="albumId">The album Id</param>
        /// <param name="tags">The search tag parameter list</param>
        /// <returns>A list of all the photos contained in the album</returns>
        Task<IEnumerable<Photo>> GetPhotosByAlbumIdAsync(string userId, string albumId, IReadOnlyList<string> tags = null);
    }
}