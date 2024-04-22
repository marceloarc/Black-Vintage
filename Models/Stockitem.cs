using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackVintage.Models
{
      internal class StockItem
    {
       
        public  int Id { get; set; }

        public string sku { get; set; }

        public string size { get; set; }

        public string quantity { get; set; }

        public string category { get; set; }

    }
}
