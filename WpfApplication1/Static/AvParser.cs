using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;

namespace WpfApplication1.Static
{
    public class AvParser
    {
        private const string avUrl = "http://av.by";

        private static IDictionary<string, int> brands; 

        public static IDictionary<string, int> Brands()
        {
            var client = new WebClient();
            var page = client.DownloadString(avUrl);
            if (brands != null)
                return brands;
            brands = new Dictionary<string, int>();

            const string template = @"<OPTION value=""(?<value>[\d]+?)"">(?<key>[\s\S]+?)</OPTION>";
            var matches = Regex.Matches(page, template);

            for (int i = 0; i < 107; i++)
            {
                brands.Add(new KeyValuePair<string,int>(matches[i].Groups["key"].Value, int.Parse(matches[i].Groups["value"].Value)));
            }

            return brands;
        }
    }
}
