using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restorizer.Data.Model
{
    public class Dish
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public Category Category { get; set; }
        public List<DishHasIngredient> Ingredients { get; set; }
        public List<OrderHasDish> Orders { get; set; }
        public bool IsArchived { get; set; }

        public string Info
        {
            get
            {
                return $"{Name}: {Price} rub.";
            }
        }
    }
}
