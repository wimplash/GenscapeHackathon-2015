using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JavaJanitor.Models;
using System.Web.Http.Description;
using Microsoft.AspNet.SignalR;

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

            Carafe carafe = new Carafe(id);
            Carafes.Add(carafe);

            var context = GlobalHost.ConnectionManager.GetHubContext<TateHub>();
            context.Clients.All.showNewCarafe(id);

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
                Carafe carafe = matches.First();
                Carafes.Remove(carafe);

                var context = GlobalHost.ConnectionManager.GetHubContext<TateHub>();
                context.Clients.All.removeCarafe(id);

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
                Carafe carafe = matches.First();
                carafe.Status = ev.State;
                carafe.Events.Add(ev);

                var context = GlobalHost.ConnectionManager.GetHubContext<TateHub>();
                context.Clients.All.sendCarafeState(carafe.Id, ev.State);

                return Request.CreateResponse(HttpStatusCode.Created);
            }
        }

        [HttpGet]
        [Route("carafes/{id}/images")]
        [ResponseType(typeof(IEnumerable<Guid>))]
        public HttpResponseMessage GetCarafeImages(int id)
        {
            IEnumerable<Carafe> matches = Carafes.Where(e => e.Id == id);
            if (matches.Count() == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, matches.First().Images);
            }
        }

        [HttpPost]
        [Route("carafes/{id}/images")]
        public HttpResponseMessage AddCarafeImage(int id, [FromBody] Guid guid)
        {
            IEnumerable<Carafe> matches = Carafes.Where(e => e.Id == id);
            if (matches.Count() == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            else
            {
                Carafe carafe = matches.First();
                carafe.Images.Add(guid);

                var context = GlobalHost.ConnectionManager.GetHubContext<TateHub>();
                context.Clients.All.sendShame(carafe.Id, guid);

                return Request.CreateResponse(HttpStatusCode.Created);
            }
        }
    }
}