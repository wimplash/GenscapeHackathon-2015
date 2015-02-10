using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace JavaJanitor.Models
{
    public enum Status { Empty, Full };

    [JsonObject("carafe-status")]
    public class CarafeStatus
    {
        public int Id { get; set; }
        public Status Status { get; set; }
    }
}