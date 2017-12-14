using Restorizer.Data.Interfaces;
using Restorizer.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restorizer.Data.Repositories
{
    class DishRepository : Repository<Dish>, IDishRepository
    {
        public DishRepository(Context context) : base(context) { }

        public Dish GetMaxProfit()
        {
            var dish = new Dish();
            var dishes = _context.Dishes.Include("Orders");
            var dishhasorder = from d in dishes
                               select d.Orders;


            return dish;
        }

        public List<Dish> Get5LeastSold()
        {
            var dishes = _context.Dishes.Include("Orders").ToList();
            var query = (from d in dishes
                         select d.Orders);
            var getorders = from q in query
                            select q.FirstOrDefault();
            return dishes;
        }
    }
}
