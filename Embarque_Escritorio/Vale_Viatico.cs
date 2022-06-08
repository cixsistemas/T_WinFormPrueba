using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AxZKFPEngXControl;

namespace Embarque_Escritorio
{
    public partial class Vale_Viatico : Form
    {
        private String Ruta = System.Configuration.ConfigurationManager.AppSettings.Get("CadenaConeccion");
        policia.clsaccesodatos servidor = new policia.clsaccesodatos();
        string __mesajeerror = "";
        public string CodigoProgramacion;

        public Vale_Viatico()
        {
            InitializeComponent();
        }

        private void Vale_Viatico_Load(object sender, EventArgs e)
        {
            //==================================================================================
            //DATOS DE TRABAJADORES
            ListaTrabajadores(CodigoProgramacion, "", "", "", "", "4");
            ListaManifiesto(CodigoProgramacion, "", "", "", "", "5");
            //==================================================================================
        }

        private void ListaTrabajadores(String Codigo, String Origen, String Destino, String Fecha, String Hora, String Opcion)
        {
            try{
                servidor.cadenaconexion = Ruta;
                if (servidor.abrirconexion() == true)
                {
                    System.Data.DataTable dt = servidor.consultar("[dbo].[BD_Pje_Listar_Programacion_Trabajadores]", Codigo, Origen, Destino, Fecha, Hora, Opcion).Tables[0];
                    if (dt.Rows.Count == 0)
                    {
                        servidor.cerrarconexion();
                        //SE COMENTO EL DIA 15/05/2018
                        //_Lista.ShowMessage(__mensaje, __pagina, "Error, al intentar recuperar datos.", "CerrarSession.aspx");
                    }
                    else
                    {
                        this.DdlTripulantes.DataSource = dt;
                        this.DdlTripulantes.DisplayMember = "NOMBRE";
                        this.DdlTripulantes.ValueMember = "CODIGO";
                        //this.DdlTripulantes.DataBind();
                        servidor.cerrarconexion();
                    }
                }
                else
                {
                    servidor.cerrarconexion();
                    __mesajeerror = servidor.getMensageError();
                }
            }catch (Exception){
                servidor.cerrarconexion();
                MessageBox.Show("Error Trabajadores", "Atencion");
            }
        }

        private void ListaManifiesto(String Codigo, String Origen, String Destino, String Fecha, String Hora, String Opcion)
        {
            try
            {
                servidor.cadenaconexion = Ruta;
                if (servidor.abrirconexion() == true)
                {
                    System.Data.DataTable dt = servidor.consultar("[dbo].[BD_Pje_Listar_Programacion_Trabajadores]", Codigo, Origen, Destino, Fecha, Hora, Opcion).Tables[0];
                    if (dt.Rows.Count == 0)
                    {
                        servidor.cerrarconexion();
                        //SE COMENTO EL DIA 15/05/2018
                        //_Lista.ShowMessage(__mensaje, __pagina, "Error, al intentar recuperar datos.", "CerrarSession.aspx");
                    }
                    else
                    {
                        TxtManifiesto.Text = dt.Rows[0].ItemArray[0].ToString();
                        //this.DdlTripulantes.DataSource = dt;
                        //this.DdlTripulantes.DisplayMember = "NOMBRE";
                        //this.DdlTripulantes.ValueMember = "CODIGO";
                        //this.DdlTripulantes.DataBind();
                        servidor.cerrarconexion();
                    }
                }
                else
                {
                    servidor.cerrarconexion();
                    __mesajeerror = servidor.getMensageError();
                }
            }
            catch (Exception)
            {
                servidor.cerrarconexion();
                MessageBox.Show("Error Manifiesto", "Atencion");
            }
        }

        private void Vale_Viatico_KeyDown(object sender, KeyEventArgs e)
        {
            // cerrar formulario con tecla ESC
            if ((e.KeyCode == Keys.Escape))
                this.Close();
        }

        private void BtnAceptar_Click(object sender, EventArgs e)
        {

        }
        
    }
}
