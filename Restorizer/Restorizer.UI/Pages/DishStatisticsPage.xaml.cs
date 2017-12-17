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
    /// Interaction logic for DishStatisticsPage.xaml
    /// </summary>
    public partial class DishStatisticsPage : Page
    {
        public DishStatisticsPage()
        {
            InitializeComponent();
            CreateComboBoxItems();
        }

        List<DishWithProperty> disheswithproperties = new List<DishWithProperty>();

        private void CreateComboBoxItems()
        {
            List<string> items = new List<string>() { "Show the product with the biggest revenue", "Show 5 products that were sold less times" };
            ReportComboBox.ItemsSource = items;
        }

        private void ButtonShow_Click(object sender, RoutedEventArgs e)
        {
            disheswithproperties.Clear();
            if (ReportComboBox.SelectedItem != null)
            {
                if (ReportComboBox.SelectedIndex == 0)
                {
                   using(var uow = new UnitOfWork())
                    {
                        disheswithproperties.Add(uow.Dishes.GetMaxRevenue());
                    }
                };
                if(ReportComboBox.SelectedIndex == 1)
                {
                    using (var uow = new UnitOfWork())
                    {
                        disheswithproperties = uow.Dishes.Get5LeastSold();
                    }
                }
                RefreshListBox();
            }
            else
                MessageBox.Show("You need to select the request");
        }

        private void RefreshListBox()
        {
            DishListBox.ItemsSource = null;
            DishListBox.ItemsSource = disheswithproperties;
        }
    }
}
