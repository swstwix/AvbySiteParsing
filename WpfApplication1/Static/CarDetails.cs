using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.Static
{
    public class CarDetails
    {
        public string Title { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }
        public string Href { get; set; }
        public int Price { get; set; }
        public int Volume { get; set; }
        public int KmAge { get; set; }
        public int Year { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}
