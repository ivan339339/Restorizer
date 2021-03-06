﻿using Restorizer.Data;
using Restorizer.Data.Model;
using Restorizer.UI.Windows;
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

namespace Restorizer.UI.Pages
{

    /// <summary>
    /// Логика взаимодействия для AddDishPage.xaml
    /// </summary>
    public partial class AddDishPage : Page, ISectionPage
    {

        private List<Ingredient> _poolIngredients = new List<Ingredient>();

        private List<Object> _selectedIngredients = new List<Object>();

        private string _preloadedName;

        private int _currentQuantity;

        public string Heading { get; } = "Dishes";

        public AddDishPage()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                CategoryComboBox.ItemsSource = unitOfWork.Categories.GetAllItems();
                _poolIngredients = unitOfWork.Ingredients.GetAllItems().ToList();
            }
        }

        private void RefreshPoolListBox()
        {
            PoolListBox.ItemsSource = null;
            PoolListBox.ItemsSource = _poolIngredients;
        }

        private void RefreshSelectedListBox()
        {
            SelectedListBox.ItemsSource = null;
            SelectedListBox.ItemsSource = _selectedIngredients;
        }

        private void GetQuantity(int quantity)
        {
            _currentQuantity = quantity;
        }

        private void AddIngredientButton_Click(object sender, RoutedEventArgs e)
        {
            var insertWindow = new InsertAmountWindow();
            insertWindow.QuantityInserted += GetQuantity;
            if (insertWindow.ShowDialog() ?? false)
            {

                var selectedIngredient = PoolListBox.SelectedItem as Ingredient;

                _selectedIngredients.Add(new
                {
                    Ingredient = selectedIngredient,
                    Amount = _currentQuantity,
                    Info = $"{selectedIngredient.Name}: {_currentQuantity} g."
                });

                _poolIngredients.Remove(selectedIngredient);
                RefreshPoolListBox();
                RefreshSelectedListBox();
            }
        }

        private void RemoveIngredientButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedObject = SelectedListBox.SelectedItem;

            var ingredient = selectedObject?.GetType().GetProperty("Ingredient")?.GetValue(selectedObject, null) as Ingredient;

            _poolIngredients.Add(ingredient);
            _selectedIngredients.Remove(selectedObject);
            RefreshPoolListBox();
            RefreshSelectedListBox();
        }

        private void AddDishButton_Click(object sender, RoutedEventArgs e)
        {
            bool result = false;
            using(var uow = new UnitOfWork())
            {
                uow.Dishes.MessageSent += ShowMessage;
                result = uow.Dishes.TryAdd(NameTextBox.Text, CategoryComboBox.SelectedItem, PriceTextBox.Text, _selectedIngredients);  
                uow.Complete();
            }
            if (result)
            {
                _preloadedName = null;
                NavigationService.GoBack();
            }
        }

        private void ShowMessage(string heading, string content)
        {
            MessageBox.Show(content, heading);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            _preloadedName = null;
            NavigationService.GoBack();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
            ClearForm();
            RefreshPoolListBox();
            RefreshSelectedListBox();
            if (_preloadedName != null)
            {
                NameTextBox.Text = _preloadedName;
            }
        }

        private void ClearForm()
        {
            if (_preloadedName == null)
                NameTextBox.Text = "";
            PriceTextBox.Text = "";
            _selectedIngredients = new List<object>();
        }

        public void PreloadName(string name)
        {
            _preloadedName = name;
        }
    }
}
