using System.Collections.Generic;
using WpfApplication1.Static;

namespace Domain.Api
{
    public interface ICarsApi
    {
        IDictionary<string, int> Brands();
        IDictionary<string, int> Models(string brandName);
        CarDetails[] Selling(string brand, string model);
        int CountPages(int brandId, int countId);
        ModelDetails MergeTo(ModelDetails sell);
    }
}
