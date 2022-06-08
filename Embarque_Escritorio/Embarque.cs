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
    public partial class Embarque : Form
    {
        private String Ruta = System.Configuration.ConfigurationManager.AppSettings.Get("CadenaConeccion");
        policia.clsaccesodatos servidor = new policia.clsaccesodatos();
        public int Id_UsuarioGlobal;

        DataTable Dt_Programacion;
        DataTable Dt_Pasajeros;
        DataTable Dt_AsientosActivos;
        DataTable Dt_PrecioViaticos = null;
        //DataTable Dt_Trabajadores;
        string __mesajeerror = "";
        public string Codigo;
        string Hora;
        int Id_Pas_Abordo = -1;

        //================== SENSOR ==================================
        private AxZKFPEngX ZkFprint1 = new AxZKFPEngX();
        private bool Check;
        string TemplateBD;
        int fpcHandle;
        //============================================================

        //================== REGISTRO DE TRIPULANTE ==================
        bool Abordo_o_no_Tripulante;
        int Id_Trab_Abordo = -1;
        string DocuTrab, DescripcionTrab, Id_Trabajador, TipoEmbarque;
        string Cargo = "";
        //============================================================

        //================== REGISTRO DE VALE DE VIATICO ==================
        int Id_Vale_Viatico = -1;
        string Manifiesto, Aprobado, Anulado
        , Observacion, HostModificacion;
        string ValidarTripulante = "";
        string ValidarUsuario = "";
        string ValidoTripulante = "";
        string ValidoUsuario = "";
        string TipoViatico = "";
        //int  Id_Usuario;
        //double Cantidad_Soles;
        //bool Cerrar_Fecha;
        //==================================================================

        public Embarque()
        {
            InitializeComponent();
        }
        private void Mensaje(String s)
        {
            __mensaje.Text = s;
        }
        private void MensajeValeViatico(String s)
        {
            __mensajeVale.Text = s;
        }  

        private void Embarque_Load(object sender, EventArgs e)
        {
            //Programacion f = new Programacion();
            //Codigo = f.Codigo.Trim();
            //textBox1.Text = Codigo;
            //===================================================================================
            //DATOS DE PROGRAMACION
            ListaProgramacion(Codigo, "", "", "", "", "2");
            TxtProgramacion.Text = Dt_Programacion.Rows[0].ItemArray[0].ToString();
            TxtOrigen.Text = Dt_Programacion.Rows[0].ItemArray[1].ToString();
            TxtDestino.Text = Dt_Programacion.Rows[0].ItemArray[2].ToString();
            //TxtFecha.Text = (Dt_Programacion.Rows[0].ItemArray[3].ToString());
            //TxtHora.Text = Dt_Programacion.Rows[0].ItemArray[4].ToString();
            TxtNroBus.Text = Dt_Programacion.Rows[0].ItemArray[5].ToString();
            TxtFechaHora.Text = Dt_Programacion.Rows[0].ItemArray[7].ToString() + " - " + Dt_Programacion.Rows[0].ItemArray[4].ToString();;
            Hora = Dt_Programacion.Rows[0].ItemArray[4].ToString();
            //Title = Convert.ToString(Fecha);
            //==================================================================================
            //==================================================================================
            //DATOS DE TRABAJADORES
            ListaTrabajadores(Codigo, "", "", "", "", "2");
            // MANIFIESTO
            ListaManifiesto(Codigo, "", "", "", "", "5");
            //==================================================================================
            // PARA LISTAR ASIENTOS EMBARCADOS Y ASIENTOS EMBARCADOS
            ListaAsientosActivos(Codigo, "", "", "", "", "3");
            TxtOcupado.Text = Dt_AsientosActivos.Rows[0].ItemArray[0].ToString();
            TxtEmbarcado2.Text = Dt_AsientosActivos.Rows[0].ItemArray[1].ToString();
            //==================================================================================

            Estado_Asientos_();
            Estado_Visible_Asientos_();

            //GetClientHostIP();

            //======================== LECTOR ===================================
            Controls.Add(ZkFprint1);
            InitialAxZkfp1();
            //==================================================================================

            if (RbLecDNI.Checked == false)
            {
                 ShowHintInfo("Seleccione medio de embarque");
            }
            if (RbLecHuella.Checked == false)
            {
                ShowHintInfo("Seleccione medio de embarque... ");
            }

        }

        public void ListaProgramacion(String Codigo, String Origen, String Destino, String Fecha, String Hora, String Opcion)
        {
            //_Lista.ShowMessage(__mensaje, __pagina, "", "");
            try
            {
                policia.clsaccesodatos servidor = new policia.clsaccesodatos();
                servidor.cadenaconexion = Ruta;
                if (servidor.abrirconexion() == true)
                {
                    Dt_Programacion = servidor.consultar("[dbo].[BD_Pje_Listar_Programacion]", Codigo, Origen, Destino, Fecha, Hora, Opcion).Tables[0];
                    if (Dt_Programacion.Rows.Count == 0)
                    {
                        servidor.cerrarconexion();
                        MessageBox.Show("No hay Datos para mostrar con el criterio de busqueda ingresado.", "Atencion");
                    }
                    else
                    {
                        //Dt_Pasajeros.Columns.Remove("CONVERTIR");
                        //this.gvReporte.DataSource = Dt_Pasajeros;
                        //this.gvReporte.DataBind();
                        servidor.cerrarconexion();
                        //btnexportar.Visible = true;
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
                MessageBox.Show("Error Programacion", "Atencion");
            }
        }

        public void ListaPasajeros(String Codigo, String Asiento, String Destino, String Fecha, String Hora, String Opcion)
        {
            //_Lista.ShowMessage(__mensaje, __pagina, "", "");
            try
            {
                policia.clsaccesodatos servidor = new policia.clsaccesodatos();
                servidor.cadenaconexion = Ruta;
                if (servidor.abrirconexion() == true)
                {
                    Dt_Pasajeros = servidor.consultar("[dbo].[BD_Pje_Listar_Programacion_Pasajeros]", Codigo, Asiento, Destino, Fecha, Hora, Opcion).Tables[0];
                    if (Dt_Pasajeros.Rows.Count == 0)
                    {
                        servidor.cerrarconexion();
                        MessageBox.Show("No hay Datos para mostrar con el criterio de busqueda ingresado.", "Atencion");
                    }
                    else
                    {
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
                MessageBox.Show("Error Pasajeros", "Atencion");
            }
        }

        public void ListaAsientosActivos(String Codigo, String Asiento, String Destino, String Fecha, String Hora, String Opcion)
        {
            //_Lista.ShowMessage(__mensaje, __pagina, "", "");
            try
            {
                policia.clsaccesodatos servidor = new policia.clsaccesodatos();
                servidor.cadenaconexion = Ruta;
                if (servidor.abrirconexion() == true)
                {
                    Dt_AsientosActivos = servidor.consultar("[dbo].[BD_Pje_Listar_Programacion_Pasajeros]", Codigo, Asiento, Destino, Fecha, Hora, Opcion).Tables[0];
                    if (Dt_AsientosActivos.Rows.Count == 0)
                    {
                        servidor.cerrarconexion();
                        MessageBox.Show("No hay Datos para mostrar con el criterio de busqueda ingresado.", "Atencion");
                    }
                    else
                    {
                        //Dt_Pasajeros.Columns.Remove("CONVERTIR");
                        //this.gvReporte.DataSource = Dt_Pasajeros;
                        //this.gvReporte.DataBind();
                        servidor.cerrarconexion();
                        //btnexportar.Visible = true;
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
                MessageBox.Show("Error Asientos", "Atencion");
            }
        }

        private void ListaTrabajadores(String Codigo, String Origen, String Destino, String Fecha, String Hora, String Opcion)
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
            }
            catch (Exception)
            {
                servidor.cerrarconexion();
                MessageBox.Show("Error Trabajadores", "Atencion");
            }
        }

        private void ListaPrecioRuta(String Origen, String Destino, String Hora, String Cargo, String Opcion)
        {
            //try
            //{
            servidor.cadenaconexion = Ruta;
            if (servidor.abrirconexion() == true)
            {
                Dt_PrecioViaticos = servidor.consultar("[dbo].[Pa_Listar_Viatico_Costo_Ruta]", Origen, Destino, Hora, Cargo, Opcion).Tables[0];
                if (Dt_PrecioViaticos.Rows.Count == 0)
                {
                    //this.mesajeerror.Text = "NO HAY REGISTROS PARA MOSTRAR";
                    //this.mesajeerror.ForeColor = Color.Red;
                    servidor.cerrarconexion();
                }
                else
                {
                    //int NroFilas = Dt_PrecioViaticos.Rows.Count;
                    //if (NroFilas == 0)
                    //{
                    //    //this.DgvLista.DataSource = null;
                    //    //this.mesajeerror.Text = "NO HAY REGISTROS PARA MOSTRAR";
                    //    //this.mesajeerror.ForeColor = Color.Red;
                    //}
                    //else
                    //{
                        TxtPrecioTrip.Text = (Dt_PrecioViaticos.Rows[0].ItemArray[3].ToString());
                        //this.DgvLista.DataSource = Dt_PrecioViaticos;
                        //this.mesajeerror.Text = "Sistema tiene " + NroFilas.ToString() + " Registro(s)";
                        servidor.cerrarconexion();
                    //}
                }
            }
            else
            {
                __mesajeerror = servidor.getMensaje();
                //__mesajeerror = "No hay precio para mostrar";
                MensajeValeViatico(__mesajeerror);
                servidor.cerrarconexion();
                //MessageBox.Show(__mesajeerror, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //MessageBox.Show("Error Viaticos", "Atencion");
            }
            //}
            //catch (Exception)
            //{
            //    servidor.cerrarconexion();
            //    _Lista.ShowMessage(__mensaje, __pagina, "Error inesperado al intentar conectarnos con el servidor.", "CerrarSession.aspx");
            //}
        }

        void Estado_Asientos_()
        {
            try
            {
                //paso para comprobar asientos vendidos 
                Embarque frm = new Embarque();

                //String Codigo = Request.QueryString["Codigo"].Trim();
                ListaPasajeros(Codigo, "", "", "", "", "1");
                for (int i = 0; i <= Dt_Pasajeros.Rows.Count - 1; i++)
                {
                    //Title = Dt_Pasajeros.Rows[i].ItemArray[11].ToString();
                    string asiento = Dt_Pasajeros.Rows[i].ItemArray[6].ToString();
                    string estado = Dt_Pasajeros.Rows[i].ItemArray[9].ToString();
                    //string Color_ = Dt_Pasajeros.Rows[i].ItemArray[11].ToString();

                    foreach (Control c in frm.panel1.Controls)
                    {
                        if (c is Button)
                        {
                            if (c.Name == "Btn" + asiento)
                            //if (c.Name.Replace("ID", "") == asiento)
                            //if (c.Name == c.Name.Replace("Btn", ""))
                            {
                                //Title = c.Name;
                                if (estado == "False")
                                {
                                    if (Btn4.Name == c.Name) { Btn4.BackColor = Color.Yellow; }
                                    if (Btn8.Name == c.Name) { Btn8.BackColor = Color.Yellow; }
                                    if (Btn12.Name == c.Name) { Btn12.BackColor = Color.Yellow; }
                                    if (Btn16.Name == c.Name) { Btn16.BackColor = Color.Yellow; }
                                    if (Btn20.Name == c.Name) { Btn20.BackColor = Color.Yellow; }
                                    if (Btn24.Name == c.Name) { Btn24.BackColor = Color.Yellow; }
                                    if (Btn28.Name == c.Name) { Btn28.BackColor = Color.Yellow; }
                                    if (Btn32.Name == c.Name) { Btn32.BackColor = Color.Yellow; }
                                    if (Btn36.Name == c.Name) { Btn36.BackColor = Color.Yellow; }
                                    if (Btn40.Name == c.Name) { Btn40.BackColor = Color.Yellow; }
                                    if (Btn44.Name == c.Name) { Btn44.BackColor = Color.Yellow; }
                                    if (Btn48.Name == c.Name) { Btn48.BackColor = Color.Yellow; }
                                    if (Btn52.Name == c.Name) { Btn52.BackColor = Color.Yellow; }
                                    if (Btn56.Name == c.Name) { Btn56.BackColor = Color.Yellow; }
                                    if (Btn60.Name == c.Name) { Btn60.BackColor = Color.Yellow; }
                                    if (Btn64.Name == c.Name) { Btn64.BackColor = Color.Yellow; }
                                    if (Btn68.Name == c.Name) { Btn68.BackColor = Color.Yellow; }
                                    if (Btn72.Name == c.Name) { Btn72.BackColor = Color.Yellow; }
                                    if (Btn76.Name == c.Name) { Btn76.BackColor = Color.Yellow; }
                                    if (Btn80.Name == c.Name) { Btn80.BackColor = Color.Yellow; }

                                    if (Btn3.Name == c.Name) { Btn3.BackColor = Color.Yellow; }
                                    if (Btn7.Name == c.Name) { Btn7.BackColor = Color.Yellow; }
                                    if (Btn11.Name == c.Name) { Btn11.BackColor = Color.Yellow; }
                                    if (Btn15.Name == c.Name) { Btn15.BackColor = Color.Yellow; }
                                    if (Btn19.Name == c.Name) { Btn19.BackColor = Color.Yellow; }
                                    if (Btn23.Name == c.Name) { Btn23.BackColor = Color.Yellow; }
                                    if (Btn27.Name == c.Name) { Btn27.BackColor = Color.Yellow; }
                                    if (Btn31.Name == c.Name) { Btn31.BackColor = Color.Yellow; }
                                    if (Btn35.Name == c.Name) { Btn35.BackColor = Color.Yellow; }
                                    if (Btn39.Name == c.Name) { Btn39.BackColor = Color.Yellow; }
                                    if (Btn43.Name == c.Name) { Btn43.BackColor = Color.Yellow; }
                                    if (Btn47.Name == c.Name) { Btn47.BackColor = Color.Yellow; }
                                    if (Btn51.Name == c.Name) { Btn51.BackColor = Color.Yellow; }
                                    if (Btn55.Name == c.Name) { Btn55.BackColor = Color.Yellow; }
                                    if (Btn59.Name == c.Name) { Btn59.BackColor = Color.Yellow; }
                                    if (Btn63.Name == c.Name) { Btn63.BackColor = Color.Yellow; }
                                    if (Btn67.Name == c.Name) { Btn67.BackColor = Color.Yellow; }
                                    if (Btn71.Name == c.Name) { Btn71.BackColor = Color.Yellow; }
                                    if (Btn75.Name == c.Name) { Btn75.BackColor = Color.Yellow; }
                                    if (Btn79.Name == c.Name) { Btn79.BackColor = Color.Yellow; }

                                    if (Btn2.Name == c.Name) { Btn2.BackColor = Color.Yellow; }
                                    if (Btn6.Name == c.Name) { Btn6.BackColor = Color.Yellow; }
                                    if (Btn10.Name == c.Name) { Btn10.BackColor = Color.Yellow; }
                                    if (Btn14.Name == c.Name) { Btn14.BackColor = Color.Yellow; }
                                    if (Btn18.Name == c.Name) { Btn18.BackColor = Color.Yellow; }
                                    if (Btn22.Name == c.Name) { Btn22.BackColor = Color.Yellow; }
                                    if (Btn26.Name == c.Name) { Btn26.BackColor = Color.Yellow; }
                                    if (Btn30.Name == c.Name) { Btn30.BackColor = Color.Yellow; }
                                    if (Btn34.Name == c.Name) { Btn34.BackColor = Color.Yellow; }
                                    if (Btn38.Name == c.Name) { Btn38.BackColor = Color.Yellow; }
                                    if (Btn42.Name == c.Name) { Btn42.BackColor = Color.Yellow; }
                                    if (Btn46.Name == c.Name) { Btn46.BackColor = Color.Yellow; }
                                    if (Btn50.Name == c.Name) { Btn50.BackColor = Color.Yellow; }
                                    if (Btn54.Name == c.Name) { Btn54.BackColor = Color.Yellow; }
                                    if (Btn58.Name == c.Name) { Btn58.BackColor = Color.Yellow; }
                                    if (Btn62.Name == c.Name) { Btn62.BackColor = Color.Yellow; }
                                    if (Btn66.Name == c.Name) { Btn66.BackColor = Color.Yellow; }
                                    if (Btn70.Name == c.Name) { Btn70.BackColor = Color.Yellow; }
                                    if (Btn74.Name == c.Name) { Btn74.BackColor = Color.Yellow; }
                                    if (Btn78.Name == c.Name) { Btn78.BackColor = Color.Yellow; }

                                    if (Btn1.Name == c.Name) { Btn1.BackColor = Color.Yellow; }
                                    if (Btn5.Name == c.Name) { Btn5.BackColor = Color.Yellow; }
                                    if (Btn9.Name == c.Name) { Btn9.BackColor = Color.Yellow; }
                                    if (Btn13.Name == c.Name) { Btn13.BackColor = Color.Yellow; }
                                    if (Btn17.Name == c.Name) { Btn17.BackColor = Color.Yellow; }
                                    if (Btn21.Name == c.Name) { Btn21.BackColor = Color.Yellow; }
                                    if (Btn25.Name == c.Name) { Btn25.BackColor = Color.Yellow; }
                                    if (Btn29.Name == c.Name) { Btn29.BackColor = Color.Yellow; }
                                    if (Btn33.Name == c.Name) { Btn33.BackColor = Color.Yellow; }
                                    if (Btn37.Name == c.Name) { Btn37.BackColor = Color.Yellow; }
                                    if (Btn41.Name == c.Name) { Btn41.BackColor = Color.Yellow; }
                                    if (Btn45.Name == c.Name) { Btn45.BackColor = Color.Yellow; }
                                    if (Btn49.Name == c.Name) { Btn49.BackColor = Color.Yellow; }
                                    if (Btn53.Name == c.Name) { Btn53.BackColor = Color.Yellow; }
                                    if (Btn57.Name == c.Name) { Btn57.BackColor = Color.Yellow; }
                                    if (Btn61.Name == c.Name) { Btn61.BackColor = Color.Yellow; }
                                    if (Btn65.Name == c.Name) { Btn65.BackColor = Color.Yellow; }
                                    if (Btn69.Name == c.Name) { Btn69.BackColor = Color.Yellow; }
                                    if (Btn73.Name == c.Name) { Btn73.BackColor = Color.Yellow; }
                                    if (Btn77.Name == c.Name) { Btn77.BackColor = Color.Yellow; }
                                }
                            }
                        }
                    }

   

                    //================================================================================
                    //================================================================================
                    // CUANDO PASAJERO ESTA EMBARCADO
                    foreach (Control c in frm.panel1.Controls)
                    {
                        if (c is Button)
                        {
                            if (c.Name == "Btn" + asiento)
                            //if (c.Name.Replace("ID", "") == asiento)
                            //if (c.Name == c.Name.Replace("Btn", ""))
                            {
                                //Title = c.Name;
                                if (estado == "True")
                                {
                                    if (Btn4.Name == c.Name) { Btn4.BackColor = Color.Lime; }
                                    if (Btn8.Name == c.Name) { Btn8.BackColor = Color.Lime; }
                                    if (Btn12.Name == c.Name) { Btn12.BackColor = Color.Lime; }
                                    if (Btn16.Name == c.Name) { Btn16.BackColor = Color.Lime; }
                                    if (Btn20.Name == c.Name) { Btn20.BackColor = Color.Lime; }
                                    if (Btn24.Name == c.Name) { Btn24.BackColor = Color.Lime; }
                                    if (Btn28.Name == c.Name) { Btn28.BackColor = Color.Lime; }
                                    if (Btn32.Name == c.Name) { Btn32.BackColor = Color.Lime; }
                                    if (Btn36.Name == c.Name) { Btn36.BackColor = Color.Lime; }
                                    if (Btn40.Name == c.Name) { Btn40.BackColor = Color.Lime; }
                                    if (Btn44.Name == c.Name) { Btn44.BackColor = Color.Lime; }
                                    if (Btn48.Name == c.Name) { Btn48.BackColor = Color.Lime; }
                                    if (Btn52.Name == c.Name) { Btn52.BackColor = Color.Lime; }
                                    if (Btn56.Name == c.Name) { Btn56.BackColor = Color.Lime; }
                                    if (Btn60.Name == c.Name) { Btn60.BackColor = Color.Lime; }
                                    if (Btn64.Name == c.Name) { Btn64.BackColor = Color.Lime; }
                                    if (Btn68.Name == c.Name) { Btn68.BackColor = Color.Lime; }
                                    if (Btn72.Name == c.Name) { Btn72.BackColor = Color.Lime; }
                                    if (Btn76.Name == c.Name) { Btn76.BackColor = Color.Lime; }
                                    if (Btn80.Name == c.Name) { Btn80.BackColor = Color.Lime; }

                                    if (Btn3.Name == c.Name) { Btn3.BackColor = Color.Lime; }
                                    if (Btn7.Name == c.Name) { Btn7.BackColor = Color.Lime; }
                                    if (Btn11.Name == c.Name) { Btn11.BackColor = Color.Lime; }
                                    if (Btn15.Name == c.Name) { Btn15.BackColor = Color.Lime; }
                                    if (Btn19.Name == c.Name) { Btn19.BackColor = Color.Lime; }
                                    if (Btn23.Name == c.Name) { Btn23.BackColor = Color.Lime; }
                                    if (Btn27.Name == c.Name) { Btn27.BackColor = Color.Lime; }
                                    if (Btn31.Name == c.Name) { Btn31.BackColor = Color.Lime; }
                                    if (Btn35.Name == c.Name) { Btn35.BackColor = Color.Lime; }
                                    if (Btn39.Name == c.Name) { Btn39.BackColor = Color.Lime; }
                                    if (Btn43.Name == c.Name) { Btn43.BackColor = Color.Lime; }
                                    if (Btn47.Name == c.Name) { Btn47.BackColor = Color.Lime; }
                                    if (Btn51.Name == c.Name) { Btn51.BackColor = Color.Lime; }
                                    if (Btn55.Name == c.Name) { Btn55.BackColor = Color.Lime; }
                                    if (Btn59.Name == c.Name) { Btn59.BackColor = Color.Lime; }
                                    if (Btn63.Name == c.Name) { Btn63.BackColor = Color.Lime; }
                                    if (Btn67.Name == c.Name) { Btn67.BackColor = Color.Lime; }
                                    if (Btn71.Name == c.Name) { Btn71.BackColor = Color.Lime; }
                                    if (Btn75.Name == c.Name) { Btn75.BackColor = Color.Lime; }
                                    if (Btn79.Name == c.Name) { Btn79.BackColor = Color.Lime; }

                                    if (Btn2.Name == c.Name) { Btn2.BackColor = Color.Lime; }
                                    if (Btn6.Name == c.Name) { Btn6.BackColor = Color.Lime; }
                                    if (Btn10.Name == c.Name) { Btn10.BackColor = Color.Lime; }
                                    if (Btn14.Name == c.Name) { Btn14.BackColor = Color.Lime; }
                                    if (Btn18.Name == c.Name) { Btn18.BackColor = Color.Lime; }
                                    if (Btn22.Name == c.Name) { Btn22.BackColor = Color.Lime; }
                                    if (Btn26.Name == c.Name) { Btn26.BackColor = Color.Lime; }
                                    if (Btn30.Name == c.Name) { Btn30.BackColor = Color.Lime; }
                                    if (Btn34.Name == c.Name) { Btn34.BackColor = Color.Lime; }
                                    if (Btn38.Name == c.Name) { Btn38.BackColor = Color.Lime; }
                                    if (Btn42.Name == c.Name) { Btn42.BackColor = Color.Lime; }
                                    if (Btn46.Name == c.Name) { Btn46.BackColor = Color.Lime; }
                                    if (Btn50.Name == c.Name) { Btn50.BackColor = Color.Lime; }
                                    if (Btn54.Name == c.Name) { Btn54.BackColor = Color.Lime; }
                                    if (Btn58.Name == c.Name) { Btn58.BackColor = Color.Lime; }
                                    if (Btn62.Name == c.Name) { Btn62.BackColor = Color.Lime; }
                                    if (Btn66.Name == c.Name) { Btn66.BackColor = Color.Lime; }
                                    if (Btn70.Name == c.Name) { Btn70.BackColor = Color.Lime; }
                                    if (Btn74.Name == c.Name) { Btn74.BackColor = Color.Lime; }
                                    if (Btn78.Name == c.Name) { Btn78.BackColor = Color.Lime; }

                                    if (Btn1.Name == c.Name) { Btn1.BackColor = Color.Lime; }
                                    if (Btn5.Name == c.Name) { Btn5.BackColor = Color.Lime; }
                                    if (Btn9.Name == c.Name) { Btn9.BackColor = Color.Lime; }
                                    if (Btn13.Name == c.Name) { Btn13.BackColor = Color.Lime; }
                                    if (Btn17.Name == c.Name) { Btn17.BackColor = Color.Lime; }
                                    if (Btn21.Name == c.Name) { Btn21.BackColor = Color.Lime; }
                                    if (Btn25.Name == c.Name) { Btn25.BackColor = Color.Lime; }
                                    if (Btn29.Name == c.Name) { Btn29.BackColor = Color.Lime; }
                                    if (Btn33.Name == c.Name) { Btn33.BackColor = Color.Lime; }
                                    if (Btn37.Name == c.Name) { Btn37.BackColor = Color.Lime; }
                                    if (Btn41.Name == c.Name) { Btn41.BackColor = Color.Lime; }
                                    if (Btn45.Name == c.Name) { Btn45.BackColor = Color.Lime; }
                                    if (Btn49.Name == c.Name) { Btn49.BackColor = Color.Lime; }
                                    if (Btn53.Name == c.Name) { Btn53.BackColor = Color.Lime; }
                                    if (Btn57.Name == c.Name) { Btn57.BackColor = Color.Lime; }
                                    if (Btn61.Name == c.Name) { Btn61.BackColor = Color.Lime; }
                                    if (Btn65.Name == c.Name) { Btn65.BackColor = Color.Lime; }
                                    if (Btn69.Name == c.Name) { Btn69.BackColor = Color.Lime; }
                                    if (Btn73.Name == c.Name) { Btn73.BackColor = Color.Lime; }
                                    if (Btn77.Name == c.Name) { Btn77.BackColor = Color.Lime; }
                                }
                            }
                        }
                    }


                }
            }
            catch (Exception)
            {
                //throw;
            }

        }

        void Estado_Visible_Asientos_()
        {
            try
            {
                //paso para comprobar asientos vendidos 
                Embarque frm = new Embarque();

                //String Codigo = Request.QueryString["Codigo"].Trim();
                ListaAsientosActivos(Codigo, "", "", "", "", "2");
                for (int i = 0; i <= Dt_AsientosActivos.Rows.Count - 1; i++)
                {
                    //Title = Dt_Pasajeros.Rows[i].ItemArray[11].ToString();
                    string asiento = Dt_AsientosActivos.Rows[i].ItemArray[2].ToString();
                    string NombrePasajero = Dt_AsientosActivos.Rows[i].ItemArray[3].ToString();
                    //string Color_ = Dt_Pasajeros.Rows[i].ItemArray[11].ToString();

                    foreach (Control c in frm.panel1.Controls)
                    {
                        if (c is Button)
                        {
                            if (c.Name == "Btn" + asiento)
                            //if (c.Name.Replace("ID", "") == asiento)
                            //if (c.Name == c.Name.Replace("Btn", ""))
                            {
                                //Title = c.Name;
                                if (NombrePasajero == "VACIO")
                                {
                                    if (Btn4.Name == c.Name) { Btn4.Visible = false; }
                                    if (Btn8.Name == c.Name) { Btn8.Visible = false; }
                                    if (Btn12.Name == c.Name) { Btn12.Visible = false; }
                                    if (Btn16.Name == c.Name) { Btn16.Visible = false; }
                                    if (Btn20.Name == c.Name) { Btn20.Visible = false; }
                                    if (Btn24.Name == c.Name) { Btn24.Visible = false; }
                                    if (Btn28.Name == c.Name) { Btn28.Visible = false; }
                                    if (Btn32.Name == c.Name) { Btn32.Visible = false; }
                                    if (Btn36.Name == c.Name) { Btn36.Visible = false; }
                                    if (Btn40.Name == c.Name) { Btn40.Visible = false; }
                                    if (Btn44.Name == c.Name) { Btn44.Visible = false; }
                                    if (Btn48.Name == c.Name) { Btn48.Visible = false; }
                                    if (Btn52.Name == c.Name) { Btn52.Visible = false; }
                                    if (Btn56.Name == c.Name) { Btn56.Visible = false; }
                                    if (Btn60.Name == c.Name) { Btn60.Visible = false; }
                                    if (Btn64.Name == c.Name) { Btn64.Visible = false; }
                                    if (Btn68.Name == c.Name) { Btn68.Visible = false; }
                                    if (Btn72.Name == c.Name) { Btn72.Visible = false; }
                                    if (Btn76.Name == c.Name) { Btn76.Visible = false; }
                                    if (Btn80.Name == c.Name) { Btn80.Visible = false; }

                                    if (Btn3.Name == c.Name) { Btn3.Visible = false; }
                                    if (Btn7.Name == c.Name) { Btn7.Visible = false; }
                                    if (Btn11.Name == c.Name) { Btn11.Visible = false; }
                                    if (Btn15.Name == c.Name) { Btn15.Visible = false; }
                                    if (Btn19.Name == c.Name) { Btn19.Visible = false; }
                                    if (Btn23.Name == c.Name) { Btn23.Visible = false; }
                                    if (Btn27.Name == c.Name) { Btn27.Visible = false; }
                                    if (Btn31.Name == c.Name) { Btn31.Visible = false; }
                                    if (Btn35.Name == c.Name) { Btn35.Visible = false; }
                                    if (Btn39.Name == c.Name) { Btn39.Visible = false; }
                                    if (Btn43.Name == c.Name) { Btn43.Visible = false; }
                                    if (Btn47.Name == c.Name) { Btn47.Visible = false; }
                                    if (Btn51.Name == c.Name) { Btn51.Visible = false; }
                                    if (Btn55.Name == c.Name) { Btn55.Visible = false; }
                                    if (Btn59.Name == c.Name) { Btn59.Visible = false; }
                                    if (Btn63.Name == c.Name) { Btn63.Visible = false; }
                                    if (Btn67.Name == c.Name) { Btn67.Visible = false; }
                                    if (Btn71.Name == c.Name) { Btn71.Visible = false; }
                                    if (Btn75.Name == c.Name) { Btn75.Visible = false; }
                                    if (Btn79.Name == c.Name) { Btn79.Visible = false; }

                                    if (Btn2.Name == c.Name) { Btn2.Visible = false; }
                                    if (Btn6.Name == c.Name) { Btn6.Visible = false; }
                                    if (Btn10.Name == c.Name) { Btn10.Visible = false; }
                                    if (Btn14.Name == c.Name) { Btn14.Visible = false; }
                                    if (Btn18.Name == c.Name) { Btn18.Visible = false; }
                                    if (Btn22.Name == c.Name) { Btn22.Visible = false; }
                                    if (Btn26.Name == c.Name) { Btn26.Visible = false; }
                                    if (Btn30.Name == c.Name) { Btn30.Visible = false; }
                                    if (Btn34.Name == c.Name) { Btn34.Visible = false; }
                                    if (Btn38.Name == c.Name) { Btn38.Visible = false; }
                                    if (Btn42.Name == c.Name) { Btn42.Visible = false; }
                                    if (Btn46.Name == c.Name) { Btn46.Visible = false; }
                                    if (Btn50.Name == c.Name) { Btn50.Visible = false; }
                                    if (Btn54.Name == c.Name) { Btn54.Visible = false; }
                                    if (Btn58.Name == c.Name) { Btn58.Visible = false; }
                                    if (Btn62.Name == c.Name) { Btn62.Visible = false; }
                                    if (Btn66.Name == c.Name) { Btn66.Visible = false; }
                                    if (Btn70.Name == c.Name) { Btn70.Visible = false; }
                                    if (Btn74.Name == c.Name) { Btn74.Visible = false; }
                                    if (Btn78.Name == c.Name) { Btn78.Visible = false; }

                                    if (Btn1.Name == c.Name) { Btn1.Visible = false; }
                                    if (Btn5.Name == c.Name) { Btn5.Visible = false; }
                                    if (Btn9.Name == c.Name) { Btn9.Visible = false; }
                                    if (Btn13.Name == c.Name) { Btn13.Visible = false; }
                                    if (Btn17.Name == c.Name) { Btn17.Visible = false; }
                                    if (Btn21.Name == c.Name) { Btn21.Visible = false; }
                                    if (Btn25.Name == c.Name) { Btn25.Visible = false; }
                                    if (Btn29.Name == c.Name) { Btn29.Visible = false; }
                                    if (Btn33.Name == c.Name) { Btn33.Visible = false; }
                                    if (Btn37.Name == c.Name) { Btn37.Visible = false; }
                                    if (Btn41.Name == c.Name) { Btn41.Visible = false; }
                                    if (Btn45.Name == c.Name) { Btn45.Visible = false; }
                                    if (Btn49.Name == c.Name) { Btn49.Visible = false; }
                                    if (Btn53.Name == c.Name) { Btn53.Visible = false; }
                                    if (Btn57.Name == c.Name) { Btn57.Visible = false; }
                                    if (Btn61.Name == c.Name) { Btn61.Visible = false; }
                                    if (Btn65.Name == c.Name) { Btn65.Visible = false; }
                                    if (Btn69.Name == c.Name) { Btn69.Visible = false; }
                                    if (Btn73.Name == c.Name) { Btn73.Visible = false; }
                                    if (Btn77.Name == c.Name) { Btn77.Visible = false; }
                                }
                            }
                        }
                    }                  
                    ////===============================================================================
                }
            }
            catch (Exception)
            {

                //throw;
            }

        }

        void Ver_Datos_Asiento(string asiento)
        {
            try
            {
                //String Codigo = Request.QueryString["Codigo"].Trim();
                ListaPasajeros(Codigo, asiento, "", "", "", "1");

                //DataTable dt = new DataTable();
                //dt = ListaPasajeros(Codigo, asiento, "", "", "", "1");

                //if (dt.Rows.Count == 0)
                //{
                //    MessageBox.Show("No se encontrarón datos.", "Atencion");
                //}
                //else
                //{
                Id_Pas_Abordo = Convert.ToInt32(Dt_Pasajeros.Rows[0].ItemArray[0]);
                TxtProgramacion.Text = Dt_Pasajeros.Rows[0].ItemArray[1].ToString();
                TxtDni.Text = Dt_Pasajeros.Rows[0].ItemArray[2].ToString();
                TxtNombre.Text = Dt_Pasajeros.Rows[0].ItemArray[3].ToString();
                TxtBoleto.Text = Dt_Pasajeros.Rows[0].ItemArray[4].ToString() + "-" + Dt_Pasajeros.Rows[0].ItemArray[5].ToString(); ;
                TxtAsiento.Text = Dt_Pasajeros.Rows[0].ItemArray[6].ToString();
                TxtPtoVenta.Text = Dt_Pasajeros.Rows[0].ItemArray[7].ToString();
                TxtTxtDestino.Text = Dt_Pasajeros.Rows[0].ItemArray[8].ToString();

                TxtBoleto1.Text = Dt_Pasajeros.Rows[0].ItemArray[4].ToString();
                TxtNumero.Text = Dt_Pasajeros.Rows[0].ItemArray[5].ToString();

                //TxtUsuario.Text = Dt_Pasajeros.Rows[0].ItemArray[9].ToString();
                //hfCodigoUsuario.Value = Dt_Pasajeros.Rows[0].ItemArray[10].ToString();
                //}

                //txtdni.Focus();
            }
            catch (Exception)
            {
                MessageBox.Show("Error Estado Asientos", "Atencion");
                //Limpiar_Datos_Pasajero();
            }

        }

        private void Limpiar_Datos_Pasajero(object sender, EventArgs e)
        {
            TxtDni.Text = "";
            TxtNombre.Text = "";
            TxtBoleto.Text = "";
            TxtAsiento.Text = "";
            //TxtDestino.Text = "";
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar_Datos_Pasajero(sender, e);
        }

       
        #region Estado_Asientos_Botones
        private void Btn1_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("1");
        }

        private void Btn2_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("2");
        }

        private void Btn3_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("3");
        }

        private void Btn4_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("4");
        }

        private void Btn5_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("5");
        }

        private void Btn6_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("6");
        }

        private void Btn7_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("7");
        }

        private void Btn8_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("8");
        }

        private void Btn9_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("9");
        }

        private void Btn10_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("10");
        }

        private void Btn11_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("11");
        }

        private void Btn12_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("12");
        }

        private void Btn13_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("13");
        }

        private void Btn14_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("14");
        }

        private void Btn15_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("15");
        }

        private void Btn16_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("16");
        }

        private void Btn17_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("17");
        }

        private void Btn18_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("18");
        }

        private void Btn19_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("19");
        }

        private void Btn20_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("20");
        }

        private void Btn21_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("21");
        }

        private void Btn22_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("22");
        }

        private void Btn23_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("23");
        }

        private void Btn24_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("24");
        }

        private void Btn25_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("25");
        }

        private void Btn26_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("26");
        }

        private void Btn27_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("27");
        }

        private void Btn28_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("28");
        }

        private void Btn29_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("29");
        }

        private void Btn30_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("30");
        }

        private void Btn31_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("31");
        }

        private void Btn32_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("32");
        }

        private void Btn33_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("33");
        }

        private void Btn34_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("34");
        }

        private void Btn35_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("35");
        }

        private void Btn36_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("36");
        }

        private void Btn37_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("37");
        }

        private void Btn38_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("38");
        }

        private void Btn39_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("39");
        }

        private void Btn40_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("40");
        }

        private void Btn41_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("41");
        }

        private void Btn42_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("42");
        }

        private void Btn43_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("43");
        }

        private void Btn44_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("44");
        }

        private void Btn45_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("45");
        }

        private void Btn46_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("46");
        }

        private void Btn47_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("47");
        }

        private void Btn48_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("48");
        }

        private void Btn49_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("49");
        }

        private void Btn50_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("50");
        }

        private void Btn51_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("51");
        }

        private void Btn52_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("52");
        }

        private void Btn53_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("53");
        }

        private void Btn54_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("54");
        }

        private void Btn55_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("55");
        }

        private void Btn56_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("56");
        }

        private void Btn57_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("57");
        }

        private void Btn58_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("58");
        }

        private void Btn59_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("59");
        }

        private void Btn60_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("60");
        }

        private void Btn61_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("61");
        }

        private void Btn62_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("62");
        }

        private void Btn63_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("63");
        }

        private void Btn64_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("64");
        }

        private void Btn65_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("65");
        }

        private void Btn66_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("66");
        }

        private void Btn67_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("67");
        }

        private void Btn68_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("68");
        }

        private void Btn69_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("69");
        }

        private void Btn70_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("70");
        }

        private void Btn71_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("71");
        }

        private void Btn72_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("72");
        }

        private void Btn73_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("73");
        }

        private void Btn74_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("74");
        }

        private void Btn75_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("75");
        }

        private void Btn76_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("76");
        }

        private void Btn77_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("77");
        }

        private void Btn78_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("78");
        }

        private void Btn79_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("79");
        }

        private void Btn80_Click(object sender, EventArgs e)
        {
            Ver_Datos_Asiento("80");
        }
        #endregion

        private void Matenimiento_PasajerosAbordo(int Id, string CodigoPas, string Dni, string Nombre, string Serie_Boleto, string Num_Boleto
        , string Asiento, string Punt_Venta, string Destino, bool Abordo_o_no, string Desc_Extra, int Id_Usuario
        , string Operacion)
        {
            //String Codigo = Request.QueryString["Codigo"].Trim();

            //try{
            policia.clsaccesodatos servidor = new policia.clsaccesodatos();
            servidor.cadenaconexion = Ruta;
            if (servidor.abrirconexiontrans() == true)
            {
                servidor.ejecutar("[dbo].[BD_Pje_pa_mantenimiento_Pasajeros_Abordo]",
                                    false,
                                    Id,
                                    CodigoPas.Trim(),
                                    Dni.Trim(),
                                    Nombre.Trim(),
                                    Serie_Boleto.Trim(),
                                    Num_Boleto.Trim(),
                                    Asiento.Trim(),
                                    Punt_Venta.Trim(),
                                    Destino.Trim(),
                                    Abordo_o_no,
                                    Desc_Extra.Trim(),
                                    Id_Usuario,
                                    Operacion,
                                    0, "");
                if (servidor.getRespuesta() == 1)
                {
                    servidor.cerrarconexiontrans();
                    __mesajeerror = servidor.getMensaje();
                    MessageBox.Show(__mesajeerror, "Atencion.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    servidor.cancelarconexiontrans();
                    __mesajeerror = servidor.getMensaje();
                    MessageBox.Show(__mesajeerror, "Atencion..", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            else
            {
                servidor.cancelarconexiontrans();
                __mesajeerror = servidor.getMensageError();
                MessageBox.Show(__mesajeerror, "Atencion...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //this.__pagina.Value = "CerrarSession.aspx";
            }
            //}catch (Exception){
            //    this.__mensaje.Value = "Error inesperado al intentar conectarnos con el servidor.";
            //    this.__pagina.Value = "CerrarSession.aspx";
            //}
        }

        private bool Validar_Datos_Asiento()
        {
            bool ok = (TxtProgramacion.Text != "");
            if (ok == false)
            {
                MessageBox.Show("Seleccione Asiento.", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //_Lista.ShowMessage(__mensaje, __pagina, "Seleccione Asiento, por favor", "");
                //this.Nombre.Focus();
                return false;
            }
            ok = ok && (TxtDni.Text != "");
            if (ok == false)
            {
                MessageBox.Show("Seleccione Asiento.", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //_Lista.ShowMessage(__mensaje, __pagina, "Seleccione Asiento, por favor", "");
                //this.Apellido_Pat.Focus();
                return false;
            }
            ok = ok && (TxtNombre.Text != "");
            if (ok == false)
            {
                MessageBox.Show("Seleccione Asiento.", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //this.Apellido_Mat.Focus();
                return false;
            }
            ok = ok && (TxtNombre.Text != "");
            if (ok == false)
            {
                MessageBox.Show("Seleccione Asiento.", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //this.Apellido_Mat.Focus();
                return false;
            }
            return ok;
        }

        private void btnEmbarcar_Click(object sender, EventArgs e)
        {
            //this.__mensaje.Value = "";
            //this.__pagina.Value = "";
            bool Abordo_o_no = true;

            if (this.Validar_Datos_Asiento() == false)
            {
                return;
            }

            Matenimiento_PasajerosAbordo(Id_Pas_Abordo, TxtProgramacion.Text.Trim(), TxtDni.Text.Trim(), TxtNombre.Text.Trim()
                , TxtBoleto1.Text.Trim(), TxtNumero.Text.Trim(), TxtAsiento.Text.Trim(), TxtPtoVenta.Text.Trim()
                , TxtTxtDestino.Text.Trim(), Abordo_o_no
                , "MANUAL", Id_UsuarioGlobal, "M");
            
            //ACTUALIZAR ASIENTOS
            Estado_Asientos_();
            Limpiar_Datos_Pasajero(sender, e);

            //==================================================================================
            // PARA LISTAR ASIENTOS EMBARCADOS Y ASIENTOS EMBARCADOS
            ListaAsientosActivos(Codigo, "", "", "", "", "3");
            TxtOcupado.Text = Dt_AsientosActivos.Rows[0].ItemArray[0].ToString();
            TxtEmbarcado2.Text = Dt_AsientosActivos.Rows[0].ItemArray[1].ToString();
            //==================================================================================
        }

        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            Estado_Asientos_();
            Limpiar_Datos_Pasajero(sender, e);
        }


        private void Matenimiento_TrabajadoresAbordo(int Id, string CodigoTrab, string DocuTrab, string TipoEmbarque
        , string DescripcionTrab, bool Abordo_o_noTrab, string Id_Trabajador, int Id_Usuario, string Operacion)
        {
            //try{
            policia.clsaccesodatos servidor = new policia.clsaccesodatos();
            servidor.cadenaconexion = Ruta;
            if (servidor.abrirconexiontrans() == true)
            {
                servidor.ejecutar("[dbo].[BD_Pje_pa_mantenimiento_Trabajadores_Abordo]",
                                    false,
                                     Id,
                                    CodigoTrab.Trim(),
                                    DocuTrab.Trim(),
                                    TipoEmbarque.Trim(),
                                    DescripcionTrab.Trim(),
                                    Abordo_o_noTrab,
                    // UsuarioTrab.Trim(),
                                    Id_Trabajador.Trim(),
                                    Id_Usuario,
                                    Operacion,
                                    0, "");
                if (servidor.getRespuesta() == 1)
                {
                    servidor.cerrarconexiontrans();
                    __mesajeerror = servidor.getMensaje();
                    Mensaje(__mesajeerror);
                    PictureMensaje.Visible = true;
                    //MessageBox.Show(__mesajeerror, "Atencion.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    servidor.cancelarconexiontrans();
                    __mesajeerror = servidor.getMensaje();
                    Mensaje(__mesajeerror);
                    PictureMensaje.Visible = true;
                    //MessageBox.Show(__mesajeerror, "Atencion...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                servidor.cancelarconexiontrans();
                __mesajeerror = servidor.getMensageError();
                MessageBox.Show(__mesajeerror, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //}catch (Exception){
            //    this.__mensaje.Value = "Error inesperado al intentar conectarnos con el servidor.";
            //    this.__pagina.Value = "CerrarSession.aspx";
            //}
        }

        private void btnEmbarcarTrip_Click(object sender, EventArgs e)
        {
            if (Abordo_o_no_Tripulante==false)
            {
                MessageBox.Show("Embarque de tripulante no ha sido validado"
               , "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            if (Abordo_o_no_Tripulante == true)
            {
                Matenimiento_TrabajadoresAbordo((Id_Trab_Abordo), TxtProgramacion.Text.Trim(), DocuTrab.Trim()
               ,TipoEmbarque.Trim() , DescripcionTrab.Trim(),  Abordo_o_no_Tripulante, Id_Trabajador
             , Id_UsuarioGlobal, "M");
            }        
        }

        public void Listar_TrabajadorBiometrico(String Codigo, String Origen, String Destino, String Fecha, String Hora, String Opcion)
        {
            try
            {
                policia.clsaccesodatos servidor = new policia.clsaccesodatos();
                servidor.cadenaconexion = Ruta;
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
                        Id_Trab_Abordo = Convert.ToInt32 (dt.Rows[0].ItemArray[0].ToString());
                        DocuTrab = dt.Rows[0].ItemArray[1].ToString();
                        TipoEmbarque = dt.Rows[0].ItemArray[2].ToString();
                        DescripcionTrab = dt.Rows[0].ItemArray[3].ToString();
                        Abordo_o_no_Tripulante = Convert.ToBoolean(dt.Rows[0].ItemArray[4].ToString());
                        Id_Trabajador = dt.Rows[0].ItemArray[5].ToString();
                        TemplateBD = dt.Rows[0].ItemArray[7].ToString();
                        Cargo = dt.Rows[0].ItemArray[8].ToString();
                        TxtEmbarcado.Text = dt.Rows[0].ItemArray[9].ToString();

                        textBox2.Text = dt.Rows[0].ItemArray[8].ToString();

                        //txtTemplate.Text = dt.Rows[0].ItemArray[7].ToString();
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
        private void DdlTripulantes_SelectedIndexChanged(object sender, EventArgs e)
        {
            Listar_TrabajadorBiometrico(Codigo, this.DdlTripulantes.Text.Trim(),"","","","3");
            ListaPrecioRuta(TxtOrigen.Text.Trim(), TxtDestino.Text.Trim(),Hora,Cargo, "2");
            Mensaje("");
            PictureMensaje.Visible = false;           
            MensajeValeViatico("");
            PictureMensaje2.Visible = false;
            ShowHintInfo("Presione huella en sensor");
            //textBox2.Text = Convert.ToString(DdlTripulantes.Text);
        }

        #region Sensor

        private void ShowHintInfo(String s)
        {
            prompt.Text = s;
        }   

        private void InitialAxZkfp1()
        {
            try
            {

                //ZkFprint.OnImageReceived += zkFprint_OnImageReceived;
                //ZkFprint.OnFeatureInfo += zkFprint_OnFeatureInfo;
                //ZkFprint.OnEnroll += zkFprint_OnEnroll;
                fpcHandle = ZkFprint1.CreateFPCacheDB();
                ZkFprint1.SensorIndex = 0;

                if (ZkFprint1.InitEngine() == 0)
                {
                    ZkFprint1.FPEngineVersion = "9";
                    ZkFprint1.EnrollCount = 3;
                    deviceSerial.Text += " " + ZkFprint1.SensorSN + " Count: " + ZkFprint1.SensorCount.ToString() 
                    + " Index: " + ZkFprint1.SensorIndex.ToString();
                    ShowHintInfo("Sensor conectado correctamente");
                }

                //ZkFprint.InitEngine() = 1;
                if (ZkFprint1.SensorSN == "")
                {
                     MessageBox.Show("Desconecte y vuelva conectar sensor"
               , "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Device init err, error: " + ex.Message
               , "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //ShowHintInfo("Device init err, error: " + ex.Message);
            }
        }

        private void zkFprint_OnFeatureInfo1(object sender, IZKFPEngXEvents_OnFeatureInfoEvent e)
        {
            String strTemp = string.Empty;
            if (ZkFprint1.EnrollIndex != 1)
            {
                if (ZkFprint1.IsRegister)
                {
                    if (ZkFprint1.EnrollIndex - 1 > 0)
                    {
                        int eindex = ZkFprint1.EnrollIndex - 1;
                        strTemp = "Please scan again ..." + eindex;
                    }
                }
            }
            //MessageBox.Show(strTemp
              //  , "Atencion...strTemp", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            ShowHintInfo(strTemp);
        }

        private void zkFprint_OnCapture1(object sender, IZKFPEngXEvents_OnCaptureEvent e)
        {
            if (RbLecHuella.Checked == false){
                //MessageBox.Show("seleccione medio de embarque"
                //, "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ShowHintInfo("Seleccione medio de embarque");
            }

            if (RbLecHuella.Checked == true){
                string template = ZkFprint1.EncodeTemplate1(e.aTemplate);
                if (ZkFprint1.VerFingerFromStr(ref template, TemplateBD, false, ref Check)){
                   // MessageBox.Show("Huella Verificada"
                   //, "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ShowHintInfo("Huella Verificada");
                    Abordo_o_no_Tripulante = true;
                    TipoEmbarque = "HUELLA";

                    //CAMBIAR ESTADO A EMBARCADOO A TRIPULANTE
                    Matenimiento_TrabajadoresAbordo((Id_Trab_Abordo), TxtProgramacion.Text.Trim(), DocuTrab.Trim()
                  , TipoEmbarque.Trim(), DescripcionTrab.Trim(), Abordo_o_no_Tripulante, Id_Trabajador
                , Id_UsuarioGlobal, "M");
                    TxtEmbarcado.Text = "EMBARCADO";
                    //==================================================================


                    //================ PARA VALE DE VIATICO ================
                    ValidarTripulante = "HUELLA";
                    ValidarUsuario = "HUELLA";
                    ValidoTripulante = "SI";
                    ValidoUsuario = "SI";
                    TipoViatico = "NORMAL";
                    //REGISTRAR VIATICO POR PRIMERA VEZ
                    Matenimiento_Vale_Viatico((Id_Vale_Viatico), Manifiesto.Trim(), Convert.ToDouble(TxtPrecioTrip.Text), "SI", "NO"
                        //, false, ValidarTripulante, ValidarUsuario, ValidoTripulante, ValidoUsuario, TipoViatico
                    , false, ValidarTripulante, ValidarUsuario, ValidoTripulante, ValidoUsuario, TipoViatico
                    , "", "", "01/01/2000", Id_Trab_Abordo
                    , Id_UsuarioGlobal, "N");
                    //==================================================================

                    DdlTripulantes.Focus();
                    TemplateBD = "";
                    // ShowHintInfo("Verified");
                    //PictureMensaje.Visible = true;

                }else
                    ShowHintInfo("Huella no Verificada. Presione huella en sensor");
                    //Mensaje("Huella no Verificada");
                    //PictureMensaje.Visible = false;
                //MessageBox.Show("Presione nuevamente huella en el sensor..ONCAPTURE"
                //, "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }
      
        private void RbLecHuella_CheckedChanged(object sender, EventArgs e)
        {
            if (RbLecHuella.Checked == true){
                if (ZkFprint1.IsRegister){
                    ZkFprint1.CancelEnroll();
                }
                ZkFprint1.OnCapture += zkFprint_OnCapture1;
                ZkFprint1.BeginCapture();
                //MessageBox.Show("Presione huella en el sensor"
                //, "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                ShowHintInfo("Presione huella en el sensor");
            }

            //if (RbLecHuella.Checked == false)
            //{
            //    ShowHintInfo("Seleccione medio de embarque...");
            //}

        }
        #endregion

        // ================================ LISTAR MANIFIESTO ==============================
        private void ListaManifiesto(String Codigo, String Origen, String Destino, String Fecha, String Hora, String Opcion)
        {
            try{
                servidor.cadenaconexion = Ruta;
                if (servidor.abrirconexion() == true){
                    System.Data.DataTable dt = servidor.consultar("[dbo].[BD_Pje_Listar_Programacion_Trabajadores]", Codigo, Origen, Destino, Fecha, Hora, Opcion).Tables[0];
                    if (dt.Rows.Count == 0){
                        servidor.cerrarconexion();
                        //SE COMENTO EL DIA 15/05/2018
                        //_Lista.ShowMessage(__mensaje, __pagina, "Error, al intentar recuperar datos.", "CerrarSession.aspx");
                    }else{
                        Manifiesto = dt.Rows[0].ItemArray[0].ToString();
                        //textBox2.Text = dt.Rows[0].ItemArray[0].ToString();
                        servidor.cerrarconexion();
                    }
                }else{
                    servidor.cerrarconexion();
                    __mesajeerror = servidor.getMensageError();
                }
            }catch (Exception){
                servidor.cerrarconexion();
                MessageBox.Show("Error Manifiesto", "Atencion");
            }
        }

        //===================== VIATICOS =========================
        private void Matenimiento_Vale_Viatico(int Id_Vale_Viatico, string Manifiesto, double Cantidad_Soles, string Aprobado
       , string Anulado, bool Cerrar_Fecha, string ValidarTripulante, string ValidarUsuario, string ValidoTripulante
       , string ValidoUsuario, string TipoViatico, string Observacion, string HostModificacion, string FechaModificacion
       , int Id_Trab_Abordo, int Id_Usuario, string Operacion)
        {
            //try{
            policia.clsaccesodatos servidor = new policia.clsaccesodatos();
            servidor.cadenaconexion = Ruta;
            if (servidor.abrirconexiontrans() == true){
                servidor.ejecutar("[dbo].[_pa_mantenimiento_Vale_Viatico]",
                                    false,
                                     Id_Vale_Viatico,
                                    Manifiesto.Trim(),
                                    Cantidad_Soles,
                                    Aprobado.Trim(),
                                    Anulado.Trim(),
                                    Cerrar_Fecha,
                                    ValidarTripulante.Trim(),
                                    ValidarUsuario.Trim(),
                                    ValidoTripulante.Trim(),
                                    ValidoUsuario.Trim(),
                                    TipoViatico.Trim(),
                                    Observacion.Trim(),
                                    HostModificacion.Trim(),
                                    FechaModificacion,
                                    Id_Trab_Abordo,
                                    Id_Usuario,
                                    Operacion,
                                    0, "");
                if (servidor.getRespuesta() == 1)
                {
                    servidor.cerrarconexiontrans();
                    __mesajeerror = servidor.getMensaje();
                    MensajeValeViatico(__mesajeerror);
                    PictureMensaje2.Visible = true;
                    //MessageBox.Show(__mesajeerror, "Atencion.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    servidor.cancelarconexiontrans();
                    __mesajeerror = servidor.getMensaje();
                    MensajeValeViatico(__mesajeerror);
                    PictureMensaje2.Visible = true;
                    //MessageBox.Show(__mesajeerror, "Atencion...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                servidor.cancelarconexiontrans();
                __mesajeerror = servidor.getMensageError();
            }
            //}catch (Exception){
            //    this.__mensaje.Value = "Error inesperado al intentar conectarnos con el servidor.";
            //    this.__pagina.Value = "CerrarSession.aspx";
            //}
        }

        private void BtnViatico_Click(object sender, EventArgs e)
        {
            DateTime FechaActual = DateTime.Today;
            bool ok;
            ok = this.TxtPrecioTrip.Text.Trim() != "";
            if (ok == false)
            {
                MessageBox.Show("Ingrese Monto de Viatico"
              , "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TxtPrecioTrip.Focus();
                return;
            }

            Matenimiento_Vale_Viatico((Id_Vale_Viatico), Manifiesto.Trim(), Convert.ToDouble(TxtPrecioTrip.Text), "SI", "NO"
                //, false, ValidarTripulante, ValidarUsuario, ValidoTripulante, ValidoUsuario, TipoViatico
            , false, ValidarTripulante, ValidarUsuario, ValidoTripulante, ValidoUsuario, TipoViatico
            , "", "", "01/01/2000", Id_Trab_Abordo
           , 2, "N");

            //Vale_Viatico f = new Vale_Viatico();
            //f.CodigoProgramacion = TxtProgramacion.Text;
            //f.ShowDialog();
        }

        private void Embarque_FormClosing(object sender, FormClosingEventArgs e)
        {
            ZkFprint1.FreeFPCacheDB(fpcHandle);
            //Application.Exit();
            ShowHintInfo("Sensor se ha desconectado");
            //MessageBox.Show("Sensor se ha desconectado Embarque_FormClosing", "Atencion...", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (ZkFprint1.InitEngine() == 1)
            {
                MessageBox.Show("Sensor se ha desconectado", "Atencion...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //ZkFprint.FPEngineVersion = "9";
                //ZkFprint.EnrollCount = 3;
                //deviceSerial.Text += " " + ZkFprint.SensorSN + " Count: " + ZkFprint.SensorCount.ToString()
                //+ " Index: " + ZkFprint.SensorIndex.ToString();
                //ShowHintInfo("Sensor conectado correctamente");
            }
        }

        private void Embarque_FormClosed(object sender, FormClosedEventArgs e)
        {
            ////ZkFprint.InitEngine() = 1;
            //ZkFprint.FreeFPCacheDB(fpcHandle);
            //MessageBox.Show("Sensor se ha desconectado closed", "Atencion...", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //if (ZkFprint.InitEngine() == 1)
            //{
            //    MessageBox.Show("Sensor se ha desconectado", "Atencion...", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    //ZkFprint.FPEngineVersion = "9";
            //    //ZkFprint.EnrollCount = 3;
            //    //deviceSerial.Text += " " + ZkFprint.SensorSN + " Count: " + ZkFprint.SensorCount.ToString()
            //    //+ " Index: " + ZkFprint.SensorIndex.ToString();
            //    //ShowHintInfo("Sensor conectado correctamente");
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
     
    }
}
