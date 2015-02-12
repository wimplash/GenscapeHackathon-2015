using JavaJanitor.Models;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace JavaJanitor.Controllers
{
    public class ImageController : ApiController
    {
        private static List<Image> Images = new List<Image>();

        [HttpGet]
        [Route("images")]
        public IEnumerable<Guid> GetAllImageGuids()
        {
            return Images.Select(i => i.Guid);
        }

        [HttpPost]
        [Route("images")]
        public HttpResponseMessage AddImage([FromBody] byte[] bytes)
        {
            Image image = new Image();
            image.Guid = System.Guid.NewGuid();
            image.Bytes = bytes;
            image.Filename = image.Guid + ".jpg";
            Images.Add(image);

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("BlobStorageConnectionString"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("terry-tates-brain");
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(image.Guid + ".jpg");
            using (var stream = new System.IO.MemoryStream(bytes))
            {
                blockBlob.UploadFromStream(stream);
            }

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
            response.Headers.Add("Location", "http://genscape-java-janitor.azurewebsites.net/images/" + image.Guid);
            return response;
        }

        [HttpGet]
        [Route("images/{guid}")]
        [ResponseType(typeof(Image))]
        public HttpResponseMessage GetImage(Guid guid)
        {
            IEnumerable<Image> matches = Images.Where(e => e.Guid == guid);
            if (matches.Count() == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, matches.First());
            }
        }

        [HttpDelete]
        [Route("images/{guid}")]
        public HttpResponseMessage DeleteImage(Guid guid)
        {
            IEnumerable<Image> matches = Images.Where(e => e.Guid == guid);
            if (matches.Count() == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            else
            {
                Images.Remove(matches.First());
                return Request.CreateResponse(HttpStatusCode.OK);
            }
        }
    }
}