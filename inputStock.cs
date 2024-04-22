using BlackVintage.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlackVintage
{
    public partial class inputStock : Form
    {

        public inputStock()
        {
            InitializeComponent();
        }

        private void inputStock_Load(object sender, EventArgs e)
        {
            sku.BorderStyle = BorderStyle.None;
            sku.Region = System.Drawing.Region.FromHrgn(Functions.CreateRoundRectRgn(0, 0, sku.Width,sku.Height, 10, 10));

            size.Region = System.Drawing.Region.FromHrgn(Functions.CreateRoundRectRgn(0, 0, size.Width, size.Height, 10, 10));
            quantity.BorderStyle = BorderStyle.None;
            quantity.Region = System.Drawing.Region.FromHrgn(Functions.CreateRoundRectRgn(0, 0, quantity.Width, quantity.Height, 10, 10));
            List<string> sizeSetting = Settings1.Default.SizesTypes.ToString().Split(',').ToList();
            foreach (string size1 in sizeSetting)
            {
                string[] result = size1.Split('=');
                size.Items.Add(result[0]);

            }
        }
    }
}
