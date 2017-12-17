using LiveCharts;
using LiveCharts.Wpf;
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
    /// Interaction logic for GraphPage.xaml
    /// </summary>
    public partial class ChartPage : Page
    {
        public ChartPage()
        {
            InitializeComponent();
            CreateChart();
        }

        public void CreateChart()
        {
            List<DayWithRevenue> dayswithrevenues = new List<DayWithRevenue>();
            using (var uow = new UnitOfWork())
            {
                dayswithrevenues = uow.Statistics.GetDaysWithProfit();
            }
            ColumnSeries cs = new ColumnSeries() { DataLabels = true, Values = new ChartValues<int>() };
            Axis ax = new Axis() { Separator = new LiveCharts.Wpf.Separator() { Step = 1, IsEnabled = false }, Title = "Date" };
            ax.Labels = new List<string>();
            foreach (var item in dayswithrevenues)
            {
                cs.Values.Add(item.Revenue);
                ax.Labels.Add(item.Day);
            }
            RevenueChart.Series.Add(cs);
            RevenueChart.AxisX.Add(ax);
            RevenueChart.AxisY.Add(new Axis() { Title = "Revenue"});
        }
    }
}
