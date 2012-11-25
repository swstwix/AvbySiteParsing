﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using WpfApplication1.CustomControls;
using WpfApplication1.Dialogs;
using WpfApplication1.Static;
using WpfApplication1.Serializer;

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
            ThreadPool.QueueUserWorkItem(delegate(Object state)
                {
                    try
                    {
                        var serializer = new Serializer<List<ModelDetails>>();
                        var obj = serializer.DeSerializeObject("cars.dat");
                        EyedModels.Dispatcher.BeginInvoke(new Action(delegate
                            {
                                EyedModels.ItemsSource = obj;
                            }));
                    }
                    catch
                    {
                        return;
                    }
                });
        }

        private void AddCarMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            var dialog = new AddCarDialog {Owner = this};
            bool? res = dialog.ShowDialog();
            if (res.HasValue && res.Value)
            {
                EyedModels.Items.Add(dialog.ModelDetails);
                EyedModels.SelectedItem = dialog.ModelDetails;
            }
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
            SellingCars.ItemsSource = selected.Cars;
        }

        private void SellingCars_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
            {
                CarDetails.Children.Clear();
                CarDetails.Children.Add(new SellingCarDetails((CarDetails)e.AddedItems[0]));
            }
        }

        private void RefreshMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            foreach (var selling in EyedModels.Items)
            {
                var sell = (ModelDetails) selling;
                AvParser.MergeTo(sell);
                EyedModelsInitListBox(sell);
                EyedModels.Items.Refresh();
            }
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            var obj = EyedModels.Items;
            var list = new List<ModelDetails>();
            foreach (var item in EyedModels.Items)
                list.Add((ModelDetails)item);
            var serializer = new Serializer<List<ModelDetails>>();
            serializer.SerializeObject("cars.dat", list);
        }
    }
}