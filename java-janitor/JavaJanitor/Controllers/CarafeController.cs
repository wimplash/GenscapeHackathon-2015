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
        private static Carafe Carafe = new Carafe();

        [HttpGet]
        [Route("carafe")]
        public Carafe GetCarafe()
        {
            return Carafe;
        }

        [HttpGet]
        [Route("carafe/events")]
        [ResponseType(typeof(IEnumerable<CarafeEvent>))]
        public HttpResponseMessage GetCarafeEvents()
        {
            return Request.CreateResponse(HttpStatusCode.OK, Carafe.Events);
        }

        [HttpPost]
        [Route("carafe/events")]
        public HttpResponseMessage AddCarafeEvent([FromBody] CarafeEvent ev)
        {
            Carafe.Status = ev.State;
            Carafe.Events.Add(ev);
            Carafe.LastUpdated = DateTime.Now;

            var context = GlobalHost.ConnectionManager.GetHubContext<TateHub>();
            context.Clients.All.sendCarafeState(ev.State);

            return Request.CreateResponse(HttpStatusCode.Created);
        }

        [HttpGet]
        [Route("carafe/images")]
        [ResponseType(typeof(IEnumerable<Guid>))]
        public HttpResponseMessage GetCarafeImageGuids()
        {
            return Request.CreateResponse(HttpStatusCode.OK, Carafe.Images);
        }

        [HttpPost]
        [Route("carafe/images")]
        public HttpResponseMessage AddCarafeImageGuid([FromBody] Guid guid)
        {
            Carafe.Images.Add(guid);
            Carafe.LastUpdated = DateTime.Now;

            var context = GlobalHost.ConnectionManager.GetHubContext<TateHub>();
            context.Clients.All.sendShame(guid);

            return Request.CreateResponse(HttpStatusCode.Created);
        }
    }
}