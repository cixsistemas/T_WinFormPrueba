using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using Servicios;

namespace Embarque_Escritorio
{
    public partial class Login : Form
    {
        private String Ruta = ConfigurationManager.AppSettings.Get("CadenaConeccion");
        policia.clsaccesodatos servidor = new policia.clsaccesodatos();

        Clssistema Clssistema_ = new Clssistema();
        Utilitarios Utilitarios_ = new Utilitarios();
        public int Id_UsuarioGlobal;
        string __mesajeerror = "";
        //DataTable _tabla_sistema;

        public Login()
        {
            InitializeComponent();
        }
        private void valida()
        {
            bool ok = false;
            ok = this.txtlogin.Text.Trim() != "";
            ok = ok & this.txtpassword.Text.Trim() != "";
            this.BtnAceptar.Enabled = ok;
        }
      

        private void Login_Load(object sender, EventArgs e)
        {
            // REM Obtenenemos la cadena de coneccion al servidor
            //Ruta = System.Configuration.ConfigurationManager.AppSettings("CadenaConeccion").ToString;
        }


       
        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnAceptar_Click_1(object sender, EventArgs e)
        {
            Encriptar Encriptar_ = new Encriptar();
            //sencriptacion se = new sencriptacion();

            policia.clsaccesodatos servidor = new policia.clsaccesodatos();
            String strLogin, strPassword;
            strLogin = this.txtlogin.Text.Trim();
            strPassword = this.txtpassword.Text.Trim();

            servidor.cadenaconexion = Ruta;

            if (servidor.abrirconexiontrans() == true)
            {
                Utilitarios_._tabla = servidor.consultar("__pr_VerificaAccesoSistemaUsuario", strLogin.Trim()).Tables[0];
                if (Utilitarios_._tabla == null)
                {
                    __mesajeerror = servidor.getMensageError();
                    servidor.cerrarconexion();
                    MessageBox.Show(__mesajeerror, "Sistema de Almacen", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    //int NroFilas = Utilitarios_._tabla.Rows.Count;
                    //if (NroFilas == 0)
                    //{
                        sencriptacion se = new sencriptacion();
                        Boolean ok;
                        ok = strLogin == Utilitarios_._tabla.Rows[0]["login"].ToString();
                        ok = ok && strPassword == se.DecryptKey(Utilitarios_._tabla.Rows[0]["password"].ToString());                       
                    //}
                        if (ok == false)
                        {
                            servidor.cerrarconexion();
                            MessageBox.Show("Login o password ingresado son incorrectos o ud. esta inactivo", "Sistema"
                                , MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    else
                    {
                        BtnCancelar.Enabled = true;
                        BtnAceptar.Enabled = false;
                        Id_UsuarioGlobal = Convert.ToInt32((Utilitarios_._tabla.Rows[0]["Id_Usuario"].ToString()));
                        FrmMenu m = new FrmMenu(Id_UsuarioGlobal);                        
                        //textBox1.Text = Convert.ToString((Utilitarios_._tabla.Rows[0]["Id_Usuario"].ToString()));
                        //m.textBox1.Text=textBox1.Text;
                        m.activamenu();
                        Hide();
                        // m.ShowDialog() ' AVECES AL CERRAR MENU, SE VUELVE ABRIR
                        m.Show(); // SE ABRE VARIOS MENU

                        //Application.DoEvents();
                        //servidor.cerrarconexion();

                        ////clssistema sistema = new clssistema();
                        //Utilitarios_._tabla_sistema = Clssistema_.consultar_formulario_autorizacion(Convert.ToInt32(Utilitarios_._tabla.Rows[0].ItemArray[0]), "MENU");
                        //if (Utilitarios_._tabla_sistema == null)
                        //    MessageBox.Show(__mesajeerror, "Sistema de Almacen", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        //else
                        //{
                        //    bool _ok;
                        //    //_ok = System.Convert.ToBoolean(_tabla_sistema.Rows[0].Equals("Aprobacion"));
                        //    _ok = System.Convert.ToBoolean(Utilitarios_._tabla_sistema.Rows[0].ItemArray[2]);
                        //    if (_ok == false)
                        //        MessageBox.Show("Ud. no tiene autorización para ingresar al sistema", "Sistema de Almacen", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        //    if (_ok == true)
                        //    {
                        //        BtnCancelar.Enabled = true;
                        //        BtnAceptar.Enabled = false;

                        //        Menu m = new Menu();
                        //        m.activamenu();
                        //        Id_UsuarioGlobal = Convert.ToInt32((Utilitarios_._tabla.Rows[0].ItemArray[0]));
                        //        textBox1.Text = Convert.ToString((Utilitarios_._tabla.Rows[0].ItemArray[0]));
                        //        Hide();
                        //        // m.ShowDialog() ' AVECES AL CERRAR MENU, SE VUELVE ABRIR
                        //        m.Show(); // SE ABRE VARIOS MENU
                        //    }
                        //}
                    }
                }
            }
            else
            {
                __mesajeerror = servidor.getMensageError();
                servidor.cerrarconexion();
                MessageBox.Show(__mesajeerror, "Sistema de Almacen", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
        }

        private void txtlogin_KeyDown(object sender, KeyEventArgs e)
        {
            Utilitarios_.saltar_Flechas(e);
            // SALTAR A OTRO CONTROL CON ENTER
            if (e.KeyCode.Equals(Keys.Enter))
                System.Windows.Forms.SendKeys.Send("{TAB}");
        }

        private void txtpassword_KeyDown(object sender, KeyEventArgs e)
        {
            Utilitarios_.saltar_Flechas(e);
            // SALTAR A OTRO CONTROL CON ENTER
            if (e.KeyCode.Equals(Keys.Enter))
                System.Windows.Forms.SendKeys.Send("{TAB}");
        }

    }
}
