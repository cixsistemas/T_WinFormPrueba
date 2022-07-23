using System;

using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;

namespace SubirExcelDatagridview
{
    class Importar
    {
        OleDbConnection conn;
        OleDbDataAdapter MyDataAdapter;
        DataTable dt;

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
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
