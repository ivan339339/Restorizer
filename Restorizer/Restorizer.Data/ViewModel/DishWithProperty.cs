using Restorizer.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restorizer.Data.ViewModel
{
    public class DishWithProperty
    {
        public Dish Dish { get; set; }
        public int Property { get; set; }
    }
}
