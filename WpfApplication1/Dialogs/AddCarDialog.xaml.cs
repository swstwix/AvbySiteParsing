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
using System.Windows.Shapes;
using WpfApplication1.Static;

namespace WpfApplication1.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для AddCarDialog.xaml
    /// </summary>
    public partial class AddCarDialog : Window
    {
        public AddCarDialog()
        {
            InitializeComponent();
            InitializeBrandComboBox();
        }

        private void InitializeBrandComboBox()
        {
            foreach (var pair in AvParser.Brands())
            {
                BrandComboBox.Items.Add(pair.Key);
            }
        }

        private void BrandComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var brand = e.AddedItems[0].ToString();
            MarkComboBox.Items.Clear();
            foreach(var pair in AvParser.Models(brand))
            {
                MarkComboBox.Items.Add(pair.Key);
            }
        }

    }
}
