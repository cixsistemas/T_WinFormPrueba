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
    public partial class ListarSucursal : Form
    {
        private String Ruta = System.Configuration.ConfigurationManager.AppSettings.Get("CadenaConeccion");
        policia.clsaccesodatos servidor = new policia.clsaccesodatos();

        DataTable Dt_;
        public string __mesajeerror = "";
        public int indice = -1;

        public ListarSucursal()
        {
            InitializeComponent();
        }
        public void Listar(string Nombre)
        {
            this.mesajeerror.ForeColor = Color.Blue;
            policia.clsaccesodatos servidor = new policia.clsaccesodatos();
            servidor.cadenaconexion = Ruta;
            if (servidor.abrirconexiontrans() == true)
            {
                if (servidor.consultar("[dbo].[Pa_Listar_Sucursal]", Nombre).Tables.Count > 0)
                    Dt_ = servidor.consultar("[dbo].[Pa_Listar_Sucursal]", Nombre).Tables[0];
                if (Dt_ == null)
                {
                    servidor.cerrarconexion();
                    this.mesajeerror.Text = "NO HAY REGISTROS PARA MOSTRAR";
                    this.mesajeerror.ForeColor = Color.Red;
                }
                else
                {
                    this.DgvLista.DataSource = Dt_;
                    int NroFilas = Dt_.Rows.Count;
                    if (NroFilas == 0)
                    {
                        this.DgvLista.DataSource = null;
                        this.mesajeerror.Text = "NO HAY REGISTROS PARA MOSTRAR";
                        this.mesajeerror.ForeColor = Color.Red;
                    }
                    else
                    {
                        this.DgvLista.Columns["ID"].Visible = false;
                        //this.dgvlista.Columns["ASIENTOS"].Visible = false;
                        //this.DgvLista.Columns("Id Almacen").Visible = false;
                        //this.DgvLista.Columns("Id Sucursal").Visible = false;
                        //this.DgvLista.Columns("Telefono").Visible = false;
                        this.mesajeerror.Text = "Sistema tiene " + NroFilas.ToString() + " Registro(s)";
                    }
                    servidor.cerrarconexion();
                }
            }
            else
            {
                __mesajeerror = servidor.getMensaje();
                servidor.cerrarconexion();
                MessageBox.Show(__mesajeerror, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ListarSucursal_Load(object sender, EventArgs e)
        {

            Listar(TxtBusca.Text.Trim());
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

        private void ListarSucursal_KeyDown(object sender, KeyEventArgs e)
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
            Listar(TxtBusca.Text.Trim());
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
