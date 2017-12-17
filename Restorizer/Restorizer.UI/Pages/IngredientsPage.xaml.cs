using Restorizer.Data;
using Restorizer.Data.Model;
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
    /// Логика взаимодействия для IngredientsPage.xaml
    /// </summary>
    public partial class IngredientsPage : Page, ISectionPage
    {

        public string Heading { get; } = "Ingredients";

        public IngredientsPage()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(PagesFactory.Default.AddIngredientsPage);

        }

        private void Suggestions_Click(object sender, RoutedEventArgs e)
        {
            if (IngredientsListView.SelectedIndex != -1)
                NavigationService.Navigate(new SuggestionsPage(IngredientsListView.SelectedItem as Ingredient));
        }

        private void RefreshListBox()
        {
            IngredientsListView.ItemsSource = null;
            IngredientsListView.ItemsSource = LoadData();
        }

        private IEnumerable<Ingredient> LoadData()
        {
            IEnumerable<Ingredient> ingredients;

            using (var uow = new UnitOfWork())
            {
                ingredients = uow.Ingredients.GetAllItems();
            }

            return ingredients;
        }

        private void IngredientsListView_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshListBox();
        }

        private void IngredientsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var buttons = new List<Button> { EditButton, SuggestionsButton };
            buttons.ForEach(b => b.IsEnabled = IngredientsListView.SelectedIndex != -1);
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (IngredientsListView.SelectedIndex != -1)
            {
                NavigationService.Navigate(new EditIngredientPage(IngredientsListView.SelectedItem as Ingredient));
            }
        }
    }
}
