using Restorizer.Data;
using Restorizer.Data.Model;
using System;
using System.Collections;
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
    /// Логика взаимодействия для SuppliesPage.xaml
    /// </summary>
    public partial class SuppliesPage : Page, ISectionPage
    {

        public string Heading { get; } = "Supplies";

        public SuppliesPage()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new AddSupplyPage());

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshListView();
        }

        private void RefreshListView()
        {
            SuppliesListView.ItemsSource = null;
            SuppliesListView.ItemsSource = LoadData();
        }

        private IEnumerable<Supply> LoadData()
        {
            IEnumerable<Supply> supplies;
            using(var uow = new UnitOfWork())
            {
                supplies = uow.Supplies.GetWithIngredients();
            }
            return supplies;
        }

    }
}