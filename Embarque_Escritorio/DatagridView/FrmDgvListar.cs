using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Embarque_Escritorio.DatagridView
{
    public partial class FrmDgvListar : Form
    {
        public FrmDgvListar()
        {
            InitializeComponent();
            //var link = new DataGridViewLinkColumn
            //{
            //    UseColumnTextForLinkValue = true,
            //    HeaderText = "User Profile",
            //    DataPropertyName = "lnkColumn",
            //    ActiveLinkColor = Color.White,
            //    LinkBehavior = LinkBehavior.SystemDefault,
            //    LinkColor = Color.Blue,
            //    Text = "View Profile",
            //    VisitedLinkColor = Color.YellowGreen
            //};

            //DgvLista.Columns.Add(link);
        }
        CsCargar _CsCargar = new CsCargar();
        DataTable dt;
        //string Concepto;
        public void CargarDatos()
        {
            dt = _CsCargar.ObtenerData("1", "");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DgvLista.Rows.Add();
                DgvLista.Rows[DgvLista.Rows.Count - 1].Cells["CONCEPTO1"].Value = dt.Rows[i][0].ToString();
                DgvLista.Rows[DgvLista.Rows.Count - 1].Cells["MONTO"].Value = dt.Rows[i][1].ToString();
            }

            //DgvLista.DataSource = _CsCargar.ObtenerData("1", Concepto);
        }
        private void FrmDgv1_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void FrmDgv1_KeyDown(object sender, KeyEventArgs e)
        {
            // cerrar formulario con tecla ESC
            if ((e.KeyCode == Keys.Escape))
                Close();
        }

        private void DgvLista_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DgvLista_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string row = e.RowIndex.ToString();
            //string column = e.ColumnIndex.ToString();
            FrmDgvListarDetalle f = new FrmDgvListarDetalle();
            f.Concepto = DgvLista.Rows[Convert.ToInt32(row)].Cells[0].Value.ToString();

            f.ShowDialog();

            //MessageBox.Show(DgvLista.Rows[Convert.ToInt32(row)].Cells[0].Value.ToString());

            //if (row == Convert.ToString(mes) && column == Convert.ToString(Correlativo))
            //{

            //}

        }
    }
}
