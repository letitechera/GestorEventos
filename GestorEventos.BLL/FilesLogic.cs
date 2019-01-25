using System;
using Microsoft.Extensions.Configuration;
using GestorEventos.BLL.Interfaces;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using GestorEventos.Models.Entities;

namespace GestorEventos.BLL
{
    public class FilesLogic : IFilesLogic
    {
        private readonly IConfiguration Configuration;
        private readonly IEventsLogic _eventsLogic;

        public FilesLogic(IConfiguration configuration, IEventsLogic eventsLogic)
        {
            Configuration = configuration;
            _eventsLogic = eventsLogic;
        }

        public async Task<string> LoadEventImage(int eventId, string fileName, IFormFile file)
        {
            var conectionString = Configuration.GetValue<string>("StorageConfig:StringConnection");
            var basePath = Configuration.GetValue<string>("StorageConfig:BaseStoragePath");

            if (CloudStorageAccount.TryParse(conectionString, out var storageAccount))
            {
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference("eventimages");
                await container.CreateIfNotExistsAsync();

                BlobContainerPermissions permissions = new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                };
                await container.SetPermissionsAsync(permissions);

                var newName = $"event_{eventId}_image.jpg";
                var newBlob = container.GetBlockBlobReference(newName);
                await newBlob.UploadFromStreamAsync(file.OpenReadStream());

                Event _event = _eventsLogic.GetEvent(eventId);
                _event.Image = $"{basePath}/eventimages/" + newName;
                _eventsLogic.SaveEvent(_event, true);

                return $"{basePath}/eventimages/" + newName;
            }
            else
            {
                // Otherwise, let the user know that they need to define the environment variable.
                //"A connection string has not been defined in the system environment variables. " +
                //"Add a environment variable named 'storageconnectionstring' with your storage " +
                //"connection string as a value."
                return "";
            }
        }
    }
}
