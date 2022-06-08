namespace Embarque_Escritorio
{
    partial class ViaticoCostoRuta2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViaticoCostoRuta2));
            this.TxtDestino = new System.Windows.Forms.TextBox();
            this.TxtOrigen = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtCargo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.BtnCancelar = new System.Windows.Forms.Button();
            this.BtnAceptar = new System.Windows.Forms.Button();
            this.CboAmPm = new System.Windows.Forms.ComboBox();
            this.TxtCosto = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.CboHra1 = new System.Windows.Forms.ComboBox();
            this.CboHra2 = new System.Windows.Forms.ComboBox();
            this.MskHora = new System.Windows.Forms.MaskedTextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // TxtDestino
            // 
            this.TxtDestino.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtDestino.Location = new System.Drawing.Point(78, 59);
            this.TxtDestino.Name = "TxtDestino";
            this.TxtDestino.Size = new System.Drawing.Size(195, 20);
            this.TxtDestino.TabIndex = 4;
            this.TxtDestino.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtDestino_KeyDown);
            this.TxtDestino.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TxtDestino_KeyUp);
            // 
            // TxtOrigen
            // 
            this.TxtOrigen.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtOrigen.Location = new System.Drawing.Point(78, 21);
            this.TxtOrigen.Name = "TxtOrigen";
            this.TxtOrigen.Size = new System.Drawing.Size(195, 20);
            this.TxtOrigen.TabIndex = 2;
            this.TxtOrigen.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtOrigen_KeyDown);
            this.TxtOrigen.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TxtOrigen_KeyUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(16, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 16);
            this.label2.TabIndex = 394;
            this.label2.Text = "Destino:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 16);
            this.label1.TabIndex = 393;
            this.label1.Text = "Origen:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(16, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 16);
            this.label3.TabIndex = 402;
            this.label3.Text = "Hora:";
            // 
            // TxtCargo
            // 
            this.TxtCargo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtCargo.Location = new System.Drawing.Point(78, 165);
            this.TxtCargo.Name = "TxtCargo";
            this.TxtCargo.Size = new System.Drawing.Size(195, 20);
            this.TxtCargo.TabIndex = 12;
            this.TxtCargo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtCargo_KeyDown);
            this.TxtCargo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TxtCargo_KeyUp);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft YaHei", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(16, 167);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 16);
            this.label4.TabIndex = 403;
            this.label4.Text = "Cargo:";
            // 
            // BtnCancelar
            // 
            this.BtnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BtnCancelar.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("BtnCancelar.Image")));
            this.BtnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnCancelar.Location = new System.Drawing.Point(156, 213);
            this.BtnCancelar.Name = "BtnCancelar";
            this.BtnCancelar.Size = new System.Drawing.Size(100, 43);
            this.BtnCancelar.TabIndex = 16;
            this.BtnCancelar.Text = "&Cancelar";
            this.BtnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnCancelar.UseVisualStyleBackColor = true;
            this.BtnCancelar.Click += new System.EventHandler(this.BtnCancelar_Click);
            // 
            // BtnAceptar
            // 
            this.BtnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BtnAceptar.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAceptar.Image = ((System.Drawing.Image)(resources.GetObject("BtnAceptar.Image")));
            this.BtnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnAceptar.Location = new System.Drawing.Point(46, 213);
            this.BtnAceptar.Name = "BtnAceptar";
            this.BtnAceptar.Size = new System.Drawing.Size(100, 43);
            this.BtnAceptar.TabIndex = 14;
            this.BtnAceptar.Text = "&Aceptar";
            this.BtnAceptar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnAceptar.UseVisualStyleBackColor = true;
            this.BtnAceptar.Click += new System.EventHandler(this.BtnAceptar_Click);
            // 
            // CboAmPm
            // 
            this.CboAmPm.FormattingEnabled = true;
            this.CboAmPm.Items.AddRange(new object[] {
            "--",
            "AM",
            "PM"});
            this.CboAmPm.Location = new System.Drawing.Point(214, 258);
            this.CboAmPm.Name = "CboAmPm";
            this.CboAmPm.Size = new System.Drawing.Size(56, 21);
            this.CboAmPm.TabIndex = 7;
            this.CboAmPm.Visible = false;
            this.CboAmPm.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CboAmPm_KeyDown);
            this.CboAmPm.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CboAmPm_KeyUp);
            // 
            // TxtCosto
            // 
            this.TxtCosto.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtCosto.Location = new System.Drawing.Point(78, 132);
            this.TxtCosto.Name = "TxtCosto";
            this.TxtCosto.Size = new System.Drawing.Size(68, 20);
            this.TxtCosto.TabIndex = 10;
            this.TxtCosto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtCosto_KeyDown);
            this.TxtCosto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtCosto_KeyPress);
            this.TxtCosto.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TxtCosto_KeyUp);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft YaHei", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(16, 134);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 16);
            this.label5.TabIndex = 1034;
            this.label5.Text = "Costo";
            // 
            // CboHra1
            // 
            this.CboHra1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboHra1.FormattingEnabled = true;
            this.CboHra1.Items.AddRange(new object[] {
            "--",
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12"});
            this.CboHra1.Location = new System.Drawing.Point(90, 258);
            this.CboHra1.Name = "CboHra1";
            this.CboHra1.Size = new System.Drawing.Size(56, 21);
            this.CboHra1.TabIndex = 5;
            this.CboHra1.Visible = false;
            this.CboHra1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CboHra1_KeyDown);
            // 
            // CboHra2
            // 
            this.CboHra2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboHra2.FormattingEnabled = true;
            this.CboHra2.Items.AddRange(new object[] {
            "--",
            "00",
            "05",
            "10",
            "15",
            "20",
            "25",
            "30",
            "35",
            "40",
            "45",
            "50",
            "55"});
            this.CboHra2.Location = new System.Drawing.Point(152, 258);
            this.CboHra2.Name = "CboHra2";
            this.CboHra2.Size = new System.Drawing.Size(56, 21);
            this.CboHra2.TabIndex = 6;
            this.CboHra2.Visible = false;
            this.CboHra2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CboHra2_KeyDown);
            // 
            // MskHora
            // 
            this.MskHora.BeepOnError = true;
            this.MskHora.Location = new System.Drawing.Point(78, 97);
            this.MskHora.Mask = "00:00>AM";
            this.MskHora.Name = "MskHora";
            this.MskHora.Size = new System.Drawing.Size(70, 20);
            this.MskHora.TabIndex = 6;
            this.MskHora.Text = "0000A";
            this.MskHora.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MskHora_KeyDown_1);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(182, 101);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 1035;
            this.textBox1.Visible = false;
            // 
            // ViaticoCostoRuta2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(304, 291);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.MskHora);
            this.Controls.Add(this.CboHra2);
            this.Controls.Add(this.CboHra1);
            this.Controls.Add(this.TxtCosto);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.CboAmPm);
            this.Controls.Add(this.BtnCancelar);
            this.Controls.Add(this.BtnAceptar);
            this.Controls.Add(this.TxtCargo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TxtDestino);
            this.Controls.Add(this.TxtOrigen);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "ViaticoCostoRuta2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Registrar Viatico Costo Ruta";
            this.Load += new System.EventHandler(this.ViaticoCostoRuta2_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ViaticoCostoRuta2_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox TxtOrigen;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        internal System.Windows.Forms.Button BtnCancelar;
        internal System.Windows.Forms.Button BtnAceptar;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox TxtDestino;
        public System.Windows.Forms.TextBox TxtCargo;
        public System.Windows.Forms.TextBox TxtCosto;
        public System.Windows.Forms.ComboBox CboAmPm;
        public System.Windows.Forms.ComboBox CboHra1;
        public System.Windows.Forms.ComboBox CboHra2;
        internal System.Windows.Forms.MaskedTextBox MskHora;
        private System.Windows.Forms.TextBox textBox1;
    }
}