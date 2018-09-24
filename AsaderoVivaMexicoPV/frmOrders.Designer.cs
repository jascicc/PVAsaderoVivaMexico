namespace AsaderoVivaMexicoPV
{
    partial class frmOrders
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
            this.pOrders = new System.Windows.Forms.Panel();
            this.btnBack = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pOrders
            // 
            this.pOrders.AutoScroll = true;
            this.pOrders.Location = new System.Drawing.Point(71, 54);
            this.pOrders.Name = "pOrders";
            this.pOrders.Size = new System.Drawing.Size(718, 456);
            this.pOrders.TabIndex = 0;
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(29, 12);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 1;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // frmOrders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(873, 534);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.pOrders);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmOrders";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frmOrders";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pOrders;
        private System.Windows.Forms.Button btnBack;
    }
}