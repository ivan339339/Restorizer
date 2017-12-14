using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restorizer.UI
{
    class PagesFactory
    {

        // CLASS LOGIC:

        private static PagesFactory _default;

        public static PagesFactory Default
        {
            get
            {
                if (_default == null)
                {
                    _default = new PagesFactory();
                }
                return _default;
            }
        }

        // PAGES:

        // Dishes branch:

        private Pages.DishesPage _dishesPage = new Pages.DishesPage();

        public Pages.DishesPage DishesPage
        {
            get { return _dishesPage; }
        }

        // Orders branch:

        private Pages.OrdersPage _ordersPage = new Pages.OrdersPage();

        public Pages.OrdersPage OrdersPage
        {
            get { return _ordersPage; }
        }

        // Supplies branch:

        private Pages.SuppliesPage _suppliesPage = new Pages.SuppliesPage();

        public Pages.SuppliesPage SuppliesPage
        {
            get { return _suppliesPage; }
        }
    }
}
