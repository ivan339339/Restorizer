using Restorizer.Data.API.DTO;
using Restorizer.Data.API;
using Restorizer.Data.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Restorizer.Data.Interfaces;

namespace Restorizer.UI.Pages
{
    /// <summary>
    /// Логика взаимодействия для SuggestionsPage.xaml
    /// </summary>
    public partial class SuggestionsPage : Page, ISectionPage
    {
        private Ingredient _currentIngredient;

        private IRecipeSearch _service;

        public string Heading { get; } = "Suggestions";

        public string IngredientName { get; set; }

        public SuggestionsPage(Ingredient ingredient)
        {
            InitializeComponent();
            _currentIngredient = ingredient;

            SetSuggestions();

        }


        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private async void SetSuggestions()
        {
            _service = new RecipeSearch();
            DishesListView.ItemsSource = null;
            DishesListView.ItemsSource = await _service.GetResult(_currentIngredient.Name);
        }

        private void DishesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var buttons = new List<Button> { BrowseButton, GoButton };
            buttons.ForEach(b => b.IsEnabled = DishesListView.SelectedIndex != -1);
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {

            var SelectedItem = DishesListView.SelectedItem as Data.API.DTO.RecipeSearchResult;

            var url = "https://www.google.ru/search" + $"?q={SelectedItem.Title.Replace("&", "and")}";
            Process.Start(url);


        }

        private void GoButton_Click(object sender, RoutedEventArgs e)
        {
            if (DishesListView.SelectedIndex != -1)
            {
                var selectedSearchResult = DishesListView.SelectedItem;
                var title = selectedSearchResult?.GetType().GetProperty("Title")?.GetValue(selectedSearchResult, null) as string;
                PagesFactory.Default.AddDishPage.PreloadName(title);
                NavigationService.Navigate(PagesFactory.Default.AddDishPage);
            }
        }
    }
}
