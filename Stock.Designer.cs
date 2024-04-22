namespace BlackVintage
{
    partial class Stock
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.roundedButton1 = new BlackVintage.RoundedButton();
            this.btn_search = new System.Windows.Forms.Button();
            this.box_search = new System.Windows.Forms.TextBox();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.OrderLabel = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.msg_error = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.panel1.Controls.Add(this.msg_error);
            this.panel1.Controls.Add(this.roundedButton1);
            this.panel1.Controls.Add(this.btn_search);
            this.panel1.Controls.Add(this.box_search);
            this.panel1.Controls.Add(this.dataGridView);
            this.panel1.Controls.Add(this.OrderLabel);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(945, 688);
            this.panel1.TabIndex = 5;
            // 
            // roundedButton1
            // 
            this.roundedButton1.BackColor = System.Drawing.Color.ForestGreen;
            this.roundedButton1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.roundedButton1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.roundedButton1.Location = new System.Drawing.Point(103, 88);
            this.roundedButton1.Name = "roundedButton1";
            this.roundedButton1.Size = new System.Drawing.Size(148, 37);
            this.roundedButton1.TabIndex = 7;
            this.roundedButton1.Text = "Adicionar produto";
            this.roundedButton1.UseVisualStyleBackColor = false;
            this.roundedButton1.Click += new System.EventHandler(this.roundedButton1_Click);
            // 
            // btn_search
            // 
            this.btn_search.FlatAppearance.BorderSize = 0;
            this.btn_search.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_search.Image = global::BlackVintage.Properties.Resources.view_icon;
            this.btn_search.Location = new System.Drawing.Point(763, 54);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(35, 32);
            this.btn_search.TabIndex = 6;
            this.btn_search.UseVisualStyleBackColor = true;
            // 
            // box_search
            // 
            this.box_search.Font = new System.Drawing.Font("Leelawadee", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.box_search.Location = new System.Drawing.Point(650, 57);
            this.box_search.Name = "box_search";
            this.box_search.Size = new System.Drawing.Size(107, 33);
            this.box_search.TabIndex = 4;
            this.box_search.TextChanged += new System.EventHandler(this.box_search_TextChanged);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(103, 146);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.Size = new System.Drawing.Size(695, 527);
            this.dataGridView.TabIndex = 2;
            // 
            // OrderLabel
            // 
            this.OrderLabel.AutoSize = true;
            this.OrderLabel.Font = new System.Drawing.Font("Leelawadee", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OrderLabel.Location = new System.Drawing.Point(304, 54);
            this.OrderLabel.Name = "OrderLabel";
            this.OrderLabel.Size = new System.Drawing.Size(289, 32);
            this.OrderLabel.TabIndex = 2;
            this.OrderLabel.Text = "Produtos em estoque";
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Transparent;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.ForeColor = System.Drawing.Color.Transparent;
            this.button4.Image = global::BlackVintage.Properties.Resources.black_cross;
            this.button4.Location = new System.Drawing.Point(835, 12);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(39, 40);
            this.button4.TabIndex = 3;
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // msg_error
            // 
            this.msg_error.AutoSize = true;
            this.msg_error.Font = new System.Drawing.Font("Leelawadee", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.msg_error.Location = new System.Drawing.Point(223, 357);
            this.msg_error.Name = "msg_error";
            this.msg_error.Size = new System.Drawing.Size(471, 39);
            this.msg_error.TabIndex = 8;
            this.msg_error.Text = "Nenhum produto em estoque!";
            this.msg_error.Visible = false;
            // 
            // Stock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 685);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Stock";
            this.Text = "Stock";
            this.Load += new System.EventHandler(this.Stock_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label OrderLabel;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.TextBox box_search;
        private System.Windows.Forms.Button btn_search;
        private RoundedButton roundedButton1;
        private System.Windows.Forms.Label msg_error;
    }
}