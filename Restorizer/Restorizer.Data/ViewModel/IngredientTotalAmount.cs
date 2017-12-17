using Restorizer.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restorizer.Data.ViewModel
{
    public class IngredientTotalAmount
    {
        public Ingredient Ingr { get; set; }
        public int TotalQuantity { get; set; }
        public string Info
        {
            get
            {
                return $"{Ingr.Name}: {TotalQuantity} gr.";
            }
        }
    }
}
