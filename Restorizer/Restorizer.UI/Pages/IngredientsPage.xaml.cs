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
            NavigationService.Navigate(PagesFactory.Default.SuggestionsPage);
        }
    }
}
