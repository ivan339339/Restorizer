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

        public event MessageCallback MessageSent;

        public List<IngredientTotalAmount> Get5LeastPopularIngredient()
        {
            var allingredients = _context.Ingredients.Include("Dishes").ToList();
            var ingredientswithdishes = from ai in allingredients
                                    select ai.Dishes;

            List<DishHasIngredient> ingredients = ingredientswithdishes.SelectMany(x => x).ToList();

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

            var sordtedingredient = ingredientstotalamount
                                    .OrderBy(ita => ita.TotalQuantity)
                                    .Take(5)
                                    .ToList();

            return sordtedingredient;
        }

        public IEnumerable<Ingredient> GetIngredientDifference(List<DishHasIngredient> ingredients)
        {
            var allIngredients = GetAllItems();
            var listOfIngredientIDs = ingredients.Select(i2 => i2.IngredientId).ToList();
            var result = allIngredients.Where(i => !(listOfIngredientIDs.Contains(i.Id)));
            return result;
        }
        
        public bool TryAdd(string name, string price)
        {
            if (IsDataValid(name, price))
            {
                var newIngredient = new Ingredient { Name = name, PricePerKg = int.Parse(price) };
                Add(newIngredient);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool TryEdit(Ingredient ingredient, string name, string price)
        {
            if (IsDataValid(name, price))
            {
                var ingredientInDB = _context.Ingredients.FirstOrDefault(i => i.Id == ingredient.Id);

                ingredientInDB.Name = name;
                ingredientInDB.PricePerKg = int.Parse(price);
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsDataValid(string name, string price)
        {
            int parsedPrice;
            if (name == "")
            {
                MessageSent?.Invoke("Error!", "The name must be not blank string expression!");
                return false;
            }
            else if (!int.TryParse(price, out parsedPrice) && parsedPrice <= 0)
            {
                MessageSent?.Invoke("Error!", "The price must be a positive integer");
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
