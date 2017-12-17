using Restorizer.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restorizer.Data.Interfaces
{
    public interface ISupplyRepository : IRepository<Supply>
    {
        IEnumerable<Supply> GetWithIngredients();

        bool TryAdd(DateTime? date, object ingredient, string price);

        event MessageCallback MessageSent;
    }
}
