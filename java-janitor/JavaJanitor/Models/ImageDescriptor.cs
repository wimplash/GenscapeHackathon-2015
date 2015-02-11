using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JavaJanitor.Models
{
    public class ImageDescriptor
    {
        public String Slug { get; set; }
        public int CarafeId { get; set; }
        public String Filename { get; set; }

        [JsonIgnore]
        public byte[] image { get; set; }
    }
}