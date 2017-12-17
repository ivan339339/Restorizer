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

namespace Restorizer.UI.Pages
{
    /// <summary>
    /// Логика взаимодействия для SuggestionsPage.xaml
    /// </summary>
    public partial class SuggestionsPage : Page, ISectionPage
    {
        private Ingredient _currentIngredient;

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
            var service = new RecipeSearch();
            DishesListView.ItemsSource = null;
            DishesListView.ItemsSource = await service.GetResult(_currentIngredient.Name);
        }

        private void DishesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BrowseButton.IsEnabled = DishesListView.SelectedIndex != -1;
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {

            var SelectedItem = DishesListView.SelectedItem as Data.API.DTO.RecipeSearchResult;

            var url = "https://www.google.ru/search" + $"?q={SelectedItem.Title}";
            Process.Start(url);


        }
    }
}
