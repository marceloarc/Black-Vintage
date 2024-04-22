using BlackVintage.Models;
using BlackVintage.Properties;
using iText.Layout.Element;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlackVintage
{
    public partial class order_table : Form
    {
        public order_table()
        {
            Functions.OrderItems = new OrderItems();
            Functions.OrderItems.order_id = "0";
            InitializeComponent();
        }

        private void dataGridView_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            string colname = dgv.Columns[e.ColumnIndex].Name;
            if (colname != "Ação")
            {
                dgv.Cursor = Cursors.Default;
            }
            else
            {
                dgv.Cursor = Cursors.Hand;
            }
        }

        private void CellFormatter(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            DataGridViewRow row = dgv.Rows[e.RowIndex];// get you required index
                                                       // check the cell value under your specific column and then you can toggle your colors
            row.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 230, 230, 230);
        }
            private void order_table_Load(object sender, EventArgs e)
        {
                var json = Functions.getJson(Settings1.Default.Api + "?orders=1");
                if(!string.IsNullOrEmpty(json))
                    {
                        DataTable jsonC = JsonConvert.DeserializeObject<DataTable>(json);

                        dataGridView1.DataSource = JsonConvert.DeserializeObject<DataTable>(json);
                        DataGridViewImageColumn viewPrintButton = new DataGridViewImageColumn();
                        viewPrintButton.Name = "Ação";
                        viewPrintButton.Image = Resources.view_icon;
                        int columnIndex = 3;
                        if (dataGridView1.Columns["Ação"] == null)
                        {
                            dataGridView1.Columns.Insert(columnIndex, viewPrintButton);
                        }
                        dataGridView1.CellClick += dataGridViewSoftware_CellClick;
                        dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.None;
                        dataGridView1.ColumnHeadersBorderStyle = (DataGridViewHeaderBorderStyle)DataGridViewCellBorderStyle.None;
                        dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 217, 217, 217);
                        dataGridView1.ColumnHeadersHeight = 50;
                        dataGridView1.RowTemplate.Height = 30;
                        dataGridView1.EnableHeadersVisualStyles = false;
                        dataGridView1.AllowUserToResizeColumns = false;
                        dataGridView1.AllowUserToDeleteRows = false;
                        dataGridView1.AllowUserToResizeRows = false;
                        dataGridView1.AutoGenerateColumns = true;
                        dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(255, 217, 217, 217);
                        dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
                        dataGridView1.CellMouseEnter += dataGridView_CellMouseEnter;
                        dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12f, FontStyle.Bold);
                        dataGridView1.Columns["id"].HeaderText = "Nº Pedido";
                        dataGridView1.Columns["name"].HeaderText = "Cliente";
                        dataGridView1.Columns["status"].HeaderText = "Situação";
                        dataGridView1.CellFormatting += CellFormatter;
                        List<Order> joge = JsonConvert.DeserializeObject<List<Order>>(json);
            }
            else
            {
                MessageBox.Show("Nenhum pedido para impressão no momento!");

            }
           

          
        }

        private void dataGridViewSoftware_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Ação"].Index)
            {
                DataGridView dgv = (DataGridView)sender;
                string order_id = dataGridView1.CurrentRow.Cells["id"].Value.ToString();
                string client_name = dataGridView1.CurrentRow.Cells["Name"].Value.ToString();
                Functions.order_id = order_id;
                
                Functions.client = client_name;
     
                loadform(new productsButtons());
            }
        }

        public void loadform(object Form)
        {
            if (this.Controls.Count > 0)
            {
                this.Controls.Clear();
            }

            Form f = Form as Form;

            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.Controls.Add(f);
            this.Tag = f;
            f.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
