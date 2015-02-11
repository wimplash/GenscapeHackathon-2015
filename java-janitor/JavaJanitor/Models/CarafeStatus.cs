using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace JavaJanitor.Models
{
    public enum Status { Empty, Full };

    public class CarafeStatus
    {
        public int Id { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Status Status { get; set; }
    }
}