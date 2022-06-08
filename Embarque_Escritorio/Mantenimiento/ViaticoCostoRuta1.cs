using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Embarque_Escritorio.Listar;
//using System;
using System.Text.RegularExpressions;

namespace Embarque_Escritorio
{
    public partial class ViaticoCostoRuta1 : Form
    {
        private String Ruta = System.Configuration.ConfigurationManager.AppSettings.Get("CadenaConeccion");
        policia.clsaccesodatos servidor = new policia.clsaccesodatos();
        Utilitarios Utilitarios_ = new Utilitarios();

        DataTable Dt_PrecioViaticos = null;
        public int Id_Viat_Ruta_Costo1;
        public string ORIGEN1;
        public string DESTINO1;
        public string COSTO1;
        public string HORA1;
        ////public string HORA2;
        ////public string HORA_AmPm;
        public string CARGO1;
        public int ID_CARGO1;
        public int Id_Suc_Origen1;
        public int Id_Suc_Des1;
        string __mesajeerror = "";

        public ViaticoCostoRuta1()
        {
            InitializeComponent();
        }


        private void ListaPrecioRuta(String Origen, String Destino, String Hora, String Cargo, String Opcion)
        {
            this.mesajeerror.ForeColor = Color.Blue;
            policia.clsaccesodatos servidor = new policia.clsaccesodatos();
            servidor.cadenaconexion = Ruta;
            if (servidor.abrirconexiontrans() == true)
            {
                if (servidor.consultar("[dbo].[Pa_Listar_Viatico_Costo_Ruta]", Origen, Destino, Hora, Cargo, Opcion).Tables.Count > 0)
                    Dt_PrecioViaticos = servidor.consultar("[dbo].[Pa_Listar_Viatico_Costo_Ruta]", Origen, Destino, Hora, Cargo, Opcion).Tables[0];
                if (Dt_PrecioViaticos == null)
                {
                    servidor.cerrarconexion();
                    this.mesajeerror.Text = "NO HAY REGISTROS PARA MOSTRAR";
                    this.mesajeerror.ForeColor = Color.Red;
                }
                else
                {
                    this.DgvLista.DataSource = Dt_PrecioViaticos;
                    int NroFilas = Dt_PrecioViaticos.Rows.Count;
                    if (NroFilas == 0)
                    {
                        this.DgvLista.DataSource = null;
                        this.mesajeerror.Text = "NO HAY REGISTROS PARA MOSTRAR";
                        this.mesajeerror.ForeColor = Color.Red;
                    }
                    else
                    {
                        this.DgvLista.Columns["ID"].Visible = false;
                        this.DgvLista.Columns["ID_CARGO"].Visible = false;
                        this.DgvLista.Columns["Id_Suc_Origen"].Visible = false;
                        this.DgvLista.Columns["Id_Suc_Des"].Visible = false;

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

        //private bool IsFirstLetterId(char c)
        //{
        //    Regex regex = new Regex("[JV]", RegexOptions.IgnoreCase);
        //    return regex.IsMatch(c);
        //}
        //protected override void OnKeyPress(System.Windows.Forms.KeyPressEventArgs e)
        //{
        //    base.OnKeyPress(e);
        //    // Si el punto de inserción no se encuentra
        //    // en la primera posición, abandonamos el
        //    // procedimiento.
        //    // 
        //    if ((this.maskedTextBox1 != 0))
        //        return;
        //    // Validamos si el primer carácter es J o V
        //    // 
        //    e.Handled = (!(this.IsFirstLetterId(e.KeyChar)));
        //}

        //// <System.Diagnostics.DebuggerStepThrough()> _
        //protected override void WndProc(ref System.Windows.Forms.Message m)
        //{
        //    const Int32 WM_PASTE = 0x302;
        //    if ((m.Msg == WM_PASTE))
        //    {
        //        // Si el contenido del Portapapeles es distinto a texto,
        //        // abandonamos el procedimiento.
        //        // 
        //        if (!(Clipboard.GetDataObject().GetDataPresent(DataFormats.Text)))
        //            return;
        //        // Obtenemos los datos del Portapapeles
        //        // 
        //        string value = Clipboard.GetText(TextDataFormat.Text);
        //        // Validamos el primer carácter del contenido del Portapapeles
        //        // 
        //        if ((!(this.IsFirstLetterId(value.Chars[0]))))
        //            return;
        //    }

        //    // Procesamos los restantes mensajes
        //    base.WndProc(m);
        //}

  

        private void ViaticoCostoRuta1_Load(object sender, EventArgs e)
        {
            ListaPrecioRuta("","","","", "1");
            alternarColoFilasDatagridview(DgvLista);
            
            //maskedTextBox1.CharacterCasing = CharacterCasing.Upper;
        }
        public void alternarColoFilasDatagridview(DataGridView dgv)
        {
            {
                var withBlock = dgv;
                withBlock.RowsDefaultCellStyle.BackColor = Color.PaleGoldenrod;
                withBlock.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
            }
        }

        private void ViaticoCostoRuta1_KeyDown(object sender, KeyEventArgs e)
        { 
            // cerrar formulario con tecla ESC
            if ((e.KeyCode == Keys.Escape))
                this.Close();   
        }

        private void DgvLista_KeyDown(object sender, KeyEventArgs e)
        {
           
        }
        private void DgvLista_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Id_Viat_Ruta_Costo1 = Convert.ToInt32(DgvLista.CurrentRow.Cells["ID"].Value.ToString());
            ORIGEN1 = DgvLista.CurrentRow.Cells["ORIGEN"].Value.ToString();
            DESTINO1 = DgvLista.CurrentRow.Cells["DESTINO"].Value.ToString();
            COSTO1 = DgvLista.CurrentRow.Cells["COSTO"].Value.ToString();
            HORA1 = DgvLista.CurrentRow.Cells["HORA"].Value.ToString();
            CARGO1 = DgvLista.CurrentRow.Cells["CARGO"].Value.ToString();
            ID_CARGO1 = Convert.ToInt32(DgvLista.CurrentRow.Cells["ID_CARGO"].Value.ToString());
            Id_Suc_Origen1 = Convert.ToInt32(DgvLista.CurrentRow.Cells["Id_Suc_Origen"].Value.ToString());
            Id_Suc_Des1 = Convert.ToInt32(DgvLista.CurrentRow.Cells["Id_Suc_Des"].Value.ToString());

            //TxtTrabaj.Text = ORIGEN1;
        }
        private void TxtDestino_KeyDown(object sender, KeyEventArgs e)
        {
            Utilitarios_.saltar_Flechas(e);
            // SALTAR A OTRO CONTROL CON ENTER
            if (e.KeyCode.Equals(Keys.Enter))
                System.Windows.Forms.SendKeys.Send("{TAB}");

            //saltar_Flechas(e);
            //if (TxtOrigen.Text == "")
            //{
            if ((e.KeyCode == Keys.F1))
            {
                e.SuppressKeyPress = true;
                try
                {
                    ListarDestino f = new ListarDestino();
                    f.TxtBusca.Focus();
                    f.ShowDialog();
                    //if (Utilitarios_.indice >= 0)
                    //this.TxtOrigen.Text = f.DgvLista["Origen", Utilitarios_.indice+1].Value.ToString();
                    f.DgvLista.Rows[0].Selected = true;
                    this.TxtDestino.Text = f.DgvLista.Rows[f.DgvLista.CurrentRow.Index - 1].Cells[1].Value.ToString();

                    //this.TxtOrigen.Text = Convert.ToString(f.DgvLista[0, f.DgvLista.CurrentRow.Index - 1].Value);                        
                }
                catch (Exception)
                {
                    MessageBox.Show("Seleccione Registro.", "Atencion");
                    this.TxtDestino.Focus();
                }
                //// indice = -1
                this.TxtDestino.Focus();
            }
            //}
        }

        private void TxtOrigen_KeyDown(object sender, KeyEventArgs e)
        {
            Utilitarios_.saltar_Flechas(e);
            // SALTAR A OTRO CONTROL CON ENTER
            if (e.KeyCode.Equals(Keys.Enter))
                System.Windows.Forms.SendKeys.Send("{TAB}");

            //saltar_Flechas(e);
            //if (TxtOrigen.Text == "")
            //{
            if ((e.KeyCode == Keys.F1))
            {
                e.SuppressKeyPress = true;
                try
                {
                    ListarOrigen f = new ListarOrigen();
                    f.TxtBusca.Focus();
                    f.ShowDialog();
                    //if (Utilitarios_.indice >= 0)
                    //this.TxtOrigen.Text = f.DgvLista["Origen", Utilitarios_.indice+1].Value.ToString();
                    f.DgvLista.Rows[0].Selected = true;
                    this.TxtOrigen.Text = f.DgvLista.Rows[f.DgvLista.CurrentRow.Index - 1].Cells[1].Value.ToString();

                    //this.TxtOrigen.Text = Convert.ToString(f.DgvLista[0, f.DgvLista.CurrentRow.Index - 1].Value);                        
                }
                catch (Exception)
                {
                    MessageBox.Show("Seleccione Registro.", "Atencion");
                    this.TxtOrigen.Focus();
                }
                //// indice = -1
                this.TxtOrigen.Focus();
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            ViaticoCostoRuta2 f = new ViaticoCostoRuta2();
            f.Nivel = "N";
            //f.MostrarListaCajaDesplegable();
            // f.txtcodigo.Focus()
            f.Id_Viat_Ruta_Costo = 0;
            f.Id_Sucursal_Origen = 0;
            f.Id_Sucursal_Destino = 0;
            f.Id_Cargo = 0;
            ////f.Id_Tipo_Unidad = 0;
            ////f.Id_Marca = 0;
            ////f.Id_Modelo = 0;
            // f.TxtSaldo.Text = 0
            f.ShowDialog();
            ListaPrecioRuta("", "", "","", "1");
        }

        private void btnmodificar_Click(object sender, EventArgs e)
        {
            ViaticoCostoRuta2 f = new ViaticoCostoRuta2();

            Boolean ok;
            ok = Id_Viat_Ruta_Costo1.ToString() != "-1";
            if ((ok == false))
            {
                MessageBox.Show("Seleccione registro a modificar, por favor.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                BtnBuscar.Focus();
                return;
            }
  


            f.Id_Viat_Ruta_Costo = Id_Viat_Ruta_Costo1;
            f.TxtOrigen.Text = ORIGEN1;
            f.TxtDestino.Text = DESTINO1;
            f.CboHra1.Text = HORA1.Substring(0, 2);
            f.CboHra2.Text = HORA1.Substring(3, 2);
            f.CboAmPm.Text = HORA1.Substring(5, 2);
            f.MskHora.Text = HORA1;
            f.TxtCosto.Text = COSTO1;
            f.TxtCargo.Text = CARGO1;
            f.Id_Sucursal_Origen = Id_Suc_Origen1;
            f.Id_Sucursal_Destino = Id_Suc_Des1;
            f.Id_Cargo = ID_CARGO1;

            f.Nivel = "M";
            f.ShowDialog();
            //indice = -1;
            ListaPrecioRuta("", "", "","", "1");
            f.Id_Viat_Ruta_Costo = -1;
            f.Id_Sucursal_Origen = -1;
            f.Id_Sucursal_Destino = -1;
            f.Id_Cargo = -1;

            Id_Viat_Ruta_Costo1 = -1;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void maskedTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void maskedTextBox1_Validating(object sender, CancelEventArgs e)
        {
            //TextBox box = sender as TextBox;
            //string pattern = "\\d{1,2}:\\d{2}\\s*(AM|PM)";
            //Regex timeRegex = new Regex(@"^([0]?[1-9]|[1][0-2]):([0-5][0-9]|[1-9]) [AP]M$");

            //if (box != null)
            //{
            //    if (!Regex.IsMatch(box.Text, pattern, RegexOptions.CultureInvariant))
            //    {
            //        MessageBox.Show("Not a valid time format ('hh:mm AM|PM').");
            //        e.Cancel = true;
            //        box.Select(0, box.Text.Length);
            //    }
            //}
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            Boolean ok;
            ok = Id_Viat_Ruta_Costo1.ToString() != "-1";
            if ((ok == false)){
                MessageBox.Show("Seleccione registro a eliminar, por favor.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                BtnBuscar.Focus();
                return;
            }

            //MessageBox.Show(Convert.ToString(Id_Viat_Ruta_Costo1), "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            if (MessageBox.Show("¿Desea Eliminar Registro?", "Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                policia.clsaccesodatos servidor = new policia.clsaccesodatos();
                servidor.cadenaconexion = Ruta;
                if (servidor.abrirconexiontrans() == true){
                    //int rpta = -1;
                    //string mensaje = "";
                    servidor.ejecutar("[dbo].[pa_mantenimiento_Viatico_Costo_Ruta]", false
                       , Convert.ToInt32(Id_Viat_Ruta_Costo1)
                       , null
                       , null
                       , null
                       , null
                       , null
                       //, Convert.ToInt32(Id_Suc_Origen1)/* TODO Change to default(_) if this is not a reference type */
                       //, Convert.ToInt32(Id_Suc_Des1)/* TODO Change to default(_) if this is not a reference type */
                       //, COSTO1
                       //, HORA1
                       //, Convert.ToInt32(ID_CARGO1)
                       , "E", 0, "");
                    if (servidor.getRespuesta() == 1){
                        servidor.cerrarconexiontrans();
                        __mesajeerror = servidor.getMensaje();
                        //__mesajeerror = mensaje;
                        MessageBox.Show(__mesajeerror, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }else {
                        servidor.cancelarconexiontrans();
                        __mesajeerror = servidor.getMensaje();
                       // __mesajeerror = mensaje;
                        MessageBox.Show(__mesajeerror, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }else{
                    __mesajeerror = servidor.getMensageError();
                    servidor.cerrarconexiontrans();
                    MessageBox.Show(__mesajeerror, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            Id_Viat_Ruta_Costo1 = -1;
            ListaPrecioRuta("", "", "", "", "1");

       
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(TxtHora.Text.Trim(), "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
            //string Hora = TxtHora.Text;
            //if (TxtHora.Text == "  :")
            //{
            //    Hora = "";
            //    ListaPrecioRuta(TxtOrigen.Text.Trim(), this.TxtDestino.Text.Trim(),
            //    Hora, "", "1");
            //}
            //textBox1.Text = Hora;

            if (TxtHora.Text == "  :")
            {
                ListaPrecioRuta(TxtOrigen.Text.Trim(), this.TxtDestino.Text.Trim(),
               "", "", "1");

            }
            else if (TxtHora.Text != "  :" && rbtAM.Checked == true)
            {
                ListaPrecioRuta(TxtOrigen.Text.Trim(), this.TxtDestino.Text.Trim(),
           TxtHora.Text.Trim() + "AM", "", "1");
            }

            else if (TxtHora.Text != "  :" && rbtPM.Checked == true)
            {
                ListaPrecioRuta(TxtOrigen.Text.Trim(), this.TxtDestino.Text.Trim(),
           TxtHora.Text.Trim() + "PM", "", "1");
            }

            //if (rbtAM.Checked == true)
            //{
            //    ListaPrecioRuta(TxtOrigen.Text.Trim(), this.TxtDestino.Text.Trim(),
            //    TxtHora.Text.Trim() + "AM", "", "1");

            //    //ListaPrecioRuta(TxtOrigen.Text.Trim(), this.TxtDestino.Text.Trim()
            //    //, TxtHora.Text.Trim() + rbtAM.Text.Trim(), "", "1");
            //}
            //if (rbtPM.Checked == true)
            //{
            //    ListaPrecioRuta(TxtOrigen.Text.Trim(), this.TxtDestino.Text.Trim(),
            //    TxtHora.Text.Trim() + "PM", "", "1");
            //}

        }

        private void TxtHora_KeyDown(object sender, KeyEventArgs e)
        {
            Utilitarios_.saltar_Flechas(e);
            // SALTAR A OTRO CONTROL CON ENTER
            if (e.KeyCode.Equals(Keys.Enter))
                System.Windows.Forms.SendKeys.Send("{TAB}");
        }

              
    }
}
