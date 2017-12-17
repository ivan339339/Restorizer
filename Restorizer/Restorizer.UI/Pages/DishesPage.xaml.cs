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
    /// Логика взаимодействия для DishesPage.xaml
    /// </summary>
    public partial class DishesPage : Page, ISectionPage
    {
        public string Heading { get; } = "Dishes";

        private Dish _selectedDish = null;

        private Dish SelectedDish
        {
            set
            {
                _selectedDish = value;
                UpdateButtons();
            }
        }

        private void UpdateButtons()
        {
            var buttons = new List<Button> { EditButton, DeleteButton };
            buttons.ForEach(btn => btn.IsEnabled = _selectedDish != null);
        }

        public DishesPage()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(PagesFactory.Default.AddDishPage);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshListView();
            SelectedDish = null;
        }


        private void RefreshListView()
        {
            DishesListView.ItemsSource = null;
            DishesListView.ItemsSource = LoadDishes();
        }

        private IEnumerable<object> LoadDishes()
        {
            IEnumerable<object> result;
            using (var uow = new UnitOfWork())
            {
                result = uow.Dishes.GetByCategories();
            }
            return result;
        }

        private void DishView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedListView = sender as ListView;
            if (selectedListView.SelectedIndex == -1)
            {
                _selectedDish = null;
            }
            else
            {
                SelectedDish = selectedListView.SelectedItem as Dish;
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditDishPage(_selectedDish));
        }
    }
}
