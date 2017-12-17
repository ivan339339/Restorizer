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
    /// Логика взаимодействия для EditDishPage.xaml
    /// </summary>
    public partial class EditDishPage : Page
    {
        private Dish _currentDish;

        private List<Ingredient> _poolIngredients = new List<Ingredient>();

        private List<DishHasIngredient> _selectedIngredients = new List<DishHasIngredient>();

        private int _currentQuantity;

        public EditDishPage(Dish dish)
        {
            _currentDish = dish;
            InitializeComponent();
        }

        private void LoadData()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                CategoryComboBox.ItemsSource = unitOfWork.Categories.GetAllItems();
                _poolIngredients = unitOfWork.Ingredients.GetIngredientDifference(_currentDish.Ingredients).ToList();
                _selectedIngredients = _currentDish.Ingredients;
            }

            RefreshPoolListBox();
            RefreshSelectedListBox();

            NameTextBox.Text = _currentDish.Name;
            PriceTextBox.Text = _currentDish.Price.ToString();

            foreach (var item in CategoryComboBox.Items)
            {
                if ((item as Category).Id == _currentDish.Category.Id)
                {
                    CategoryComboBox.SelectedItem = item;
                    break;
                }
            }
        }

        private void EditDishButton_Click(object sender, RoutedEventArgs e)
        {
            bool result = false;
            using (var uow = new UnitOfWork())
            {
                uow.Dishes.MessageSent += ShowMessage;
                result = uow.Dishes.TryEdit(_currentDish, NameTextBox.Text, CategoryComboBox.SelectedItem, PriceTextBox.Text, _selectedIngredients);
                uow.Complete();
            }
            if (result)
                NavigationService.GoBack();
        }

        private void AddIngredientButton_Click(object sender, RoutedEventArgs e)
        {
            var insertWindow = new InsertAmountWindow();
            insertWindow.QuantityInserted += GetQuantity;
            insertWindow.ShowDialog();

            var selectedIngredient = PoolListBox.SelectedItem as Ingredient;

            _selectedIngredients.Add(new DishHasIngredient
            {
                //DishId = _currentDish.Id,
                //IngredientId = selectedIngredient.Id,
                //Dish = _currentDish,
                Ingredient = selectedIngredient,
                AmountInG = _currentQuantity
            });

            _poolIngredients.Remove(selectedIngredient);
            RefreshPoolListBox();
            RefreshSelectedListBox();
        }

        private void RemoveIngredientButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedObject = SelectedListBox.SelectedItem as DishHasIngredient;

            var ingredient = selectedObject?.GetType().GetProperty("Ingredient")?.GetValue(selectedObject, null) as Ingredient;

            _poolIngredients.Add(ingredient);
            _selectedIngredients.Remove(selectedObject);
            RefreshPoolListBox();
            RefreshSelectedListBox();
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

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void GetQuantity(int quantity)
        {
            _currentQuantity = quantity;
        }

        private void ShowMessage(string heading, string content)
        {
            MessageBox.Show(content, heading);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
