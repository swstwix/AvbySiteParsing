using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AvByApi;
using Domain.Api;

namespace WebApi.Controllers
{
    public class HomeController : Controller
    {
        private ICarsApi carsApi = new AvParser();

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Selling(string brand, string model)
        {
            return Json(carsApi.Selling(brand, model), JsonRequestBehavior.AllowGet);
        }
    }
}
