using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Epita.BlobStorage.Logic.Contracts;
using Epita.BlobStorage.Model;
using Epita.BlobStorage.Services.Contracts;

namespace Epita.BlobStorage.Logic
{
    public class PhotoLogic : IPhotoLogic
    {
        private readonly IPhotoService photoService;
        private readonly IOrchestratorService orchestratorService;

        public PhotoLogic(
            IPhotoService photoService,
            IOrchestratorService orchestratorService)
        {
            this.photoService = photoService;
            this.orchestratorService = orchestratorService;
        }

        public async Task<string> UploadAsync(
            Stream file, 
            string userId, 
            string fileName)
        {
            string photoId = await photoService.UploadAsync(file, userId, fileName).ConfigureAwait(false);

            if (string.IsNullOrEmpty(photoId))
            {
                return null;
            }

            // DO NOT REMOVE
            bool success = await orchestratorService.NotifyAsync<Photo>(userId, photoId).ConfigureAwait(false);

            if (success)
            {
                return photoId;
            }

            return null;
        }

        public Task<Photo> GetByIdAsync(string userId, string photoId)
        {
            // TODO
            throw new NotImplementedException();
        }

        public Task<bool> DeleteByIdAsync(string userId, string photoId)
        {
            // TODO
            throw new NotImplementedException();
        }

        public Task<CloudFile> DownloadByIdAsync(string userId, string photoId)
        {
            // TODO
            throw new NotImplementedException();
        }

        public Task<Uri> GetSharedAccessUriAsync(string userId, string photoId)
        {
            // TODO
            throw new NotImplementedException();
        }

        public async Task<bool> AddOrUpdateTagsAsync(string userId, string photoId, IEnumerable<string> tags)
        {
            bool added = await photoService.AddOrUpdateTagsAsync(userId, photoId, tags).ConfigureAwait(false);

            // DO NOT REMOVE
            bool success = await orchestratorService.NotifyAsync<Photo>(userId, photoId).ConfigureAwait(false);

            return success && added;
        }

        public Task<IEnumerable<Photo>> GetAsync(string userId, IReadOnlyList<string> tags = null)
        {
            // TODO
            throw new NotImplementedException();
        }
    }
}