using Restorizer.Data.Model;
using Restorizer.Data.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restorizer.Data.Interfaces
{
    public interface IIngredientRepository : IRepository<Ingredient>
    {
        List<IngredientTotalAmount> Get5LeastPopularIngredient();
    }
}
