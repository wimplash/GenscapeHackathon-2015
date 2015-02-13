using JavaJanitor.Models;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace JavaJanitor.Controllers
{
    public class ImageController : ApiController
    {
        private static List<Image> Images = new List<Image>();

        [HttpGet]
        [Route("images")]
        public IEnumerable<ImageGuid> GetAllImageGuids()
        {
            return Images.Select(i => new ImageGuid(i.Guid));
        }

        [HttpPost]
        [Route("images")]
        public async Task<HttpResponseMessage> AddImage()
        {
            byte[] input = await Request.Content.ReadAsByteArrayAsync();

            Image image = new Image();
            Guid guid = System.Guid.NewGuid();
            image.Guid = guid;
            image.Filename = guid + ".jpg";
            Images.Add(image);

            System.Drawing.Image i = System.Drawing.Image.FromStream(new MemoryStream(input), false, true);

            MemoryStream output = new MemoryStream();
            i.Save(output, ImageFormat.Jpeg);

            string setting = CloudConfigurationManager.GetSetting("BlobStorageConnectionString") ?? ConfigurationManager.ConnectionStrings["BlobStorageConnectionString"].ConnectionString;
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(setting);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("terry-tates-brain");
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(guid + ".jpg");
            await blockBlob.UploadFromStreamAsync(output);
            blockBlob.Properties.ContentType = "image/jpg";
            blockBlob.SetProperties();

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
            response.Headers.Add("Location", "http://genscape-java-janitor.azurewebsites.net/images/" + guid);
            return response;
        }

        [HttpGet]
        [Route("images/{guid}")]
        public async Task<HttpResponseMessage> GetImage(Guid guid)
        {
            IEnumerable<Image> matches = Images.Where(e => e.Guid == guid);
            if (matches.Count() == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            else
            {
                string setting = CloudConfigurationManager.GetSetting("BlobStorageConnectionString") ?? ConfigurationManager.ConnectionStrings["BlobStorageConnectionString"].ConnectionString;
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(setting);
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference("terry-tates-brain");
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(guid + ".jpg");

                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                MemoryStream output = new MemoryStream();
                await blockBlob.DownloadToStreamAsync(output);
                result.Content = new StreamContent(output);
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpg");
                return result;
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