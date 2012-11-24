using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WpfApplication1.Static
{
    public class ParsingRegexHelper
    {
        public static int Count(string page)
        {
            return Regex.Matches(page, @". <b>[^<]*").Count;
        }

        public static string[] Names(string page)
        {
            var matches = Regex.Matches(page, @". <b>[^<]*");
            var ans = new string[matches.Count];
            Func<string, string> convert = x => Regex.Match(x.Split('\n')[2], @"([\w-\(\)]+\s)*[\w-\(\)]+").ToString();
            for (int i = 0; i < matches.Count; i++)
            {
                ans[i] = convert(matches[i].ToString());
            }
            return ans;
        }

        public static string[] Brands(string page)
        {
            var matches = Regex.Matches(page, @". <b>[^<]*");
            var ans = new string[matches.Count];
            Func<string, string> convert = x => Regex.Match(x.Split('\n')[1], @"([\w-\(\)]+\s)*[\w-\(\)]+").ToString();
            for (int i = 0; i < matches.Count; i++)
            {
                ans[i] = convert(matches[i].ToString());
            }
            return ans;
        }

        public static int[] Years(string page)
        {
            var matches = Regex.Matches(page, @"\d{4}\W+г\.в\.");
            Func<string, int> convert = x => int.Parse(x.Substring(0, 4));
            var res = new int[matches.Count];
            for (int i = 0; i < matches.Count; i++)
                res[i] = convert(matches[i].ToString());
            return res;
        }

        public static int[] Volumes(string page)
        {
            var matches = Regex.Matches(page, @"объем\W+\d+");
            Func<string, int> convert = x => int.Parse(Regex.Match(x, @"\d+").ToString());
            var res = new int[matches.Count];
            for (int i = 0; i < matches.Count; i++)
                res[i] = convert(matches[i].ToString());
            return res;
        }

        public static int[] KmAges(string page)
        {
            var matches = Regex.Matches(page, @"пробег\W+\d+");
            Func<string, int> convert = x => int.Parse(Regex.Match(x, @"\d+").ToString());
            var res = new int[matches.Count];
            for (int i = 0; i < matches.Count; i++)
                res[i] = convert(matches[i].ToString());
            return res;
        }

        public static int[] Prices(string html)
        {
            var matches = Regex.Matches(html, @"([$€]|Br) \d+");
            Func<string, int> convert = x =>
            {
                double k;
                if (x[0] == '$')
                    k = 1;
                else if (x[0] == '€')
                    k = 1.2815;
                else
                    k = 1 / 8600;

                return (int)(double.Parse(Regex.Match(x, @"\d+").ToString()) * k);
            };
            var res = new int[matches.Count];
            for (int i = 0; i < matches.Count; i++)
                res[i] = convert(matches[i].ToString());
            return res;
        }

        public static string[] Hrefs(string html)
        {
            List<string> list = new List<string>();
            var matches = Regex.Matches(html, @"public.php\?event=View&public_id=\d*");
            foreach (var match in matches)
            {
                list.Add(string.Concat("http://av.by/public/", match.ToString()));
            }
            return list.Where((x, i) => i%2 == 0).ToArray();
        }

        public static string[] Titles(string html)
        {
            var list = new List<string>();
            var matches = Regex.Matches(html, @"</b>. <b>(?<Title>[^<]*)");
            for (int i = 0; i < matches.Count; i++)
            {
                var title = matches[i].Groups["Title"].ToString();
                var formattedTitle = title.Replace("\n", "").Replace("\r", "");
                list.Add(Regex.Replace(formattedTitle, " +", " "));
            }
            return list.ToArray();
        }

        public static string[] ImageHrefs(string html)
        {
            var matches = Regex.Matches(html, @"http://static.av.by/public[^""]+.jpg");
            var list = new List<string>();
            for (int i = 0; i < matches.Count; i++)
                list.Add(matches[i].ToString());
            return list.ToArray();
        }
    }
}
