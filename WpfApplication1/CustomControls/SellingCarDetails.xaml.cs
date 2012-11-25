using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApplication1.Static;

namespace WpfApplication1.CustomControls
{
    /// <summary>
    /// Логика взаимодействия для SellingCarDetails.xaml
    /// </summary>
    public partial class SellingCarDetails : UserControl
    {
        private CarDetails car;

        public SellingCarDetails(CarDetails car)
        {
            this.car = car;
            this.DataContext = car;
            InitializeComponent();
        }

        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start(Hyperlink.NavigateUri.ToString());
        }
    }
}
