namespace Restorizer.Data.Migrations
{
    using Model;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Restorizer.Data.Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Restorizer.Data.Context context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            //Category[] categories =
            //{
            //    new Category { Name = "Starters" },
            //    new Category { Name = "Main" },
            //    new Category { Name = "Sides" },
            //    new Category { Name = "Dessert" },
            //    new Category { Name = "Drinks" }
            //};

            //Dish[] dishes =
            //{
            //    new Dish { Name ="Olivie", Category = categories[0], Price = 300 },
            //    new Dish { Name ="Shuba", Category = categories[0], Price = 350 },
            //    new Dish { Name ="Burger", Category = categories[1], Price = 500 },
            //    new Dish { Name ="Pinsa", Category = categories[1], Price = 400 },
            //    new Dish { Name ="French Fries", Category = categories[2], Price = 115 },
            //    new Dish { Name ="Mac'n'Cheese", Category = categories[2], Price = 220 },
            //    new Dish { Name ="Tea", Category = categories[3], Price = 70 },
            //    new Dish { Name ="Milk$hake", Category = categories[3], Price = 95 },
            //    new Dish { Name ="Ice cream", Category = categories[4], Price = 280 },
            //    new Dish { Name ="Cake", Category = categories[4], Price = 310 }
            //};

            //Ingredient[] ingredients =
            //{
            //    new Ingredient { Name = "Flour" },
            //    new Ingredient { Name = "Sugar" },
            //    new Ingredient { Name = "Beef" },
            //    new Ingredient { Name = "Cucumbers" },
            //    new Ingredient { Name = "Salt" },
            //    new Ingredient { Name = "Milk" },
            //    new Ingredient { Name = "Pasta" },
            //    new Ingredient { Name = "Cheese" },
            //    new Ingredient { Name = "Water" },
            //    new Ingredient { Name = "Potatoes" },
            //    new Ingredient { Name = "Sugar" },
            //    new Ingredient { Name = "Fish" },
            //    new Ingredient { Name = "Tea leaves" },
            //    new Ingredient { Name = "Sugar" },
            //    new Ingredient { Name = "Buns" }
            //};

            //context.Categories.AddOrUpdate(
            //c => c.Name,
            //categories
            //);


            //context.Ingredients.AddOrUpdate(
            //i => i.Name,
            //ingredients
            //);


            //context.Dishes.AddOrUpdate(
            //d => d.Name,
            //dishes
            //);

        }
    }
}
