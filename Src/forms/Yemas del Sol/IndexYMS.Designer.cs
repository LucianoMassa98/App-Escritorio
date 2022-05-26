namespace E_Shop.Src.forms.Yemas_del_Sol
{
    partial class IndexYMS
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
            this.button1 = new System.Windows.Forms.Button();
            this.buttonPersonalizado1 = new E_Shop.ButtonPersonalizado();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(296, 113);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // buttonPersonalizado1
            // 
            this.buttonPersonalizado1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.buttonPersonalizado1.FlatAppearance.BorderSize = 0;
            this.buttonPersonalizado1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPersonalizado1.ForeColor = System.Drawing.Color.White;
            this.buttonPersonalizado1.Location = new System.Drawing.Point(48, 106);
            this.buttonPersonalizado1.Name = "buttonPersonalizado1";
            this.buttonPersonalizado1.Size = new System.Drawing.Size(154, 30);
            this.buttonPersonalizado1.TabIndex = 1;
            this.buttonPersonalizado1.Text = "buttonPersonalizado1";
            this.buttonPersonalizado1.UseVisualStyleBackColor = false;
            this.buttonPersonalizado1.Click += new System.EventHandler(this.buttonPersonalizado1_Click);
            // 
            // IndexYMS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 561);
            this.Controls.Add(this.buttonPersonalizado1);
            this.Controls.Add(this.button1);
            this.Name = "IndexYMS";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "IndexYMS";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.IndexYMS_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private ButtonPersonalizado buttonPersonalizado1;
    }
}