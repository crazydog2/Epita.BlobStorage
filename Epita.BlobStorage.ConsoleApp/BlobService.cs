using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;

namespace Epita.BlobStorage.ConsoleApp
{
    public class BlobService
    {
        private readonly CloudBlobClient blobClient;

        public BlobService(string connectionString)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);

            blobClient = storageAccount.CreateCloudBlobClient();
        }

        #region Create

        public async Task<bool> UploadFromStreamAsync(
            string containerName,
            string blobName,
            Stream stream)
        {
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);

            await container.CreateIfNotExistsAsync().ConfigureAwait(false);

            CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobName);

            if (blockBlob == null)
            {
                return false;
            }

            await blockBlob.UploadFromStreamAsync(stream).ConfigureAwait(false);

            return true;
        }

        public async Task<bool> AddOrUpdateMetatdataAsync(
            string containerName,
            string blobName,
            IDictionary<string, string> metadata)
        {
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);

            await container.CreateIfNotExistsAsync().ConfigureAwait(false);

            CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobName);

            if (blockBlob == null)
            {
                return false;
            }

            if (metadata != null && metadata.Count > 0)
            {
                foreach (KeyValuePair<string, string> meta in metadata)
                {
                    blockBlob.Metadata[meta.Key] = meta.Value;
                }
            }

            await blockBlob.SetMetadataAsync().ConfigureAwait(false);

            return true;
        }

        #endregion Create

        #region Read

        public string GetSasUri(
            string containerName,
            string blobName)
        {
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);
            
            //Set the expiry time and permissions for the blob.
            //In this case, the start time is specified as a few minutes in the past, to mitigate clock skew.
            //The shared access signature will be valid immediately.
            var sasConstraints = new SharedAccessBlobPolicy
            {
                SharedAccessStartTime = DateTimeOffset.UtcNow.AddMinutes(-5),
                SharedAccessExpiryTime = DateTimeOffset.UtcNow.AddMinutes(10),
                Permissions = SharedAccessBlobPermissions.Read
            };

            CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobName);

            //Generate the shared access signature on the blob, setting the constraints directly on the signature.
            string sasBlobToken = blockBlob.GetSharedAccessSignature(sasConstraints);

            //Return the URI string for the container, including the SAS token.
            return blockBlob.Uri + sasBlobToken;
        }

        public async Task<Stream> ReadFileAsync(
            string containerName,
            string blobName)
        {
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);

            CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobName);

            return await blockBlob.OpenReadAsync();
        }

        #endregion Read

        #region Delete

        public async Task<bool> DeleteAsync(
            string containerName,
            string blobName)
        {
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);
            
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobName);

            await blockBlob.DeleteAsync().ConfigureAwait(false);

            return true;
        }

        #endregion Delete
    }
}