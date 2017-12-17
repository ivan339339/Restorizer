using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restorizer.Data.API.DTO
{
    class ResultItem
    {

        public int id { get; set; }
        public string title { get; set; }
        public string[] imageUrls { get; set; }
        public string image { get; set; }

    }
}
