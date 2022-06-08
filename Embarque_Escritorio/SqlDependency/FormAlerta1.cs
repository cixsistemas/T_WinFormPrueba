using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Embarque_Escritorio.SqlDependency
{
    public partial class FormAlerta1 : Form
    {
        private static string ConexionRuta = ConfigurationManager.AppSettings.Get("CadenaConeccionSoporte");
        SqlConnection conexion = null;
        public delegate void NewHome();
        public event NewHome OnNewHome;

        public delegate void NewMessage();
        public event NewMessage OnNewMessage;

        DataTable Dt_PrecioViaticos = null;
        private void ListaPrecioRuta()
        {
            this.mesajeerror.ForeColor = Color.Blue;
            policia.clsaccesodatos servidor = new policia.clsaccesodatos();
            servidor.cadenaconexion = ConexionRuta;
            if (servidor.abrirconexiontrans() == true)
            {
                if (servidor.consultar("[dbo].[_pa_lista_Colaboradores]").Tables.Count > 0)
                    Dt_PrecioViaticos = servidor.consultar("[dbo].[_pa_lista_Colaboradores]").Tables[0];
                if (Dt_PrecioViaticos == null)
                {
                    servidor.cerrarconexion();
                    this.mesajeerror.Text = "NO HAY REGISTROS PARA MOSTRAR";
                    this.mesajeerror.ForeColor = Color.Red;
                }
                else
                {
                    this.dataGridView1.DataSource = Dt_PrecioViaticos;
                    int NroFilas = Dt_PrecioViaticos.Rows.Count;
                    if (NroFilas == 0)
                    {
                        this.dataGridView1.DataSource = null;
                        this.mesajeerror.Text = "NO HAY REGISTROS PARA MOSTRAR";
                        this.mesajeerror.ForeColor = Color.Red;
                    }
                    else
                    {
                        //this.DgvLista.Columns["ID"].Visible = false;
                        //this.DgvLista.Columns["ID_CARGO"].Visible = false;
                        //this.DgvLista.Columns["Id_Suc_Origen"].Visible = false;
                        //this.DgvLista.Columns["Id_Suc_Des"].Visible = false;

                        this.mesajeerror.Text = "Sistema tiene " + NroFilas.ToString() + " Registro(s)";
                    }
                    servidor.cerrarconexion();
                }
            }
            else
            {
                //__mesajeerror = servidor.getMensaje();
                //servidor.cerrarconexion();
                //MessageBox.Show(__mesajeerror, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        public FormAlerta1()
        {
            InitializeComponent();

            // Create a dependency connection.
            //System.Data.SqlClient.SqlDependency.Start(ConexionRuta);

            try
            {
                SqlClientPermission ss = new SqlClientPermission(System.Security.Permissions.PermissionState.Unrestricted);
                ss.Demand();
            }
            catch (Exception)
            {
                throw;
            }
            System.Data.SqlClient.SqlDependency.Stop(ConexionRuta);
            System.Data.SqlClient.SqlDependency.Start(ConexionRuta);
            conexion = new SqlConnection(ConexionRuta);
        }
     
       
        private void FormAlerta_Load(object sender, EventArgs e)
        {
            OnNewHome += new NewHome(FormAlerta_OnNewHome);
           ObtenerData();
        }

        private void FormAlerta_OnNewHome()
        {
            ISynchronizeInvoke i = this;
            if (i.InvokeRequired)
            {
                NewHome dd = new NewHome(FormAlerta_OnNewHome);
                i.BeginInvoke(dd, null);
                return;
            }
            ObtenerData();
        }

        public DataTable ObtenerData()
        {
            DataTable dt = new DataTable();
            try
            {
                // Create command
                // Command must use two part names for tables
                // SELECT <field> FROM dbo.Table rather than 
                // SELECT <field> FROM Table
                // Open the connection if necessary
                if (conexion.State == ConnectionState.Closed)
                    conexion.Open();
                System.Data.SqlClient.SqlDependency.Start(conexion.ConnectionString);

                // Query also can not use *, fields must be designated
                SqlCommand cmd = new SqlCommand
                    ("SELECT dbo.Personal.CodigoP AS CODIGO,  dbo.Personal.NroDNI AS DNI,  dbo.Personal.ApellidoPaterno AS[APELLIDO PATERNO], dbo.Personal.ApellidoMaterno AS[APELLIDO MATERNO], dbo.Personal.Nombre AS NOMBRE, dbo.Personal.Direccion AS DIRECCION, dbo.Personal.TelefonoFijo AS TELEFONO, dbo.Personal.TelefonoMovil AS CELULAR, dbo.Personal.Login AS USUARIO FROM dbo.Personal "
                    , conexion);
                //cmd.CommandType = CommandType.StoredProcedure;

                System.Data.SqlClient.SqlDependency dependency = new System.Data.SqlClient.SqlDependency(cmd);
                dependency.OnChange += new OnChangeEventHandler(Dependency_OnChange);

                // Clear any existing notifications
                cmd.Notification = null;

                // Create the dependency for this command
                //System.Data.SqlClient.SqlDependency dependency = new System.Data.SqlClient.SqlDependency(cmd);

                // Add the event handler
                //dependency.OnChange += new OnChangeEventHandler(OnChange);

                

                // Get the messages
                dt.Load(cmd.ExecuteReader(CommandBehavior.CloseConnection));
                System.Data.SqlClient.SqlDependency.Stop(conexion.ConnectionString);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            dataGridView1.DataSource = dt;
            return dt;

            //////servidor.CadenaConexion = CadenaConexion;
            ////List<EColaborador> lista = new List<EColaborador>();

            //try
            //{
            //    using (conexion = new SqlConnection(ConexionRuta))
            //    {
            //        using (SqlCommand comando = new SqlCommand("[dbo].[_pa_lista_Colaboradores]", conexion))
            //        {
            //            comando.CommandType = CommandType.StoredProcedure;
            //            //comando.Parameters.Add(new SqlParameter());


            //            comando.Notification = null;
            //            System.Data.SqlClient.SqlDependency de = new System.Data.SqlClient.SqlDependency(comando);
            //            de.OnChange += new OnChangeEventHandler(de_OnChange);

            //            //System.Data.SqlClient.SqlDependency dependency = new System.Data.SqlClient.SqlDependency(comando);
            //            //// Maintain the reference in a class member.

            //            //// Subscribe to the SqlDependency event.
            //            //dependency.OnChange += new
            //            //   OnChangeEventHandler(OnDependencyChange);
            //            conexion.Open();
            //            dt.Load(comando.ExecuteReader(CommandBehavior.CloseConnection));
            //            //SqlDataReader reader = comando.ExecuteReader();
            //            //while (reader.Read())
            //            //{
            //            //    EColaborador entidad = new EColaborador();
            //            //    entidad.CODIGO = Convert.ToString(reader["CODIGO"]);
            //            //    //entidad.NroDNI = Convert.ToString(reader["NroDNI"]);
            //            //    entidad.NOMBRE = Convert.ToString(reader["NOMBRE"]);
            //            //    entidad.DIRECCION = Convert.ToString(reader["DIRECCION"]);
            //            //    entidad.TELEFONO = Convert.ToString(reader["TELEFONO"]);
            //            //    lista.Add(entidad);
            //            //}
            //        }
            //    }

            //}
            //catch (Exception ex)
            //{
            //    ex.Message.ToString();
            //    //throw;
            //}
            //finally
            //{
            //    //conexion.Close();
            //}
            //dataGridView1.DataSource = dt;
            ////return lista;

        }

        private void Dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
            }

        }

        public static string DBConnectionString
        {
            get
            {
                return "Server ='com111\\SQLEXPRESS'; Database='MLData'; User ID='sa1'; Password ='pass100';Trusted_Connection=False;MultipleActiveResultSets=true;integrated security=SSPI";
            }
        }

        void OnChange(object sender, SqlNotificationEventArgs e)
        {
            System.Data.SqlClient.SqlDependency dependency = sender as System.Data.SqlClient.SqlDependency;

            // Notices are only a one shot deal
            // so remove the existing one so a new 
            // one can be added
            dependency.OnChange -= OnChange;

            // Fire the event
            if (OnNewMessage != null)
            {
                OnNewMessage();
            }
        }

        public void de_OnChange(object sender, SqlNotificationEventArgs e)
        {
            System.Data.SqlClient.SqlDependency de = sender as System.Data.SqlClient.SqlDependency;
            de.OnChange -= de_OnChange;
            OnNewHome?.Invoke();

        }
        // Handler method
        void OnDependencyChange(object sender,
           SqlNotificationEventArgs e)
        {
            // Handle the event (for example, invalidate this cache entry).
        }

        ////void Termination()
        ////{
        ////    // Release the dependency.
        ////    System.Data.SqlClient.SqlDependency.Stop(ConexionRuta);
        ////}

    }
}
