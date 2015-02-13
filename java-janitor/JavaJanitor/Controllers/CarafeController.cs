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
        public static Carafe Carafe = new Carafe();

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
        public HttpResponseMessage AddCarafeEvent([FromBody] CarafeState state)
        {
            Carafe.Status = state.Status;
            CarafeEvent ev = new CarafeEvent();
            ev.State = state.Status;
            ev.Timestamp = DateTime.Now;
            Carafe.LastUpdated = ev.Timestamp;
            Carafe.Events.Add(ev);

            var context = GlobalHost.ConnectionManager.GetHubContext<TateHub>();
            context.Clients.All.sendCarafeState(state.Status.ToString(), ev.Timestamp);

            return Request.CreateResponse(HttpStatusCode.Created);
        }

        [HttpGet]
        [Route("carafe/images")]
        [ResponseType(typeof(IEnumerable<ImageGuid>))]
        public HttpResponseMessage GetCarafeImageGuids()
        {
            return Request.CreateResponse(HttpStatusCode.OK, Carafe.Images);
        }

        [HttpPost]
        [Route("carafe/images")]
        public HttpResponseMessage AddCarafeImageGuid([FromBody] ImageGuid imageGuid)
        {
            Carafe.Images.Add(imageGuid.Guid);
            Carafe.LastUpdated = DateTime.Now;

            var context = GlobalHost.ConnectionManager.GetHubContext<TateHub>();
            context.Clients.All.sendShame(imageGuid.Guid);

            return Request.CreateResponse(HttpStatusCode.Created);
        }
    }
}