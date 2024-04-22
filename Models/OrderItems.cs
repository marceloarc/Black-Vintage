using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackVintage.Models
{
    internal class OrderItems
    {
        [JsonProperty("order_items")]
        public List<OrderItem> items { get; set; }
        public int count { get; set; }

        public string order_id { get; set; }
    }
}
