using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JavaJanitor.Models
{
    public class ImageGuid
    {
        public Guid Guid { get; set; }

        public ImageGuid(Guid guid)
        {
            Guid = guid;
        }
    }
}