using BlackVintage.Properties;
using System;
using System.Windows.Forms;

namespace BlackVintage
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
            panel2.BorderStyle = BorderStyle.None;
            panel2.Region = System.Drawing.Region.FromHrgn(Functions.CreateRoundRectRgn(0, 0, panel2.Width, panel2.Height, 20, 20));
            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                print_box.Items.Add(printer);
            }
           
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Settings1.Default.SizesTypes = sizetypes.Text;
            Settings1.Default.Colors = colors.Text;
            Settings1.Default.Categories = categories.Text;
            Settings1.Default.Api = api.Text;
            Settings1.Default.FolderPrint = folder.Text;
            Settings1.Default.FolderPrepare = prepare.Text;
            Settings1.Default.FolderSumatra = sumatra.Text;
            Settings1.Default.FolderOrders = orders.Text;

            if (print_box.SelectedIndex != -1)
            {
                // The combo box's Text property returns the selected item's text, which is the printer name.
                Settings1.Default.Printer = print_box.Text;
            }
            Settings1.Default.Save();
            MessageBox.Show("Configurações salvas!");
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            sizetypes.Text = Settings1.Default.SizesTypes;
            colors.Text = Settings1.Default.Colors;
           categories.Text =  Settings1.Default.Categories;
            api.Text = Settings1.Default.Api;
            folder.Text = Settings1.Default.FolderPrint;
            prepare.Text = Settings1.Default.FolderPrepare;
            sumatra.Text = Settings1.Default.FolderSumatra;
            orders.Text = Settings1.Default.FolderOrders;
            print_box.SelectedItem = Settings1.Default.Printer;
            print_box.SelectedItem = Settings1.Default.Stock;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loadform(new Home());
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

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                sumatra.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                folder.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
               prepare.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                orders.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
    }
}
