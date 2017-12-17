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
    /// Логика взаимодействия для DishViewPage.xaml
    /// </summary>
    public partial class DishViewPage : Page
    {
        private Dish _currentDish;


        public DishViewPage(Dish dish)
        {
            InitializeComponent();
            _currentDish = dish;
        }

        private void LoadData()
        {
            DishNameTextBlock.Text = _currentDish.Name;
            RefreshListBox();
        }

        private void RefreshListBox()
        {
            IngredientsListBox.ItemsSource = null;
            IngredientsListBox.ItemsSource = _currentDish.Ingredients;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
