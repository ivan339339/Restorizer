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
using System.Windows.Shapes;

namespace Restorizer.UI.Windows
{
    public delegate void QuantityCallback(int quantity);
    /// <summary>
    /// Interaction logic for InsertQuantityWindow.xaml
    /// </summary>
    public partial class InsertAmountWindow : Window
    {

        public event QuantityCallback QuantityInserted;
         
        public InsertAmountWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        { 
            int quantity;
            if (QuantityTextBox.Text != "" && int.TryParse(QuantityTextBox.Text, out quantity) && quantity > 0)
            {
                QuantityInserted?.Invoke(quantity);
                DialogResult = true;
            }
            else
                MessageBox.Show("Incorrect input: the amount must be a positive integer", "Error");
        }
    }
}
