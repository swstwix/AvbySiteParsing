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

        public IDictionary<string, int> Brands()
        {
            return carsApi.Brands();
        }

        public IDictionary<string, int> Models(string brandName)
        {
            return carsApi.Models(brandName);
        }

        public CarDetails[] Selling(string brand, string model)
        {
            return carsApi.Selling(brand, model);
        }

        public int CountPages(int brandId, int countId)
        {
            return carsApi.CountPages(brandId, countId);
        }

        public ModelDetails MergeTo(ModelDetails sell)
        {
            return carsApi.MergeTo(sell);
        }
    }
}