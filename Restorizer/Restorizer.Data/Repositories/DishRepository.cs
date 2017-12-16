﻿using Restorizer.Data.Interfaces;
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

        public IEnumerable<object> GetByCategories()
        {
            var result = _context.Dishes.GroupBy(d => d.Category).Select(g => new { Category = g.Key, Dishes = g.ToList() }).ToList();
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


    }
}
