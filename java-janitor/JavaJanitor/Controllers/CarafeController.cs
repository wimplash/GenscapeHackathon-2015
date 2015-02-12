using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JavaJanitor.Models;
using System.Web.Http.Description;

namespace JavaJanitor.Controllers
{
    public class CarafeController : ApiController
    {
        private static List<Carafe> Carafes = new List<Carafe>();

        [HttpGet]
        [Route("carafes")]
        public IEnumerable<int> GetAllCarafeIds()
        {
            return Carafes.Select(c => c.Id);
        }

        [HttpPost]
        [Route("carafes")]
        public HttpResponseMessage AddCarafe()
        {
            int id = (Carafes.Max(c => (int?) c.Id) ?? 0) + 1;

            Carafe carafe = new Carafe();
            carafe.Id = id;
            Carafes.Add(carafe);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
            response.Headers.Add("Location", "http://genscape-java-janitor.azurewebsites.net/carafes/" + carafe.Id);
            return response;
        }

        [HttpGet]
        [Route("carafes/{id}")]
        [ResponseType(typeof(Carafe))]
        public HttpResponseMessage GetCarafe(int id)
        {
            IEnumerable<Carafe> matches = Carafes.Where(e => e.Id == id);
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
        [Route("carafes/{id}")]
        public HttpResponseMessage DeleteCarafe(int id)
        {
            IEnumerable<Carafe> matches = Carafes.Where(e => e.Id == id);
            if (matches.Count() == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            else
            {
                Carafes.Remove(matches.First());
                return Request.CreateResponse(HttpStatusCode.OK);
            }
        }

        [HttpGet]
        [Route("carafes/{id}/events")]
        [ResponseType(typeof(IEnumerable<CarafeEvent>))]
        public HttpResponseMessage GetCarafeEvents(int id)
        {
            IEnumerable<Carafe> matches = Carafes.Where(e => e.Id == id);
            if (matches.Count() == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, matches.First().Events);
            }
        }

        [HttpPost]
        [Route("carafes/{id}/events")]
        public HttpResponseMessage AddCarafeEvent(int id, [FromBody] CarafeEvent ev)
        {
            IEnumerable<Carafe> matches = Carafes.Where(e => e.Id == id);
            if (matches.Count() == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            else
            {
                matches.First().Status = ev.State;
                matches.First().Events.Add(ev);
                return Request.CreateResponse(HttpStatusCode.Created);
            }
        }
    }
}