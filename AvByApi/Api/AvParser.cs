﻿using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Domain.Api;
using WpfApplication1.Static;

namespace AvByApi
{

    public class AvParser : ICarsApi
    {
        private const string avUrl = "http://av.by/?event=Show_Main";
        private const string avModelPattern =
            "http://av.by/public/search.php?event=Search&category_parent%5B0%5D={0}&category_id%5B0%5D=0&year_id=0&year_id_max=0&engine_type_id2=1&engine_type_all=1&body_type_id=0&transmission_id=0&price_value=0&price_value_max=0&currency_id=0&country_id=0&city_id2=0&order_id=0&submit_presearch=%CF%EE%EA%E0%E7%E0%F2%FC%3A+9";

        private const string avSellingPattern =
            "http://av.by/public/search.php?event=Search&category_parent%5B0%5D={0}&category_id%5B0%5D={1}&year_id=0&year_id_max=0&engine_type_id2=1&engine_type_all=1&body_type_id=0&transmission_id=0&price_value=0&price_value_max=0&currency_id=0&country_id=0&city_id2=0&order_id=0&submit_presearch=%CF%EE%EA%E0%E7%E0%F2%FC%3A+9&page={2}";

        private const string avCountTemplate =
            "http://av.by/public/parameters.php?event=Number_PreSearch&category_parent[0]={0}&category_id[0]={1}";

        WebClient client = new WebClient() { Encoding = Encoding.GetEncoding("windows-1251") };

        private IDictionary<string, int> brands;

        public  IDictionary<string, int> Brands()
        {
            if (brands != null)
                return brands;

            string page = client.DownloadString(avUrl);

            brands = ParsingRegexHelper.AllBrand(page);

            return brands;
        }

        public  IDictionary<string, int> Models(string brandName)
        {
            string page = client.DownloadString(string.Format(avModelPattern, Brands()[brandName]));

            return ParsingRegexHelper.AllModels(page);
        }

        public  CarDetails[] Selling(string brand, string model)
        {
            var brandId = Brands()[brand];
            var modelId = Models(brand)[model];
            var list = new List<CarDetails>();
            var currentPage = 1;
            var countPages = CountPages(brandId, modelId);

            do
            {
                var carDetails = GetCarDetailsByUrl(string.Format(avSellingPattern, brandId, modelId, currentPage));
                list.AddRange(carDetails);
                currentPage++;
            } while(list.Count < countPages);

            return list.ToArray();
        }

        public  int CountPages(int brandId, int countId)
        {
            var page = client.DownloadString(string.Format(avCountTemplate, brandId, countId));
            return int.Parse(page);
        }

        private  IEnumerable<CarDetails> GetCarDetailsByUrl(string url)
        {
            var list = new List<CarDetails>();
            var html = client.DownloadString(url);

            var count = ParsingRegexHelper.Count(html);

            var names = ParsingRegexHelper.Names(html);
            var brands = ParsingRegexHelper.Brands(html);
            var titles = ParsingRegexHelper.Titles(html);
            var hrefs = ParsingRegexHelper.Hrefs(html);
            var years = ParsingRegexHelper.Years(html);
            var volumes = ParsingRegexHelper.Volumes(html);
            var prices = ParsingRegexHelper.Prices(html);
            var kmAges = ParsingRegexHelper.KmAges(html);
            var imageHrefs = ParsingRegexHelper.ImageHrefs(html);

            for (int i = 0; i < count; i++)
                list.Add(new CarDetails()
                {
                    Brand = brands[i],
                    Name = names[i],
                    Year = years[i],
                    Volume = volumes[i],
                    KmAge = kmAges[i],
                    Href = hrefs[i],
                    Title = titles[i],
                    ImageHref = imageHrefs[i],
                    State = CarState.New,
                    PricesHistrory = new List<int>(){prices[i]}
                });

            return list;
        }

        public  ModelDetails MergeTo(ModelDetails sell)
        {
            sell.Cars = sell.Cars.Where(x => x.State != CarState.Deleted).ToArray();

            var newSelling = Selling(sell.Brand, sell.Model);

            //// Test Green
            /*var l1 = newSelling.ToList();
            l1.Add(new CarDetails(){Title = "Hellop", Href = "fedos", PricesHistrory = new List<int>(){3}});
            newSelling = l1.ToArray();*/

            //// Test Yellow
            //newSelling[0].PricesHistrory.RemoveAll(x => true);
            //newSelling[0].PricesHistrory.Add(1234);

            //// Test Red
            //var l = newSelling.ToList();
            //l.RemoveAt(1);
            //newSelling = l.ToArray();

            var previosCarsData = sell.Cars.ToDictionary(x => x.Href, y=> y);
            var newCarsData = newSelling.ToDictionary(x => x.Href, y => y);
            
            var newSell = from x in newSelling
                            where !previosCarsData.ContainsKey(x.Href)
                            select x;
            var priceUpdatedSell =  from x in newSelling
                                        where previosCarsData.ContainsKey(x.Href) && previosCarsData[x.Href].Price != x.Price
                                        select previosCarsData[x.Href];
            var deletedNewCell =    from x in sell.Cars
                                        where !newCarsData.ContainsKey(x.Href)
                                        select x;

            foreach (var car in sell.Cars)
                car.State = CarState.NotUpdated;
            foreach (var car in priceUpdatedSell)
            {
                car.PricesHistrory.Add(newCarsData[car.Href].Price);
                car.State = CarState.Updated;
            }
            foreach (var car in deletedNewCell)
            {
                car.State = CarState.Deleted;
            }
            var newArray = newSell.ToList();
            newArray.AddRange(sell.Cars);
            sell.Cars = newArray.ToArray();
            sell.Count = sell.Cars.Length;

            return sell;
        }
    }
}