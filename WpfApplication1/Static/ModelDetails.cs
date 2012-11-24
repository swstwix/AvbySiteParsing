using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.Static
{
    public class ModelDetails
    {
        public int BrandId { get; set; }
        public int ModelId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Count { get; set; }

        public CarDetails[] Cars { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1}({2})", Brand, Model, Count);
        }
    }

}
