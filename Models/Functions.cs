using System;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using System.IO;
using iTextSharp.text;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using iText.IO.Image;
using System.Runtime.InteropServices;
using System.Printing;
using System.Threading;
using System.Drawing;
using System.Net.Http;
using BlackVintage.Models;
using System.Linq;
using System.Text;
using PdfSharp.Pdf.Content.Objects;

namespace BlackVintage
{
    static internal class Functions
    {

        public static string order_id { get; set; }

        public static StockItems StockItems { get; set; }

        public static List<DataGridView> dgvList { get; set; }

        public static string size { get; set; }

        public static string path { get; set; } 

        public static List<DirectoryInfo> folders { get; set; }    

        public static string type { get; set; }

        public static string backPath { get; set; }

        public static Form currentForm { get; set; }
        public static bool isPaused { get; set;}

        public static Button btnPause { get; set; } 

        public static string Pdf_label { get; set; }   
        
        public static string[] files { get; set; }

        public static OrderItems OrderItems { get; set; }

        public static string client { get; set; }

        public static List<Models.Image> images { get; set; } 
            
        public static string getJson(string url)
        {
            var json = "";
            using (WebClient wc = new WebClient())
            {
                try
                {

                    json = wc.DownloadString(url);
                }
                catch(Exception e) {

                    MessageBox.Show("Erro! Verifique sua conexão com a internet!");
                }
              
            }
            return json;
        }

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        public static extern IntPtr CreateRoundRectRgn
        (
        int nLeftRect,     // x-coordinate of upper-left corner
        int nTopRect,      // y-coordinate of upper-left corner
        int nRightRect,    // x-coordinate of lower-right corner
        int nBottomRect,   // y-coordinate of lower-right corner
        int nWidthEllipse, // height of ellipse
        int nHeightEllipse // width of ellipse
        );

       public async static Task<System.Drawing.Image> DownloadImage(string uri)
        {

            System.Drawing.Image image = null;
            HttpClient httpClient = new HttpClient();

            try
            {
                Stream stream = await httpClient.GetStreamAsync(uri);
                image = (Bitmap)System.Drawing.Image.FromStream(stream, false, false);
            }
            catch (Exception ex)
            {


            }

            return image;
        }

        public static String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }
        public static bool PrinterHasErrors()
        {
       
            var server = new LocalPrintServer();

            //Load queue for correct printer
            try
            {
             
               PrintQueue queue = server.GetPrintQueue(Settings1.Default.Printer, new string[0] { });
              //  foreach (var job in queue.GetPrintJobInfoCollection())
               // {     
              //    Console.WriteLine(string.Format("jobname={0} name={1} size={2} status={3}", job.JobName, job.Name, job.JobSize, job.JobStatus));
                    
             //   }
                
                //Check some properties of printQueue    
                bool isInError = queue.IsInError;
                bool isPrinting = queue.IsPrinting;
                bool isPaperJammed = queue.IsPaperJammed;
                bool isOutOfPaper = queue.IsOutOfPaper;
                bool isOffline = queue.IsOffline;
                bool isBusy = queue.IsBusy;

                if (isInError)
                {
                    MessageBox.Show("Algo de errado com a impressora!");
                    return true;
                }

                if (isOutOfPaper)
                {
                    MessageBox.Show("Impressora sem papel!");
                    return true;
                }

                if (isOffline)
                {
                    MessageBox.Show("Impressora offline!");
                    return true;
                }

                if (isPaperJammed)
                {
                    MessageBox.Show("Impressora com Papel preso!");
                    return true;
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("Impressora não encontrada por favor verifique se está configurada corretamente!");
                return true;

            }

       
            return false;
        }

        public static void sendPost(string requestParams)
        {
            HttpWebRequest webRequest;

             //format information you need to pass into that string ('info={ "EmployeeID": [ "1234567", "7654321" ], "Salary": true, "BonusPercentage": 10}');

            webRequest = (HttpWebRequest)WebRequest.Create(Settings1.Default.Api);

            webRequest.Method = "POST";
            webRequest.ContentType = "application/json";

            byte[] byteArray = Encoding.UTF8.GetBytes(requestParams);
            webRequest.ContentLength = byteArray.Length;
            using (Stream requestStream = webRequest.GetRequestStream())
            {
                requestStream.Write(byteArray, 0, byteArray.Length);
            }

            // Get the response.
            using (WebResponse response = webRequest.GetResponse())
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader rdr = new StreamReader(responseStream, Encoding.UTF8);
                    string Json = rdr.ReadToEnd(); // response from server
                    Console.WriteLine(Json);

                }
            }
        }
        public static async void PrintLoop(Button btn)
        {
            
            bool error = false;
            foreach (DataGridView dgv in dgvList)
            {
                string pCategory = null;
                bool printedOne = false;
                string pSize = null;
                foreach (DataGridViewRow row in dgv.Rows)
                {

                    dgv.ClearSelection();

                    if ((bool)row.Cells["Print"].Value)
                    {

                        if (isPaused)
                        {
                            btnPause.Visible = false;
                            break;
                        }
                        string sku = row.Cells["Sku"].Value.ToString();
                        int qtd = Convert.ToInt32(row.Cells["Quantity"].Value);
                        int parts = Convert.ToInt32(row.Cells["Parts"].Value);
                        string size = row.Cells["Size"].Value.ToString();
                        pSize = size;
                        string category = row.Cells["Categories"].Value.ToString();
                        pCategory = category;
                        OrderItem item = OrderItems.items.Find(x => x.Sku == sku);
                        bool action = false;
                        bool IsCustom = false;
                        if (row.Cells["Color"].Value?.ToString() == btn.Name)
                        {
                            row.Selected = true;
                            if (type == "imprimir")
                            {
                                btnPause.Visible = true;
                                if (PrinterHasErrors())
                                {
                                    error = true;
                                    btnPause.Visible = false;
                                    break;
                                }

                                foreach (Category cat in item.Categories)
                                {
                                    if(cat.Name == "personalizados")
                                    {
                                        IsCustom = true;
                                    }

                                    List<string> CategorySetting = Settings1.Default.Categories.ToString().Split(',').ToList();
                                    pCategory = CategorySetting.Where(x => x.Trim() == cat.Name).FirstOrDefault();
                                 
                                    if(pCategory != null)
                                    {
                                        break;
                                    }
                                }

                                if (IsCustom)
                                {
                                    action = CreatePDF(sku, qtd, parts, size);
                                }
                                else
                                {
                                    
                                    action = PrintPdf(sku, qtd, parts, size);
                                    printedOne = true;
                                }


                            }
                            else
                            {
                                action = PreparePdf(order_id, sku, qtd, parts, size);
                            }
                            if (action)
                            {

                                row.Cells["print"].Value = false;
                            }
                            else
                            {
                                error = true;
                                MessageBox.Show("Erro ao imprimir verifique se o pdf existe ou se impressora está configurada corretamente");
                                btnPause.Visible = false;
                                break;
                            }

                        }
                        else
                        {
                            if (type == "preparar")
                            {
                                row.Selected = true;
                                action = PreparePdf(Functions.order_id, sku, qtd, parts, size);
                                if (action)
                                {

                                    row.Cells["print"].Value = false;

                                }
                                else
                                {
                                    error = true;
                                    MessageBox.Show("Erro ao preparar verifique se o pdf existe ou se impressora está configurada corretamente");
                                    btnPause.Visible = false;
                                    break;
                                }
                            }
         

                        }

                    }
                    if (Functions.type == "imprimir")
                    {
                        await Task.Delay(TimeSpan.FromSeconds(2));
                    }
                }
                
                if (printedOne && pCategory != null && pSize != null)
                {
                    
                    string pdfName = pCategory.Trim() + "_" + pSize + "_" + btn.Name;
          
                    string caminhoPDF = Settings1.Default.FolderPrint + "\\" + pdfName + ".pdf";
                    Console.WriteLine(caminhoPDF);
                    String arguments = "-print-to \"" + Settings1.Default.Printer + "\" -silent \"" + caminhoPDF + "\" -print-settings \"" + 1 + "x\"";
                    System.Diagnostics.Process.Start(Settings1.Default.FolderSumatra + "\\SumatraPDF.exe", arguments);
                }
            }
            btnPause.Visible = false;
            if (Directory.Exists(Settings1.Default.FolderPrint + "\\TEMP"))
            {
                Directory.Delete(Settings1.Default.FolderPrint + "\\TEMP", true);
            }
            if (!error)
            {
                getJson(Settings1.Default.Api + "?set_printed_orders=1");
            }
        }

        public static bool PrintPdf(string sku, int qtd, int parts, string size)
        {
            for (int i = 1; i < parts + 1; i++)
            {
                if (!PrinterHasErrors())
                {
                    try
                    {

                        System.IO.Directory.CreateDirectory(Settings1.Default.FolderPrint + "\\TEMP");
                        string caminhoDoNovoPDF = Settings1.Default.FolderPrint + "\\TEMP\\" + sku + "-" + i + ".pdf";
                        string caminhoPDF = Settings1.Default.FolderPrint + "\\" + size + "\\" + sku + ".pdf";
                        using (iTextSharp.text.pdf.PdfReader reader = new iTextSharp.text.pdf.PdfReader(caminhoPDF))
                        using (FileStream fs = new FileStream(caminhoDoNovoPDF, FileMode.Create))
                        using (Document doc = new Document())
                        using (PdfCopy copy = new PdfCopy(doc, fs))
                        {
                            doc.Open();
                            copy.AddPage(copy.GetImportedPage(reader, i));
                        }
                        //Get local print server

                        var stock = Functions.StockItems.items.SingleOrDefault(x => (x.sku == sku) && (x.size == size));

                        if (stock != null)
                        {
                            foreach (String file in Directory.GetFiles(Settings1.Default.FolderPrint + "\\" + size + "\\", "*.qtd"))
                            {
                                if (File.Exists(file))
                                {
                                    int qtdPrint = int.Parse(Path.GetFileNameWithoutExtension(file));
                                    if(qtd <= int.Parse(stock.quantity))
                                    {
                                        Functions.getJson(Settings1.Default.Api + "?remove_stock_product=" + qtd + "&sku=" + sku + "&size=" + size);
                                        qtd = 0;

                                    }
                                    else
                                    {
                                        qtd = qtd - int.Parse(stock.quantity);
                                        decimal quantidade = (decimal)qtd / (decimal)qtdPrint;
                                        decimal f = quantidade - Math.Truncate(quantidade);
                                        qtd = (int)Math.Ceiling(quantidade);
                                   
                                        if (f > 0)
                                        {
                                            decimal qtdStock = (1 - f) * qtdPrint;

                                            Functions.getJson(Settings1.Default.Api + "?set_stock_product=" + qtdStock + "&sku=" + sku + "&size=" + size);

                                        }
                                    }
                                }
                            }
                        }

                        if(qtd > 0)
                        {
                            String arguments = "-print-to \"" + Settings1.Default.Printer + "\" -silent \"" + caminhoDoNovoPDF + "\" -print-settings \"" + qtd + "x\"";
                            System.Diagnostics.Process.Start(Settings1.Default.FolderSumatra + "\\SumatraPDF.exe", arguments);
                            getJson(Settings1.Default.Api + "?set_printed_products=" + qtd);
                        }

                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.ToString());
                        return false;
                    }
                }
                else
                {
                    break;
                }
            }

            return true;

        }
        public static bool CreatePDF(string sku, int qtd, int parts, string size)
        {

                Document document = new Document(PageSize.LETTER);
                try
                {

                    // step 2:
                    // we create a writer that listens to the document
                    // and directs a PDF-stream to a file

                    PdfWriter.GetInstance(document, new FileStream("C:\\Users\\Administrator\\Desktop\\BV\\BASE\\" + size+ "\\"+size+".pdf", FileMode.Open));

                    // step 3: we open the document
                    document.Open();

                    foreach (Models.Image image in Functions.images)
                    {
                        if(image.sku == sku)
                        {

                        string url = image.src;
                            iTextSharp.text.Image pic = iTextSharp.text.Image.GetInstance(new Uri(url+ "?original"), true);
                            pic.ScaleToFit(document.PageSize.Width, document.PageSize.Height);
                            pic.SetAbsolutePosition(0, 0);
                            document.Add(pic);
                            document.NewPage();
                        
                        }

                    }
                }
                catch (DocumentException de)
                {
                    Console.Error.WriteLine(de.Message);
                    return false;
                }
                catch (IOException ioe)
                {
                    Console.Error.WriteLine(ioe.Message);
                    return false;
                }

                // step 5: we close the document
                document.Close();
                
           
            return true;
        }
        public static bool PreparePdf(string order_id,string sku, int qtd, int parts, string size)
        {
         
                for (int i = 1; i < parts + 1; i++)
                {
                string kitPart = "";
                if (parts > 1)
                {
                    kitPart = "-"+ToAlpha(i);
                }
                try
                {

                    System.IO.Directory.CreateDirectory(Settings1.Default.FolderOrders + "\\" + order_id + "\\" + size);
                    string caminhoDoNovoPDF = Settings1.Default.FolderOrders + "\\" + order_id + "\\" + "\\" + size + "\\" + sku + kitPart + "-"+qtd+".pdf";

                    using (iTextSharp.text.pdf.PdfReader reader = new iTextSharp.text.pdf.PdfReader(Settings1.Default.FolderPrepare + "\\" + size + "\\" + sku + ".pdf"))
                    using (FileStream fs = new FileStream(caminhoDoNovoPDF, FileMode.Create))
                    using (Document doc = new Document())
                    using (PdfCopy copy = new PdfCopy(doc, fs))
                    {
                        doc.Open();
                        copy.AddPage(copy.GetImportedPage(reader, i));
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                    return false;
                }
            }

                return true;

            }

        private static string ToAlpha(int i)
        {
            string result = "";
            do
            {
                result = (char)((i - 1) % 26 + 'A') + result;
                i = (i - 1) / 26;
            } while (i > 0);
            return result;
        }
    }
    
}


                

