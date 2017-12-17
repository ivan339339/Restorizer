using Restorizer.Data.Interfaces;
using Restorizer.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restorizer.Data.Repositories
{
    class SupplyRepository : Repository<Supply>, ISupplyRepository
    {
        public SupplyRepository(Context context) : base(context) { }

        public event MessageCallback MessageSent;

        public bool TryAdd(DateTime? date, object ingredient, string amount)
        {
            if (IsDataValid(date, ingredient, amount))
            {
                var localIngredient = ingredient as Ingredient;
                var ingredientInDB = _context.Ingredients.FirstOrDefault(i => i.Id == localIngredient.Id);
                var notNullDate = (DateTime)date;
                _context.Supplies.Add(new Supply
                {

                    Date = notNullDate,
                    Ingredient = ingredientInDB,
                    IngredientId = ingredientInDB.Id,
                    Amount = int.Parse(amount),
                    PricePerKg = ingredientInDB.PricePerKg
                });
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsDataValid(DateTime? date, object ingredient, string amount)
        {
            int parsedAmount;
            if (ingredient == null || !(ingredient is Ingredient))
            {
                MessageSent?.Invoke("Error!", "Ingredient wasn't chosen");
                return false;
            }
            else if (!int.TryParse(amount, out parsedAmount) || parsedAmount <= 0)
            {
                MessageSent?.Invoke("Error!", "Amount must be a positive integer");
                return false;
            }
            else if (date == null || date <= DateTime.Now.AddDays(-1))
            {
                MessageSent?.Invoke("Error!", "The date wasn't chosen or was in the past");
                return false;
            }
            else
            {
                return true;
            }
        }

        public IEnumerable<Supply> GetWithIngredients()
        {
            return _context.Supplies.Include("Ingredient").ToList();
        }
    }
}
