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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Kassa));
            this.txbHuidigProdNaam = new System.Windows.Forms.TextBox();
            this.lblProdNaam = new System.Windows.Forms.Label();
            this.btnVoegToe = new System.Windows.Forms.Button();
            this.btnKorting = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txbKorting = new System.Windows.Forms.TextBox();
            this.errorProv = new System.Windows.Forms.ErrorProvider(this.components);
            this.lvProducten = new System.Windows.Forms.ListView();
            this.columnProducten = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnEenheidsPrijs = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnAantal = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnTotaalPrijs = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label2 = new System.Windows.Forms.Label();
            this.lblTotPrijs = new System.Windows.Forms.Label();
            this.pnlKorting = new System.Windows.Forms.Panel();
            this.btnManuelePrijs = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnBevestigPrijs = new System.Windows.Forms.Button();
            this.txbNieuwePrijs = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.nmudAantal = new System.Windows.Forms.NumericUpDown();
            this.btnVerwijderProd = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.errorProv)).BeginInit();
            this.pnlKorting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmudAantal)).BeginInit();
            this.SuspendLayout();
            // 
            // txbHuidigProdNaam
            // 
            this.txbHuidigProdNaam.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txbHuidigProdNaam.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbHuidigProdNaam.Location = new System.Drawing.Point(465, 12);
            this.txbHuidigProdNaam.Name = "txbHuidigProdNaam";
            this.txbHuidigProdNaam.Size = new System.Drawing.Size(317, 30);
            this.txbHuidigProdNaam.TabIndex = 2;
            this.txbHuidigProdNaam.TextChanged += new System.EventHandler(this.txbHuidigProdNaam_TextChanged);
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
            this.btnVoegToe.Size = new System.Drawing.Size(194, 66);
            this.btnVoegToe.TabIndex = 23;
            this.btnVoegToe.Text = "In winkelmand";
            this.btnVoegToe.UseVisualStyleBackColor = true;
            this.btnVoegToe.Click += new System.EventHandler(this.btnVoegToe_Click);
            // 
            // btnKorting
            // 
            this.btnKorting.Location = new System.Drawing.Point(588, 307);
            this.btnKorting.Name = "btnKorting";
            this.btnKorting.Size = new System.Drawing.Size(194, 30);
            this.btnKorting.TabIndex = 24;
            this.btnKorting.Text = "Korting";
            this.btnKorting.UseVisualStyleBackColor = true;
            this.btnKorting.Click += new System.EventHandler(this.btnKorting_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(-2, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 25);
            this.label1.TabIndex = 26;
            this.label1.Text = "%";
            // 
            // txbKorting
            // 
            this.txbKorting.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbKorting.Location = new System.Drawing.Point(29, 3);
            this.txbKorting.Name = "txbKorting";
            this.txbKorting.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txbKorting.Size = new System.Drawing.Size(168, 30);
            this.txbKorting.TabIndex = 25;
            this.txbKorting.Text = "0";
            this.txbKorting.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // errorProv
            // 
            this.errorProv.ContainerControl = this;
            // 
            // lvProducten
            // 
            this.lvProducten.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnProducten,
            this.columnEenheidsPrijs,
            this.columnAantal,
            this.columnTotaalPrijs});
            this.lvProducten.FullRowSelect = true;
            this.lvProducten.HideSelection = false;
            this.lvProducten.Location = new System.Drawing.Point(12, 12);
            this.lvProducten.Name = "lvProducten";
            this.lvProducten.Size = new System.Drawing.Size(307, 479);
            this.lvProducten.TabIndex = 27;
            this.lvProducten.UseCompatibleStateImageBehavior = false;
            this.lvProducten.View = System.Windows.Forms.View.Details;
            // 
            // columnProducten
            // 
            this.columnProducten.Text = "Product";
            this.columnProducten.Width = 110;
            // 
            // columnEenheidsPrijs
            // 
            this.columnEenheidsPrijs.Text = "Eenheids prijs";
            this.columnEenheidsPrijs.Width = 80;
            // 
            // columnAantal
            // 
            this.columnAantal.Text = "Aantal";
            this.columnAantal.Width = 50;
            // 
            // columnTotaalPrijs
            // 
            this.columnTotaalPrijs.Text = "Totaal prijs";
            this.columnTotaalPrijs.Width = 75;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(7, 507);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 25);
            this.label2.TabIndex = 28;
            this.label2.Text = "Totaal prijs: €";
            // 
            // lblTotPrijs
            // 
            this.lblTotPrijs.AutoSize = true;
            this.lblTotPrijs.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotPrijs.Location = new System.Drawing.Point(142, 507);
            this.lblTotPrijs.Name = "lblTotPrijs";
            this.lblTotPrijs.Size = new System.Drawing.Size(50, 25);
            this.lblTotPrijs.TabIndex = 29;
            this.lblTotPrijs.Text = "0.00";
            // 
            // pnlKorting
            // 
            this.pnlKorting.Controls.Add(this.btnManuelePrijs);
            this.pnlKorting.Controls.Add(this.label3);
            this.pnlKorting.Controls.Add(this.btnBevestigPrijs);
            this.pnlKorting.Controls.Add(this.txbNieuwePrijs);
            this.pnlKorting.Controls.Add(this.txbKorting);
            this.pnlKorting.Controls.Add(this.label1);
            this.pnlKorting.Location = new System.Drawing.Point(588, 343);
            this.pnlKorting.Name = "pnlKorting";
            this.pnlKorting.Size = new System.Drawing.Size(217, 148);
            this.pnlKorting.TabIndex = 30;
            this.pnlKorting.Visible = false;
            // 
            // btnManuelePrijs
            // 
            this.btnManuelePrijs.Location = new System.Drawing.Point(3, 75);
            this.btnManuelePrijs.Name = "btnManuelePrijs";
            this.btnManuelePrijs.Size = new System.Drawing.Size(194, 30);
            this.btnManuelePrijs.TabIndex = 33;
            this.btnManuelePrijs.Text = "Manuele prijs";
            this.btnManuelePrijs.UseVisualStyleBackColor = true;
            this.btnManuelePrijs.Click += new System.EventHandler(this.btnManuelePrijs_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(-2, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 25);
            this.label3.TabIndex = 32;
            this.label3.Text = "€";
            // 
            // btnBevestigPrijs
            // 
            this.btnBevestigPrijs.Location = new System.Drawing.Point(3, 111);
            this.btnBevestigPrijs.Name = "btnBevestigPrijs";
            this.btnBevestigPrijs.Size = new System.Drawing.Size(194, 30);
            this.btnBevestigPrijs.TabIndex = 31;
            this.btnBevestigPrijs.Text = "Bevestig prijs";
            this.btnBevestigPrijs.UseVisualStyleBackColor = true;
            this.btnBevestigPrijs.Click += new System.EventHandler(this.btnBevestigPrijs_Click);
            // 
            // txbNieuwePrijs
            // 
            this.txbNieuwePrijs.Enabled = false;
            this.txbNieuwePrijs.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbNieuwePrijs.Location = new System.Drawing.Point(29, 39);
            this.txbNieuwePrijs.Name = "txbNieuwePrijs";
            this.txbNieuwePrijs.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txbNieuwePrijs.Size = new System.Drawing.Size(168, 30);
            this.txbNieuwePrijs.TabIndex = 31;
            this.txbNieuwePrijs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(380, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 25);
            this.label4.TabIndex = 32;
            this.label4.Text = "Aantal: ";
            // 
            // nmudAantal
            // 
            this.nmudAantal.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nmudAantal.Location = new System.Drawing.Point(465, 48);
            this.nmudAantal.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nmudAantal.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmudAantal.Name = "nmudAantal";
            this.nmudAantal.ReadOnly = true;
            this.nmudAantal.Size = new System.Drawing.Size(117, 30);
            this.nmudAantal.TabIndex = 33;
            this.nmudAantal.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnVerwijderProd
            // 
            this.btnVerwijderProd.Location = new System.Drawing.Point(465, 84);
            this.btnVerwijderProd.Name = "btnVerwijderProd";
            this.btnVerwijderProd.Size = new System.Drawing.Size(117, 30);
            this.btnVerwijderProd.TabIndex = 34;
            this.btnVerwijderProd.Text = "Verwijder product";
            this.btnVerwijderProd.UseVisualStyleBackColor = true;
            this.btnVerwijderProd.Click += new System.EventHandler(this.btnVerwijderProd_Click);
            // 
            // Kassa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 541);
            this.Controls.Add(this.btnVerwijderProd);
            this.Controls.Add(this.nmudAantal);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pnlKorting);
            this.Controls.Add(this.lblTotPrijs);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lvProducten);
            this.Controls.Add(this.btnKorting);
            this.Controls.Add(this.btnVoegToe);
            this.Controls.Add(this.lblProdNaam);
            this.Controls.Add(this.txbHuidigProdNaam);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Kassa";
            this.Text = "Kassa";
            this.Load += new System.EventHandler(this.Kassa_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProv)).EndInit();
            this.pnlKorting.ResumeLayout(false);
            this.pnlKorting.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmudAantal)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txbHuidigProdNaam;
        private System.Windows.Forms.Label lblProdNaam;
        private System.Windows.Forms.Button btnVoegToe;
        private System.Windows.Forms.Button btnKorting;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txbKorting;
        private System.Windows.Forms.ErrorProvider errorProv;
        public System.Windows.Forms.ListView lvProducten;
        public System.Windows.Forms.ColumnHeader columnProducten;
        private System.Windows.Forms.ColumnHeader columnEenheidsPrijs;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTotPrijs;
        private System.Windows.Forms.Panel pnlKorting;
        private System.Windows.Forms.Button btnBevestigPrijs;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox txbNieuwePrijs;
        private System.Windows.Forms.Button btnManuelePrijs;
        private System.Windows.Forms.NumericUpDown nmudAantal;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ColumnHeader columnAantal;
        private System.Windows.Forms.Button btnVerwijderProd;
        private System.Windows.Forms.ColumnHeader columnTotaalPrijs;
    }
}