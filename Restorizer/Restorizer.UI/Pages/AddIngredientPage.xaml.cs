using Restorizer.Data;
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
    /// Логика взаимодействия для AddIngredienPage.xaml
    /// </summary>
    public partial class AddIngredientPage : Page
    {
        public AddIngredientPage()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void AddDishButton_Click(object sender, RoutedEventArgs e)
        {
            bool result = false;
            using (var uow = new UnitOfWork())
            {
                uow.Ingredients.MessageSent += ShowMessage;
                result = uow.Ingredients.TryAdd(NameTextBox.Text, PriceTextBox.Text);
                uow.Complete();
            }
            if (result)
            {
                NavigationService.GoBack();
            }
        }

        private void ShowMessage(string heading, string content)
        {
            MessageBox.Show(content, heading);
        }
    }
}
