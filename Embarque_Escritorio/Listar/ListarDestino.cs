using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Embarque_Escritorio.Listar
{
    public partial class ListarDestino : Form
    {
        private String Ruta = System.Configuration.ConfigurationManager.AppSettings.Get("CadenaConeccion");
        policia.clsaccesodatos servidor = new policia.clsaccesodatos();

        DataTable Dt_Origen;
        public string __mesajeerror = "";
        public int indice = -1;

        public ListarDestino()
        {
            InitializeComponent();
        }
        public void Obtener_Destino(string Opcion, string Nombre)
        {
            this.mesajeerror.ForeColor = Color.Blue;
            policia.clsaccesodatos servidor = new policia.clsaccesodatos();
            servidor.cadenaconexion = Ruta;
            if (servidor.abrirconexiontrans() == true)
            {
                if (servidor.consultar("[dbo].[BD_Pje_pr_Obtener_Varios_Escritorio]", Opcion, Nombre).Tables.Count > 0)
                    Dt_Origen = servidor.consultar("[dbo].[BD_Pje_pr_Obtener_Varios_Escritorio]", Opcion, Nombre).Tables[0];
                if (Dt_Origen == null)
                {
                    servidor.cerrarconexion();
                    this.mesajeerror.Text = "NO HAY REGISTROS PARA MOSTRAR";
                    this.mesajeerror.ForeColor = Color.Red;
                }
                else
                {
                    this.DgvLista.DataSource = Dt_Origen;
                    int NroFilas = Dt_Origen.Rows.Count;
                    if (NroFilas == 0)
                    {
                        this.DgvLista.DataSource = null;
                        this.mesajeerror.Text = "NO HAY REGISTROS PARA MOSTRAR";
                        this.mesajeerror.ForeColor = Color.Red;
                    }
                    else
                    {
                        //this.DgvLista.Columns("Id Almacen").Visible = false;
                        //this.DgvLista.Columns("Id Sucursal").Visible = false;
                        //this.DgvLista.Columns("Telefono").Visible = false;
                        this.mesajeerror.Text = "Sistema de Almacen tiene " + NroFilas.ToString() + " Almacen(es)";
                    }
                    servidor.cerrarconexion();
                }
            }
            else
            {
                __mesajeerror = servidor.getMensaje();
                servidor.cerrarconexion();
                MessageBox.Show(__mesajeerror, "Sistema de Almacen", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ListarDestino_Load(object sender, EventArgs e)
        {

            Obtener_Destino("2", TxtBusca.Text.Trim());
            alternarColoFilasDatagridview(DgvLista);
        }
        public void alternarColoFilasDatagridview(DataGridView dgv)
        {
            {
                var withBlock = dgv;
                withBlock.RowsDefaultCellStyle.BackColor = Color.PaleGoldenrod;
                withBlock.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
            }
        }

        private void ListarDestino_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                e.SuppressKeyPress = true;
                this.TxtBusca.Focus();
            }
            //// cerrar formulario con tecla ENTER
            //if ((e.KeyCode == Keys.Enter))
            //    this.Close();
        }

        private void TxtBusca_TextChanged(object sender, EventArgs e)
        {
            Obtener_Destino("2", TxtBusca.Text.Trim());
        }

        private void TxtBusca_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Down)
            {
                SendKeys.Send("{TAB}");
                this.DgvLista.Focus();
            }
            if (e.KeyCode == Keys.Enter)
            {
                this.DgvLista.Focus();
                DgvLista.Rows[0].Selected = true;
            }
        }

        private void TxtBusca_Enter(object sender, EventArgs e)
        {
            this.TxtBusca.BackColor = Color.Azure;
            this.Label1.ForeColor = Color.Red;
        }

        private void TxtBusca_Leave(object sender, EventArgs e)
        {
            this.TxtBusca.BackColor = Color.White;
            this.Label1.ForeColor = Color.Black;
        }

        private void DgvLista_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            indice = e.RowIndex;
        }

        private void DgvLista_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            indice = e.RowIndex;
            this.Close();
        }

        private void DgvLista_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            indice = e.RowIndex;
        }

        private void DgvLista_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Close();
                //this.DgvLista.Focus();
                //DgvLista.Rows[0].Selected = true;
            }
        }      
    }
}
