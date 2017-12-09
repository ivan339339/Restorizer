using Restorizer.Data.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restorizer.Data
{
    public class Context : DbContext
    {
        public DbSet<Dish> Dishes { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Ingredient> Ingredients { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }



        public Context() : base("RestorizerDB")
        {

        }
    }
}
