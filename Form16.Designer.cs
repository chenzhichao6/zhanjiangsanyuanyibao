namespace WindowsFormsApp1
{
    partial class Form16
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form16));
            this.Pvd = new System.Windows.Forms.PrintPreviewDialog();
            this.Psd = new System.Windows.Forms.PageSetupDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Pvd
            // 
            this.Pvd.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.Pvd.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.Pvd.ClientSize = new System.Drawing.Size(400, 300);
            this.Pvd.Enabled = true;
            this.Pvd.Icon = ((System.Drawing.Icon)(resources.GetObject("Pvd.Icon")));
            this.Pvd.Name = "Pvd";
            this.Pvd.Visible = false;
            this.Pvd.Load += new System.EventHandler(this.Pvd_Load);
            // 
            // Psd
            // 
            this.Psd.EnableMetric = true;
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(101, 113);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(82, 36);
            this.button3.TabIndex = 13;
            this.button3.Text = "打印";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form16
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.button3);
            this.Name = "Form16";
            this.Text = "Form16";
            this.Load += new System.EventHandler(this.Form16_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PrintPreviewDialog Pvd;
        private System.Windows.Forms.PageSetupDialog Psd;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.Button button3;
    }
}