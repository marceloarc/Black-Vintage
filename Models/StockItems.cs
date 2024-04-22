using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackVintage.Models
{
    internal class StockItems
    {
        [JsonProperty("stock_items")]
        public List<StockItem> items { get; set; }
        public int count { get; set; }

    }
}
