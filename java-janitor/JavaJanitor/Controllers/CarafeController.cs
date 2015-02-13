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
        [Route("carafe/offender/image")]
        [ResponseType(typeof(Image))]
        public HttpResponseMessage GetOffenderImage()
        {
            return Request.CreateResponse(HttpStatusCode.OK, Carafe.Image);
        }

        [HttpPost]
        [Route("carafe/offender/image/{url}")]
        public HttpResponseMessage AddOffenderImage(string url)
        {
            DateTime now = DateTime.Now;
            Image image = new Image();
            image.Filename = url;
            image.Timestamp = now;
            Carafe.Image = image;
            Carafe.LastUpdated = now;

            var context = GlobalHost.ConnectionManager.GetHubContext<TateHub>();
            context.Clients.All.setOffenderImage(image);

            return Request.CreateResponse(HttpStatusCode.Created);
        }

        [HttpGet]
        [Route("carafe/offender/name")]
        [ResponseType(typeof(string))]
        public HttpResponseMessage GetOffenderName()
        {
            return Request.CreateResponse(HttpStatusCode.OK, Carafe.Name);
        }

        [HttpPost]
        [Route("carafe/offender/name/{name}")]
        public HttpResponseMessage SetOffenderName(string name)
        {
            Carafe.Name = name;
            Carafe.LastUpdated = DateTime.Now;

            var context = GlobalHost.ConnectionManager.GetHubContext<TateHub>();
            context.Clients.All.setOffenderName(name);

            return Request.CreateResponse(HttpStatusCode.Created);
        }
    }
}