using BlackVintage.Models;
using BlackVintage.Properties;

using Newtonsoft.Json;
using System;
using System.Collections.Generic;

using System.Data;
using System.Drawing;

using System.Linq;

using System.Windows.Forms;
using Button = System.Windows.Forms.Button;
using Image = System.Drawing.Image;
using Size = BlackVintage.Models.Size;

namespace BlackVintage
{
    public partial class Products : Form
    {
        Point mPicturePos;
        public Products()
        {
            InitializeComponent();
            mPicturePos = button4.Location;
            Functions.btnPause = btn_stop;
        }

        private void panel_Scroll(object sender, ScrollEventArgs e)
        {
            button4.Location = mPicturePos;
        }

        private async void PrintBtn_Click(object sender, EventArgs e)

        {
            Button btn = (Button)sender;
            Functions.isPaused = false;
            
            Functions.PrintLoop(btn);
 
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

        private async void CellFormatter(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            DataGridViewRow row = dgv.Rows[e.RowIndex];// get you required index
                                                         // check the cell value under your specific column and then you can toggle your colors
            row.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 230, 230, 230);
            if (
                 dgv.Columns[e.ColumnIndex].Name == "ImageColumn")
            {
                var item = (DataRowView)dgv.Rows[e.RowIndex].DataBoundItem;
                var uri = item.Row.Field<string>("Thumb");
            
                if (e.Value == null || e.Value == DBNull.Value)
                {
                    dgv.Rows[e.RowIndex]
                        .Cells["ImageColumn"].Value = await Functions.DownloadImage(Settings1.Default.Api+"?image_url="+uri);
                }
               
            }

        }
        public void loadform(object Form)
        {
            if (this.Controls.Count >= 0)
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

        // below method will download the image from given url
 
            private void dataGridViewPrint_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (e.ColumnIndex == dgv.Columns["Ação"].Index)
            {
  
            
                string sku = dgv.CurrentRow.Cells["Sku"].Value.ToString();
                int qtd = Convert.ToInt32(dgv.CurrentRow.Cells["Quantity"].Value);
                int parts = Convert.ToInt32(dgv.CurrentRow.Cells["Parts"].Value);
                string size = dgv.CurrentRow.Cells["Size"].Value.ToString();
                bool action = false;
                if (Functions.type == "imprimir")
                {

                    if (!Functions.PrinterHasErrors())
                    {
                        action = Functions.PrintPdf(sku, qtd, parts, size);
                    }
                   
                }
                else
                {
                    action = Functions.PreparePdf(Functions.order_id, sku, qtd, parts, size);
                }
                if (action)
                {

                    dgv.CurrentRow.Cells["print"].Value = false;
                }
                else
                {
                    MessageBox.Show("Erro ao imprimir/preparar verifique se o pdf existe ou se impressora está configurada corretamente");
             
                }
            }
        }

        private void Products_Load(object sender, EventArgs e)
        {


            OrderLabel.Text = "#" + Functions.order_id + " " + Functions.client;


      
           
            List<Size> sizes = new List<Size>
            {
                new Size() { size = Functions.size },
            };
            List<Category> categories = new List<Category>();

            List<string> CategorySetting = Settings1.Default.Categories.ToString().Split(',').ToList();

            foreach (string category in CategorySetting) {

                categories.Add(new Category { Name = category.Trim() });
            
            }

            List<Models.Color> colors = new List<Models.Color>();
             List<string> colorSetting = Settings1.Default.Colors.ToString().Split(',').ToList();

            foreach (string color in colorSetting)
            {

                colors.Add(new Models.Color { color = color.Trim() });

            }

            
            List<DataGridView> dgvList = new List<DataGridView>();
            foreach (Category category in categories)
            {
                if (category.Name == "personalizados")
                {
                    List<Models.Image> images = new List<Models.Image>();
                    foreach (OrderItem item in Functions.OrderItems.items)
                    {
                        foreach (Models.Image image in item.Images)
                        {
                            images.Add(new Models.Image { src = image.src, sku = item.Sku });
                        }
                    }
                    Functions.images = images;
                }
                foreach (Size size in sizes)
                {
                    List<OrderItem> orderItems = new List<OrderItem>();
                    foreach (OrderItem item in Functions.OrderItems.items)
                    {
                        if ((item.Size == size.size))
                        {
                            foreach(Category cat in item.Categories)
                            {
                                if(cat.Name == category.Name)
                                {
                                    orderItems.Add(item);
                                }
                            }
                           
                        }
                    }

                    if(orderItems.Count>0) {
                        string json2 = JsonConvert.SerializeObject(orderItems);
                        DataGridView dgv = new DataGridView();

                        // Assign some properties
                        System.Windows.Forms.Label lblCategory = new System.Windows.Forms.Label();
                        lblCategory.Text = category.Name.ToUpper(); ;
                        lblCategory.Margin = new Padding(50,10,10,0);
                        lblCategory.Height = 30;
                        lblCategory.Width = 500;
                        lblCategory.Font = new Font("Leelawadee", 18f, FontStyle.Bold);
                        
                        this.panel1.Controls.Add(lblCategory);
                        dgv.AutoGenerateColumns = true;
                        dgv.BackgroundColor = System.Drawing.Color.FromArgb(255, 217, 217, 217);
                        dgv.BorderStyle = System.Windows.Forms.BorderStyle.None;
                  
                        // Assing Data (this can be a DataTable or you can create each column through Columns Colecction)
                        dgv.DataSource = JsonConvert.DeserializeObject<DataTable>(json2);
                        dgv.CellBorderStyle = DataGridViewCellBorderStyle.None;
                        dgv.ColumnHeadersBorderStyle = (DataGridViewHeaderBorderStyle)DataGridViewCellBorderStyle.None;
                        dgv.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 217, 217, 217);
                        dgv.ColumnHeadersHeight = 50;
                        dgv.Margin = new Padding(70,10,50,10);
                        dgv.EnableHeadersVisualStyles = false;
                        dgv.AllowUserToResizeColumns = false;
                        dgv.AllowUserToDeleteRows = false;
                        dgv.AllowUserToResizeRows = false;
                        dgv.AutoSize = false;
                        dgv.Height = 300;
                        dgv.Width = 800;
                        dgv.RowHeadersVisible = false;

                        dgv.RowTemplate.Height = 50;
                        dgv.CellFormatting += CellFormatter;
                        this.panel1.Controls.Add(dgv);
                        dgv.AllowUserToAddRows = false;
                        dgv.CellMouseEnter += dataGridView_CellMouseEnter;
                        dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Leelawadee", 12f ,FontStyle.Bold);
                        DataGridViewImageColumn viewPrintButton = new DataGridViewImageColumn();
                  
                        viewPrintButton.Name = "Ação";
                        viewPrintButton.Image = Resources.print_icon;
                        viewPrintButton.HeaderText = "Ação";
                      
                        int columnIndex = 10;
                        if (dgv.Columns["Ação"] == null)
                        {
                            dgv.Columns.Insert(columnIndex, viewPrintButton);
                        }
                        dgv.Columns.Insert(2,new DataGridViewImageColumn()
                        {
                            Name = "ImageColumn",
                            HeaderText = "Imagem",
                            ImageLayout = DataGridViewImageCellLayout.Zoom,
                            Width = 100,                             
                        });
                        dgv.CellClick += dataGridViewPrint_CellClick;
                        dgv.Columns["Id"].Visible = false;
                        dgv.Columns["Images"].Visible = false;
                        dgv.Columns["Thumb"].Visible = false;
                        dgv.Columns["Parts"].Visible = false;
                        dgv.Columns["Categories"].Visible = false;
                        dgv.Columns["Name"].Visible = false;
                        dgv.Columns["Name"].HeaderText = "Nome";
                        dgv.Columns["Size"].HeaderText = "Tamanho";
                        dgv.Columns["Print"].HeaderText = "";
                        dgv.Columns["Color"].HeaderText = "Cor da moldura";
                        dgv.Columns["Quantity"].HeaderText = "Quantidade";
                        dgvList.Add(dgv);
                  
                        Functions.dgvList = dgvList;
                        
                    }

                }

            }

            if (dgvList.Count == 0) {
               
                MessageBox.Show("Este pedido não possui produtos deste tamanho!");
                loadform(new productsButtons());
            }
            else
            {

                if (Functions.type == "imprimir")
                {

                    foreach (Models.Color color in colors)
                    {
                        RoundedButton btn = new RoundedButton();

                        btn.Margin = new Padding(10);
                        btn.Font = new Font("Leelawadee", 14f, FontStyle.Bold);
                        btn.Width = 193;
                        btn.Height = 70;
                        btn.rdus = 20;
                        btn.Name = color.color;
                        btn.Text = Functions.type + " " + color.color;
                        btn.Text = btn.Text.ToUpper();
                        btn.Dock = DockStyle.None;
       
                        btn.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                        btn.BackColor = System.Drawing.Color.FromArgb(255, 57, 57, 57);
                        btn.FlatStyle = FlatStyle.Flat;
                        btn.ForeColor = System.Drawing.Color.FromArgb(255, 215, 215, 215);
                        btn.FlatAppearance.BorderSize = 0;
                        btn.Click += new EventHandler(PrintBtn_Click);
                        btn.TabStop = false;

                        this.panel2.Controls.Add(btn);

                    }
                }
                else
                {
                    RoundedButton btn = new RoundedButton();

                    btn.Margin = new Padding(10);
                    btn.Font = new Font("Leelawadee", 14f, FontStyle.Bold);
                    btn.Width = 400;
                    btn.Height = 70;
                    btn.rdus = 20;
                    btn.Name = "Preparar";
                    btn.Text = Functions.type + " Todos";
                    btn.Text = btn.Text.ToUpper();
                    btn.Dock = DockStyle.None;
                    btn.Location = new System.Drawing.Point((this.ClientSize.Width - btn.Width)/2);
                    btn.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                    btn.BackColor = System.Drawing.Color.FromArgb(255, 57, 57, 57);
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.ForeColor = System.Drawing.Color.FromArgb(255, 215, 215, 215);
                    btn.FlatAppearance.BorderSize = 0;
                    btn.Click += new EventHandler(PrintBtn_Click);
                    btn.TabStop = false;

                    this.panel2.Controls.Add(btn);
          
                }
            }

        }


        private void panel2_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void roundedButton1_Click(object sender, EventArgs e)
        {
            btn_stop.Visible = false;
            Functions.isPaused = true;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Controls.Remove(this);
            loadform(new productsButtons());
        }
    }
}
