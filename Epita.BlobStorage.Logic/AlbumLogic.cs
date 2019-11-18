using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Epita.BlobStorage.Logic.Contracts;
using Epita.BlobStorage.Model;
using Epita.BlobStorage.Services.Contracts;

namespace Epita.BlobStorage.Logic
{
    public class AlbumLogic : IAlbumLogic
    {
        private readonly IAlbumService albumService;
        private readonly IPhotoService photoService;
        private readonly IOrchestratorService orchestratorService;

        public AlbumLogic(
            IAlbumService albumService,
            IPhotoService photoService, IOrchestratorService orchestratorService)
        {
            this.albumService = albumService;
            this.photoService = photoService;
            this.orchestratorService = orchestratorService;
        }

        public async Task<string> CreateAsync(string userId, string name)
        {
            string albumId = await albumService.CreateAsync(userId, name).ConfigureAwait(false);

            if (string.IsNullOrEmpty(albumId))
            {
                return null;
            }
            
            // DO NOT REMOVE
            bool success = await orchestratorService.NotifyAsync<Album>(userId, albumId).ConfigureAwait(false);

            return success ? albumId : null;
            // DO NOT REMOVE
        }

        public Task<Album> GetByIdAsync(string userId, string albumId)
        {
            // TODO
            throw new NotImplementedException();
        }

        public Task<bool> DeleteByIdAsync(string userId, string albumId)
        {
            // TODO
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Album>> GetAsync(string userId)
        {
            // TODO
            throw new NotImplementedException();
        }

        public async Task<bool> AddPhotoToAlbumByAsync(string userId, string albumId, string photoId)
        {
            // TODO
            throw new NotImplementedException();

            // Once the photo has been added to the album, then notify

            // DO NOT REMOVE
            bool success = await orchestratorService.NotifyAsync<Album>(userId, albumId).ConfigureAwait(false);
            // DO NOT REMOVE


            return success;
        }

        public Task<IEnumerable<Photo>> GetPhotosByAlbumIdAsync(string userId, string albumId, IReadOnlyList<string> tags = null)
        {
            // TODO
            throw new NotImplementedException();
        }
    }
}