using Restorizer.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restorizer.Data.Interfaces
{
    public interface IDishRepository : IRepository<Dish>
    {
        bool TryAdd(string name, object category, string price, List<object> ingredients);

        IEnumerable<object> GetByCategories();

        event MessageCallback MessageSent;
    }
}
