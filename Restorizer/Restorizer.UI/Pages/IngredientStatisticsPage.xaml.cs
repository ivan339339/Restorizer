using Restorizer.Data;
using Restorizer.Data.ViewModel;
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
    /// Interaction logic for IngredientStatisticsPage.xaml
    /// </summary>
    public partial class IngredientStatisticsPage : Page
    {
        public IngredientStatisticsPage()
        {
            InitializeComponent();
        }

        private void ButtonShow_Click(object sender, RoutedEventArgs e)
        {
            using(var uow = new UnitOfWork())
            {
                IngredientListBox.ItemsSource = uow.Ingredients.Get5LeastPopularIngredient();
            }
        }
    }
}
