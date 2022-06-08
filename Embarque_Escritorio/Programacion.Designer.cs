namespace Embarque_Escritorio
{
    partial class Programacion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Programacion));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtOrigen = new System.Windows.Forms.TextBox();
            this.TxtDestino = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.DtpFecha = new System.Windows.Forms.DateTimePicker();
            this.TxtHora = new System.Windows.Forms.MaskedTextBox();
            this.rbtPM = new System.Windows.Forms.RadioButton();
            this.rbtAM = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.BtnBuscar = new System.Windows.Forms.Button();
            this.dgvlista = new System.Windows.Forms.DataGridView();
            this.mesajeerror = new System.Windows.Forms.Label();
            this.TxtCodigo = new System.Windows.Forms.TextBox();
            this.RbPasajeros = new System.Windows.Forms.RadioButton();
            this.RbEquipajes = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvlista)).BeginInit();
            this.panel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Origen";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(176, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Destino";
            // 
            // TxtOrigen
            // 
            this.TxtOrigen.Location = new System.Drawing.Point(20, 55);
            this.TxtOrigen.Name = "TxtOrigen";
            this.TxtOrigen.Size = new System.Drawing.Size(134, 20);
            this.TxtOrigen.TabIndex = 2;
            this.TxtOrigen.Text = "CHICLAYO";
            this.TxtOrigen.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtOrigen_KeyDown);
            // 
            // TxtDestino
            // 
            this.TxtDestino.Location = new System.Drawing.Point(179, 55);
            this.TxtDestino.Name = "TxtDestino";
            this.TxtDestino.Size = new System.Drawing.Size(134, 20);
            this.TxtDestino.TabIndex = 3;
            this.TxtDestino.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtDestino_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(338, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 16);
            this.label3.TabIndex = 382;
            this.label3.Text = "Fecha";
            // 
            // DtpFecha
            // 
            this.DtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DtpFecha.Location = new System.Drawing.Point(341, 55);
            this.DtpFecha.Name = "DtpFecha";
            this.DtpFecha.Size = new System.Drawing.Size(113, 20);
            this.DtpFecha.TabIndex = 383;
            // 
            // TxtHora
            // 
            this.TxtHora.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtHora.ForeColor = System.Drawing.Color.Blue;
            this.TxtHora.Location = new System.Drawing.Point(471, 55);
            this.TxtHora.Mask = "00:00";
            this.TxtHora.Name = "TxtHora";
            this.TxtHora.Size = new System.Drawing.Size(43, 20);
            this.TxtHora.TabIndex = 384;
            this.TxtHora.ValidatingType = typeof(System.DateTime);
            // 
            // rbtPM
            // 
            this.rbtPM.AutoSize = true;
            this.rbtPM.Font = new System.Drawing.Font("Dungeon", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtPM.Location = new System.Drawing.Point(578, 57);
            this.rbtPM.Name = "rbtPM";
            this.rbtPM.Size = new System.Drawing.Size(42, 16);
            this.rbtPM.TabIndex = 386;
            this.rbtPM.Text = "PM";
            this.rbtPM.UseVisualStyleBackColor = true;
            // 
            // rbtAM
            // 
            this.rbtAM.AutoSize = true;
            this.rbtAM.Checked = true;
            this.rbtAM.Font = new System.Drawing.Font("Dungeon", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtAM.Location = new System.Drawing.Point(531, 57);
            this.rbtAM.Name = "rbtAM";
            this.rbtAM.Size = new System.Drawing.Size(41, 16);
            this.rbtAM.TabIndex = 385;
            this.rbtAM.TabStop = true;
            this.rbtAM.Text = "AM";
            this.rbtAM.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.BackColor = System.Drawing.Color.IndianRed;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label5.Font = new System.Drawing.Font("Dungeon", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(1, -1);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(904, 31);
            this.label5.TabIndex = 387;
            this.label5.Text = "LISTA DE PROGRAMACIONES";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BtnBuscar
            // 
            this.BtnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnBuscar.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnBuscar.ForeColor = System.Drawing.Color.Red;
            this.BtnBuscar.Location = new System.Drawing.Point(637, 45);
            this.BtnBuscar.Name = "BtnBuscar";
            this.BtnBuscar.Size = new System.Drawing.Size(75, 38);
            this.BtnBuscar.TabIndex = 388;
            this.BtnBuscar.Text = "&Buscar";
            this.BtnBuscar.UseVisualStyleBackColor = true;
            this.BtnBuscar.Click += new System.EventHandler(this.BtnBuscar_Click);
            // 
            // dgvlista
            // 
            this.dgvlista.AllowUserToAddRows = false;
            this.dgvlista.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvlista.Location = new System.Drawing.Point(20, 94);
            this.dgvlista.Name = "dgvlista";
            this.dgvlista.ReadOnly = true;
            this.dgvlista.Size = new System.Drawing.Size(863, 206);
            this.dgvlista.TabIndex = 389;
            this.dgvlista.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvlista_CellClick);
            this.dgvlista.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvlista_CellContentClick);
            this.dgvlista.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvlista_CellFormatting);
            this.dgvlista.DoubleClick += new System.EventHandler(this.dgvlista_DoubleClick);
            // 
            // mesajeerror
            // 
            this.mesajeerror.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mesajeerror.Location = new System.Drawing.Point(23, 302);
            this.mesajeerror.Name = "mesajeerror";
            this.mesajeerror.Size = new System.Drawing.Size(860, 23);
            this.mesajeerror.TabIndex = 390;
            this.mesajeerror.Text = "label4";
            this.mesajeerror.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TxtCodigo
            // 
            this.TxtCodigo.Location = new System.Drawing.Point(783, 302);
            this.TxtCodigo.Name = "TxtCodigo";
            this.TxtCodigo.Size = new System.Drawing.Size(100, 20);
            this.TxtCodigo.TabIndex = 392;
            this.TxtCodigo.Visible = false;
            // 
            // RbPasajeros
            // 
            this.RbPasajeros.AutoSize = true;
            this.RbPasajeros.Checked = true;
            this.RbPasajeros.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbPasajeros.Location = new System.Drawing.Point(31, 4);
            this.RbPasajeros.Name = "RbPasajeros";
            this.RbPasajeros.Size = new System.Drawing.Size(76, 20);
            this.RbPasajeros.TabIndex = 393;
            this.RbPasajeros.TabStop = true;
            this.RbPasajeros.Text = "Pasajeros";
            this.RbPasajeros.UseVisualStyleBackColor = true;
            // 
            // RbEquipajes
            // 
            this.RbEquipajes.AutoSize = true;
            this.RbEquipajes.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbEquipajes.Location = new System.Drawing.Point(31, 27);
            this.RbEquipajes.Name = "RbEquipajes";
            this.RbEquipajes.Size = new System.Drawing.Size(76, 20);
            this.RbEquipajes.TabIndex = 394;
            this.RbEquipajes.Text = "Equipajes";
            this.RbEquipajes.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.RbEquipajes);
            this.panel1.Controls.Add(this.RbPasajeros);
            this.panel1.Location = new System.Drawing.Point(762, 35);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(119, 52);
            this.panel1.TabIndex = 395;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel3});
            this.statusStrip1.Location = new System.Drawing.Point(0, 334);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(903, 22);
            this.statusStrip1.TabIndex = 396;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BackColor = System.Drawing.Color.ForestGreen;
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.toolStripStatusLabel1.ForeColor = System.Drawing.Color.Blue;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(127, 17);
            this.toolStripStatusLabel1.Text = "  CON VENTAS  ";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.BackColor = System.Drawing.Color.Red;
            this.toolStripStatusLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.toolStripStatusLabel3.ForeColor = System.Drawing.Color.White;
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(115, 17);
            this.toolStripStatusLabel3.Text = "  BUS LLENO  ";
            // 
            // Programacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(903, 356);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.TxtCodigo);
            this.Controls.Add(this.mesajeerror);
            this.Controls.Add(this.dgvlista);
            this.Controls.Add(this.BtnBuscar);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.rbtPM);
            this.Controls.Add(this.rbtAM);
            this.Controls.Add(this.TxtHora);
            this.Controls.Add(this.DtpFecha);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TxtDestino);
            this.Controls.Add(this.TxtOrigen);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "Programacion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Programacion";
            this.Load += new System.EventHandler(this.Programacion_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Programacion_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvlista)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtDestino;
        internal System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox TxtOrigen;
        private System.Windows.Forms.DateTimePicker DtpFecha;
        private System.Windows.Forms.MaskedTextBox TxtHora;
        private System.Windows.Forms.RadioButton rbtPM;
        private System.Windows.Forms.RadioButton rbtAM;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button BtnBuscar;
        private System.Windows.Forms.DataGridView dgvlista;
        private System.Windows.Forms.Label mesajeerror;
        public System.Windows.Forms.TextBox TxtCodigo;
        private System.Windows.Forms.RadioButton RbPasajeros;
        private System.Windows.Forms.RadioButton RbEquipajes;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
    }
}