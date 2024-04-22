namespace BlackVintage
{
    partial class FolderPDF
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FolderPDF));
            this.panel_PDF2 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_search = new System.Windows.Forms.Button();
            this.box_search = new System.Windows.Forms.TextBox();
            this.label_pdf = new System.Windows.Forms.Label();
            this.panel_PDF = new System.Windows.Forms.FlowLayoutPanel();
            this.button4 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel_PDF2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_PDF2
            // 
            this.panel_PDF2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.panel_PDF2.Controls.Add(this.panel1);
            this.panel_PDF2.Controls.Add(this.panel_PDF);
            this.panel_PDF2.Location = new System.Drawing.Point(21, 71);
            this.panel_PDF2.Name = "panel_PDF2";
            this.panel_PDF2.Size = new System.Drawing.Size(846, 603);
            this.panel_PDF2.TabIndex = 0;
            this.panel_PDF2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_PDF_Paint);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.btn_search);
            this.panel1.Controls.Add(this.box_search);
            this.panel1.Controls.Add(this.label_pdf);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(843, 100);
            this.panel1.TabIndex = 0;
            // 
            // btn_search
            // 
            this.btn_search.FlatAppearance.BorderSize = 0;
            this.btn_search.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_search.Image = global::BlackVintage.Properties.Resources.view_icon;
            this.btn_search.Location = new System.Drawing.Point(741, 23);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(35, 32);
            this.btn_search.TabIndex = 2;
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // box_search
            // 
            this.box_search.Font = new System.Drawing.Font("Leelawadee", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.box_search.Location = new System.Drawing.Point(628, 30);
            this.box_search.Name = "box_search";
            this.box_search.Size = new System.Drawing.Size(107, 33);
            this.box_search.TabIndex = 1;
            // 
            // label_pdf
            // 
            this.label_pdf.AutoSize = true;
            this.label_pdf.Font = new System.Drawing.Font("Leelawadee", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_pdf.Location = new System.Drawing.Point(308, 15);
            this.label_pdf.Name = "label_pdf";
            this.label_pdf.Size = new System.Drawing.Size(100, 35);
            this.label_pdf.TabIndex = 0;
            this.label_pdf.Text = "label1";
            // 
            // panel_PDF
            // 
            this.panel_PDF.AutoScroll = true;
            this.panel_PDF.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.panel_PDF.Location = new System.Drawing.Point(3, 109);
            this.panel_PDF.Name = "panel_PDF";
            this.panel_PDF.Size = new System.Drawing.Size(843, 504);
            this.panel_PDF.TabIndex = 1;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Transparent;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.ForeColor = System.Drawing.Color.Transparent;
            this.button4.Image = ((System.Drawing.Image)(resources.GetObject("button4.Image")));
            this.button4.Location = new System.Drawing.Point(828, 12);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(39, 42);
            this.button4.TabIndex = 11;
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.Transparent;
            this.button1.Image = global::BlackVintage.Properties.Resources.icon_arrow;
            this.button1.Location = new System.Drawing.Point(3, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(106, 91);
            this.button1.TabIndex = 5;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FolderPDF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.ClientSize = new System.Drawing.Size(905, 703);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.panel_PDF2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FolderPDF";
            this.Text = "FolderPDF";
            this.Load += new System.EventHandler(this.FolderPDF_Load);
            this.panel_PDF2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel panel_PDF2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label_pdf;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.TextBox box_search;
        private System.Windows.Forms.FlowLayoutPanel panel_PDF;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button1;
    }
}