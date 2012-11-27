using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AvByApi;
using Domain.Api;
using WpfApplication1.Static;

namespace WebApi.Controllers
{
    public class CarsController : ApiController, ICarsApi
    {
        private ICarsApi carsApi = new AvParser();

        [HttpGet]
        public IDictionary<string, int> Brands()
        {
            return carsApi.Brands();
        }

        [HttpGet]
        public IDictionary<string, int> Models(string brandName)
        {
            return carsApi.Models(brandName);
        }

        [HttpGet]
        public CarDetails[] Selling(string brand, string model)
        {
            return carsApi.Selling(brand, model);
        }

        [HttpGet]
        public int CountPages(int brandId, int countId)
        {
            return carsApi.CountPages(brandId, countId);
        }

        [HttpGet]
        public ModelDetails MergeTo(ModelDetails sell)
        {
            return carsApi.MergeTo(sell);
        }
    }
}