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
using System.Windows.Shapes;
using WpfApplication1.Static;

namespace WpfApplication1.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для AddCarDialog.xaml
    /// </summary>
    public partial class AddCarDialog : Window
    {
        public int BrandId;
        public int ModelId;
        private IDictionary<string, int> brands;
        private IDictionary<string, int> models;

        public AddCarDialog()
        {
            InitializeComponent();
            ThreadPool.QueueUserWorkItem(LoadBrandData);

        }

        public void LoadBrandData(object obj)
        {
            brands = AvParser.Brands();
            CompleteLabel.Dispatcher.BeginInvoke(new Action(LoadStarted));
            BrandComboBox.Dispatcher.BeginInvoke(new Action(IntializeBrandComboBox));
            CompleteLabel.Dispatcher.BeginInvoke(new Action(LoadComplited));
        }

        public void LoadMarkData(object obj)
        {
            CompleteLabel.Dispatcher.BeginInvoke(new Action(LoadStarted));
            models = AvParser.Models(obj.ToString());
            BrandComboBox.Dispatcher.BeginInvoke(new Action(delegate
                {
                    ModelComboBox.Items.Clear();
                    foreach (var pair in models)
                    {
                        ModelComboBox.Items.Add(pair.Key);
                    }
                }));
            CompleteLabel.Dispatcher.BeginInvoke(new Action(LoadComplited));
        }

        private void LoadStarted()
        {
            CompleteLabel.Content = "Wait...";
        }

        private void LoadComplited()
        {
            CompleteLabel.Content = "Complited";
        }

        private void IntializeBrandComboBox()
        {
            foreach (var pair in AvParser.Brands())
            {
                BrandComboBox.Items.Add(pair.Key);
            }
        }

        private void BrandComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ThreadPool.QueueUserWorkItem(LoadMarkData, e.AddedItems[0]);
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            if (BrandComboBox.SelectedItem != null)
                if (ModelComboBox.SelectedItem != null)
                {
                    BrandId = brands[BrandComboBox.SelectedItem.ToString()];
                    ModelId = models[ModelComboBox.SelectedItem.ToString()];
                    this.DialogResult = true;
                    return;
                }
            this.DialogResult = false;
        }

    }
}
