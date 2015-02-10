using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JavaJanitor.Models;

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
        public CarafeStatus Get(int id)
        {
            IEnumerable<CarafeStatus> matches = Stats.Where(e => e.Id == id);
            if (matches.Count() == 0)
            {
                // return 404
                return null;
            }
            else
            {
                return matches.First();
            }
        }

        // POST api/<controller>
        public void Post([FromBody] CarafeStatus newStat)
        {
            IEnumerable<CarafeStatus> matches = Stats.Where(e => e.Id == newStat.Id);
            if (matches.Count() == 0)
            {
                Stats.Add(newStat);
            }
            else
            {
                // return appropriate status code
            }
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] CarafeStatus newStat)
        {
            IEnumerable<CarafeStatus> matches = Stats.Where(e => e.Id == id);
            if (matches.Count() == 0)
            {
                // return 404
            }
            else
            {
                Stats.Remove(matches.First());
                Stats.Add(newStat);
            }
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            IEnumerable<CarafeStatus> matches = Stats.Where(e => e.Id == id);
            if (matches.Count() == 0)
            {
                // return 404
            }
            else
            {
                Stats.Remove(matches.First());
            }
        }
    }
}