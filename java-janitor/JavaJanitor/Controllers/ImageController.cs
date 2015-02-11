using JavaJanitor.Models;
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
        private static List<ImageDescriptor> Descriptors = new List<ImageDescriptor>();

        [HttpGet]
        [Route("images")]
        public IEnumerable<ImageDescriptor> GetAllImageDescriptors()
        {
            return Descriptors;
        }

        [HttpPost]
        [Route("images")]
        public HttpResponseMessage AddImageDescriptor([FromBody] ImageDescriptor descriptor)
        {
            IEnumerable<ImageDescriptor> matches = Descriptors.Where(e => e.Slug == descriptor.Slug);
            if (matches.Count() == 0)
            {
                Descriptors.Add(descriptor);
                return Request.CreateResponse(HttpStatusCode.Created);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        [HttpGet]
        [Route("images/{slug}")]
        [ResponseType(typeof(ImageDescriptor))]
        public HttpResponseMessage GetImageDescriptor(string slug)
        {
            IEnumerable<ImageDescriptor> matches = Descriptors.Where(e => e.Slug == slug);
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
        [Route("images/{slug}")]
        public HttpResponseMessage DeleteImageDescriptor(string slug)
        {
            IEnumerable<ImageDescriptor> matches = Descriptors.Where(e => e.Slug == slug);
            if (matches.Count() == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            else
            {
                Descriptors.Remove(matches.First());
                return Request.CreateResponse(HttpStatusCode.OK);
            }
        }
    }
}