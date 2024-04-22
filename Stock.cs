using BlackVintage.Models;
using BlackVintage.Properties;
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

namespace BlackVintage
{
    public partial class Stock : Form
    {
        public Stock()
        {
            InitializeComponent();
            var json2 = Functions.getJson(Settings1.Default.Api + "?get_stock_product=1");
            if (!string.IsNullOrEmpty(json2))
            {
                Functions.StockItems = JsonConvert.DeserializeObject<StockItems>(json2);

            }
            else
            {
                Functions.StockItems = new StockItems();
                msg_error.Visible = true;
            }
         
        }
        private void CellFormatter(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            DataGridViewRow row = dgv.Rows[e.RowIndex];// get you required index
                                                       // check the cell value under your specific column and then you can toggle your colors
            row.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 230, 230, 230);
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

        private void dataGridViewSoftware_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView.Columns["Ação"].Index)
            {
                DataGridView dgv = (DataGridView)sender;
                string sku = dataGridView.CurrentRow.Cells["sku"].Value.ToString();
                string size = dataGridView.CurrentRow.Cells["size"].Value.ToString();
                string qtd = Microsoft.VisualBasic.Interaction.InputBox("Digite a quantidade que deseja remover do estoque",
           "Quantidade",
           "1"

           );




                if (qtd != null)
                {
                    var isNumeric = int.TryParse(qtd, out _);

                    if (isNumeric)
                    {
                        Functions.getJson(Settings1.Default.Api + "?remove_stock_product=" + qtd + "&sku=" + sku + "&size=" + size);

                        MessageBox.Show("Removido " + qtd + " do produto " + sku + " do estoque!");
                        loadform(new Stock());
                    }
                }
            }
        }

        private void Stock_Load(object sender, EventArgs e)
        {

            string json3;
            box_search.BorderStyle = BorderStyle.None;
            box_search.Region = System.Drawing.Region.FromHrgn(Functions.CreateRoundRectRgn(0, 0, box_search.Width, box_search.Height, 10, 10));
            if (Functions.StockItems.items != null)
            {
                json3 = Newtonsoft.Json.JsonConvert.SerializeObject(Functions.StockItems.items);
                if (!string.IsNullOrEmpty(json3))
                {

                    dataGridView.DataSource = JsonConvert.DeserializeObject<DataTable>(json3);
                    DataGridViewImageColumn viewPrintButton = new DataGridViewImageColumn();
                    viewPrintButton.Name = "Ação";
                    viewPrintButton.Image = Resources.red_cross;
                    int columnIndex = 4;
                    if (dataGridView.Columns["Ação"] == null)
                    {
                        dataGridView.Columns.Insert(columnIndex, viewPrintButton);
                    }
                    dataGridView.CellClick += dataGridViewSoftware_CellClick;
                    dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.None;
                    dataGridView.ColumnHeadersBorderStyle = (DataGridViewHeaderBorderStyle)DataGridViewCellBorderStyle.None;
                    dataGridView.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 217, 217, 217);
                    dataGridView.ColumnHeadersHeight = 50;
                    dataGridView.RowTemplate.Height = 30;
                    dataGridView.EnableHeadersVisualStyles = false;
                    dataGridView.AllowUserToResizeColumns = false;
                    dataGridView.AllowUserToDeleteRows = false;
                    dataGridView.AllowUserToResizeRows = false;
                    dataGridView.AutoGenerateColumns = true;
                    dataGridView.BackgroundColor = System.Drawing.Color.FromArgb(255, 217, 217, 217);
                    dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
                    dataGridView.CellMouseEnter += dataGridView_CellMouseEnter;
                    dataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12f, FontStyle.Bold);
                    dataGridView.Columns["id"].Visible = false;
                    dataGridView.Columns["size"].HeaderText = "Tamanho";
                    dataGridView.Columns["quantity"].HeaderText = "Quantidade";
                    dataGridView.Columns["category"].Visible = false;

                    dataGridView.CellFormatting += CellFormatter;

                }
                else
                {
                    MessageBox.Show("Nenhum produto em estoque no momento!");

                }
            }
        
     


        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void box_search_TextChanged(object sender, EventArgs e)
        {
            (dataGridView.DataSource as DataTable).DefaultView.RowFilter = string.Format("sku like '{0}%' OR size like '{0}%'", box_search.Text);
        }

        private void roundedButton1_Click(object sender, EventArgs e)
        {
            using (inputStock inputstock = new inputStock())
            {
                if(inputstock.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string sku = inputstock.sku.Text.Trim();
                    string size = inputstock.size.Text.Trim();
                    string quantity = inputstock.quantity.Text.Trim();

                    if (String.IsNullOrEmpty(sku) || String.IsNullOrEmpty(size) || String.IsNullOrEmpty(quantity))
                    {
                        MessageBox.Show("Por favor digite valores válidos!");
                    }
                    else
                    {
                        Functions.getJson(Settings1.Default.Api + "?set_stock_product=" + quantity + "&sku=" + sku + "&size=" + size);
                        MessageBox.Show("Produto adicionado com sucesso!");
                        loadform(new Stock());
                    }

                }

            }
        }
    }
}
