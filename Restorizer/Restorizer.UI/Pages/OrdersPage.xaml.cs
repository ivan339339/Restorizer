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
    /// Логика взаимодействия для OrdersPage.xaml
    /// </summary>
    public partial class OrdersPage : Page, ISectionPage
    {

        public string Heading { get; } = "Orders";

        public OrdersPage()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddOrderPage());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshListView();
        }

        private void RefreshListView()
        {
            OrdersListView.ItemsSource = null;
            OrdersListView.ItemsSource = LoadData();
        }

        private IEnumerable<Order> LoadData()
        {
            IEnumerable<Order> orders;
            using (var uow = new UnitOfWork())
            {
                orders = uow.Orders.GetWithDishes();
            }
            return orders;
        }

        private void ViewButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
