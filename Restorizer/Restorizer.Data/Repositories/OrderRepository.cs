using Restorizer.Data.Interfaces;
using Restorizer.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restorizer.Data.Repositories
{
    class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(Context context) : base(context) { }

        public event MessageCallback MessageSent;

        public bool TryAdd(List<Object> dishes)
        {
            if (dishes != null && dishes.Count > 0)
            {
                var newOrder = new Order
                {
                    Date = DateTime.Now,
                    Dishes = new List<OrderHasDish>()
                };

                foreach (var dish in dishes)
                {
                    var dishObject = dish?.GetType().GetProperty("Dish")?.GetValue(dish, null) as Dish;
                    var dishInDB = _context.Dishes.FirstOrDefault(d => d.Id == dishObject.Id);

                    newOrder.Dishes.Add(new OrderHasDish
                    {
                        DishId = dishInDB.Id,
                        Dish = dishInDB,
                        Quantity = (int)dish?.GetType().GetProperty("Quantity")?.GetValue(dish, null)
                    });
                }

                Add(newOrder);
                return true;
            }
            else
            {
                MessageSent?.Invoke("Error!", "No dishes were selected!");
                return false;
            }
        }

        public IEnumerable<Order> GetWithDishes()
        {
            return _context.Orders.Include("Dishes").Include("Dishes.Dish").ToList();
        }

    }
}
