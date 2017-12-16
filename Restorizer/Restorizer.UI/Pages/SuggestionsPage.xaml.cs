using Restorizer.Data.DTO;
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
    /// Логика взаимодействия для SuggestionsPage.xaml
    /// </summary>
    public partial class SuggestionsPage : Page, ISectionPage
    {

        public string Heading { get; } = "Suggestions";
        public string Title { get; set; }

        public SuggestionsPage(string title)
        {

            Title = title;

            InitializeComponent();

        }


        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private async void SetSuggestions()
        {

            var service = new RecipeSearch();

            DishesListView.ItemsSource = await service.GetResult(Title);

        }

    }
}
