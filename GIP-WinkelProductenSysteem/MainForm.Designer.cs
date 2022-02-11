
namespace GIP_WinkelProductenSysteem
{
    partial class MainForm
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
            this.btnChangeProdFrm = new System.Windows.Forms.Button();
            this.btnKassaFrm = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnChangeProdFrm
            // 
            this.btnChangeProdFrm.Location = new System.Drawing.Point(12, 12);
            this.btnChangeProdFrm.Name = "btnChangeProdFrm";
            this.btnChangeProdFrm.Size = new System.Drawing.Size(142, 60);
            this.btnChangeProdFrm.TabIndex = 0;
            this.btnChangeProdFrm.Text = "Producten wijzigen";
            this.btnChangeProdFrm.UseVisualStyleBackColor = true;
            this.btnChangeProdFrm.Click += new System.EventHandler(this.btnChangeProd_Click);
            // 
            // btnKassaFrm
            // 
            this.btnKassaFrm.Location = new System.Drawing.Point(12, 118);
            this.btnKassaFrm.Name = "btnKassaFrm";
            this.btnKassaFrm.Size = new System.Drawing.Size(142, 60);
            this.btnKassaFrm.TabIndex = 1;
            this.btnKassaFrm.Text = "Kassa";
            this.btnKassaFrm.UseVisualStyleBackColor = true;
            this.btnKassaFrm.Click += new System.EventHandler(this.btnKassaFrm_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnKassaFrm);
            this.Controls.Add(this.btnChangeProdFrm);
            this.Name = "MainForm";
            this.Text = "MainPage";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnChangeProdFrm;
        private System.Windows.Forms.Button btnKassaFrm;
    }
}

