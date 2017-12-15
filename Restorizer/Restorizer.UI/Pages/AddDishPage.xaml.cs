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
    /// Логика взаимодействия для AddDishPage.xaml
    /// </summary>
    public partial class AddDishPage : Page
    {

        private Dish newDish = new Dish();

        private List<Ingredient> _poolIngredients = new List<Ingredient>();

        private List<Ingredient> _selectedIngredients = new List<Ingredient>();

        public AddDishPage()
        {
            InitializeComponent();
            LoadData();
            RefreshPoolListBox();
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

        private int currentquantity;

        private void GetQuantity(int quantity)
        {
            currentquantity = quantity;
        }

        private void AddIngredientButton_Click(object sender, RoutedEventArgs e)
        {
            var insertWindow = new InsertQuantityWindow();
            insertWindow.QuantityInserted += GetQuantity;
            insertWindow.ShowDialog();
     
            var selected = PoolListBox.SelectedItem as Ingredient;
            //var dishhasingredient = new DishHasIngredient(newDish, selected, currentquantity);
            //newDish.Ingredients.Add(dishhasingredient);
            _selectedIngredients.Add(selected);
            _poolIngredients.Remove(selected);
            RefreshPoolListBox();
            RefreshSelectedListBox();
        }

        private void RemoveIngredientButton_Click(object sender, RoutedEventArgs e)
        {
            var selected = SelectedListBox.SelectedItem as Ingredient;
            DishHasIngredient deletedingredient = new DishHasIngredient();
           
            //foreach (var item in newDish.Ingredients)
            //{
            //    if (item.Ingredient == selected)
            //        deletedingredient = item;
            //    break;
            //}
            //newDish.Ingredients.Remove(deletedingredient);
            _poolIngredients.Add(selected);
            _selectedIngredients.Remove(selected);
            RefreshPoolListBox();
            RefreshSelectedListBox();
        }

        private void AddDishButton_Click(object sender, RoutedEventArgs e)
        {
            if (NameTextBox.Text != null)
                newDish.Name = NameTextBox.Text;
            else
                MessageBox.Show("Type the name");
            if (CategoryComboBox.SelectedIndex != -1)
                newDish.Category = CategoryComboBox.SelectedItem as Category;
            else
                MessageBox.Show("You should choose the category");
            using (var unitOfWork = new UnitOfWork())
            {
                unitOfWork.Dishes.Add(newDish);
                unitOfWork.Complete();
            }
        }
    }
}
