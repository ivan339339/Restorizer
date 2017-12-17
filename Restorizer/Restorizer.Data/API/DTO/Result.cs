using Restorizer.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restorizer.Data.API.DTO
{
    class Result
    {

        public ulong expires { get; set; }
        public ResultItem[] results { get; set; }
        public int number { get; set; }
        public int offset { get; set; }
        public int processingTimeMs { get; set; }
        public bool isStale { get; set; }

    }
}
