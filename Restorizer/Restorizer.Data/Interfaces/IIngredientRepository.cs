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
        event MessageCallback MessageSent;

        bool TryAdd(string name, string price);

        bool TryEdit(Ingredient ingredient, string name, string price);


        List<IngredientTotalAmount> Get5LeastPopularIngredient();

        IEnumerable<Ingredient> GetIngredientDifference(List<DishHasIngredient> ingredients);
    }
}
