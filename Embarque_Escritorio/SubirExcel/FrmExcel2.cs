using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Embarque_Escritorio.SubirExcel
{
    public partial class FrmExcel2 : Form
    {
        //private string Ruta = System.Configuration.ConfigurationManager.AppSettings.Get("CadenaConeccion");
        //AccesoDatos.clsaccesodatos servidor = new AccesoDatos.clsaccesodatos();

        private static DataTable dt = new DataTable();
        string USERID;
        DateTime CHECKTIME;

        int rpta = -1;
        string mensaje = "";
        public string __mesajeerror = "";
       

        public FrmExcel2()
        {
            InitializeComponent();
        }

        OleDbConnection conn;
        OleDbDataAdapter MyDataAdapter;
        //DataTable dt;

        public void importarExcel(DataGridView dgv, String nombreHoja)
        {
            String ruta = "";
            try
            {
                OpenFileDialog openfile1 = new OpenFileDialog();
                openfile1.Filter = "Excel Files |*.xlsx";
                openfile1.Title = "Seleccione el archivo de Excel";
                if (openfile1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (openfile1.FileName.Equals("") == false)
                    {
                        ruta = openfile1.FileName;
                    }
                }
                ////connExcel.Open();
                ////DataTable dtExcelSchema;
                ////dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                ////string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();

                conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;data source=" + ruta + ";Extended Properties='Excel 12.0 Xml;HDR=Yes'");
                MyDataAdapter = new OleDbDataAdapter("Select * from [" + nombreHoja + "$]", conn);
                dt = new DataTable();
                MyDataAdapter.Fill(dt);
                dgv.DataSource = dt;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                //MessageBox.Show("Nombre de hoja, no corresponde al archivo de excel", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //textBox1.Focus();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool ok;
            ok = this.textBox1.Text != "";
            if ((ok == false))
            {
                MessageBox.Show("Ingrese Nombre de hoja de excel a subir", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBox1.Focus();
                return;
            }

            try
            {
                importarExcel(dataGridView1, textBox1.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Nombre de hoja, no corresponde al archivo de excel", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //throw;
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //GetExcelSheetNames(textBox1.Text);
        }

        private String[] GetExcelSheetNames(string excelFile)
        {
            OleDbConnection objConn = null;
            System.Data.DataTable dt = null;

            try
            {
                // Connection String. Change the excel file to the file you
                // will search.
                String connString = "Provider=Microsoft.Jet.OLEDB.4.0;" +
                    "Data Source=" + excelFile + ";Extended Properties=Excel 8.0;";
                // Create connection object by using the preceding connection string.
                objConn = new OleDbConnection(connString);
                // Open connection with the database.
                objConn.Open();
                // Get the data table containg the schema guid.
                dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                if (dt == null)
                {
                    return null;
                }

                String[] excelSheets = new String[dt.Rows.Count];
                int i = 0;

                // Add the sheet name to the string array.
                foreach (DataRow row in dt.Rows)
                {
                    excelSheets[i] = row["TABLE_NAME"].ToString();
                    i++;
                }

                // Loop through all of the sheets if you want too...
                for (int j = 0; j < excelSheets.Length; j++)
                {
                    // Query each excel sheet.
                }

                return excelSheets;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                // Clean up.
                if (objConn != null)
                {
                    objConn.Close();
                    objConn.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }

    //    private void Matenimiento_PasajerosAbordo(string USERID, DateTime CHECKTIME, int Id_Unico
    //, string Operacion)
    //    {
    //        //try{
    //        AccesoDatos.clsaccesodatos servidor = new AccesoDatos.clsaccesodatos();
    //        servidor.cadenaconexion = Ruta;
    //        //int rpta = -1;
    //        //string mensaje = "";
    //        if (servidor.abrirconexiontrans() == true)
    //        {
    //            servidor.ejecutar("[dbo].[pa_mantenimiento_EXCEL]"
    //            , false
    //            , ref rpta
    //            , ref mensaje
    //            , USERID
    //            , CHECKTIME,
    //            Id_Unico,
    //            Operacion,
    //            0, "");
    //            if (rpta > 0)
    //            {
    //                servidor.cerrarconexiontrans();
    //                __mesajeerror = mensaje;
    //                //mensaje= __mesajeerror;
    //                //MessageBox.Show(__mesajeerror, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
    //            }
    //            else
    //            {
    //                servidor.cancelarconexiontrans();
    //                __mesajeerror = mensaje;
    //                MessageBox.Show(__mesajeerror, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
    //            }
    //        }
    //        else
    //        {
    //            servidor.cancelarconexiontrans();
    //            __mesajeerror = servidor.getMensageError();
    //            //mensaje = servidor.getMensageError();
    //            MessageBox.Show(__mesajeerror, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
    //        }
    //        //}catch (Exception){
    //        //    this.__mensaje.Value = "Error inesperado al intentar conectarnos con el servidor.";
    //        //    this.__pagina.Value = "CerrarSession.aspx";
    //        //}
    //    }

        private void BtnRegistrar_Click(object sender, EventArgs e)
        {
            __mesajeerror = mensaje;
            __mesajeerror = "";
            if (MessageBox.Show("Esta seguro que desea registrar " , "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                //for (int j = 0; j <= dt.Rows.Count - 1; j++)
                //{
                //    USERID = (dt.Rows[j].ItemArray[1].ToString());
                //    CHECKTIME = Convert.ToDateTime(dt.Rows[j].ItemArray[0].ToString());
                //    Matenimiento_PasajerosAbordo(USERID, CHECKTIME, 1, "N");

                //}
                MessageBox.Show(__mesajeerror, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            dataGridView1.DataSource = null;
            dt.Clear();
        }


    }
}
