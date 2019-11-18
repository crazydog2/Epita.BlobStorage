using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Epita.BlobStorage.Model;
using Epita.BlobStorage.Services.Configuration;
using Epita.BlobStorage.Services.Contracts;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;

namespace Epita.BlobStorage.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly CloudBlobClient blobClient;

        public PhotoService(AzureConfiguration configuration)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(configuration.ConnectionString);

            blobClient = storageAccount.CreateCloudBlobClient();
        }

        public Task<string> UploadAsync(Stream file, string userId, string fileName)
        {
            // TODO
            throw new NotImplementedException();
        }

        public Task<Photo> GetByIdAsync(string userId, string photoId)
        {
            // TODO
            throw new NotImplementedException();
        }

        public Task<bool> DeleteByIdAsync(string userId, string photoId)
        {
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

        public Task<bool> AddOrUpdateTagsAsync(string userId, string photoId, IEnumerable<string> tags)
        {
            // TODO
            throw new NotImplementedException();
        }

        public Task<bool> AttachToAlbumsAsync(string userId, string photoId, IEnumerable<string> albumIds)
        {
            // TODO
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Photo>> GetAsync(string userId, IReadOnlyList<string> tags = null)
        {
            // TODO
            throw new NotImplementedException();
        }
    }
}