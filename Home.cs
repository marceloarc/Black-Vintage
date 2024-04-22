using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BlackVintage.Models;

namespace BlackVintage
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
            this.MouseDown += Form1_MouseDown;
            this.MouseUp += Form1_MouseUp;
            this.MouseMove += Form1_MouseMove;
            var json = Functions.getJson(Settings1.Default.Api + "?get_printed_all=1");
            var result = JsonConvert.DeserializeObject<JToken>(json);
            label_products.Text = result["quantidade_produtos"].ToString();
            label_orders.Text = result["quantidade_pedidos"].ToString();
            dateTimePicker1.CustomFormat = "MMMM dd, yyyy - dddd";
            label_orders.Left = (panel5.Width - label_orders.Width) / 2;
            label_products.Left = (panel6.Width - label_products.Width) / 2;
            var json2 = Functions.getJson(Settings1.Default.Api + "?get_stock_product=1");
            Functions.StockItems = JsonConvert.DeserializeObject<StockItems>(json2);
            chart1.Series["Pedidos"].Points.AddXY("", Int32.Parse(result["quantidade_pedidos"].ToString()));
            chart1.Series["Produtos"].Points.AddXY("", Int32.Parse(result["quantidade_produtos"].ToString()));
            Functions.currentForm = this;
        }
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST)
                m.Result = (IntPtr)(HT_CAPTION);
        }

        private const int WM_NCHITTEST = 0x84;
        private const int HT_CLIENT = 0x1;
        private const int HT_CAPTION = 0x2;
        private bool mouseDown;
        private Point lastLocation;

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }
        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            Pen myPen = new Pen(System.Drawing.Color.FromArgb(255, 57, 57, 57));

            myPen.Width = 30.0f;

            // Set the LineJoin property
            myPen.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;


            // Draw the rectangle
            e.Graphics.DrawRectangle(myPen, new Rectangle(15, 15, panel3.Width - 30, panel3.Height - 30));
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
            Pen myPen = new Pen(System.Drawing.Color.FromArgb(255, 57, 57, 57));

            myPen.Width = 30.0f;

            // Set the LineJoin property
            myPen.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;


            // Draw the rectangle
            e.Graphics.DrawRectangle(myPen, new Rectangle(15, 15, panel4.Width - 30, panel4.Height - 30));
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void roundedButton1_Click(object sender, EventArgs e)
        {

            string date1 = dateTimePicker1.Value.ToString("yyyy/MM/dd");
            string date2 = dateTimePicker2.Value.ToString("yyyy/MM/dd");
            roundedButton1.Text = "CARREGANDO...";
            roundedButton1.Enabled = false;
            var json = Functions.getJson(Settings1.Default.Api + "?get_printed_all=1&date1=" + date1 + "&date2=" + date2);
            var result = JsonConvert.DeserializeObject<JToken>(json);
            label_products.Text = result["quantidade_produtos"].ToString();
            label_orders.Text = result["quantidade_pedidos"].ToString();
            roundedButton1.Text = "FILTRAR";
            roundedButton1.Enabled = true;
            dateTimePicker1.CustomFormat = "MMMM dd, yyyy - dddd";
            label_orders.Left = (panel5.Width - label_orders.Width) / 2;
            label_products.Left = (panel6.Width - label_products.Width) / 2;
            chart1.Series["Pedidos"].Points.Clear();
            chart1.Series["Produtos"].Points.Clear();
            chart1.Series["Pedidos"].Points.AddXY("", Int32.Parse(result["quantidade_pedidos"].ToString()));
            chart1.Series["Produtos"].Points.AddXY("", Int32.Parse(result["quantidade_produtos"].ToString()));
        }
    }
}
