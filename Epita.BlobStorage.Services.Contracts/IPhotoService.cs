using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Epita.BlobStorage.Model;

namespace Epita.BlobStorage.Services.Contracts
{
    public interface IPhotoService
    {
        /// <summary>
        /// Upload a photo represented as a Stream
        /// </summary>
        /// <param name="file">The photo Stream</param>
        /// <param name="userId">The userId of the Owner of the photo</param>
        /// <param name="fileName">The original name of the file</param>
        /// <returns>The unique identifier of teh newly created photo</returns>
        Task<string> UploadAsync(
            Stream file,
            string userId,
            string fileName);
        
        /// <summary>
        /// Get the Information of the photo
        /// Only the owner of the photo can access the photo
        /// </summary>
        /// <param name="userId">The owner of the photo</param>
        /// <param name="photoId">The identifier of the photo</param>
        /// <returns>The photo information matching the photoId</returns>
        Task<Photo> GetByIdAsync(string userId, string photoId);

        /// <summary>
        /// Delete the photo of the user
        /// </summary>
        /// <param name="userId">The owner of the photo</param>
        /// <param name="photoId">The identifier of the photo</param>
        /// <returns>true if success, false otherwise</returns>
        Task<bool> DeleteByIdAsync(string userId, string photoId);

        /// <summary>
        /// Download the photo as a CloudFile (containing the stream)
        /// Only the owner of the photo can access the photo
        /// </summary>
        /// <param name="userId">The owner of the photo</param>
        /// <param name="photoId">The identifier of the photo</param>
        /// <returns>A CloudFile matching the photoId</returns>
        Task<CloudFile> DownloadByIdAsync(string userId, string photoId);

        /// <summary>
        /// Build a Shared Access Uri for a photoId
        /// Only the owner of the photo can build the url for the photo
        /// </summary>
        /// <param name="userId">The owner of the photo</param>
        /// <param name="photoId">The identifier of the photo</param>
        /// <returns>The Shared Access Uri</returns>
        Task<Uri> GetSharedAccessUriAsync(string userId, string photoId);

        /// <summary>
        /// Add some tags on a photo.
        /// If the list of tags is null or Empty, therefore the existing tags onto the photo will be deleted
        /// </summary>
        /// <param name="userId">The owner of the photo</param>
        /// <param name="photoId">The identifier of the photo</param>
        /// <param name="tags">The list of the tags we want update</param>
        /// <returns>true if success, false otherwise</returns>
        Task<bool> AddOrUpdateTagsAsync(string userId, string photoId, IEnumerable<string> tags);

        /// <summary>
        /// Attach the photo to the albumIds.
        /// If the list of albumId is null or Empty, therefore the photo will be removed from the album.
        /// </summary>
        /// <param name="userId">The owner of the photo</param>
        /// <param name="photoId">The identifier of the photo</param>
        /// <param name="albumIds">The list of the albumId that we want the photo to be attached to</param>
        /// <returns>true if success, false otherwise</returns>
        Task<bool> AttachToAlbumsAsync(string userId, string photoId, IEnumerable<string> albumIds);

        /// <summary>
        /// Search the list of all photo for a specific user
        /// If the tags is null or empty, all the photo a user should be retrieved
        /// The search onto the tags should be an OR kind of search
        /// </summary>
        /// <param name="userId">The owner of the photo</param>
        /// <param name="tags">The search tag parameter list</param>
        /// <returns></returns>
        Task<IEnumerable<Photo>> GetAsync(string userId, IReadOnlyList<string> tags = null);
    }
}