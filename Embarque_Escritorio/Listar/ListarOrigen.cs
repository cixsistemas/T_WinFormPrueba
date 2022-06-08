using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace Embarque_Escritorio
{
    public partial class ListarOrigen : Form
    {
        private String Ruta = System.Configuration.ConfigurationManager.AppSettings.Get("CadenaConeccion");
        policia.clsaccesodatos servidor = new policia.clsaccesodatos();

        DataTable Dt_Origen;
        public string __mesajeerror = "";
        //public int indice = -1;
        Utilitarios Utilitarios_ = new Utilitarios();

        public ListarOrigen()
        {
            InitializeComponent();
        }
        public void Obtener_Origen(string Opcion, string Nombre)
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

     
        private void ListarOrigen_Load(object sender, EventArgs e)
        {
            Obtener_Origen("1", TxtBusca.Text.Trim());
            alternarColoFilasDatagridview(DgvLista);
            ////DgvLista.Rows[1].Selected = true;
            
        }
        public void alternarColoFilasDatagridview(DataGridView dgv)
        {
            {
                var withBlock = dgv;
                withBlock.RowsDefaultCellStyle.BackColor = Color.PaleGoldenrod;
                withBlock.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
            }
        }

        private void TxtBusca_TextChanged(object sender, EventArgs e)
        {
            Obtener_Origen("1", TxtBusca.Text.Trim());
            //textBox1.Text = Convert.ToString(indice);
        }

        private void ListarOrigen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                e.SuppressKeyPress = true;
                this.TxtBusca.Focus();
            }

            //// CERRAR FORMULARIO CON TECLA ENTER
                //if (DgvLista.CurrentRow == null)
                //{
                //    MessageBox.Show("Seleccione Registro....", "Atencion");
                //    this.TxtBusca.Focus();
                //}

                //if (e.KeyCode == Keys.Enter)
                //{
                //    //DgvLista.CurrentCell = DgvLista.Rows[0].Cells[0];
                //    DgvLista.Rows[1].Selected = true;
                //    this.Close();
                //}


                //else { 
                
                //}

                //string Celda = DgvLista.SelectedRows[0].Cells[0].Value.ToString();
                //if (Celda == "")
                //{
                //    MessageBox.Show("Seleccione Registro.", "Atencion");
                //}
                //DgvLista.Focus();
               
            //}
                

            ////if (this.textBox1.Text =="")
            ////{
            ////    MessageBox.Show("Seleccione Registro.", "Atencion");
            ////}
            //Utilitarios_.indice = -1;
        }

        private void DgvLista_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Utilitarios_.indice = e.RowIndex;
            //if ((Utilitarios_.indice == e.RowIndex)==false )
            //{
            //    MessageBox.Show("Seleccione Registro.", "Atencion");
            //    //this.TxtOrigen.Focus();
            //}
            //Utilitarios_.indice = e.RowIndex;

            //this.textBox1.Text = DgvLista.SelectedRows[0].Cells[0].Value.ToString();
          //DataGridViewRow row = DgvLista.Rows[Utilitarios_.indice];
          //this.textBox1.Text = row.Cells["Origen"].Value.ToString();

        }

        //NO FUNCIONA
        private void DgvLista_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //Utilitarios_.indice = e.RowIndex;
            ////Programacion f = new Programacion();
            ////f.TxtOrigen.Text = DgvLista.Rows[DgvLista.CurrentRow.Index + 1].Cells[1].Value.ToString();
            //this.Close();
        }
        private void DgvLista_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            Utilitarios_.indice = e.RowIndex;
            
            //////DgvLista.Rows[0].Selected = true;
            //try
            //{
            //    Programacion f = new Programacion();
            //    f.TxtOrigen.Text = DgvLista.Rows[DgvLista.CurrentRow.Index - 1].Cells[1].Value.ToString();
            //}
            //catch (Exception)
            //{
                
            //    //throw;
            //}        
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

        private void ListarOrigen_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        

       

    }
}
