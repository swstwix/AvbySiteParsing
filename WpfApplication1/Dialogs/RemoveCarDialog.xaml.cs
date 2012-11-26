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
    /// Interaction logic for RemoveCarDialog.xaml
    /// </summary>
    public partial class RemoveCarDialog : Window
    {
        public object CarToDelete;

        public RemoveCarDialog(ItemCollection models)
        {
            InitializeComponent();
            foreach (var model in models)
                CarComboBox.Items.Add(model);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (CarComboBox.SelectedItem != null)
            {
                CarToDelete = CarComboBox.SelectedItem;
                this.DialogResult = true;
                return;
            }
            this.DialogResult = false;
        }
    }
}
