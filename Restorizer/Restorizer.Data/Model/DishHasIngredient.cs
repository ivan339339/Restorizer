using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restorizer.Data.Model
{
    public class DishHasIngredient
    {
        [Key]
        [Column(Order = 1)]
        public int DishId { get; set; }
        
        [Key]
        [Column(Order = 2)]
        public int IngredientId { get; set; }

        public Dish Dish { get; set; }

        public Ingredient Ingredient { get; set; }

        public int AmountInG { get; set; }

    }
}
