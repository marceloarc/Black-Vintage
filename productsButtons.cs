
using BlackVintage.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Size = BlackVintage.Models.Size;

namespace BlackVintage
{
    public partial class productsButtons : Form
    {
        public productsButtons()
        {
            InitializeComponent();
            panel_btn.BorderStyle = BorderStyle.None;
            panel_btn.Region = System.Drawing.Region.FromHrgn(Functions.CreateRoundRectRgn(0, 0, panel_btn.Width, panel_btn.Height, 10, 10));
        }

   
        private void SizeButtonHandler_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string[] result = btn.Name.Split(',');
            Functions.type = result[0];
            Functions.size = result[1];
            loadform(new Products());
        }
            public void loadform(object Form)
        {
            if (this.Controls.Count > 0)
            {
                this.Controls.RemoveAt(0);
            }

            Form f = Form as Form;

            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.Controls.Add(f);
            this.Tag = f;
            f.Show();
        }


        private void productsButtons_Load(object sender, EventArgs e)
        {
         
            if (Functions.OrderItems.order_id != null)
            {
                if (Functions.OrderItems.order_id != Functions.order_id)
                {
                    var json = Functions.getJson(Settings1.Default.Api + "?order_id=" + Functions.order_id);
                    Functions.OrderItems = JsonConvert.DeserializeObject<OrderItems>(json);
                }
            }


            List<Size> sizes = new List<Size>();

            List<string> sizeSetting = Settings1.Default.SizesTypes.ToString().Split(',').ToList();

            foreach (string size in sizeSetting)
            {
                string[] result = size.Split('=');
                bool isInOrder = false;
                foreach(OrderItem item in Functions.OrderItems.items)
                {
                    
                    if(item.Size == result[0].Trim())
                    {
                        isInOrder = true;
                    }
                }

                sizes.Add(new Models.Size{ size = result[0].Trim(), type = result[1].Trim(),isInOrder = isInOrder});
            }


            lblorder.Text = Functions.order_id;
            Functions.OrderItems.order_id = Functions.order_id;
            foreach (Models.Size size in sizes)
            {
                if (size.isInOrder)
                {
                    RoundedButton btn = new RoundedButton();
                    btn.Margin = new Padding(10);
                    btn.Font = new Font("Leelawadee", 14f, FontStyle.Bold);
                    btn.Width = 173;
                    btn.Height = 103;
                    btn.rdus = 20;
                    btn.Name = size.type + "," + size.size;
                    btn.Text = size.type + " " + size.size;
                    btn.Text = btn.Text.ToUpper();
                    btn.Dock = DockStyle.None;
                    btn.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
                    btn.BackColor = System.Drawing.Color.FromArgb(255, 57, 57, 57);
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.ForeColor = System.Drawing.Color.FromArgb(255, 215, 215, 215);
                    btn.FlatAppearance.BorderSize = 0;
                    btn.Click += new EventHandler(SizeButtonHandler_Click);
                    btn.TabStop = false;
                    this.panel_btn.Controls.Add(btn);

                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            panel2.BorderStyle = BorderStyle.None;
            panel2.Region = System.Drawing.Region.FromHrgn(Functions.CreateRoundRectRgn(0, 0, panel2.Width, panel2.Height, 20, 20));
        }
    }
}
