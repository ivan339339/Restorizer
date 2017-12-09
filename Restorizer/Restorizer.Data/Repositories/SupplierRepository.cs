using Restorizer.Data.Interfaces;
using Restorizer.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restorizer.Data.Repositories
{
    class SupplierRepository : Repository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(Context context) : base(context) { }
    }
}
