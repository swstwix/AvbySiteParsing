using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using WpfApplication1.Dialogs;
using WpfApplication1.Static;

namespace WpfApplication1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddCarMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            var dialog = new AddCarDialog {Owner = this};
            bool? res = dialog.ShowDialog();
            if (res.HasValue && res.Value)
                MessageBox.Show(String.Format("{0},{1}", dialog.BrandId, dialog.ModelId));
            else
                MessageBox.Show("Fail");
        }
    }
}
