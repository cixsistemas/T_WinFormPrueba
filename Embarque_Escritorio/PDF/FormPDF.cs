using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

namespace Embarque_Escritorio.PDF
{
    public partial class FormPDF : Form
    {
        // Ajusta según necesites (usuario/clave si no usas Trusted Connection)
        private const string ConnectionString =
            @"Server=SISTEMAS-PC\SQLSERVER2022;
      Database=BDPASAJE_N;
      Trusted_Connection=True;";


        public FormPDF()
        {
            InitializeComponent();
        }

        private void FormPDF_Load(object sender, EventArgs e)
        {

        }

        private void btnCargarPdf_Click(object sender, EventArgs e)
        {
            string manifiesto = txtManifiesto.Text.Trim();
            if (string.IsNullOrEmpty(manifiesto))
            {
                MessageBox.Show("Por favor ingresa un número de manifiesto.",
                                "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                byte[] pdfBytes = ObtenerPdfDesdeBase(manifiesto);
                if (pdfBytes == null)
                {
                    MessageBox.Show($"No se encontró PDF para el manifiesto “{manifiesto}”.",
                                    "No encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string tempFile = Path.Combine(
                    Path.GetTempPath(),
                    $"manifiesto_{manifiesto}.pdf"
                );
                File.WriteAllBytes(tempFile, pdfBytes);

                // Abrir con el lector predeterminado
                Process.Start(new ProcessStartInfo(tempFile) { UseShellExecute = true });

                //// Navegar en el WebBrowser al PDF
                //webView21.Navigate(tempFile);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el PDF:\n" + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private byte[] ObtenerPdfDesdeBase(string numeManifiesto)
        {
            const string sql = @"
                SELECT TOP 1 pdf_data 
                FROM dbo.tb_manifiesto_pdf
                WHERE nume_manifiesto = @nume_manifiesto";

            using (var conn = new SqlConnection(ConnectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@nume_manifiesto", SqlDbType.VarChar, 50)
                   .Value = numeManifiesto;

                conn.Open();
                object result = cmd.ExecuteScalar();
                return result == DBNull.Value || result == null
                    ? null
                    : (byte[])result;
            }
        }
    }
}
