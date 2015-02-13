using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JavaJanitor.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Genscape's Virtual Terry Tate";
            ViewBag.CarafeState = CarafeController.Carafe.Status;
            ViewBag.CarafeLastUpdated = CarafeController.Carafe.LastUpdated;
            ViewBag.OffenderImage = CarafeController.Carafe.Image.Filename;
            ViewBag.OffenderName = CarafeController.Carafe.Name;
            ViewBag.OffenderLastUpdated = CarafeController.Carafe.Image.Timestamp;
            return View();
        }
    }
}