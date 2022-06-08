using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Embarque_Escritorio
{
    public partial class Trabajador_Huella1 : Form
    {
        private String Ruta = System.Configuration.ConfigurationManager.AppSettings.Get("CadenaConeccion");
        policia.clsaccesodatos servidor = new policia.clsaccesodatos();
        Utilitarios Utilitarios_ = new Utilitarios();
        public int Id_UsuarioGlobal;

        DataTable Dt_Trabajador;
        ////public DataTable Dt_Trabajador2;
        string __mesajeerror = "";
        int indice = -1;
        public int Id_trajador1;
        public string Nombre1;
        public string Template1;
        public string Calidad1;
        //public byte[] imageData;
        //Initialize image variable
        public Image newImage;

        public Trabajador_Huella1()
        {
            InitializeComponent();
        }
        public void Listar_Trabajador(string Nombre)
        {
            this.mesajeerror.ForeColor = Color.Blue;
            policia.clsaccesodatos servidor = new policia.clsaccesodatos();
            servidor.cadenaconexion = Ruta;
            if (servidor.abrirconexiontrans() == true)
            {
                if (servidor.consultar("[dbo].[pa_Listar_Trabajador_Biometrico]", Nombre).Tables.Count > 0)
                    Dt_Trabajador = servidor.consultar("[dbo].[pa_Listar_Trabajador_Biometrico]", Nombre).Tables[0];
                if (Dt_Trabajador == null)
                {
                    servidor.cerrarconexion();
                    this.mesajeerror.Text = "NO HAY REGISTROS PARA MOSTRAR";
                    this.mesajeerror.ForeColor = Color.Red;
                }
                else
                {
                    this.DgvLista.DataSource = Dt_Trabajador;
                    int NroFilas = Dt_Trabajador.Rows.Count;
                    if (NroFilas == 0)
                    {
                        this.DgvLista.DataSource = null;
                        this.mesajeerror.Text = "NO HAY REGISTROS PARA MOSTRAR";
                        this.mesajeerror.ForeColor = Color.Red;
                    }
                    else
                    {
                        this.DgvLista.Columns["Id_Trabajador"].Visible = false;
 
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
        private void Trabajador_Huella1_Load(object sender, EventArgs e)
        {

            Listar_Trabajador(TxtBusca.Text.Trim());
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

        private void Trabajador_Huella1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                e.SuppressKeyPress = true;
                this.TxtBusca.Focus();
            }
            // cerrar formulario con tecla ESC
            if ((e.KeyCode == Keys.Escape))
                this.Close();
        }

        private void TxtBusca_TextChanged(object sender, EventArgs e)
        {
            Listar_Trabajador(TxtBusca.Text.Trim());
            TxtTrabaj.Text = "";
        }

        private void TxtBusca_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Down)
            {
                SendKeys.Send("{TAB}");
                this.DgvLista.Focus();
            }
        }

        private void TxtBusca_Leave(object sender, EventArgs e)
        {
            this.TxtBusca.BackColor = Color.White;
            this.Label1.ForeColor = Color.Black;
        }

        private void TxtBusca_Enter(object sender, EventArgs e)
        {
            this.TxtBusca.BackColor = Color.Azure;
            this.Label1.ForeColor = Color.Red;          
        }

        private void DgvLista_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //indice = e.RowIndex;
            Nombre1 = DgvLista.CurrentRow.Cells["NOMBRE"].Value.ToString();
            Template1 = DgvLista.CurrentRow.Cells["TEMPLATE"].Value.ToString();
            Calidad1 = DgvLista.CurrentRow.Cells["CALIDAD"].Value.ToString();
            Id_trajador1 = Convert.ToInt32(DgvLista.CurrentRow.Cells["Id_Trabajador"].Value.ToString());

            TxtTrabaj.Text = Nombre1;
            //imageData = (byte[])Dt_Trabajador.Rows[0].ItemArray[0];
            //imageData = ImageHelper.ByteArrayToImage((byte[])DgvLista.CurrentRow.Cells["HUELLA"]);
            //listar_(Nombre1);
        }

        private void DgvLista_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            indice = e.RowIndex;
        }      

        private void DgvLista_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            this.btnmodificar.Enabled = System.Convert.ToBoolean(DgvLista.Rows.Count > 0 ? true: false);
            this.BtnEliminar.Enabled = System.Convert.ToBoolean(DgvLista.Rows.Count > 0 ? true : false);
        }
       
        private void DgvLista_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            this.btnmodificar.Enabled = System.Convert.ToBoolean(DgvLista.Rows.Count > 0 ? true : false);
            this.BtnEliminar.Enabled = System.Convert.ToBoolean(DgvLista.Rows.Count > 0 ? true : false);
        }

        private void DgvLista_VisibleChanged(object sender, EventArgs e)
        {
            this.btnmodificar.Enabled = System.Convert.ToBoolean(DgvLista.Rows.Count > 0 ? true : false);
            this.BtnEliminar.Enabled = System.Convert.ToBoolean(DgvLista.Rows.Count > 0 ? true : false);
        }
       

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            //// ===================================================================================================================================================================
            //Clssistema sistema = new Clssistema();
            //Utilitarios_._tabla_sistema = sistema.consultar_formulario_derecho_autorizacion(Convert.ToInt32(Id_UsuarioGlobal), "PERIODO", "NUEVO");
            //if (Utilitarios_._tabla_sistema == null){
            //    MessageBox.Show("Ud. no tiene derecho y permisos necesarios para agregar registro en el sistema", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    // Close()
            //    return;
            //}else{
            //    bool _ok;
            //    _ok = System.Convert.ToBoolean(Utilitarios_._tabla_sistema.Rows[0]["Aprobacion"].ToString());
            //    if (_ok == false)
            //        return;
            //}
            //// ===================================================================================================================================================================

            Trabajador_Huella2 f = new Trabajador_Huella2();
            f.Nivel = "N";
            //f.MostrarListaCajaDesplegable();
            // f.txtcodigo.Focus()
            f.Id_Trabajador = 0;
            ////f.Id_Tipo_Unidad = 0;
            ////f.Id_Marca = 0;
            ////f.Id_Modelo = 0;
            // f.TxtSaldo.Text = 0
            f.ShowDialog();
            Listar_Trabajador(TxtBusca.Text.Trim());
        }
        private void btnmodificar_Click(object sender, EventArgs e)
        {
            Trabajador_Huella2 f = new Trabajador_Huella2();

            Boolean ok;
            ok = TxtTrabaj.Text != "";
            if ((ok == false))
            {
                MessageBox.Show("Seleccione registro a modificar, por favor.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                TxtBusca.Focus();
                return;
            }
            //if ((Nombre1.ToString() == ""))
            //{
            //    MessageBox.Show("Seleccione Registro a modificar", "Sistema de Almacen", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    return;
            //}
            ////Initialize image variable
            //Image newImage;
            ////Read image data into a memory stream
            //using (MemoryStream ms = new MemoryStream(imageData, 0, imageData.Length))
            //{
            //    ms.Write(imageData, 0, imageData.Length);
            //    //Set image variable value using memory stream.
            //    newImage = Image.FromStream(ms, true);
            //}
            ////set picture

            f.Id_Trabajador = Id_trajador1;
            f.TxtTrabajador.Text = Nombre1;
            f.txtTemplate.Text = Template1;
            f.TxtCalidad.Text = Calidad1;
           
            ////=========================================================================
            //byte[] imageData = (byte[])Dt_Trabajador2.Rows[0].ItemArray[1];

            ////Read image data into a memory stream
            //using (MemoryStream ms = new MemoryStream(imageData, 0, imageData.Length))
            //{
            //    ms.Write(imageData, 0, imageData.Length);
            //    //Set image variable value using memory stream.
            //    newImage = Image.FromStream(ms, true);
            //}
            ////set picture
            //f.fpicture.Image = newImage;
            ////f.TxtTrabajador.Text = System.Convert.ToString(DgvLista.Item("Id_Trabajador", indice).Value.ToString.Trim);
            
            f.Nivel = "M";
            f.ShowDialog();
            indice = -1;
            Listar_Trabajador(TxtBusca.Text.Trim());
            f.Id_Trabajador = -1;
        }

        
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            Boolean ok;
            ok = Id_trajador1.ToString() != "-1";
            if ((ok == false))
            {
                MessageBox.Show("Seleccione registro a eliminar, por favor.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                TxtBusca.Focus();
                return;
            }

            //MessageBox.Show(Convert.ToString(Id_Viat_Ruta_Costo1), "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            if (MessageBox.Show("¿Desea Eliminar Registro?", "Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                policia.clsaccesodatos servidor = new policia.clsaccesodatos();
                servidor.cadenaconexion = Ruta;
                if (servidor.abrirconexiontrans() == true)
                {
                    //int rpta = -1;
                    //string mensaje = "";
                    servidor.ejecutar("[dbo].[pa_mantenimiento_Trabajador_Biometrico]"
                       , false
                       , Convert.ToInt32(Id_trajador1)
                       , null
                       , null
                       , null
                       //, null
                       //, null
                       , "E", 0, "");
                    if (servidor.getRespuesta() == 1)
                    {
                        servidor.cerrarconexiontrans();
                        __mesajeerror = servidor.getMensaje();
                        //__mesajeerror = mensaje;
                        MessageBox.Show(__mesajeerror, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        servidor.cancelarconexiontrans();
                        __mesajeerror = servidor.getMensaje();
                        // __mesajeerror = mensaje;
                        MessageBox.Show(__mesajeerror, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    __mesajeerror = servidor.getMensageError();
                    servidor.cerrarconexiontrans();
                    MessageBox.Show(__mesajeerror, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            Id_trajador1 = -1;
            Listar_Trabajador(TxtBusca.Text.Trim());
        }      
    }
}
