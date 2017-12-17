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

namespace Restorizer.UI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DishesButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(PagesFactory.Default.DishesPage);
        }

        private void OrdersButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(PagesFactory.Default.OrdersPage);
        }

        private void SuppliesButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(PagesFactory.Default.SuppliesPage);
        }

        private void IngredientsButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(PagesFactory.Default.IngredientsPage);
        }

        private void DashboardButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(PagesFactory.Default.DashBoardPage);
        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            if (MainFrame.Content is ISectionPage)
            {
                var sectionPage = MainFrame.Content as ISectionPage;
                if (sectionPage != null)
                {
                    SectionNameLabel.Content = sectionPage.Heading;
                }
            }
        }
    }
}
