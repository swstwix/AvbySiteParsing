using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using Domain.Api;
using WpfApplication1.Static;

namespace WpfApplication1
{
    public class AppHarborApi : ICarsApi 
    {
        public IDictionary<string, int> Brands()
        {
            var html = new WebClient() { Encoding = Encoding.UTF8 }.DownloadString("http://carswebapi.apphb.com/Cars/Brands");
            var model = Json.Decode<Dictionary<string, int>>(html);
            return model; 
        }

        public IDictionary<string, int> Models(string brandName)
        {
            var html = new WebClient() { Encoding = Encoding.UTF8 }.DownloadString(string.Format("http://carswebapi.apphb.com/Cars/Models?brandName={0}", brandName));
            var model = Json.Decode<Dictionary<string, int>>(html);
            return model; 
        }

        public CarDetails[] Selling(string brand, string model)
        {
            var html = new WebClient() { Encoding = Encoding.UTF8 }.DownloadString(string.Format("http://carswebapi.apphb.com/Cars/Selling?brand={0}&model={1}", brand, model));
            var modell = Json.Decode<CarDetails[]>(html);
            return modell;
        }

        public int CountPages(int brandId, int countId)
        {
            throw new NotImplementedException();
        }

        public ModelDetails MergeTo(ModelDetails sell)
        {
            throw new NotImplementedException();
        }
    }
}
