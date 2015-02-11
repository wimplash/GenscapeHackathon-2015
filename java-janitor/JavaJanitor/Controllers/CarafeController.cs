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
        public IEnumerable<Carafe> GetAllCarafes()
        {
            return Carafes;
        }

        [HttpPost]
        [Route("carafes")]
        public HttpResponseMessage AddCarafe([FromBody] Carafe carafe)
        {
            IEnumerable<Carafe> matches = Carafes.Where(e => e.Id == carafe.Id);
            if (matches.Count() == 0)
            {
                Carafes.Add(carafe);
                return Request.CreateResponse(HttpStatusCode.Created);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
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