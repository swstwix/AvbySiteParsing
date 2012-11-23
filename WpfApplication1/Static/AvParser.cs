using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;

namespace WpfApplication1.Static
{
    public class AvParser
    {
        private const string avUrl = "http://av.by";

        private const string avModelPattern =
            "http://av.by/public/search.php?event=Search&category_parent%5B0%5D={0}&category_id%5B0%5D=0&year_id=0&year_id_max=0&engine_type_id2=1&engine_type_all=1&body_type_id=0&transmission_id=0&price_value=0&price_value_max=0&currency_id=0&country_id=0&city_id2=0&order_id=0&submit_presearch=%CF%EE%EA%E0%E7%E0%F2%FC%3A+9";

        private static IDictionary<string, int> brands;

        public static IDictionary<string, int> Brands()
        {
            var client = new WebClient();
            string page = client.DownloadString(avUrl);
            if (brands != null)
                return brands;
            brands = new Dictionary<string, int>();

            string pagePart = Regex.Match(page, @"<OPTION value=""1"">Alfa[\s\S]*Урал</OPTION>").ToString();
            const string template = @"<OPTION value=""(?<value>[\d]+?)"">(?<key>[\s\S]+?)</OPTION>";
            MatchCollection matches = Regex.Matches(pagePart, template);

            for (int i = 0; i < matches.Count; i++)
            {
                brands.Add(new KeyValuePair<string, int>(matches[i].Groups["key"].Value,
                                                         int.Parse(matches[i].Groups["value"].Value)));
            }

            return brands;
        }

        public static IDictionary<string, int> Models(string brandName)
        {
            var client = new WebClient();
            IDictionary<string, int> models = new Dictionary<string, int>();
            string page = client.DownloadString(string.Format(avModelPattern, Brands()[brandName]));
            string pagePart = Regex.Match(page, @"Модель:</td>[\W]*[\s\S]*Ещё транспорт для поиска?").ToString();
            const string template = @"<OPTION value=""(?<value>[\d]+?)"">(?<key>[\s\S]+?)</OPTION>";
            MatchCollection matches = Regex.Matches(pagePart, template);

            for (int i = 0; i < matches.Count; i++)
            {
                models.Add(new KeyValuePair<string, int>(matches[i].Groups["key"].Value,
                                                         int.Parse(matches[i].Groups["value"].Value)));
            }
            return models;
        }
    }
}