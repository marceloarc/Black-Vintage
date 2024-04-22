using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackVintage.Models
{
      internal class OrderItem
    {
        public OrderItem()
        {
            this.Print = true;
        }

       
        public  int Id { get; set; }

        public bool Print { get; set; }

        public string Thumb { get; set; }

        public string Name { get; set; }

        public  string Sku { get; set; }

        public  string Size { get; set; }

        public  string Color { get; set; }

        public virtual List<Category> Categories { get; set; }

        public int Parts { get; set; }

        public int Quantity{ get; set; } 

        public virtual List<Image> Images { get; set; }
    }
}
