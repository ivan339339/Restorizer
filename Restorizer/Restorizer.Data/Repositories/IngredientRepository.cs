using Restorizer.Data.Interfaces;
using Restorizer.Data.Model;
using Restorizer.Data.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restorizer.Data.Repositories
{
    class IngredientRepository : Repository<Ingredient>, IIngredientRepository
    {
        public IngredientRepository(Context context) : base(context) { }

        public List<IngredientTotalAmount> Get5LeastPopularIngredient()
        {
            var AllIngredients = _context.Ingredients.ToList();
            var ingredientsamount = from ai in AllIngredients
                                    select ai.Dishes;

            List<DishHasIngredient> ingredients = ingredientsamount.SelectMany(x => x).ToList();

            List<IngredientTotalAmount> ingredientstotalamount = new List<IngredientTotalAmount>();

            var groupedingredient = from i in ingredients
                                   group i by i.Ingredient;
            foreach (var item in groupedingredient)
            {
                int totalamount = 0;
                foreach (var item1 in item)
                {
                    totalamount += item1.AmountInG;
                }
                ingredientstotalamount.Add(new IngredientTotalAmount { Ingr = item.Key, TotalQuantity = totalamount });
            }

            var sordtedingredient = (from ita in ingredientstotalamount
                                    orderby ita.TotalQuantity ascending
                                    select ita).ToList();

            return new List<IngredientTotalAmount> { sordtedingredient[0], sordtedingredient[1], sordtedingredient[2], sordtedingredient[3], sordtedingredient[4] };
        }
    }
}
