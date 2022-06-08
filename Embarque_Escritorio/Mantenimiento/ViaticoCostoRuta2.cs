using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Embarque_Escritorio.Listar;

namespace Embarque_Escritorio
{
    public partial class ViaticoCostoRuta2 : Form
    {
        private String Ruta = System.Configuration.ConfigurationManager.AppSettings.Get("CadenaConeccion");
        policia.clsaccesodatos servidor = new policia.clsaccesodatos();
        string __mesajeerror = "";
        public String Nivel = "";
        Utilitarios Utilitarios_ = new Utilitarios();

        public int Id_Viat_Ruta_Costo = -1;
        public int Id_Sucursal_Origen = -1;
        public int Id_Sucursal_Destino = -1;
        public int Id_Cargo = -1;

        string Hora = "0";

        public ViaticoCostoRuta2()
        {
            InitializeComponent();
        }

        private void ViaticoCostoRuta2_Load(object sender, EventArgs e)
        {
            //if (Nivel=="N")
            //{
            //    CboHra1.SelectedIndex = 0;
            //    CboHra2.SelectedIndex = 0;
            //    CboAmPm.SelectedIndex = 0;
            //    //CboAmPm.Text = "--";
            //}   
            //if (MskHora.Text=="00:00AM")
            //{
            //    MskHora.ForeColor = Color.White;
            //}
     
        }

        private void ViaticoCostoRuta2_KeyDown(object sender, KeyEventArgs e)
        {
            // cerrar formulario con tecla ESC
            if ((e.KeyCode == Keys.Escape))
                this.Close();
        }
        private void TxtOrigen_KeyDown(object sender, KeyEventArgs e)
        {
            Utilitarios_.saltar_Flechas(e);
            if (TxtOrigen.Text != "")
            {
                // SALTAR A OTRO CONTROL CON ENTER
                if (e.KeyCode.Equals(Keys.Enter))
                    System.Windows.Forms.SendKeys.Send("{TAB}");
            }

            if (TxtOrigen.Text == ""){
                if ((e.KeyCode == Keys.F1)){
                    e.SuppressKeyPress = true;
                    try{
                        ListarSucursal f = new ListarSucursal();
                        f.TxtBusca.Focus();
                        f.ShowDialog();
                        //if (Utilitarios_.indice >= 0)
                        //this.TxtOrigen.Text = f.DgvLista["Origen", Utilitarios_.indice+1].Value.ToString();
                        f.DgvLista.Rows[0].Selected = true;
                        this.Id_Sucursal_Origen = Convert.ToInt32(f.DgvLista.Rows[f.DgvLista.CurrentRow.Index - 1].Cells[0].Value.ToString());
                        this.TxtOrigen.Text = f.DgvLista.Rows[f.DgvLista.CurrentRow.Index - 1].Cells[1].Value.ToString();
                        
                        //this.TxtOrigen.Text = Convert.ToString(f.DgvLista[0, f.DgvLista.CurrentRow.Index - 1].Value);                        
                    }
                    catch (Exception){
                        MessageBox.Show("Seleccione Registro.", "Atencion");
                        this.TxtOrigen.Focus();
                    }
                    //// indice = -1
                    this.TxtDestino.Focus();
                }
            }
        }

        private void TxtDestino_KeyDown(object sender, KeyEventArgs e)
        {
            Utilitarios_.saltar_Flechas(e);
            //if (TxtDestino.Text != ""){
            //    // SALTAR A OTRO CONTROL CON ENTER
            //    if (e.KeyCode.Equals(Keys.Enter))
            //        System.Windows.Forms.SendKeys.Send("{TAB}");
            //    //MskHora.Focus();
            //}

            if (TxtDestino.Text == ""){
                if ((e.KeyCode == Keys.F1))
                {
                    e.SuppressKeyPress = true;
                    try{
                        ListarSucursal f = new ListarSucursal();
                        f.TxtBusca.Focus();
                        f.ShowDialog();
                        //if (Utilitarios_.indice >= 0)
                        //this.TxtOrigen.Text = f.DgvLista["Origen", Utilitarios_.indice+1].Value.ToString();
                        f.DgvLista.Rows[0].Selected = true;
                        this.Id_Sucursal_Destino = Convert.ToInt32(f.DgvLista.Rows[f.DgvLista.CurrentRow.Index - 1].Cells[0].Value.ToString());
                        this.TxtDestino.Text = f.DgvLista.Rows[f.DgvLista.CurrentRow.Index - 1].Cells[1].Value.ToString();

                        //this.TxtOrigen.Text = Convert.ToString(f.DgvLista[0, f.DgvLista.CurrentRow.Index - 1].Value);                        
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Seleccione Registro.", "Atencion");
                        this.TxtDestino.Focus();
                    }
                    //// indice = -1
                    
                }
                this.MskHora.Focus();
            }
        }

        private void TxtCargo_KeyDown(object sender, KeyEventArgs e)
        {
            Utilitarios_.saltar_Flechas(e);
            if (TxtCargo.Text != ""){
                // SALTAR A OTRO CONTROL CON ENTER
                if (e.KeyCode.Equals(Keys.F1))
                    System.Windows.Forms.SendKeys.Send("{TAB}");
            }

            if (TxtCargo.Text == ""){
                if ((e.KeyCode == Keys.Enter)){
                    e.SuppressKeyPress = true;
                    try{
                        ListarCargo f = new ListarCargo();
                        f.TxtBusca.Focus();
                        f.ShowDialog();
                        //if (Utilitarios_.indice >= 0)
                        //this.TxtOrigen.Text = f.DgvLista["Origen", Utilitarios_.indice+1].Value.ToString();
                        f.DgvLista.Rows[0].Selected = true;
                        this.Id_Cargo = Convert.ToInt32(f.DgvLista.Rows[f.DgvLista.CurrentRow.Index - 1].Cells[0].Value.ToString());
                        this.TxtCargo.Text = f.DgvLista.Rows[f.DgvLista.CurrentRow.Index - 1].Cells[1].Value.ToString();

                        //this.TxtOrigen.Text = Convert.ToString(f.DgvLista[0, f.DgvLista.CurrentRow.Index - 1].Value);                        
                    }catch (Exception){
                        MessageBox.Show("Seleccione Registro.", "Atencion");
                        this.TxtCargo.Focus();
                    }
                    //// indice = -1
                    this.BtnAceptar.Focus();
                }
            }
        }

        private void BtnAceptar_Click(object sender, EventArgs e)
        {
            Boolean ok;
            ok = Id_Sucursal_Origen != 0 ;
            if ((ok == false))
            {
                MessageBox.Show("Seleccione Origen, por favor.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                TxtOrigen.Focus();
                return;
            }
            ok = Id_Sucursal_Destino != 0;
            if ((ok == false))
            {
                MessageBox.Show("Seleccione Destino, por favor.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                TxtDestino.Focus();
                return;
            }

            ok = TxtOrigen.Text != "";
            if ((ok == false))
            {
                MessageBox.Show("Seleccione Origen, por favor.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                TxtOrigen.Focus();
                return;
            }
            ok = TxtDestino.Text != "";
            if ((ok == false))
            {
                MessageBox.Show("Seleccione Destino, por favor.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                TxtDestino.Focus();
                return;
            }

            //string Hora = "0";
            Hora = (MskHora.Text.Trim());
            Hora.Substring(0, 2);
            textBox1.Text = Hora.Substring(3, 2);

            //if ((Hora.Substring(0, 2)==":") )
            //{
            //    Hora = "0";
            //&& Hora.Substring(3, 2) == ""
            try
            {
                ok = (Convert.ToInt32(Hora.Substring(0, 2)) < 13);
                //if (Convert.ToInt32(Hora.Substring(0, 2)) > 12 )
                if ((ok == false))
                {
                    MessageBox.Show("Ingrese Hora el formato correcto, por favor.131.......", "Sistema"
                    , MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    MskHora.Focus();
                    return;
                    //    TxtCosto.Focus(); 
                }

                //ok = ((Hora.Substring(0, 2) == "") && (textBox1.Text != ""));
                //if ((ok == false))
                ////if ((Hora.Substring(0, 2) != "") && (textBox1.Text == ""))
                //{
                //    MessageBox.Show("Ingrese Hora el formato correcto, por favor.15531.......", "Sistema"
                //    , MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //    return;
                //}
            }
            catch (Exception)
            {

                //throw;
            }
                
           // }

            //if (Convert.ToInt32(Hora.Substring(0, 2)) > 0 && Hora.Substring(0, 2) == "")
            //{
            //    MessageBox.Show("Ingrese Hora el formato correcto, por favor.131.......", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    //    TxtCosto.Focus(); 
            //}

            //MskHora.Text = HORA1.Substring(5, 2);

            //ok = CboHra1.Text == "--";
            //if ((ok == false))
            //{
            //    MessageBox.Show("Ingrese costo, por favor........", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    TxtCosto.Focus();
            //    return;
            //}

            ok = TxtCosto.Text != "";
            if ((ok == false))
            {
                MessageBox.Show("Ingrese costo, por favor.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                TxtCosto.Focus();
                return;
            }
            ok = Id_Cargo != 0;
            if ((ok == false))
            {
                MessageBox.Show("Ingrese Cargo, por favor.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                TxtCargo.Focus();
                return;
            }

            ok = TxtCargo.Text != "";
            if ((ok == false))
            {
                MessageBox.Show("Seleccione Cargo, por favor.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                TxtCargo.Focus();
                return;
            }

            ////textBox1.Text = Convert.ToString(Id_Sucursal_Origen);

            policia.clsaccesodatos servidor = new policia.clsaccesodatos();
            servidor.cadenaconexion = Ruta;
            if (servidor.abrirconexiontrans() == true)
            {
                servidor.ejecutar("[dbo].[pa_mantenimiento_Viatico_Costo_Ruta]",
                    false
                    , Id_Viat_Ruta_Costo
                    , Id_Sucursal_Origen
                    , Id_Sucursal_Destino
                    , TxtCosto.Text.Trim()
                    , MskHora.Text
                    //, CboHra1.Text.Trim() + ":" + CboHra2.Text.Trim()+ CboAmPm.Text.Trim()
                    , Id_Cargo
                    , Nivel, 0, "");
                if (servidor.getRespuesta() == 1)
                {
                    servidor.cerrarconexiontrans();
                    __mesajeerror = servidor.getMensaje();
                    MessageBox.Show(__mesajeerror, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Hide();
                }
                else
                {
                    servidor.cancelarconexiontrans();
                    __mesajeerror = servidor.getMensaje();
                    MessageBox.Show(__mesajeerror, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                __mesajeerror = servidor.getMensaje();
                servidor.cerrarconexiontrans();
                MessageBox.Show(__mesajeerror, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        private void TxtCosto_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            //{
            //    MessageBox.Show("Solo se permiten numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    e.Handled = true;
            //    return;
            //}
            //Para obligar a que sólo se introduzcan números
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
                if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso
                {
                    e.Handled = false;
                }
                else
                {
                    //el resto de teclas pulsadas se desactivan
                    e.Handled = true;
                } 
        }

        private void MskHora_KeyDown(object sender, KeyEventArgs e)
        {
            Utilitarios_.saltar_Flechas(e);
            // SALTAR A OTRO CONTROL CON ENTER
            if (e.KeyCode.Equals(Keys.Enter))
                System.Windows.Forms.SendKeys.Send("{TAB}");
        }

        private void TxtCosto_KeyDown(object sender, KeyEventArgs e)
        {
            Utilitarios_.saltar_Flechas(e);
            // SALTAR A OTRO CONTROL CON ENTER
            if (e.KeyCode.Equals(Keys.Enter))
                System.Windows.Forms.SendKeys.Send("{TAB}");
        }

        private void MskHora_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                CboAmPm.Focus();
        }

        private void CboAmPm_KeyUp(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //    TxtCosto.Focus();
        }

        private void TxtCargo_KeyUp(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //    BtnAceptar.Focus();
        }
        private void TxtOrigen_KeyUp(object sender, KeyEventArgs e)
        {
            //if (TxtOrigen.Text != "")
            //{
            //    if (e.KeyCode == Keys.Enter)
            //        TxtDestino.Focus();
            //}
        }
        private void TxtDestino_KeyUp(object sender, KeyEventArgs e)
        {
            if (TxtDestino.Text != ""){
                if (e.KeyCode == Keys.Enter)
                    MskHora.Focus();
            }
        }

        private void TxtCosto_KeyUp(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //    TxtCargo.Focus();
        }
        private void CboAmPm_KeyDown(object sender, KeyEventArgs e)
        {
            // Algúna regla para evitar que se salte al otro control?
            if (e.KeyCode.Equals(Keys.Enter))
                System.Windows.Forms.SendKeys.Send("{TAB}");
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CboHra1_KeyDown(object sender, KeyEventArgs e)
        {
            // Algúna regla para evitar que se salte al otro control?
            if (e.KeyCode.Equals(Keys.Enter))
                System.Windows.Forms.SendKeys.Send("{TAB}");
        }

        private void CboHra2_KeyDown(object sender, KeyEventArgs e)
        {
            // Algúna regla para evitar que se salte al otro control?
            if (e.KeyCode.Equals(Keys.Enter))
                System.Windows.Forms.SendKeys.Send("{TAB}");
        }

        private void MskHora_KeyDown_1(object sender, KeyEventArgs e)
        {
            Utilitarios_.saltar_Flechas(e);
            // Algúna regla para evitar que se salte al otro control?
            if (e.KeyCode.Equals(Keys.Enter))
                System.Windows.Forms.SendKeys.Send("{TAB}");
        }

        

    }
}
