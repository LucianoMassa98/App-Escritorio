﻿namespace E_Shop
{
    partial class CargarProducto
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CargarProducto));
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button5 = new System.Windows.Forms.Button();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CantidadEst = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Costo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Precio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.RosyBrown;
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.label14);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.comboBox1);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(942, 155);
            this.panel2.TabIndex = 106;
            // 
            // panel3
            // 
            this.panel3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Location = new System.Drawing.Point(395, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(378, 151);
            this.panel3.TabIndex = 126;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label14.Location = new System.Drawing.Point(188, 43);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(103, 20);
            this.label14.TabIndex = 125;
            this.label14.Text = "Stock: $0000";
            this.label14.Click += new System.EventHandler(this.label14_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(79, 105);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(96, 13);
            this.label11.TabIndex = 124;
            this.label11.Text = "Buscar por nombre";
            // 
            // comboBox1
            // 
            this.comboBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBox1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(82, 121);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(220, 26);
            this.comboBox1.TabIndex = 80;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Verdana", 27.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label9.Location = new System.Drawing.Point(74, 55);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(228, 45);
            this.label9.TabIndex = 79;
            this.label9.Text = "Productos";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(242)))), ((int)(((byte)(227)))));
            this.panel1.Controls.Add(this.button5);
            this.panel1.Controls.Add(this.textBox10);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.textBox4);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.textBox9);
            this.panel1.Controls.Add(this.textBox8);
            this.panel1.Controls.Add(this.textBox7);
            this.panel1.Controls.Add(this.textBox6);
            this.panel1.Controls.Add(this.textBox5);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(730, 177);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 337);
            this.panel1.TabIndex = 117;
            // 
            // button5
            // 
            this.button5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button5.BackgroundImage")));
            this.button5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button5.Location = new System.Drawing.Point(158, 6);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(39, 34);
            this.button5.TabIndex = 124;
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // textBox10
            // 
            this.textBox10.Enabled = false;
            this.textBox10.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox10.Location = new System.Drawing.Point(61, 46);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(100, 27);
            this.textBox10.TabIndex = 123;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 49);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(40, 13);
            this.label12.TabIndex = 122;
            this.label12.Text = "Codigo";
            // 
            // textBox4
            // 
            this.textBox4.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox4.Location = new System.Drawing.Point(61, 274);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(100, 27);
            this.textBox4.TabIndex = 13;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 281);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 13);
            this.label10.TabIndex = 12;
            this.label10.Text = "Venta";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(86, 300);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 11;
            this.button3.Text = "Guardar";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // textBox9
            // 
            this.textBox9.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox9.Location = new System.Drawing.Point(61, 237);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(100, 27);
            this.textBox9.TabIndex = 10;
            // 
            // textBox8
            // 
            this.textBox8.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox8.Location = new System.Drawing.Point(61, 199);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(100, 27);
            this.textBox8.TabIndex = 9;
            // 
            // textBox7
            // 
            this.textBox7.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox7.Location = new System.Drawing.Point(61, 161);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(100, 27);
            this.textBox7.TabIndex = 8;
            // 
            // textBox6
            // 
            this.textBox6.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox6.Location = new System.Drawing.Point(61, 124);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(100, 27);
            this.textBox6.TabIndex = 7;
            // 
            // textBox5
            // 
            this.textBox5.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox5.Location = new System.Drawing.Point(61, 82);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(100, 27);
            this.textBox5.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 244);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Costo";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 202);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 26);
            this.label7.TabIndex = 4;
            this.label7.Text = "Cantidad\r\nEstandar";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 161);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "Cantidad";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 127);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Descripcion";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Nombre";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "Modificar";
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.Location = new System.Drawing.Point(264, 215);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 27);
            this.textBox2.TabIndex = 114;
            this.textBox2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox2_KeyPress);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(155, 215);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 27);
            this.textBox1.TabIndex = 113;
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(242)))), ((int)(((byte)(227)))));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Codigo,
            this.Nombre,
            this.Descripcion,
            this.Cantidad,
            this.CantidadEst,
            this.Costo,
            this.Precio});
            this.dataGridView1.Location = new System.Drawing.Point(39, 259);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(610, 255);
            this.dataGridView1.TabIndex = 107;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // Codigo
            // 
            this.Codigo.HeaderText = "Codigo";
            this.Codigo.Name = "Codigo";
            this.Codigo.ReadOnly = true;
            // 
            // Nombre
            // 
            this.Nombre.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Nombre.HeaderText = "Nombre";
            this.Nombre.Name = "Nombre";
            this.Nombre.ReadOnly = true;
            this.Nombre.Width = 69;
            // 
            // Descripcion
            // 
            this.Descripcion.HeaderText = "Descripcion";
            this.Descripcion.Name = "Descripcion";
            this.Descripcion.ReadOnly = true;
            // 
            // Cantidad
            // 
            this.Cantidad.HeaderText = "Cantidad";
            this.Cantidad.Name = "Cantidad";
            this.Cantidad.ReadOnly = true;
            // 
            // CantidadEst
            // 
            this.CantidadEst.HeaderText = "Cantidad Estandar";
            this.CantidadEst.Name = "CantidadEst";
            this.CantidadEst.ReadOnly = true;
            // 
            // Costo
            // 
            this.Costo.HeaderText = "Costo";
            this.Costo.Name = "Costo";
            this.Costo.ReadOnly = true;
            // 
            // Precio
            // 
            this.Precio.HeaderText = "Precio";
            this.Precio.Name = "Precio";
            this.Precio.ReadOnly = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.DodgerBlue;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(152, 199);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 16);
            this.label1.TabIndex = 111;
            this.label1.Text = "Nombre";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.DodgerBlue;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(261, 199);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(90, 16);
            this.label13.TabIndex = 110;
            this.label13.Text = "Descripcion";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.LightGreen;
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Transparent;
            this.button1.Location = new System.Drawing.Point(404, 198);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(49, 51);
            this.button1.TabIndex = 108;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox3
            // 
            this.textBox3.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox3.Location = new System.Drawing.Point(44, 215);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 27);
            this.textBox3.TabIndex = 121;
            this.textBox3.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            this.textBox3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox3_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.DodgerBlue;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(41, 199);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 16);
            this.label2.TabIndex = 120;
            this.label2.Text = "Codigo";
            // 
            // CargarProducto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(942, 524);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.button1);
            this.Name = "CargarProducto";
            this.Text = "CargarProducto";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.CargarProducto_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Codigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn CantidadEst;
        private System.Windows.Forms.DataGridViewTextBoxColumn Costo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Precio;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Panel panel3;
    }
}