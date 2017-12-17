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
                return _default ?? (_default = new PagesFactory());
            }
        }

        // PAGES:

        // Dishes branch:

        private Pages.DishesPage _dishesPage = new Pages.DishesPage();

        public Pages.DishesPage DishesPage
        {
            get { return _dishesPage; }
        }

        private Pages.AddDishPage _addDishPage = new Pages.AddDishPage();

        public Pages.AddDishPage AddDishPage
        {
            get { return _addDishPage; }
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

        private Pages.AddSupplyPage _addSupplyPage = new Pages.AddSupplyPage();

        public Pages.AddSupplyPage AddSupplyPage
        {
            get { return _addSupplyPage; }
        }


        // Ingredients branch:

        private Pages.IngredientsPage _ingredientsPage = new Pages.IngredientsPage();

        public Pages.IngredientsPage IngredientsPage
        {
            get { return _ingredientsPage; }
        }

        private Pages.AddIngredientPage _addIngredientsPage = new Pages.AddIngredientPage();

        public Pages.AddIngredientPage AddIngredientsPage
        {
            get { return _addIngredientsPage; }
        }

    }
}
