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
    /// Логика взаимодействия для EditIngredientPage.xaml
    /// </summary>
    public partial class EditIngredientPage : Page
    {
        private Ingredient _currentIngredient;

        public EditIngredientPage(Ingredient ingredient)
        {
            InitializeComponent();
            _currentIngredient = ingredient;
        }

        private void EditDishButton_Click(object sender, RoutedEventArgs e)
        {
            bool result = false;
            using (var uow = new UnitOfWork())
            {
                uow.Ingredients.MessageSent += ShowMessage;
                result = uow.Ingredients.TryEdit(_currentIngredient, NameTextBox.Text, PriceTextBox.Text);
                uow.Complete();
            }
            if (result)
            {
                NavigationService.GoBack();
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void ShowMessage(string heading, string content)
        {
            MessageBox.Show(content, heading);
        }

        private void LoadData()
        {
            NameTextBox.Text = _currentIngredient.Name;
            PriceTextBox.Text = _currentIngredient.PricePerKg.ToString();
        }
    }
}
