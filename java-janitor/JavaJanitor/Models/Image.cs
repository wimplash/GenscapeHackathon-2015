using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JavaJanitor.Models
{
    public class Image
    {
        public Guid Guid { get; set; }
        public String Filename { get; set; }
    }
}