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
        public int Id { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]        
        public Status Status { get; set; }

        [JsonIgnore]
        public List<CarafeEvent> Events { get; set; }

        public List<Guid> Images { get; set; }
  
        public Carafe(int id)
        {
            Id = id;
            Status = Status.Empty;
            Events = new List<CarafeEvent>();
            Images = new List<Guid>();
        }
    }
}