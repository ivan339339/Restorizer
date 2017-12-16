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
    class DishRepository : Repository<Dish>, IDishRepository
    {
        public DishRepository(Context context) : base(context) { }

        public event MessageCallback MessageSent;

        public bool TryAdd(string name, object category, string price, List<object> ingredients)
        {
            int parsedPrice;

            if (name == "")
            {
                MessageSent?.Invoke("Error!", "The name must not be null");
                return false;
            }
            else if (category == null || !(category is Category))
            {
                MessageSent?.Invoke("Error!", "The category wasn't chosen");
                return false;
            }
            else if (!int.TryParse(price, out parsedPrice) && parsedPrice <= 0)
            {
                MessageSent?.Invoke("Error!", "The price must be a positive integer");
                return false;
            }
            else if (ingredients == null || ingredients.Count == 0)
            {
                MessageSent?.Invoke("Error!", "The ingredients weren't chosen");
                return false;
            }
            else
            {
                var localCategory = category as Category;
                var categoryInDB = _context.Categories.FirstOrDefault(c => c.Id == localCategory.Id);

                var newDish = new Dish
                {
                    Name = name,
                    Category = categoryInDB,
                    Price = parsedPrice,
                    Ingredients = new List<DishHasIngredient>()
                };

                foreach (var ingredient in ingredients) {

                    var ingredientObject = ingredient?.GetType().GetProperty("Ingredient")?.GetValue(ingredient, null) as Ingredient;
                    var ingredientInDB = _context.Ingredients.FirstOrDefault(i => i.Id == ingredientObject.Id);

                    newDish.Ingredients.Add(new DishHasIngredient
                    {
                        IngredientId = ingredientInDB.Id,
                        Ingredient = ingredientInDB,
                        AmountInG = (int)ingredient?.GetType().GetProperty("Amount")?.GetValue(ingredient, null)
                    });
                }

                Add(newDish);
                return true;
            }
        }

        public DishWithProperty GetMaxProfit()
        {
            DishWithProperty dishwithptofit = new DishWithProperty();
            var dishes = _context.Dishes.Include("Orders");
            var dishhasorder = from d in dishes
                               select d.Orders;
            return dishwithptofit;
        }

        public List<DishWithProperty> Get5LeastSold()
        {
            var alldishes = _context.Dishes.Include("Orders").ToList();

            var disheswithorders = from d in alldishes
                         select d.Orders;

            List<OrderHasDish> dishes = disheswithorders.SelectMany(x => x).ToList();

            List<DishWithProperty> disheswithquantity = new List<DishWithProperty>();

            var groupeddishes = from d in dishes
                                group d by d.Dish;

            foreach (var item in groupeddishes)
            {
                int quantity = 0;

                foreach (var item1 in item)
                {
                    quantity += item1.Quantity;
                }

                disheswithquantity.Add(new DishWithProperty { Dish = item.Key, Property = quantity });
            }

            var ordereddishes = (from dwq in disheswithquantity
                                 orderby dwq.Property ascending
                                 select dwq).ToList();

            return new List<DishWithProperty> { ordereddishes[0], ordereddishes[1], ordereddishes[2], ordereddishes[3], ordereddishes[4]};
        }


    }
}
