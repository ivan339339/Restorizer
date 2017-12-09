using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restorizer.Data.Model
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public List<OrderHasDish> Dishes { get; set; }
    }
}
