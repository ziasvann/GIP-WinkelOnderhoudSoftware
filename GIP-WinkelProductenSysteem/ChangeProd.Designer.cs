﻿
namespace GIP_WinkelProductenSysteem
{
    partial class ChangeProd
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("");
            this.errorProv = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnMaakProduct = new System.Windows.Forms.Button();
            this.btnChangeProd = new System.Windows.Forms.Button();
            this.btnDelProduct = new System.Windows.Forms.Button();
            this.pnlProductEigenschappen = new System.Windows.Forms.Panel();
            this.btnBevestigProducten = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txbAantalBestAanwezig = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txbAantalAanwezig = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txbNaam = new System.Windows.Forms.TextBox();
            this.txbCategorie = new System.Windows.Forms.TextBox();
            this.columnProducten = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnCategorie = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnAanwezig = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnAantalBest = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvProducten = new System.Windows.Forms.ListView();
            ((System.ComponentModel.ISupportInitialize)(this.errorProv)).BeginInit();
            this.pnlProductEigenschappen.SuspendLayout();
            this.SuspendLayout();
            // 
            // errorProv
            // 
            this.errorProv.ContainerControl = this;
            this.errorProv.RightToLeft = true;
            // 
            // btnMaakProduct
            // 
            this.btnMaakProduct.Location = new System.Drawing.Point(48, 12);
            this.btnMaakProduct.Name = "btnMaakProduct";
            this.btnMaakProduct.Size = new System.Drawing.Size(142, 41);
            this.btnMaakProduct.TabIndex = 13;
            this.btnMaakProduct.Text = "Maak product";
            this.btnMaakProduct.UseVisualStyleBackColor = true;
            this.btnMaakProduct.Click += new System.EventHandler(this.btnMaakProduct_Click);
            // 
            // btnChangeProd
            // 
            this.btnChangeProd.Location = new System.Drawing.Point(48, 59);
            this.btnChangeProd.Name = "btnChangeProd";
            this.btnChangeProd.Size = new System.Drawing.Size(142, 41);
            this.btnChangeProd.TabIndex = 14;
            this.btnChangeProd.Text = "Wijzig product";
            this.btnChangeProd.UseVisualStyleBackColor = true;
            this.btnChangeProd.Click += new System.EventHandler(this.btnChangeProd_Click);
            // 
            // btnDelProduct
            // 
            this.btnDelProduct.Location = new System.Drawing.Point(48, 106);
            this.btnDelProduct.Name = "btnDelProduct";
            this.btnDelProduct.Size = new System.Drawing.Size(142, 41);
            this.btnDelProduct.TabIndex = 15;
            this.btnDelProduct.Text = "Verwijder product";
            this.btnDelProduct.UseVisualStyleBackColor = true;
            this.btnDelProduct.Click += new System.EventHandler(this.btnDelProduct_Click);
            // 
            // pnlProductEigenschappen
            // 
            this.pnlProductEigenschappen.BackColor = System.Drawing.Color.Transparent;
            this.pnlProductEigenschappen.Controls.Add(this.btnBevestigProducten);
            this.pnlProductEigenschappen.Controls.Add(this.label4);
            this.pnlProductEigenschappen.Controls.Add(this.txbAantalBestAanwezig);
            this.pnlProductEigenschappen.Controls.Add(this.label3);
            this.pnlProductEigenschappen.Controls.Add(this.txbAantalAanwezig);
            this.pnlProductEigenschappen.Controls.Add(this.label1);
            this.pnlProductEigenschappen.Controls.Add(this.label2);
            this.pnlProductEigenschappen.Controls.Add(this.txbNaam);
            this.pnlProductEigenschappen.Controls.Add(this.txbCategorie);
            this.pnlProductEigenschappen.Location = new System.Drawing.Point(12, 153);
            this.pnlProductEigenschappen.Name = "pnlProductEigenschappen";
            this.pnlProductEigenschappen.Size = new System.Drawing.Size(178, 280);
            this.pnlProductEigenschappen.TabIndex = 16;
            this.pnlProductEigenschappen.Visible = false;
            // 
            // btnBevestigProducten
            // 
            this.btnBevestigProducten.Location = new System.Drawing.Point(32, 233);
            this.btnBevestigProducten.Name = "btnBevestigProducten";
            this.btnBevestigProducten.Size = new System.Drawing.Size(142, 41);
            this.btnBevestigProducten.TabIndex = 14;
            this.btnBevestigProducten.Text = "Bevestigen";
            this.btnBevestigProducten.UseVisualStyleBackColor = true;
            this.btnBevestigProducten.Click += new System.EventHandler(this.btnBevestigProducten_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 186);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(147, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Geef aantal best aanwezig in:";
            // 
            // txbAantalBestAanwezig
            // 
            this.txbAantalBestAanwezig.Location = new System.Drawing.Point(33, 202);
            this.txbAantalBestAanwezig.Multiline = true;
            this.txbAantalBestAanwezig.Name = "txbAantalBestAanwezig";
            this.txbAantalBestAanwezig.Size = new System.Drawing.Size(142, 28);
            this.txbAantalBestAanwezig.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(54, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Geef aantal aanwezig in:";
            // 
            // txbAantalAanwezig
            // 
            this.txbAantalAanwezig.Location = new System.Drawing.Point(33, 142);
            this.txbAantalAanwezig.Multiline = true;
            this.txbAantalAanwezig.Name = "txbAantalAanwezig";
            this.txbAantalAanwezig.Size = new System.Drawing.Size(142, 28);
            this.txbAantalAanwezig.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Geef naam van product in:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(87, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Geef categorie in:";
            // 
            // txbNaam
            // 
            this.txbNaam.Location = new System.Drawing.Point(30, 22);
            this.txbNaam.Multiline = true;
            this.txbNaam.Name = "txbNaam";
            this.txbNaam.Size = new System.Drawing.Size(142, 28);
            this.txbNaam.TabIndex = 6;
            // 
            // txbCategorie
            // 
            this.txbCategorie.Location = new System.Drawing.Point(33, 80);
            this.txbCategorie.Multiline = true;
            this.txbCategorie.Name = "txbCategorie";
            this.txbCategorie.Size = new System.Drawing.Size(142, 28);
            this.txbCategorie.TabIndex = 8;
            // 
            // columnProducten
            // 
            this.columnProducten.Text = "Product";
            this.columnProducten.Width = 116;
            // 
            // columnCategorie
            // 
            this.columnCategorie.Text = "Categorie";
            this.columnCategorie.Width = 89;
            // 
            // columnAanwezig
            // 
            this.columnAanwezig.Text = "Aantal aanwezig";
            this.columnAanwezig.Width = 92;
            // 
            // columnAantalBest
            // 
            this.columnAantalBest.Text = "Aantal best aanwezig";
            this.columnAantalBest.Width = 119;
            // 
            // lvProducten
            // 
            this.lvProducten.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnProducten,
            this.columnCategorie,
            this.columnAanwezig,
            this.columnAantalBest});
            this.lvProducten.FullRowSelect = true;
            this.lvProducten.HideSelection = false;
            listViewItem1.StateImageIndex = 0;
            this.lvProducten.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.lvProducten.Location = new System.Drawing.Point(196, 12);
            this.lvProducten.Name = "lvProducten";
            this.lvProducten.Size = new System.Drawing.Size(592, 417);
            this.lvProducten.TabIndex = 4;
            this.lvProducten.UseCompatibleStateImageBehavior = false;
            this.lvProducten.View = System.Windows.Forms.View.Details;
            // 
            // ChangeProd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 441);
            this.Controls.Add(this.pnlProductEigenschappen);
            this.Controls.Add(this.btnDelProduct);
            this.Controls.Add(this.btnChangeProd);
            this.Controls.Add(this.btnMaakProduct);
            this.Controls.Add(this.lvProducten);
            this.Name = "ChangeProd";
            this.Text = "Verander product";
            this.Load += new System.EventHandler(this.ChangeProd_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProv)).EndInit();
            this.pnlProductEigenschappen.ResumeLayout(false);
            this.pnlProductEigenschappen.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.TextBox txbNaam;
        public System.Windows.Forms.ErrorProvider errorProv;
        public System.Windows.Forms.Button btnDelProduct;
        public System.Windows.Forms.Button btnChangeProd;
        public System.Windows.Forms.Button btnMaakProduct;
        public System.Windows.Forms.Panel pnlProductEigenschappen;
        public System.Windows.Forms.Button btnBevestigProducten;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox txbAantalBestAanwezig;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox txbAantalAanwezig;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txbCategorie;
        public System.Windows.Forms.ListView lvProducten;
        public System.Windows.Forms.ColumnHeader columnProducten;
        public System.Windows.Forms.ColumnHeader columnCategorie;
        public System.Windows.Forms.ColumnHeader columnAanwezig;
        public System.Windows.Forms.ColumnHeader columnAantalBest;
    }
}