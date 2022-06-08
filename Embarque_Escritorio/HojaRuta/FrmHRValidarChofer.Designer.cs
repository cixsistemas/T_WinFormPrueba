namespace Embarque_Escritorio.BDPasajes
{
    partial class FrmHojaRuta1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmHojaRuta1));
            this.@__mensajeVale = new System.Windows.Forms.Label();
            this.PictureMensaje = new System.Windows.Forms.PictureBox();
            this.btnConectar = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.txtCodigoChofer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PictureMensaje)).BeginInit();
            this.SuspendLayout();
            // 
            // __mensajeVale
            // 
            this.@__mensajeVale.BackColor = System.Drawing.Color.White;
            this.@__mensajeVale.Font = new System.Drawing.Font("Microsoft YaHei", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.@__mensajeVale.ForeColor = System.Drawing.Color.Red;
            this.@__mensajeVale.Location = new System.Drawing.Point(57, 123);
            this.@__mensajeVale.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.@__mensajeVale.Name = "__mensajeVale";
            this.@__mensajeVale.Size = new System.Drawing.Size(550, 84);
            this.@__mensajeVale.TabIndex = 1052;
            this.@__mensajeVale.Text = ".";
            // 
            // PictureMensaje
            // 
            this.PictureMensaje.Image = ((System.Drawing.Image)(resources.GetObject("PictureMensaje.Image")));
            this.PictureMensaje.Location = new System.Drawing.Point(37, 108);
            this.PictureMensaje.Margin = new System.Windows.Forms.Padding(4);
            this.PictureMensaje.Name = "PictureMensaje";
            this.PictureMensaje.Size = new System.Drawing.Size(589, 118);
            this.PictureMensaje.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureMensaje.TabIndex = 1053;
            this.PictureMensaje.TabStop = false;
            // 
            // btnConectar
            // 
            this.btnConectar.BackColor = System.Drawing.SystemColors.Desktop;
            this.btnConectar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConectar.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnConectar.Location = new System.Drawing.Point(37, 30);
            this.btnConectar.Name = "btnConectar";
            this.btnConectar.Size = new System.Drawing.Size(168, 59);
            this.btnConectar.TabIndex = 1054;
            this.btnConectar.Text = "CONECTAR";
            this.btnConectar.UseVisualStyleBackColor = false;
            this.btnConectar.Click += new System.EventHandler(this.btnConectar_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "17539227",
            "43986031"});
            this.comboBox1.Location = new System.Drawing.Point(415, 30);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(211, 24);
            this.comboBox1.TabIndex = 1055;
            // 
            // txtCodigoChofer
            // 
            this.txtCodigoChofer.Location = new System.Drawing.Point(225, 67);
            this.txtCodigoChofer.Name = "txtCodigoChofer";
            this.txtCodigoChofer.Size = new System.Drawing.Size(135, 22);
            this.txtCodigoChofer.TabIndex = 1056;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(225, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 17);
            this.label1.TabIndex = 1057;
            this.label1.Text = "Codigo Chofer";
            // 
            // FrmHojaRuta1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(659, 521);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCodigoChofer);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.btnConectar);
            this.Controls.Add(this.@__mensajeVale);
            this.Controls.Add(this.PictureMensaje);
            this.MaximizeBox = false;
            this.Name = "FrmHojaRuta1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hoja de Ruta";
            this.Load += new System.EventHandler(this.FrmHojaRuta1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PictureMensaje)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label __mensajeVale;
        private System.Windows.Forms.PictureBox PictureMensaje;
        private System.Windows.Forms.Button btnConectar;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox txtCodigoChofer;
        private System.Windows.Forms.Label label1;
    }
}