﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using AvByApi;
using Domain.Api;
using WpfApplication1.Static;

namespace WpfApplication1.Dialogs
{
    /// <summary>
    ///     Логика взаимодействия для AddCarDialog.xaml
    /// </summary>
    public partial class AddCarDialog : Window
    {
        public ModelDetails ModelDetails;
        private IDictionary<string, int> brands;
        private IDictionary<string, int> models;
        private readonly ICarsApi carsApi;

        public AddCarDialog(ICarsApi carsApi)
        {
            this.carsApi = carsApi;
            InitializeComponent();
            ThreadPool.QueueUserWorkItem(LoadBrandData);
        }

        public void LoadBrandData(object obj)
        {
            brands = carsApi.Brands();
            CompleteLabel.Dispatcher.BeginInvoke(new Action(LoadStarted));
            BrandComboBox.Dispatcher.BeginInvoke(new Action(IntializeBrandComboBox));
            CompleteLabel.Dispatcher.BeginInvoke(new Action(LoadComplited));
        }

        public void LoadMarkData(object obj)
        {
            CompleteLabel.Dispatcher.BeginInvoke(new Action(LoadStarted));
            models = carsApi.Models(obj.ToString());
            BrandComboBox.Dispatcher.BeginInvoke(new Action<IDictionary<string,int>>(InitializeModelComboBox), models);
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
            foreach (var pair in carsApi.Brands())
            {
                BrandComboBox.Items.Add(pair.Key);
            }
        }

        private void InitializeModelComboBox(IDictionary<string, int> models )
        {
            ModelComboBox.Items.Clear();
            foreach (var pair in models)
            {
                ModelComboBox.Items.Add(pair.Key);
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
                    ModelDetails = new ModelDetails()
                        {
                            Brand = BrandComboBox.SelectedItem.ToString(),
                            Model = ModelComboBox.SelectedItem.ToString(),
                            BrandId = brands[BrandComboBox.SelectedItem.ToString()],
                            ModelId = models[ModelComboBox.SelectedItem.ToString()],
                        };
                    ModelDetails.Count = carsApi.CountPages(ModelDetails.BrandId, ModelDetails.ModelId);
                    DialogResult = true;
                    return;
                }
            DialogResult = false;
        }
    }
}