﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JavaJanitor.Controllers
{
    public class DailyAnalyticsController : ApiController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
