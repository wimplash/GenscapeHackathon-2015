using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JavaJanitor.Models
{
    public enum Status { Empty, Full };

    public class Carafe
    {
        [JsonConverter(typeof(StringEnumConverter))]        
        public Status Status { get; set; }

        public List<CarafeEvent> Events { get; set; }

        public Image Image { get; set; }
        public string Name { get; set; }

        public DateTime LastUpdated { get; set; }

        public Carafe()
        {
            LastUpdated = DateTime.Now;
            Status = Status.Empty;
            Events = new List<CarafeEvent>();
        }

    }
}