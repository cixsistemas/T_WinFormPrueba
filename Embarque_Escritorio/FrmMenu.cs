using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Embarque_Escritorio
{
    public partial class FrmMenu : Form
    {
        Utilitarios Utilitarios_ = new Utilitarios();
        Login Login_ = new Login();
        public int Id_UsuarioGlobal;

        public FrmMenu(int Id_UsuarioGlobal)
        {
            InitializeComponent();
            this.Id_UsuarioGlobal = Id_UsuarioGlobal;
        }

        public void activamenu()
        {
            string[] lista = new[] {
                                    "ASISTENCIA"
                                    ,"PERMISO-DESCANSO"
                                    }; // SE INGRESA FORMULARIOS PRINCIPALES
            bool __ok;
            for (int i = 0; i <= lista.Length - 1; i++)
            {
                Clssistema sistema = new Clssistema();
                //DataTable _tabla_sistema = sistema.consultar_formulario_autorizacion(Convert.ToInt32(_tabla.Rows[0].Equals("Codigo")), lista[i]);
                ////textBox1.Text = Convert.ToString( Login_.Id_UsuarioGlobal);
                //textBox1.Text = Login_.textBox1.Text;
                //Utilitarios_._tabla_sistema = sistema.consultar_formulario_autorizacion(Login_.Id_UsuarioGlobal, lista[i]);
                Utilitarios_._tabla_sistema = sistema.consultar_formulario_autorizacion(Id_UsuarioGlobal, lista[i]);
                //Utilitarios_._tabla_sistema = sistema.consultar_formulario_autorizacion(Convert.ToInt32(Utilitarios_._tabla.Rows[0]["Id_Usuario"].ToString()), lista[i]);
                //Utilitarios_._tabla_sistema = sistema.consultar_formulario_autorizacion(Convert.ToInt32(Utilitarios_._tabla.Rows[0].ItemArray[0]), lista[i]);
                //textBox1.Text = Convert.ToString( (Utilitarios_._tabla.Rows[0].ItemArray[1]));
                if (Utilitarios_._tabla_sistema == null)
                    __ok = false;
                else
                    __ok = System.Convert.ToBoolean(Utilitarios_._tabla_sistema.Rows[0].ItemArray[2]);
                switch (i)
                {
                    case 0:
                        {
                            // UsuariosDelSistemaToolStripMenuItem.Enabled = __ok
                            programacionToolStripMenuItem.Visible = __ok;
                            break;
                        }

                    case 1:
                        {
                            // ControlDeAccesosToolStripMenuItem.Enabled = __ok
                            trabajadorToolStripMenuItem.Visible = __ok;
                            break;
                        }
                }
            }
        }


        private void programacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Programacion frm = new Programacion();
            frm.Id_UsuarioGlobal = Convert.ToInt32(Id_UsuarioGlobal);
            frm.MdiParent = this;
            frm.Show();
        }

        private void acercaDeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Acercade frm = new Acercade();
            frm.MdiParent = this;
            frm.Show();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ViaticoCostoRuta1 frm = new ViaticoCostoRuta1();
            
            frm.MdiParent = this;
            frm.Show();
        }

        private void trabajadorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Trabajador_Huella1 frm = new Trabajador_Huella1();
            frm.Id_UsuarioGlobal = Convert.ToInt32(Id_UsuarioGlobal);
            frm.MdiParent = this;
            frm.Show();
        }
        private void viaticoManualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Vale_Viatico frm = new Vale_Viatico();
            //frm.Id_UsuarioGlobal = Convert.ToInt32(Id_UsuarioGlobal);
            frm.MdiParent = this;
            frm.Show();
        }
        private void Menu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void FrmMenu_Load(object sender, EventArgs e)
        {

        }
    }
}
