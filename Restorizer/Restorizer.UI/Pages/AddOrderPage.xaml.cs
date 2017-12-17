using Restorizer.Data;
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
    /// Логика взаимодействия для AddOrderPage.xaml
    /// </summary>
    public partial class AddOrderPage : Page
    {
        private List<Dish> _poolDishes = new List<Dish>();

        private List<object> _selectedDishes = new List<object>();

        private int _currentQuantity;

        public AddOrderPage()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            using (var uow = new UnitOfWork())
            {
                _poolDishes = uow.Dishes.GetAllActive().ToList();
            }
        }

        private void RefreshPoolListBox()
        {
            PoolListBox.ItemsSource = null;
            PoolListBox.ItemsSource = _poolDishes;
        }

        private void RefreshSelectedListBox()
        {
            SelectedListBox.ItemsSource = null;
            SelectedListBox.ItemsSource = _selectedDishes;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            bool result = false;
            using (var uow = new UnitOfWork())
            {
                result = uow.Orders.TryAdd(_selectedDishes);
                uow.Complete();
            }
            if (result)
            {
                NavigationService.GoBack();
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
            RefreshPoolListBox();
        }

        private void GetQuantity(int quantity)
        {
            _currentQuantity = quantity;
        }

        private void AddDishButton_Click(object sender, RoutedEventArgs e)
        {
            var insertWindow = new InsertQuantityWindow();
            insertWindow.QuantityInserted += GetQuantity;
            if (insertWindow.ShowDialog() ?? false)
            {
                var selectedDish = PoolListBox.SelectedItem as Dish;

                _selectedDishes.Add(new
                {
                    Dish = selectedDish,
                    Quantity = _currentQuantity,
                    Info = $"{selectedDish.Name}: {_currentQuantity} pc."
                });

                _poolDishes.Remove(selectedDish);
                RefreshPoolListBox();
                RefreshSelectedListBox();
            }
        }

        private void RemoveDishButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedObject = SelectedListBox.SelectedItem;

            var dish = selectedObject?.GetType().GetProperty("Dish")?.GetValue(selectedObject, null) as Dish;

            _poolDishes.Add(dish);
            _selectedDishes.Remove(selectedObject);
            RefreshPoolListBox();
            RefreshSelectedListBox();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
