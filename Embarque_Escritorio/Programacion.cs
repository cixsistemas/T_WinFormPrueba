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
    public partial class Programacion : Form
    {
        Utilitarios Utilitarios_ = new Utilitarios();
        private String Ruta = System.Configuration.ConfigurationManager.AppSettings.Get("CadenaConeccion");
        policia.clsaccesodatos servidor = new policia.clsaccesodatos();
        public int Id_UsuarioGlobal;
        public int AsientosLibres;

        string __mesajeerror = "";
        //private int cuenta = 100;
        DataTable Dt_Programacion;
        DataTable Dt_Pasajeros;
        DataTable Dt_Trabajadores;
        int Id_Trab_Abordo = -1;
        int Id_Pas_Abordo = -1;
        string Operacion;
        string Codigo;

        public Programacion()
        {
            InitializeComponent();
        }
        public void ListarProgramacion(String Codigo, String Origen, String Destino, Object Fecha, String Hora, String Opcion)
        {        
            //try
            //{
                this.mesajeerror.ForeColor = Color.Blue;
                policia.clsaccesodatos servidor = new policia.clsaccesodatos();
                servidor.cadenaconexion = Ruta;
                if (servidor.abrirconexion() == true)
                {
                    Dt_Programacion = servidor.consultar("[dbo].[BD_Pje_Listar_Programacion]", Codigo, Origen, Destino, Fecha, Hora, Opcion).Tables[0];
                    if (Dt_Programacion == null)
                    {
                        __mesajeerror = servidor.getMensageError();
                        servidor.cerrarconexion();
                        MessageBox.Show(__mesajeerror, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        int NroFilas = Dt_Programacion.Rows.Count;
                        if (NroFilas == 0)
                        {
                            servidor.cerrarconexion();
                            this.dgvlista.DataSource = Dt_Programacion;
                            this.mesajeerror.Text = "NO HAY DATOS PARA MOSTRAR";
                            this.mesajeerror.ForeColor = Color.Red;
                        }
                        else
                        {
                            this.dgvlista.DataSource = Dt_Programacion;
                            servidor.cerrarconexion();
                            this.mesajeerror.Text = "Sistema tiene " + NroFilas.ToString() + " Registro(s)";
                        }
                        this.dgvlista.Columns["COLOR"].Visible = false;
                        this.dgvlista.Columns["ASIENTOS"].Visible = false;
                        this.dgvlista.Columns["OPERACION"].Visible = false;
                        this.dgvlista.Columns["VENDIDO"].Visible = false;

                        
                        ////this.dgvlista.Columns.for("Fecha Pago").DefaultCellStyle.Format = "dd/MM/yyyy";
                    }
                }
                else
                {
                    __mesajeerror = servidor.getMensageError();
                    servidor.cerrarconexion();
                    MessageBox.Show(__mesajeerror, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            //}
            //catch (Exception)
            //{
            //    MessageBox.Show("Ingrese Fecha a Buscar", "Balanza de Camiones", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    //dtpfin.Focus();
            //}


                try{
                    //dgvlista.ClearSelection();
                    //int nRowIndex = dgvlista.Rows.Count - 1;
                    //dgvlista.Rows[nRowIndex].Selected = true;
                    //dgvlista.Rows[nRowIndex].Cells[2].Selected = true;

                    //dgvlista.CurrentCell = dgvlista[2, 1];
                    //dgvlista.Rows[3].Selected = true;

                    this.dgvlista.ClearSelection();
                    this.dgvlista.CurrentCell = this.dgvlista.Rows[this.dgvlista.RowCount - 1].Cells[2];
                    //dgvlista.Rows[3].Selected = true;

                }catch (Exception){
                }
        }

        private void Programacion_Load(object sender, EventArgs e)
        {
            ListarProgramacion("", "1", this.TxtDestino.Text.Trim(), "10/03/2021",
                "" + "", "1");
            //textBox1.Text = string.Format("{tt}", DateTime.Now);
            //TxtHora.Text=DateTime.Now.Hour.ToString();
        //    ListarProgramacion("", TxtOrigen.Text.Trim(), this.TxtDestino.Text.Trim(), DtpFecha.Value.ToString(), 
        //    TxtHora.Text.Trim() + CboAmPm.Text.Trim(), "1");
            alternarColoFilasDatagridview(dgvlista);
            ////DgvLista.Rows[1].Selected = true;
            //dgvlista.CurrentCell = dgvlista.Rows[dgvlista.RowCount-1].Cells[5];

        }

        public void alternarColoFilasDatagridview(DataGridView dgv)
        {
            {
                var withBlock = dgv;
                withBlock.RowsDefaultCellStyle.BackColor = Color.Azure;
                withBlock.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
            }
        }

        private void Programacion_KeyDown(object sender, KeyEventArgs e)
        {
            // cerrar formulario con tecla ESC
            if ((e.KeyCode == Keys.Escape))
                this.Close();
        }

        private void TxtOrigen_KeyDown(object sender, KeyEventArgs e)
        {
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
            //}
        }

        private void TxtDestino_KeyDown(object sender, KeyEventArgs e)
        {
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

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            //VALIDAR FECHA
            if (this.TxtOrigen.Text.Trim() == ""){
                //_Lista.ShowMessage(__mensaje, __pagina, "Complete datos formulario.\n\nIngrese fecha a buscar por favor.", ""); this.fi.Focus();
                MessageBox.Show("Ingrese Origen.", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                TxtOrigen.Focus();
                return;
            }

            if (this.TxtHora.MaskFull == false){
                //_Lista.ShowMessage(__mensaje, __pagina, "Complete datos formulario.\n\nIngrese fecha a buscar por favor.", ""); this.fi.Focus();
                MessageBox.Show("Ingrese Hora.", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                TxtHora.Focus();
                return;
            }

            if (rbtAM.Checked == true){
                ListarProgramacion("", TxtOrigen.Text.Trim(), this.TxtDestino.Text.Trim(), DtpFecha.Value.ToShortDateString(),
          TxtHora.Text.Trim() + "AM", "1");
            }
            if (rbtPM.Checked == true){
                ListarProgramacion("", TxtOrigen.Text.Trim(), this.TxtDestino.Text.Trim(), DtpFecha.Value.ToShortDateString(),
          TxtHora.Text.Trim() + "PM", "1");
            }         
        }

        private void dgvlista_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if ((string)this.dgvlista.Rows[e.RowIndex].Cells["COLOR"].Value == "VERDE")
                {
                    if (dgvlista.Columns[e.ColumnIndex].Name == "CODIGO")  //Si es la columna a evaluar
                    {
                        ////aplicar a todas las celdas de esa fila
                        ////el estilo que necesitemos          
                        e.CellStyle.BackColor = Color.ForestGreen;
                        e.CellStyle.ForeColor = Color.White;
                    }
                }

                if ((string)this.dgvlista.Rows[e.RowIndex].Cells["COLOR"].Value == "ROJO")
                {
                    if (dgvlista.Columns[e.ColumnIndex].Name == "CODIGO")  //Si es la columna a evaluar
                    {
                        ////aplicar a todas las celdas de esa fila
                        ////el estilo que necesitemos          
                        e.CellStyle.BackColor = Color.Red;
                        e.CellStyle.ForeColor = Color.White;
                    }
                }
            }
            catch (Exception)
            {
                
                //throw;
            }
           

            //DataGridView dgv = sender as DataGridView;
            //if (dgv.Columns[e.ColumnIndex].Name == "COLOR")  //Si es la columna a evaluar
            //{
            //    if (e.Value.ToString().Contains("VERDE"))   //Si el valor de la celda contiene la palabra hora
            //    {
            //        e.CellStyle.ForeColor = Color.Red;
            //    }
            //}
        }

        public void ListaProgramacion2(String Codigo, String Origen, String Destino, String Fecha, String Hora, String Opcion)
        {
           // _Lista.ShowMessage(__mensaje, __pagina, "", "");
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
                        //_Lista.ShowMessage(__mensaje, __pagina, "No hay Datos para mostrar con el criterio de busqueda ingresado.", "");
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
                    __mesajeerror= servidor.getMensageError();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error Programacion 2", "Atencion");
            }
        }

        public void ListaPasajeros(String Codigo, String Asiento, String Destino, String Fecha, String Hora, String Opcion)
        {
           
            try
            {
                policia.clsaccesodatos servidor = new policia.clsaccesodatos();
                servidor.cadenaconexion = Ruta;
                if (servidor.abrirconexion() == true)
                {
                    Dt_Pasajeros = servidor.consultar("[dbo].[BD_Pje_Listar_Programacion_Pasajeros_2]", Codigo, Asiento, Destino, Fecha, Hora, Opcion).Tables[0];
                    if (Dt_Pasajeros.Rows.Count == 0)
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
                MessageBox.Show("Error Pasajeros", "Atencion");
            }
        }

        public void ListaTrabajadores(String Codigo, String Origen, String Destino, String Fecha, String Hora, String Opcion)
        {
            //_Lista.ShowMessage(__mensaje, __pagina, "", "");
            try
            {
                policia.clsaccesodatos servidor = new policia.clsaccesodatos();
                servidor.cadenaconexion = Ruta;
                if (servidor.abrirconexion() == true)
                {
                    Dt_Trabajadores = servidor.consultar("[dbo].[BD_Pje_Listar_Programacion_Trabajadores]", Codigo, Origen, Destino, Fecha, Hora, Opcion).Tables[0];
                    if (Dt_Trabajadores.Rows.Count == 0)
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
                MessageBox.Show("Error Trabajadores", "Atencion");
            }
        }
    
        void Traer_form_Embarque()
        {
            //try{
                if (Operacion == "N")
                {
                    MessageBox.Show("Embarque Registrado", "Atencion.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //MessageBox.Show("Embarque Registrado", "SISTEMA");
                    Embarque dr = new Embarque();
                    //CAPTURAR CODIGO DE FORMULARIO PROGRAMACION Y MANDARLO A FORMULARIO
                    // DE EMBARQUE
                    dr.Id_UsuarioGlobal = Convert.ToInt32(Id_UsuarioGlobal);
                    dr.TxtLibres.Text = Convert.ToString(AsientosLibres);
                    dr.Codigo = Codigo;
                    dr.ShowDialog();
                }
                else
                {
                    Embarque dr = new Embarque();
                    //CAPTURAR CODIGO DE FORMULARIO PROGRAMACION Y MANDARLO A FORMULARIO
                    // DE EMBARQUE
                    dr.Codigo = Codigo;
                    dr.Id_UsuarioGlobal = Convert.ToInt32(Id_UsuarioGlobal);
                    dr.TxtLibres.Text = Convert.ToString(AsientosLibres);
                    dr.ShowDialog();
                }
            //}catch (Exception ex){
            //    MessageBox.Show("TRAER FORM" + ex.Message, "ATENCION");
            //}
        }

        private void Matenimiento_Programacion(string Codigo, string Origen, string Destino, DateTime Fecha, string Hora
       , string placa_bus, string Servicio, DateTime FechaActual, String Cerrada, int Id_Usuario, string Operacion)
        {
            //try
            //{
                policia.clsaccesodatos servidor = new policia.clsaccesodatos();
                servidor.cadenaconexion = Ruta;
                if (servidor.abrirconexiontrans() == true)
                {
                    servidor.ejecutar("[dbo].[BD_Pje_pa_mantenimiento_Programacion]",
                                        false,
                                        Codigo.Trim(),
                                        Origen.Trim(),
                                        Destino.Trim(),
                                        Fecha,
                                        Hora.Trim(),
                                        placa_bus.Trim(),
                                        Servicio.Trim(),
                                        FechaActual,
                                        Cerrada.Trim(),
                                        Id_Usuario,
                                        Operacion,
                                        0, "");
                    if (servidor.getRespuesta() == 1)
                    {
                        servidor.cerrarconexiontrans();
                        __mesajeerror = servidor.getMensaje();
                        //MessageBox.Show(__mesajeerror, "Atencion.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        servidor.cancelarconexiontrans();
                        __mesajeerror = servidor.getMensaje();
                        //MessageBox.Show(__mesajeerror, "Atencion.", MessageBoxButtons.OK, MessageBoxIcon.Information);;
                    }
                }
                else
                {
                    servidor.cancelarconexiontrans();
                    __mesajeerror = servidor.getMensageError();
                    MessageBox.Show(__mesajeerror, "Atencion.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //this.__pagina.Value = "CerrarSession.aspx";
                }
            //}
            //catch (Exception)
            //{
            //    //this.__mensaje.Value = "Error inesperado al intentar conectarnos con el servidor.";
            //    //this.__pagina.Value = "CerrarSession.aspx";
            //}
        }

        private void Matenimiento_PasajerosAbordo(int Id, string Codigo, string Dni, string Nombre, string Serie_Boleto, string Num_Boleto
        , string Asiento, string Punt_Venta, string Destino, bool Abordo_o_no, string Desc_Extra
        , int Id_Usuario, string Operacion)
        {
            //try{
            policia.clsaccesodatos servidor = new policia.clsaccesodatos();
            servidor.cadenaconexion = Ruta;
            if (servidor.abrirconexiontrans() == true)
            {
                servidor.ejecutar("[dbo].[BD_Pje_pa_mantenimiento_Pasajeros_Abordo]",
                                    false,
                                    Id,
                                    Codigo.Trim(),
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
                    //this.__pagina.Value = "empacadora.aspx";
                }
                else
                {
                    servidor.cancelarconexiontrans();
                    __mesajeerror = servidor.getMensaje();
                    //this.__pagina.Value = "empacadora.aspx";
                }
            }
            else
            {
                servidor.cancelarconexiontrans();
                __mesajeerror = servidor.getMensageError();
                //this.__pagina.Value = "CerrarSession.aspx";
            }
            //}catch (Exception){
            //    this.__mensaje.Value = "Error inesperado al intentar conectarnos con el servidor.";
            //    this.__pagina.Value = "CerrarSession.aspx";
            //}
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
                                    Id_Trabajador.Trim(),
                                    Id_Usuario,
                                    Operacion,
                                    0, "");
                if (servidor.getRespuesta() == 1)
                {
                    servidor.cerrarconexiontrans();
                    __mesajeerror = servidor.getMensaje();
                }
                else
                {
                    servidor.cancelarconexiontrans();
                    __mesajeerror = servidor.getMensaje();
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

        private void dgvlista_DoubleClick(object sender, EventArgs e)
        {
            Operacion = dgvlista.CurrentRow.Cells["OPERACION"].Value.ToString();
            Codigo = dgvlista.CurrentRow.Cells["CODIGO"].Value.ToString();
            ListaProgramacion2(Codigo, "", "", "", "", "1");
            string TxtProgramacion = Dt_Programacion.Rows[0].ItemArray[0].ToString();
            string TxtOrigen = Dt_Programacion.Rows[0].ItemArray[1].ToString();
            string TxtDestino = Dt_Programacion.Rows[0].ItemArray[2].ToString();
            string TxtFecha = (Dt_Programacion.Rows[0].ItemArray[3].ToString());
            string TxtHora = Dt_Programacion.Rows[0].ItemArray[4].ToString();
            string TxtServicio = Dt_Programacion.Rows[0].ItemArray[5].ToString();
            string TxtNroBus = Dt_Programacion.Rows[0].ItemArray[6].ToString();
             //Title = Dt_Programacion.Rows[0].ItemArray[0].ToString();
            //instancia de form2 pasandole el valor del textbox
            ////using (Programacion frm1 = new Programacion(Operacion))
            ////{
            ////    frm1.ShowDialog();
            ////}


            if (RbPasajeros.Checked == true)
            {
                if ((Operacion.Trim()) == "N")
                {
                    if (MessageBox.Show("Esta seguro que desea registrar embarque " + Codigo, "REGISTRO", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                    {

                        //PARA TABLA codigos_prog
                        Matenimiento_Programacion(
                       TxtProgramacion.Trim(), TxtOrigen.Trim(), TxtDestino.Trim()
                       , Convert.ToDateTime(TxtFecha), TxtHora.Trim(), TxtNroBus.Trim(), TxtServicio.Trim(), Convert.ToDateTime(TxtFecha)
                       , "NO", Id_UsuarioGlobal,
                        "N");

                        //PARA TABLA Pasajeros_Abordo
                        ListaPasajeros((TxtProgramacion.Trim()), "", "", "", "", "1");
                        string CodigoPas, Dni, Nombre, Serie_Boleto, Num_Boleto, Asiento, Punt_Venta, Destino, Desc_Extra, Usuario;
                        bool Abordo_o_no;
                        for (int j = 0; j <= Dt_Pasajeros.Rows.Count - 1; j++)
                        {
                            CodigoPas = Dt_Pasajeros.Rows[j].ItemArray[0].ToString();
                            Dni = Dt_Pasajeros.Rows[j].ItemArray[1].ToString();
                            Nombre = Dt_Pasajeros.Rows[j].ItemArray[2].ToString();
                            Serie_Boleto = Dt_Pasajeros.Rows[j].ItemArray[3].ToString();
                            Num_Boleto = Dt_Pasajeros.Rows[j].ItemArray[4].ToString();
                            Asiento = Dt_Pasajeros.Rows[j].ItemArray[5].ToString();
                            Punt_Venta = Dt_Pasajeros.Rows[j].ItemArray[6].ToString();
                            Destino = Dt_Pasajeros.Rows[j].ItemArray[7].ToString();
                            Abordo_o_no = Convert.ToBoolean(Dt_Pasajeros.Rows[j].ItemArray[8]);
                            Desc_Extra = Dt_Pasajeros.Rows[j].ItemArray[9].ToString();
                            Usuario = Dt_Pasajeros.Rows[j].ItemArray[10].ToString();

                            Matenimiento_PasajerosAbordo(Id_Pas_Abordo, CodigoPas.Trim(), Dni.Trim(), Nombre.Trim(), Serie_Boleto.Trim()
                          , Num_Boleto.Trim(), Asiento.Trim(), Punt_Venta.Trim(), Destino.Trim(), Abordo_o_no
                          , Desc_Extra.Trim(), Id_UsuarioGlobal, "N");
                        }

                        //PARA TABLA Trabajadores_Abordo --> BD_Pje_Listar_Programacion_Pasajeros_2
                        ListaTrabajadores((TxtProgramacion.Trim()), "", "", "", "", "1");
                        string CodigoTrab, DocuTrab, DescripcionTrab, Id_Trabajador;
                        bool Abordo_o_noTrab;
                        for (int j = 0; j <= Dt_Trabajadores.Rows.Count - 1; j++)
                        {
                            CodigoTrab = Dt_Trabajadores.Rows[j].ItemArray[0].ToString();
                            //DniTrab = Dt_Trabajadores.Rows[j].ItemArray[1].ToString();
                            DocuTrab = Dt_Trabajadores.Rows[j].ItemArray[1].ToString();
                            //NombreTrab = Dt_Trabajadores.Rows[j].ItemArray[3].ToString();
                            DescripcionTrab = Dt_Trabajadores.Rows[j].ItemArray[2].ToString();
                            Abordo_o_noTrab = Convert.ToBoolean(Dt_Trabajadores.Rows[j].ItemArray[3]);
                            //UsuarioTrab = Dt_Trabajadores.Rows[j].ItemArray[6].ToString();
                            Id_Trabajador = Dt_Trabajadores.Rows[j].ItemArray[4].ToString();

                            Matenimiento_TrabajadoresAbordo((Id_Trab_Abordo)
                            , CodigoTrab.Trim(), DocuTrab.Trim(), "NINGUNO"
                            , DescripcionTrab.Trim(), Abordo_o_noTrab, Id_Trabajador.Trim()
                            , Id_UsuarioGlobal, "N");
                        }
                        Traer_form_Embarque();
                       
                    }
                    //ListarProgramacion("", this.TxtDestino.Text.Trim(), this.TxtDestino.Text.Trim(), "",
                    //   "", "1");
                }
                if ((Operacion.Trim()) == "M")
                {
                    if (MessageBox.Show("Esta seguro que desea modificar embarque " + Codigo, "MODIFICAR", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                    {
                        Traer_form_Embarque();
                    }
                }
            }
            

                            
            //this.BackgroundWorker1.RunWorkerAsync();
            //TRAER_NEW_EMBARQUE();        
            //dgvlista.CellClick +=new DataGridViewCellEventHandler(dgvlista_CellClick);
        }

        private void dgvlista_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TxtCodigo.Text = dgvlista.CurrentRow.Cells["CODIGO"].Value.ToString();
            AsientosLibres = Convert.ToInt32(dgvlista.CurrentRow.Cells["ASIENTOS"].Value) - Convert.ToInt32(dgvlista.CurrentRow.Cells["VENDIDO"].Value);
        }

        private void dgvlista_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           // textBox1.Text = dgvlista.CurrentRow.Cells["CODIGO"].Value.ToString();
        }

    }
}
