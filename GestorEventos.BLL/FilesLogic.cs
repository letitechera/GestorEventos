using System;
using Microsoft.Extensions.Configuration;
using GestorEventos.BLL.Interfaces;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using GestorEventos.Models.Entities;
using System.IO;
using OfficeOpenXml;
using System.Linq;

namespace GestorEventos.BLL
{
    public class FilesLogic : IFilesLogic
    {
        private readonly IConfiguration Configuration;
        private readonly IEventsLogic _eventsLogic;
        private readonly IAttendantsLogic _attendantsLogic;

        public FilesLogic(IConfiguration configuration, IEventsLogic eventsLogic, IAttendantsLogic attendantsLogic)
        {
            Configuration = configuration;
            _eventsLogic = eventsLogic;
            _attendantsLogic = attendantsLogic;
        }

        public async Task<string> LoadEventImage(int eventId, IFormFile file)
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

                var guid = Guid.NewGuid();

                var newName = $"event_{eventId}_{guid}.jpg";
                var newBlob = container.GetBlockBlobReference(newName);
                await newBlob.UploadFromStreamAsync(file.OpenReadStream());

                if (eventId != 0)
                {
                    Event _event = _eventsLogic.GetEvent(eventId);
                    _event.Image = $"{basePath}/eventimages/" + newName;
                    _eventsLogic.SaveEvent(_event, true);
                }

                return $"{basePath}/eventimages/" + newName;
            }
            else
            {
                return "";
            }
        }

        public bool ImportAttendantsFromXml(IFormFile file)
        {

            Stream stream = file.OpenReadStream();

            var excel = new ExcelPackage(stream);
            var workbook = excel.Workbook;

            var sheet = excel.Workbook.Worksheets.First();
            int colCount = sheet.Dimension.End.Column;  //get Column Count
            int rowCount = sheet.Dimension.End.Row;     //get row count

            for (int row = 2; row <= rowCount; row++)
            {
                var newAttendant = new Attendant();
                for (int col = 1; col <= colCount; col++)
                {
                    var header = sheet.Cells[1, col].Value.ToString().Trim();
                    var cell = sheet.Cells[row, col].Value.ToString().Trim();
                    switch (header)
                    {
                        case "FirstName":
                            newAttendant.FirstName = cell;
                            break;
                        case "LastName":
                            newAttendant.LastName = cell;
                            break;
                        case "Email":
                            newAttendant.Email = cell;
                            break;
                        case "Phone":
                            newAttendant.Email = cell;
                            break;
                        case "CellPhone":
                            newAttendant.Email = cell;
                            break;
                        default:
                            break;
                    }
                }
                _attendantsLogic.SaveAttendant(newAttendant);
            }

            return true;

            //var rawContent = string.Empty;
            //using (var reader = new StreamReader(file.OpenReadStream()))
            //{
            //    rawContent = reader.ReadToEnd();
            //}

            //string content;
            //try
            //{
            //    var aux = rawContent.Split(new[] { "<?xml" }, StringSplitOptions.None);
            //    aux[1] = "<?xml" + aux[1];
            //    var clean = aux[1].Split(new[] { "------" }, StringSplitOptions.None);
            //    content = clean[0];
            //}
            //catch (Exception e)
            //{
            //    return false;
            //}

            //var xlist = new ExportableList();
            //try
            //{
            //    if (content != "")
            //        xlist = (ExportableList)XMLManagement.XMLtoObject(content, typeof(ExportableList));
            //}
            //catch (Exception e)
            //{
            //    return false;
            //}

            //if (xlist == null) return false;

            //foreach (Attendant user in xlist.Members)
            //{
            //    try
            //    {
            //        if (ExistsAttendant(user.Email) == null)
            //        {
            //            var added = _attendantsLogic.SaveAttendant(user);
            //            if (added)
            //            {
            //                return true;
            //            }
            //        }
            //    }
            //    catch (Exception e)
            //    {
            //        throw e;
            //    }
            //}
            //return true;
        }

        public Attendant ExistsAttendant(string email)
        {
            return _attendantsLogic.ExistsAttendant(email);
        }
    }
}
