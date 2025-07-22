namespace Embarque_Escritorio.PDF
{
    partial class FormPDF
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
            this.txtManifiesto = new System.Windows.Forms.TextBox();
            this.btnCargarPdf = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtManifiesto
            // 
            this.txtManifiesto.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtManifiesto.Location = new System.Drawing.Point(38, 164);
            this.txtManifiesto.Name = "txtManifiesto";
            this.txtManifiesto.Size = new System.Drawing.Size(208, 26);
            this.txtManifiesto.TabIndex = 0;
            // 
            // btnCargarPdf
            // 
            this.btnCargarPdf.BackColor = System.Drawing.Color.Snow;
            this.btnCargarPdf.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCargarPdf.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCargarPdf.Location = new System.Drawing.Point(60, 212);
            this.btnCargarPdf.Name = "btnCargarPdf";
            this.btnCargarPdf.Size = new System.Drawing.Size(148, 46);
            this.btnCargarPdf.TabIndex = 1;
            this.btnCargarPdf.Text = "BUSCAR";
            this.btnCargarPdf.UseVisualStyleBackColor = false;
            this.btnCargarPdf.Click += new System.EventHandler(this.btnCargarPdf_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(38, 126);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Nro. Manifiesto";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(38, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(263, 75);
            this.label2.TabIndex = 4;
            this.label2.Text = "Aca vamos a buscar en BDPASAJES y mostrar el PDF de manifiesto";
            // 
            // FormPDF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(902, 464);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCargarPdf);
            this.Controls.Add(this.txtManifiesto);
            this.Name = "FormPDF";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormPDF";
            this.Load += new System.EventHandler(this.FormPDF_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtManifiesto;
        private System.Windows.Forms.Button btnCargarPdf;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}