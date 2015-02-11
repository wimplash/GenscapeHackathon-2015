using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JavaJanitor.Models
{
    public class CarafeEvent
    {
        public DateTime Timestamp { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Status State { get; set; }
    }
}