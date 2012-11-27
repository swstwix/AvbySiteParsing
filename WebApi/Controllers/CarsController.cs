using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using AvByApi;
using Domain.Api;
using WpfApplication1.Static;

namespace WebApi.Controllers
{
    public class CarsController : Controller
    {
        private ICarsApi carsApi = new AvParser();

        public JsonResult Brands()
        {
            return Json(carsApi.Brands(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Models(string brandName)
        {
            return Json(carsApi.Models(brandName), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Selling(string brand, string model)
        {
            return Json(carsApi.Selling(brand, model), JsonRequestBehavior.AllowGet);
        }

        public JsonResult CountPages(int brandId, int countId)
        {
            return Json(carsApi.CountPages(brandId, countId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult MergeTo(ModelDetails sell)
        {
            return Json(carsApi.MergeTo(sell), JsonRequestBehavior.AllowGet);
        }
    }
}