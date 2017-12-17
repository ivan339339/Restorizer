using Restorizer.Data.Interfaces;
using Restorizer.Data.ViewModel;
using Restorizer.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restorizer.Data.Logic
{
    public class StatisticsLogic : IStatisticsLogic
    {
        private Context _context;

        public StatisticsLogic(Context context)
        {
            _context = context;
        }

        public List<DayWithRevenue> GetDaysWithProfit()
        {
            var allorders = _context.Orders.Include("Dishes").Include("Dishes.Dish").ToList();
            var groupedorders = from o in allorders
                                group o by o.Date;

            List<DayWithRevenue> result = new List<DayWithRevenue>();

            foreach (var ordersinday in groupedorders)
            {
                int revenue = 0;
                foreach (var order in ordersinday)
                {
                    foreach (var dish in order.Dishes)
                    {
                        revenue += dish.Quantity * dish.Dish.Price;
                    }
                }
                result.Add(new DayWithRevenue { Day = ordersinday.Key.ToShortDateString(), Revenue = revenue });
            }
            return result;
        }

    }
}
