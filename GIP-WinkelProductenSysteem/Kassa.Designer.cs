namespace GIP_WinkelProductenSysteem
{
    partial class Kassa
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
            this.lvProducten = new System.Windows.Forms.ListView();
            this.columnPrijs = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnNaam = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txbHuidigProdNaam = new System.Windows.Forms.TextBox();
            this.lblPrijs = new System.Windows.Forms.Label();
            this.txbHuidigProdPrijs = new System.Windows.Forms.TextBox();
            this.lblProdNaam = new System.Windows.Forms.Label();
            this.btnVoegToe = new System.Windows.Forms.Button();
            this.btnKorting = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txbKorting = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lvProducten
            // 
            this.lvProducten.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnPrijs,
            this.columnNaam});
            this.lvProducten.HideSelection = false;
            this.lvProducten.Location = new System.Drawing.Point(12, 12);
            this.lvProducten.Name = "lvProducten";
            this.lvProducten.Size = new System.Drawing.Size(307, 517);
            this.lvProducten.TabIndex = 0;
            this.lvProducten.UseCompatibleStateImageBehavior = false;
            // 
            // txbHuidigProdNaam
            // 
            this.txbHuidigProdNaam.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbHuidigProdNaam.Location = new System.Drawing.Point(465, 12);
            this.txbHuidigProdNaam.Name = "txbHuidigProdNaam";
            this.txbHuidigProdNaam.Size = new System.Drawing.Size(317, 30);
            this.txbHuidigProdNaam.TabIndex = 2;
            // 
            // lblPrijs
            // 
            this.lblPrijs.AutoSize = true;
            this.lblPrijs.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrijs.Location = new System.Drawing.Point(442, 51);
            this.lblPrijs.Name = "lblPrijs";
            this.lblPrijs.Size = new System.Drawing.Size(23, 25);
            this.lblPrijs.TabIndex = 21;
            this.lblPrijs.Text = "€";
            // 
            // txbHuidigProdPrijs
            // 
            this.txbHuidigProdPrijs.Enabled = false;
            this.txbHuidigProdPrijs.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbHuidigProdPrijs.Location = new System.Drawing.Point(465, 48);
            this.txbHuidigProdPrijs.Name = "txbHuidigProdPrijs";
            this.txbHuidigProdPrijs.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txbHuidigProdPrijs.Size = new System.Drawing.Size(117, 30);
            this.txbHuidigProdPrijs.TabIndex = 20;
            this.txbHuidigProdPrijs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblProdNaam
            // 
            this.lblProdNaam.AutoSize = true;
            this.lblProdNaam.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProdNaam.Location = new System.Drawing.Point(325, 15);
            this.lblProdNaam.Name = "lblProdNaam";
            this.lblProdNaam.Size = new System.Drawing.Size(134, 25);
            this.lblProdNaam.TabIndex = 22;
            this.lblProdNaam.Text = "Naam product";
            // 
            // btnVoegToe
            // 
            this.btnVoegToe.Location = new System.Drawing.Point(588, 48);
            this.btnVoegToe.Name = "btnVoegToe";
            this.btnVoegToe.Size = new System.Drawing.Size(194, 30);
            this.btnVoegToe.TabIndex = 23;
            this.btnVoegToe.Text = "In winkelmand";
            this.btnVoegToe.UseVisualStyleBackColor = true;
            this.btnVoegToe.Click += new System.EventHandler(this.btnVoegToe_Click);
            // 
            // btnKorting
            // 
            this.btnKorting.Location = new System.Drawing.Point(588, 84);
            this.btnKorting.Name = "btnKorting";
            this.btnKorting.Size = new System.Drawing.Size(194, 30);
            this.btnKorting.TabIndex = 24;
            this.btnKorting.Text = "Korting";
            this.btnKorting.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(435, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 25);
            this.label1.TabIndex = 26;
            this.label1.Text = "%";
            // 
            // txbKorting
            // 
            this.txbKorting.Enabled = false;
            this.txbKorting.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbKorting.Location = new System.Drawing.Point(465, 84);
            this.txbKorting.Name = "txbKorting";
            this.txbKorting.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txbKorting.Size = new System.Drawing.Size(117, 30);
            this.txbKorting.TabIndex = 25;
            this.txbKorting.Text = "0";
            this.txbKorting.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Kassa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(889, 541);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txbKorting);
            this.Controls.Add(this.btnKorting);
            this.Controls.Add(this.btnVoegToe);
            this.Controls.Add(this.lblProdNaam);
            this.Controls.Add(this.lblPrijs);
            this.Controls.Add(this.txbHuidigProdPrijs);
            this.Controls.Add(this.txbHuidigProdNaam);
            this.Controls.Add(this.lvProducten);
            this.Name = "Kassa";
            this.Text = "Kassa";
            this.Load += new System.EventHandler(this.Kassa_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvProducten;
        private System.Windows.Forms.TextBox txbHuidigProdNaam;
        private System.Windows.Forms.Label lblPrijs;
        public System.Windows.Forms.TextBox txbHuidigProdPrijs;
        private System.Windows.Forms.ColumnHeader columnPrijs;
        private System.Windows.Forms.ColumnHeader columnNaam;
        private System.Windows.Forms.Label lblProdNaam;
        private System.Windows.Forms.Button btnVoegToe;
        private System.Windows.Forms.Button btnKorting;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txbKorting;
    }
}