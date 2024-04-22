using BlackVintage.Properties;
using Patagames.Pdf.Net;
using System;
using System.Linq;
using System.Drawing;
using System.IO;

using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Collections;
using iText.Layout.Element;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace BlackVintage
{
    public partial class FolderPDF : Form
    {

        public FolderPDF()
        {
            InitializeComponent();

        }

        private void PdfBtn_Click(object sender, EventArgs e)
        {
            string qtd = Microsoft.VisualBasic.Interaction.InputBox("Digite a quantidade que deseja imprimir",
                       "Quantidade",
                       "1"
                       
                       );

            Button btn = (Button)sender;
            string parte = null;

                if (qtd != null)
                {
                    var isNumeric = int.TryParse(qtd, out _);

                    if (isNumeric)
                    {


                        try
                        {
                        parte = Microsoft.VisualBasic.Interaction.InputBox("Digite a parte (se for kit) que deseja imprimir A = 1, B = 2, C = 3 e assim por diante, SE NÃO HOUVER PARTES APENAS DEIXE VAZIO!",
"Parte",
"");
                    }
                    catch (Exception error)
                        { 
                    
                        }

             
                            var isNumeric2 = int.TryParse(parte, out _);

                            if (isNumeric2)
                            {
                                int i = int.Parse(parte);
                                string sku = Path.GetFileNameWithoutExtension(btn.Name);
                                System.IO.Directory.CreateDirectory(Settings1.Default.FolderPrint + "\\TEMP");
                                string caminhoDoNovoPDF = Settings1.Default.FolderPrint + "\\TEMP\\" + sku + "-" + i + ".pdf";
                                using (iTextSharp.text.pdf.PdfReader reader = new iTextSharp.text.pdf.PdfReader(btn.Name))
                                using (FileStream fs = new FileStream(caminhoDoNovoPDF, FileMode.Create))
                                using (Document doc = new Document())
                                using (PdfCopy copy = new PdfCopy(doc, fs))
                                {
                                    doc.Open();
                            try
                            {
                                copy.AddPage(copy.GetImportedPage(reader, i));
                                String arguments = "-print-to \"" + Settings1.Default.Printer + "\" -silent \"" + caminhoDoNovoPDF + "\" -print-settings \"" + qtd + "x\"";
                                System.Diagnostics.Process.Start(Settings1.Default.FolderSumatra + "\\SumatraPDF.exe", arguments);
                                Functions.getJson(Settings1.Default.Api + "?set_printed_products=" + qtd);
                            }
                            catch(Exception error)
                            {
                                MessageBox.Show("PDF não possui esta quantidade de partes!");
                                copy.AddPage(copy.GetImportedPage(reader, 1));
                            }
                                   
                                }
                        }
                        else
                        {
                            string path = Path.GetDirectoryName(btn.Name);
                            string size = new DirectoryInfo(path).Name;
                            string sku = Path.GetFileNameWithoutExtension(btn.Name);
                            foreach (String file in Directory.GetFiles(path, "*.qtd"))
                            {
                                if (File.Exists(file))
                                {
                                    int qtdPrint = int.Parse(Path.GetFileNameWithoutExtension(file));
                                    int qtd2 = int.Parse(qtd);
                                    if (qtdPrint == qtd2)
                                    {
                                        qtd = "1";
                                    }
                                    else if (qtd2 < qtdPrint)
                                    {

                                        Functions.getJson(Settings1.Default.Api + "?set_stock_product=" + (qtdPrint - qtd2) + "&sku=" + sku + "&size=" + size);

                                    }
                                    else
                                    {

                                        decimal quantidade = (decimal)qtd2 / (decimal)qtdPrint;
                                        decimal f = quantidade - Math.Truncate(quantidade);
                                        qtd = Math.Ceiling(quantidade).ToString();
                                        Console.WriteLine((1 - f) * qtdPrint);
                                        if (f > 0)
                                        {
                                            decimal qtdStock = (1 - f) * qtdPrint;

                                            Functions.getJson(Settings1.Default.Api + "?set_stock_product=" + qtdStock + "&sku=" + sku + "&size=" + size);
                                        }

                                    }
                                }
                            }



                            String arguments = "-print-to \"" + Settings1.Default.Printer + "\" -silent \"" + btn.Name + "\" -print-settings \"" + 0 + "x\"";
                            System.Diagnostics.Process.Start(Settings1.Default.FolderSumatra + "\\SumatraPDF.exe", arguments);
                            Functions.getJson(Settings1.Default.Api + "?set_printed_products=" + 0);
                        if (Directory.Exists(Settings1.Default.FolderPrint + "\\TEMP"))
                        {
                            Directory.Delete(Settings1.Default.FolderPrint + "\\TEMP", true);
                        }
                    }

                    }


                    

                


                }

            
        }
        private void PdfFolderBtn_Click(object sender, EventArgs e)
        {

            Button btn = (Button)sender;
            string[] files = Directory.GetFiles(btn.Name, "*.pdf");
            string path = btn.Name;
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

            Functions.backPath = path.Replace("\\"+Dictiontory.Name, "");
            Console.WriteLine(Functions.backPath);
            loadform(new FolderPDF());
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
        private void FolderPDF_Load(object sender, EventArgs e)
        {
      
            label_pdf.Text = Functions.Pdf_label;
            label_pdf.Left = (panel_PDF.Width - label_pdf.Width) / 2;
            string path = "";
            int i = 0;
         
            foreach (string file in Functions.files)
            {
                i++;

                if(i == 400)
                {
                    break;
                }  
          
              
                RoundedButton btn = new RoundedButton();

                btn.Margin = new Padding(10);
                btn.Font = new System.Drawing.Font("Leelawadee", 12f, FontStyle.Bold);
                btn.Width = 120;
                btn.Height = 150;
                btn.rdus = 20;
                btn.Name = file;
                btn.Text = Path.GetFileName(file);
                btn.Text = btn.Text.ToUpper();
                btn.Dock = DockStyle.None;
                btn.Image = Resources.icon_pdf;
                btn.ImageAlign = ContentAlignment.MiddleCenter;
                btn.TextAlign = ContentAlignment.BottomCenter;
                //btn.Location = new System.Drawing.Point((this.ClientSize.Width - btn.Width) / 2, this.panel2.Top + 20);
                btn.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                btn.BackColor = System.Drawing.Color.FromArgb(255, 230, 230, 230); ;
                btn.FlatStyle = FlatStyle.Flat;
                btn.ForeColor = System.Drawing.Color.Black;
                btn.FlatAppearance.BorderSize = 0;
                btn.Click += new EventHandler(PdfBtn_Click);
                btn.TabStop = false;

                this.panel_PDF.Controls.Add(btn);
         
            }
            foreach (DirectoryInfo folder in Functions.folders)
            {


                RoundedButton btn = new RoundedButton();

                btn.Margin = new Padding(10);
                btn.Font = new System.Drawing.Font("Leelawadee", 12f, FontStyle.Bold);
                btn.Width = 120;
                btn.Height = 150;
                btn.rdus = 20;
                btn.Name = folder.FullName;
                btn.Text = Path.GetFileName(folder.Name);
                btn.Text = btn.Text.ToUpper();
                btn.Dock = DockStyle.None;
                btn.Image = Resources.icon_folder;
                btn.ImageAlign = ContentAlignment.MiddleCenter;
                btn.TextAlign = ContentAlignment.BottomCenter;
                //btn.Location = new System.Drawing.Point((this.ClientSize.Width - btn.Width) / 2, this.panel2.Top + 20);
                btn.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                btn.BackColor = System.Drawing.Color.FromArgb(255, 230, 230, 230); ;
                btn.FlatStyle = FlatStyle.Flat;
                btn.ForeColor = System.Drawing.Color.Black;
                btn.FlatAppearance.BorderSize = 0;
                btn.Click += new EventHandler(PdfFolderBtn_Click);
                btn.TabStop = false;

                this.panel_PDF.Controls.Add(btn);

            }
        }

        private void panel_PDF_Paint(object sender, PaintEventArgs e)
        {
            panel_PDF2.BorderStyle = BorderStyle.None;
            panel_PDF2.Region = System.Drawing.Region.FromHrgn(Functions.CreateRoundRectRgn(0, 0, panel_PDF2.Width, panel_PDF2.Height, 20, 20));
            box_search.BorderStyle = BorderStyle.None;
            box_search.Region = System.Drawing.Region.FromHrgn(Functions.CreateRoundRectRgn(0, 0, box_search.Width, box_search.Height, 10, 10));

        }

        private void pdfViewer1_Load(object sender, EventArgs e)
        {

        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(box_search.Text))
            {
                int i = 0;
                panel_PDF.Controls.Clear(); 
                string[] files = Functions.files;
                files = files.Where(s => Path.GetFileName(s).Contains(box_search.Text)).ToArray();

                foreach (string file in files)
                {
                    i++;

                    if (i == 400)
                    {
                        break;
                    }
                    RoundedButton btn = new RoundedButton();

                    btn.Margin = new Padding(10);
                    btn.Font = new System.Drawing.Font("Leelawadee", 14f, FontStyle.Bold);
                    btn.Width = 120;
                    btn.Height = 150;
                    btn.rdus = 20;
                    btn.Name = file;
                    btn.Text = Path.GetFileName(file);
                    btn.Text = btn.Text.ToUpper();
                    btn.Dock = DockStyle.None;
                    btn.Image = Resources.icon_pdf;
                    btn.ImageAlign = ContentAlignment.MiddleCenter;
                    btn.TextAlign = ContentAlignment.BottomCenter;
                    //btn.Location = new System.Drawing.Point((this.ClientSize.Width - btn.Width) / 2, this.panel2.Top + 20);
                    btn.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                    btn.BackColor = System.Drawing.Color.FromArgb(255, 230, 230, 230); ;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.ForeColor = System.Drawing.Color.Black;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.DoubleClick += new EventHandler(PdfBtn_Click);
                    btn.TabStop = false;

                    this.panel_PDF.Controls.Add(btn);
             
                }
                foreach (DirectoryInfo folder in Functions.folders)
                {


                    RoundedButton btn = new RoundedButton();

                    btn.Margin = new Padding(10);
                    btn.Font = new System.Drawing.Font("Leelawadee", 12f, FontStyle.Bold);
                    btn.Width = 120;
                    btn.Height = 150;
                    btn.rdus = 20;
                    btn.Name = folder.FullName;
                    btn.Text = Path.GetFileName(folder.Name);
                    btn.Text = btn.Text.ToUpper();
                    btn.Dock = DockStyle.None;
                    btn.Image = Resources.icon_folder;
                    btn.ImageAlign = ContentAlignment.MiddleCenter;
                    btn.TextAlign = ContentAlignment.BottomCenter;
                    //btn.Location = new System.Drawing.Point((this.ClientSize.Width - btn.Width) / 2, this.panel2.Top + 20);
                    btn.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                    btn.BackColor = System.Drawing.Color.FromArgb(255, 230, 230, 230); ;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.ForeColor = System.Drawing.Color.Black;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.Click += new EventHandler(PdfFolderBtn_Click);
                    btn.TabStop = false;

                    this.panel_PDF.Controls.Add(btn);

                }

            }
            else
            {
                int i = 0;
                panel_PDF.Controls.Clear();
                foreach (string file in Functions.files)
                {
                    RoundedButton btn = new RoundedButton();
                    i++;

                    if (i == 400)
                    {
                        break;
                    }
                    btn.Margin = new Padding(10);
                    btn.Font = new System.Drawing.Font("Leelawadee", 14f, FontStyle.Bold);
                    btn.Width = 120;
                    btn.Height = 150;
                    btn.rdus = 20;
                    btn.Name = file;
                    btn.Text = Path.GetFileName(file);
                    btn.Text = btn.Text.ToUpper();
                    btn.Dock = DockStyle.None;
                    btn.Image = Resources.icon_pdf;
                    btn.ImageAlign = ContentAlignment.MiddleCenter;
                    btn.TextAlign = ContentAlignment.BottomCenter;
                    //btn.Location = new System.Drawing.Point((this.ClientSize.Width - btn.Width) / 2, this.panel2.Top + 20);
                    btn.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                    btn.BackColor = System.Drawing.Color.FromArgb(255,230,230,230);
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.ForeColor = System.Drawing.Color.Black;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.Click += new EventHandler(PdfBtn_Click);
                    btn.TabStop = false;

                    this.panel_PDF.Controls.Add(btn);
                   
                }
                foreach (DirectoryInfo folder in Functions.folders)
                {


                    RoundedButton btn = new RoundedButton();

                    btn.Margin = new Padding(10);
                    btn.Font = new System.Drawing.Font("Leelawadee", 12f, FontStyle.Bold);
                    btn.Width = 120;
                    btn.Height = 150;
                    btn.rdus = 20;
                    btn.Name = folder.FullName;
                    btn.Text = Path.GetFileName(folder.Name);
                    btn.Text = btn.Text.ToUpper();
                    btn.Dock = DockStyle.None;
                    btn.Image = Resources.icon_folder;
                    btn.ImageAlign = ContentAlignment.MiddleCenter;
                    btn.TextAlign = ContentAlignment.BottomCenter;
                    //btn.Location = new System.Drawing.Point((this.ClientSize.Width - btn.Width) / 2, this.panel2.Top + 20);
                    btn.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                    btn.BackColor = System.Drawing.Color.FromArgb(255, 230, 230, 230); ;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.ForeColor = System.Drawing.Color.Black;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.Click += new EventHandler(PdfFolderBtn_Click);
                    btn.TabStop = false;

                    this.panel_PDF.Controls.Add(btn);

                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string[] files = Directory.GetFiles(Functions.backPath, "*.pdf");
            string path = Functions.backPath;
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
            if(Functions.backPath != Settings1.Default.FolderPrint)
            {
                Functions.backPath = path.Replace(Dictiontory.Name, "");
                button1.Visible = true;
            }
            else
            {
                button1.Visible = false;
            }
           
            Console.WriteLine(Functions.backPath);
            loadform(new FolderPDF());
        }
    }
}
