using System;
using System.Windows;
using WpfApplication1.Dialogs;

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
                EyedModels.Items.Add(String.Format("{0}:{1}", dialog.Brand, dialog.Model));
            else
                MessageBox.Show("Fail");
        }
    }
}