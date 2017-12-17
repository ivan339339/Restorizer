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

        public IEnumerable<Dish> GetAllActive()
        {
            return _context.Dishes.Where(d => d.IsArchived == false);
        }

        public bool TryAdd(string name, object category, string price, List<object> ingredients)
        {
            if (IsDataValid(name, category, price, ingredients))
            {
                int parsedPrice = int.Parse(price);
                var localCategory = category as Category;
                var categoryInDB = _context.Categories.FirstOrDefault(c => c.Id == localCategory.Id);

                var newDish = new Dish
                {
                    Name = name,
                    Category = categoryInDB,
                    Price = parsedPrice,
                    Ingredients = new List<DishHasIngredient>()
                };

                foreach (var ingredient in ingredients)
                {

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
            else
                return false;
        }

        public bool TryEdit(Dish dish, string name, object category, string price, List<DishHasIngredient> ingredients)
        {
            if (IsDataValid(name, category, price, ingredients))
            {
                var dishInDB = _context.Dishes.Include("Ingredients").FirstOrDefault(d => d.Id == dish.Id);

                var localCategroy = category as Category;

                var categoryInDB = _context.Categories.FirstOrDefault(c => c.Id == localCategroy.Id);

                dishInDB.Name = name;
                dishInDB.Category = categoryInDB;
                dishInDB.Price = int.Parse(price);
                dishInDB.Ingredients.Clear();

                foreach (var ing in ingredients)
                {   
                    var ingredientInDB = _context.Ingredients.FirstOrDefault(i => i.Id == ing.Ingredient.Id);
                    dishInDB.Ingredients.Add(
                        new DishHasIngredient
                        {
                            IngredientId = ing.IngredientId,
                            Ingredient = ingredientInDB,
                            AmountInG = ing.AmountInG
                        });
                }
                return true;
            }
            else
                return false;
        }

        private bool IsDataValid<T>(string name, object category, string price, List<T> ingredients)
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
            else if (!int.TryParse(price, out parsedPrice) || parsedPrice <= 0)
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
                return true;
            }
        }

        public IEnumerable<Object> GetByCategories()
        {
            var dishes = _context.Dishes.Include("Ingredients").Include("Ingredients.Ingredient").Include("Category").Where(d => d.IsArchived == false).ToList();
            var result = dishes.GroupBy(d => d.Category).Select(g => g.Key).ToList();
            return result;
        }
        
        public IEnumerable<IGrouping<Dish, OrderHasDish>> GetGrouped()
        {
            var alldishes = _context.Dishes.Include("Orders").ToList();

            var disheswithorders = from d in alldishes
                                   select d.Orders;

            List<OrderHasDish> dishes = disheswithorders.SelectMany(x => x).ToList();

            return from d in dishes
                   group d by d.Dish;
        }

        public DishWithProperty GetMaxProfit()
        {

            List<DishWithProperty> disheswithprofits = new List<DishWithProperty>();

            DishWithProperty dishwithptofit = new DishWithProperty();

            var groupeddishes = GetGrouped();

            foreach (var item in groupeddishes)
            {
                int profit = 0;

                foreach (var item1 in item)
                {
                    profit += item1.Dish.Price * item1.Quantity;
                }

                disheswithprofits.Add(new DishWithProperty { Dish = item.Key, Property = profit });

            }

            dishwithptofit = disheswithprofits
                             .OrderByDescending(d => d.Property)
                             .FirstOrDefault();

            return dishwithptofit;
        }

        public List<DishWithProperty> Get5LeastSold()
        {
            List<DishWithProperty> disheswithquantity = new List<DishWithProperty>();

            var groupeddishes = GetGrouped();

            foreach (var item in groupeddishes)
            {
                int quantity = 0;

                foreach (var item1 in item)
                {
                    quantity += item1.Quantity;
                }
                disheswithquantity.Add(new DishWithProperty { Dish = item.Key, Property = quantity });
            }

            var ordereddishes = disheswithquantity
                                .OrderBy(d => d.Property)
                                .Take(5)
                                .ToList();

            return ordereddishes;
        }

        public bool TryArchive(object dish)
        {
            if (dish is Dish)
            {
                var localDish = dish as Dish;
                var dishInDB = _context.Dishes.FirstOrDefault(d => d.Id == localDish.Id);
                dishInDB.IsArchived = true;
                return true;
            }
            else
            {
                MessageSent?.Invoke("Error!", "The dish wasn't chosen correctly!");
                return false;
            }
        }

    }
}
