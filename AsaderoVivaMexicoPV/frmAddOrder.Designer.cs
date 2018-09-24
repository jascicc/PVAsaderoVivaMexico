namespace AsaderoVivaMexicoPV
{
    partial class frmAddOrder
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
            this.pCategories = new System.Windows.Forms.Panel();
            this.pProducts = new System.Windows.Forms.Panel();
            this.btnBack = new System.Windows.Forms.Button();
            this.pbCart = new System.Windows.Forms.PictureBox();
            this.lblTotalProducts = new System.Windows.Forms.Label();
            this.btnPendientes = new System.Windows.Forms.Button();
            this.btnPagadas = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbCart)).BeginInit();
            this.SuspendLayout();
            // 
            // pCategories
            // 
            this.pCategories.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pCategories.Location = new System.Drawing.Point(12, 98);
            this.pCategories.Name = "pCategories";
            this.pCategories.Size = new System.Drawing.Size(133, 414);
            this.pCategories.TabIndex = 0;
            // 
            // pProducts
            // 
            this.pProducts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pProducts.Location = new System.Drawing.Point(160, 98);
            this.pProducts.Name = "pProducts";
            this.pProducts.Size = new System.Drawing.Size(482, 414);
            this.pProducts.TabIndex = 1;
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(12, 12);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 2;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // pbCart
            // 
            this.pbCart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbCart.Location = new System.Drawing.Point(685, 98);
            this.pbCart.Name = "pbCart";
            this.pbCart.Size = new System.Drawing.Size(100, 82);
            this.pbCart.TabIndex = 3;
            this.pbCart.TabStop = false;
            this.pbCart.Click += new System.EventHandler(this.pbCart_Click);
            // 
            // lblTotalProducts
            // 
            this.lblTotalProducts.AutoSize = true;
            this.lblTotalProducts.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblTotalProducts.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalProducts.Location = new System.Drawing.Point(764, 85);
            this.lblTotalProducts.Name = "lblTotalProducts";
            this.lblTotalProducts.Size = new System.Drawing.Size(31, 33);
            this.lblTotalProducts.TabIndex = 4;
            this.lblTotalProducts.Text = "0";
            // 
            // btnPendientes
            // 
            this.btnPendientes.Location = new System.Drawing.Point(626, 23);
            this.btnPendientes.Name = "btnPendientes";
            this.btnPendientes.Size = new System.Drawing.Size(99, 23);
            this.btnPendientes.TabIndex = 5;
            this.btnPendientes.Text = "Pendientes";
            this.btnPendientes.UseVisualStyleBackColor = true;
            this.btnPendientes.Click += new System.EventHandler(this.btnPendientes_Click);
            // 
            // btnPagadas
            // 
            this.btnPagadas.Location = new System.Drawing.Point(326, 39);
            this.btnPagadas.Name = "btnPagadas";
            this.btnPagadas.Size = new System.Drawing.Size(75, 23);
            this.btnPagadas.TabIndex = 6;
            this.btnPagadas.Text = "Pagadas";
            this.btnPagadas.UseVisualStyleBackColor = true;
            this.btnPagadas.Click += new System.EventHandler(this.btnPagadas_Click);
            // 
            // frmAddOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(819, 538);
            this.Controls.Add(this.btnPagadas);
            this.Controls.Add(this.btnPendientes);
            this.Controls.Add(this.lblTotalProducts);
            this.Controls.Add(this.pbCart);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.pProducts);
            this.Controls.Add(this.pCategories);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmAddOrder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frmCreateOrder";
            ((System.ComponentModel.ISupportInitialize)(this.pbCart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pCategories;
        private System.Windows.Forms.Panel pProducts;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.PictureBox pbCart;
        private System.Windows.Forms.Label lblTotalProducts;
        private System.Windows.Forms.Button btnPendientes;
        private System.Windows.Forms.Button btnPagadas;
    }
}