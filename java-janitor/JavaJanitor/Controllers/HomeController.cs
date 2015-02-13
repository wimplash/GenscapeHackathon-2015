using JavaJanitor.Models;
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
            Carafe carafe = CarafeController.Carafe;
            ViewBag.CarafeState = carafe.Status;
            ViewBag.CarafeLastUpdated = carafe.LastUpdated;
            ViewBag.OffenderName = carafe.Name;
            Image image = carafe.Image;
            if (image != null)
            {
                ViewBag.OffenderImage = image.Filename;
                ViewBag.OffenderLastUpdated = image.Timestamp;
            }
            return View();
        }
    }
}