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
    /// Логика взаимодействия для AddSupplyPage.xaml
    /// </summary>
    public partial class AddSupplyPage : Page
    {
        public AddSupplyPage()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void AddSupplyButton_Click(object sender, RoutedEventArgs e)
        {
            bool result;
            using(var uow = new UnitOfWork())
            {
                uow.Supplies.MessageSent += ShowMessage;
                result = uow.Supplies.TryAdd(DatePicker.SelectedDate, IngredientComboBox.SelectedItem, AmountTextBox.Text);
                uow.Complete();
            }
            if (result)
            {
                NavigationService.GoBack();
            }
        }

        private void LoadData()
        {
            IngredientComboBox.ItemsSource = null;
            IngredientComboBox.ItemsSource = LoadIngredients();

            DatePicker.SelectedDate = DateTime.Now;
            DatePicker.DisplayDateStart = DateTime.Now;
        }

        private IEnumerable<Ingredient> LoadIngredients()
        {
            IEnumerable<Ingredient> ingredients;
            using(var uow = new UnitOfWork())
            {
                ingredients = uow.Ingredients.GetAllItems();
            }
            return ingredients;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void ShowMessage(string heading, string content)
        {
            MessageBox.Show(content, heading);
        }
    }
}
