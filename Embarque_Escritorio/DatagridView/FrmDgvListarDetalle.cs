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
    public partial class FrmDgvListarDetalle : Form
    {
        public FrmDgvListarDetalle()
        {
            InitializeComponent();
        }
        CsCargar _CsCargar = new CsCargar();
        DataTable dt;
        public string Concepto = "";
        public void CargarDatos()
        {
            dt = _CsCargar.ObtenerData("2", Concepto);
            DgvLista.DataSource = dt;

        }
        private void FrmDgvListarDetalle_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void FrmDgvListarDetalle_KeyDown(object sender, KeyEventArgs e)
        {
            // cerrar formulario con tecla ESC
            if ((e.KeyCode == Keys.Escape))
                Close();
        }
    }
}
