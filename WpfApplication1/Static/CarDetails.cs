using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfApplication1.Static
{
    [Serializable]
    public class CarDetails
    {
        #region Av.by data

        public string Title { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }
        public string Href { get; set; }
        public int Price    { get { return PricesHistrory.Last(); } }
        public int Volume { get; set; }
        public int KmAge { get; set; }
        public int Year { get; set; }
        public string ImageHref { get; set; }

        #endregion

        #region Statistics data

        public CarState State;
        public List<int> PricesHistrory { get; set; }
        public Brush Color { get
        {
            switch (State)
            {
                case CarState.Deleted:  return Brushes.Crimson;
                case CarState.New:      return Brushes.Chartreuse;
                case CarState.Updated:  return Brushes.Yellow;
                default:
                    return Brushes.White;
            }
        } }

        #endregion

        public override string ToString()
        {
            return Title;
        }
    }
}
