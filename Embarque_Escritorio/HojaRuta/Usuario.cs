using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Embarque_Escritorio.Clases;

namespace Embarque_Escritorio.BDPasajes
{
    public partial class Usuario : Form
    {
        public Usuario()
        {
            InitializeComponent();
        }
        DataTable Dt_Origen;
        public string __mesajeerror = "";
        CsUsuario _CsUsuario = new CsUsuario();
        CsSeguridad _CsSeguridad = new CsSeguridad();
        private void Usuario_Load(object sender, EventArgs e)
        {
            Obtener_Origen("1", "");
        }

        public void Obtener_Origen(string Opcion, string Nombre)
        {
            mesajeerror.ForeColor = Color.Blue;
            //policia.clsaccesodatos servidor = new policia.clsaccesodatos();
            //servidor.cadenaconexion = Ruta;
            //if (servidor.abrirconexiontrans() == true)
            //{
            //if (servidor.consultar("[dbo].[BD_Pje_pr_Obtener_Varios_Escritorio]", Opcion, Nombre).Tables.Count > 0)
            Dt_Origen = _CsUsuario.ListarUsuarios(Opcion, Nombre);
            if (Dt_Origen == null)
            {
                //servidor.cerrarconexion();
                mesajeerror.Text = "NO HAY REGISTROS PARA MOSTRAR";
                mesajeerror.ForeColor = Color.Red;
            }
            else
            {
                DgvLista.DataSource = Dt_Origen;
                int NroFilas = Dt_Origen.Rows.Count;
                if (NroFilas == 0)
                {
                    DgvLista.DataSource = null;
                    mesajeerror.Text = "NO HAY REGISTROS PARA MOSTRAR";
                    mesajeerror.ForeColor = Color.Red;
                }
                else
                {
                    //this.DgvLista.Columns("Id Almacen").Visible = false;
                    //this.DgvLista.Columns("Id Sucursal").Visible = false;
                    //this.DgvLista.Columns("Telefono").Visible = false;
                    mesajeerror.Text = "Sistema de Almacen tiene " + NroFilas.ToString() + " Almacen(es)";
                }
                //servidor.cerrarconexion();
            }
            //}
            //else
            //{
            //    __mesajeerror = servidor.getMensaje();
            //    servidor.cerrarconexion();
            //    MessageBox.Show(__mesajeerror, "Sistema de Almacen", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void DgvLista_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = DgvLista.CurrentRow.Cells["CLAVE"].Value.ToString();
        }

        private void btnmodificar_Click(object sender, EventArgs e)
        {
           MessageBox.Show(_CsSeguridad.Desencripta(textBox1.Text, "JLM_2010/*SEGURIDAD"));

        }
    }
}
