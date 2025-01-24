using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Auth;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Com.DanLiris.Service.EmergencyAttendance.Lib.Services.BlobStorage
{
    public class AzureBlobStorage : IBlobStorage
    {
        private readonly CloudStorageAccount _storageAccount;
        private readonly CloudBlobClient _blobClient;
        private readonly CloudBlobContainer _container;

        public AzureBlobStorage(IAzureStorageConfiguration azureStorageConfiguration)
        {
            _storageAccount = new CloudStorageAccount(new StorageCredentials(azureStorageConfiguration.AccountName, azureStorageConfiguration.Key), true);
            _blobClient = _storageAccount.CreateCloudBlobClient();
            _container = _blobClient.GetContainerReference("hr-portal");
            _container.CreateIfNotExists();
            _container.SetPermissions(new BlobContainerPermissions()
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            });
        }

        public Task<string> GetFileUrl(string filename)
        {
            throw new NotImplementedException();
        }

        public async Task<string> Upload(Stream stream, string filename)
        {
            filename = DateTime.Now.Ticks.ToString() + filename;

            var blob = _container.GetAppendBlobReference(filename);
            await blob.UploadFromStreamAsync(stream);
            
            return blob.Uri.AbsoluteUri;
        }
    }
}
