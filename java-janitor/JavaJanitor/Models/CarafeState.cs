using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JavaJanitor.Models
{
    public class CarafeState
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public Status Status { get; set; }
    }
}