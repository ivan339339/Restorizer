using Restorizer.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restorizer.Data.Logic
{
    public class StatisticsLogic : IStatisticsLogic
    {
        private Context _context;

        public StatisticsLogic(Context context)
        {
            _context = context;
        }

    }
}
