using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restorizer.Data.Model
{
    public class Supply
    {
        [Key]
        [Column(Order = 1)]
        public int SupplierId { get; set; }

        [Key]
        [Column(Order = 2)]
        public int IngredientId { get; set; }

        public Supplier Supplier { get; set; }

        public Ingredient Ingredient { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime Date { get; set; }

        public int Amount { get; set; }

        public int PricePerKg { get; set; }
    }
}
