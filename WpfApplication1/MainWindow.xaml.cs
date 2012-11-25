using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using WpfApplication1.CustomControls;
using WpfApplication1.Dialogs;
using WpfApplication1.Static;

namespace WpfApplication1
{
    /// <summary>
    ///     Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ThreadPool.QueueUserWorkItem(delegate(Object state)
                {
                    var x = AvParser.Brands();
                });
        }

        private void AddCarMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            var dialog = new AddCarDialog {Owner = this};
            bool? res = dialog.ShowDialog();
            if (res.HasValue && res.Value)
                EyedModels.Items.Add(dialog.ModelDetails);
        }

        private void EyedModels_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = (ModelDetails) e.AddedItems[0];
            ThreadPool.QueueUserWorkItem(LoadSellingForModel, selected);
        }

        private void LoadSellingForModel(object obj)
        {
            var selected = (ModelDetails) obj;
            if (selected.Cars == null)
                selected.Cars = AvParser.Selling(selected.Brand, selected.Model);
            SellingCars.Dispatcher.BeginInvoke(new Action<ModelDetails>(EyedModelsInitListBox), selected);
        }

        private void EyedModelsInitListBox(ModelDetails selected)
        {
            SellingCars.Items.Clear();
            foreach (var car in selected.Cars)
                SellingCars.Items.Add(car);
        }

        private void SellingCars_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
            {
                CarDetails.Children.Clear();
                CarDetails.Children.Add(new SellingCarDetails((CarDetails)e.AddedItems[0]));
            }
        }
    }
}