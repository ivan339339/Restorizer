using Restorizer.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restorizer.Data.Repositories
{
    class Repository<T> : IRepository<T> where T : class
    {
        protected Context _context;

        public Repository(Context context)
        {
            _context = context;
        }

        public void Add(T item)
        {
            _context.Set<T>().Add(item);
        }

        public void Remove(T item)
        {
            _context.Set<T>().Remove(item);
        }

        public IEnumerable<T> GetAllItems()
        {
            return _context.Set<T>().ToList();
        }
    }
}
