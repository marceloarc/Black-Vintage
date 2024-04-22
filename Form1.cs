using System;
using System.Collections.Generic;

using System.Drawing;

using System.Windows.Forms;
using System.IO;


using BlackVintage.Properties;
using System.Net;
using System.IO.Compression;
using System.Diagnostics;

namespace BlackVintage
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            
            InitializeComponent();
    
            this.MouseDown += Form1_MouseDown;
            this.MouseUp += Form1_MouseUp;
            this.MouseMove += Form1_MouseMove;
            this.Icon = Resources.icon_bv;
            WebClient webClient = new WebClient();
            var client = new WebClient();
            if (!webClient.DownloadString("https://blackevintage.com.br/Version.txt?"+Functions.GetTimestamp(DateTime.Now)).Contains("1.6.2"))
            {
                if (MessageBox.Show("Uma nova atualização do software está disponível, deseja baixa-la?", "BV", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        if (File.Exists(@".\BVSetup.zip")) { File.Delete(@".\BVSetup.zip"); }
                        if (File.Exists(@".\BVSetup.msi")) {

                                File.Delete(@".\BVSetup.msi");
                 
                            

                        }
                        client.DownloadFile("https://blackevintage.com.br/BVSetup.zip?"+Functions.GetTimestamp(DateTime.Now), @"BVSetup.zip");
                        string zipPath = @".\BVSetup.zip";
                        string extractPath = @".\";
                        ZipFile.ExtractToDirectory(zipPath, extractPath);

                        Process process = new Process();
                        process.StartInfo.FileName = "msiexec.exe";
                        process.StartInfo.Arguments = string.Format("/i BVSetup.msi");
             
                        process.Start();
                        System.Environment.Exit(1);

                    }
                    catch
                    {
                    }
                    System.Environment.Exit(1);
                }
            }
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
       
        }

        public void loadform(object Form)
        {
            if(this.panel1.Controls.Count > 0) {
                this.panel1.Controls.RemoveAt(0);           
            }

            Form f = Form as Form;

            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.panel1.Controls.Add(f);
            this.panel1.Tag = f;
            f.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loadform(new Home());

        }

        private void button2_Click(object sender, EventArgs e)
        {

  
            loadform(new order_table());
          
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(Settings1.Default.FolderPrint != "")
            {
                string[] files = Directory.GetFiles(Settings1.Default.FolderPrint, "*.pdf");
                string path = Settings1.Default.FolderPrint;
                List<DirectoryInfo> folders = new List<DirectoryInfo>();
                DirectoryInfo Dictiontory = new DirectoryInfo(path);
                DirectoryInfo[] Dir = Dictiontory.GetDirectories();// this get all subfolder //name in folder NetOffice.
                foreach (DirectoryInfo directory in Dir)
                {
                    folders.Add(directory);
                }
                Functions.Pdf_label = Dictiontory.Name;
                Functions.files = files;
                Functions.folders = folders;
                Functions.backPath = path;
                loadform(new FolderPDF());
            }
            else
            {
                MessageBox.Show("Por favor configure a pasta imprimir direto!");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Settings1.Default.Api))
            {
                loadform(new Settings());
            }
            else
            {
                loadform(new Home());
            }
            
  
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            loadform(new Settings());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void button5_Click(object sender, EventArgs e)
        {
            
            string[] files = Directory.GetFiles(Settings1.Default.FolderPrint+ "\\25x30");
            Functions.Pdf_label = "Quadros 25x30";
            Functions.files = files;
            loadform(new FolderPDF());
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string[] files = Directory.GetFiles(Settings1.Default.FolderPrint + "\\34x44");
            Functions.Pdf_label = "Quadros 34x44";
            Functions.files = files;
            loadform(new FolderPDF());
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            loadform(new Stock());
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
