using Restorizer.Data.Interfaces;
using Restorizer.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restorizer.Data
{
    public delegate void MessageCallback(string heading, string content);

    public class UnitOfWork : IDisposable
    {

        Context _context = new Context();

        public IDishRepository Dishes { get; }
        public ICategoryRepository Categories { get; }
        public IOrderRepository Orders { get; }
        public IIngredientRepository Ingredients { get; }
        public ISupplyRepository Supplies { get; }

        public UnitOfWork()
        {
            Dishes = new DishRepository(_context);
            Categories = new CategoryRepository(_context);
            Orders = new OrderRepository(_context);
            Ingredients = new IngredientRepository(_context);
            Supplies = new SupplyRepository(_context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
