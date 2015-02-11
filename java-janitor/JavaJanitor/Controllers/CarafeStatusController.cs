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
    public class CarafeStatusController : ApiController
    {
        private List<CarafeStatus> Stats = new List<CarafeStatus>();

        // GET api/<controller>
        public IEnumerable<int> Get()
        {
            return Stats.Select(e => e.Id);
        }

        // GET api/<controller>/5
        [ResponseType(typeof(CarafeStatus))]
        public HttpResponseMessage Get(int id)
        {
            IEnumerable<CarafeStatus> matches = Stats.Where(e => e.Id == id);
            if (matches.Count() == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, matches.First());
            }
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody] CarafeStatus newStat)
        {
            IEnumerable<CarafeStatus> matches = Stats.Where(e => e.Id == newStat.Id);
            if (matches.Count() == 0)
            {
                Stats.Add(newStat);
                return Request.CreateResponse(HttpStatusCode.Created);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put(int id, [FromBody] CarafeStatus newStat)
        {
            IEnumerable<CarafeStatus> matches = Stats.Where(e => e.Id == id);
            if (matches.Count() == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            else
            {
                Stats.Remove(matches.First());
                Stats.Add(newStat);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
        }

        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(int id)
        {
            IEnumerable<CarafeStatus> matches = Stats.Where(e => e.Id == id);
            if (matches.Count() == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            else
            {
                Stats.Remove(matches.First());
                return Request.CreateResponse(HttpStatusCode.OK);
            }
        }
    }
}