using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace Embarque_Escritorio.SubirExcel
{
    public partial class FrmExcel : Form
    {
        public FrmExcel()
        {
            InitializeComponent();
        }
        private string path = @"C:\Trabajos\Data-Sistemas\Prueba\Embarque_Escritorio\SubirExcel\Taller.xlsx";
        private void FrmExcel_Load(object sender, EventArgs e)
        {
            SLDocument sl = new SLDocument(path);
            int iRow = 2;
            List<PersonasViewModel> lst = new List<PersonasViewModel>();
            while (!string.IsNullOrEmpty (sl.GetCellValueAsString(iRow, 1) ))
            {
                PersonasViewModel oPersona = new PersonasViewModel();
                //oPersona.Tiempo = sl.GetCellValueAsString(iRow, 1);
                oPersona.Id = sl.GetCellValueAsString(iRow, 1);
                lst.Add(oPersona);
                iRow++;
            }

            dataGridView1.DataSource = lst;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
