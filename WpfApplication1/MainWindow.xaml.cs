using System;
using System.Windows;
using System.Windows.Controls;
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
            var cars = AvParser.Selling(selected.Brand, selected.Model);
            SellingCars.Items.Clear();
            foreach (var car in cars)
                SellingCars.Items.Add(car);
        }
    }
}