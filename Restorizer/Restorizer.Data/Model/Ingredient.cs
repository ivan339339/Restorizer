using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restorizer.Data.Model
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<DishHasIngredient> Dishes { get; set; }
        public List<Supply> Supplies { get; set; }
        public int PricePerKg { get; set; }

    }
}
