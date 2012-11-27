using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApplication1.Static;

namespace AvByApi.Services
{
    public interface ICarsApi
    {
        IDictionary<string, int> Brands();
        IDictionary<string, int> Models(string brandName);
        CarDetails[] Selling(string brand, string model);
        int CountPages(int brandId, int countId);
        void MergeTo(ModelDetails sell);
    }
}
