using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.Enums;
using TableDependency.SqlClient.Base.EventArgs;



namespace Embarque_Escritorio.SqlDependency
{
    public partial class FormAlerta : Form
    {
        public FormAlerta()
        {
            InitializeComponent();
        }
        public SqlTableDependency<Personal> people_table_dependency;
        private string connection_string_people = ConfigurationManager.AppSettings.Get("CadenaConeccionSoporte");
        //string connection_string_people2 = "Data Source=ABRAHAM-VAIO;Initial Catalog = SoporteTecnico; Persist Security Info=True;User ID = sa; Password=cuevas135";
        SqlConnection conexion = null;
        private void FormAlerta_Load(object sender, EventArgs e)
        {
            //form load event
            refresh_table();
            start_people_table_dependency();
        }

        private void FormAlerta_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                stop_people_table_dependency();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message.ToString());
                //log_file(ex.ToString());
            }
        }

        private bool start_people_table_dependency()
        {
            try
            {
                people_table_dependency = new SqlTableDependency<Personal>(connection_string_people);
                people_table_dependency.OnChanged += people_table_dependency_Changed;
                people_table_dependency.OnError += people_table_dependency_OnError;
                people_table_dependency.Start();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + " start_people_table_dependency");
                //log_file(ex.ToString());
            }
            return false;

        }
        private bool stop_people_table_dependency()
        {
            try
            {
                if (people_table_dependency != null)
                {
                    people_table_dependency.Stop();

                    return true;
                }
            }
            catch (Exception ex) {
               // log_file(ex.ToString());
            }

            return false;

        }
        private void people_table_dependency_OnError(object sender, ErrorEventArgs e)
        {
            MessageBox.Show(e.Error.Message.ToString() + " ...");
            //log_file(e.Error.Message);
        }
        public void log_file(string logText)
        {
            // logText += DateTime.Now.ToString() + Environment.NewLine + logText;
            ThreadSafe(() => richTextBox1.AppendText(DateTime.Now.ToString("HH:mm:ss:fff") + "\t" + logText + Environment.NewLine));
            System.IO.File.AppendAllText(Application.StartupPath + "\\log.txt", logText);

        }
        private void people_table_dependency_Changed(object sender, RecordChangedEventArgs<Personal> e)
        {
            try
            {
                var changedEntity = e.Entity;

                switch (e.ChangeType)
                {
                    case ChangeType.Insert:
                        {

                            //log_file("Insert values:\tname:" + changedEntity.NroDNI.ToString() 
                            //    + "\tage:" + changedEntity.NroDNI.ToString());
                            refresh_table();

                        }
                        break;

                    case ChangeType.Update:
                        {
                            //log_file("Update values:\tname:" + changedEntity.NOMBRE.ToString() 
                            //    + "\tage:" + changedEntity.NOMBRE.ToString());
                            refresh_table();

                        }
                        break;

                    case ChangeType.Delete:
                        {
                            //log_file("Delete values:\tname:" + changedEntity.DIRECCION.ToString() 
                            //    + "\tage:" + changedEntity.DIRECCION.ToString());
                            refresh_table();
                        }
                        break;
                };

            }
            catch (Exception ex) { log_file(ex.ToString()); }
        }
        private void refresh_table()
        {
            DataTable dt = new DataTable();
            SqlDataReader leer;
            //SqlCommand comando = new SqlCommand();

            //comando.Connection = conexion;
            //conexion.Open();
            //comando.CommandText = "_pa_lista_Colaboradores";
            //comando.CommandType = CommandType.StoredProcedure;
            //leer = comando.ExecuteReader();
            //dt.Load(leer);
            //conexion.Close();
            //dataGridView1.DataSource = dt;

            using (conexion = new SqlConnection(connection_string_people))
            {
                using (SqlCommand comando = new SqlCommand("[dbo].[_pa_lista_Colaboradores]", conexion))
                {
                    if (conexion.State == ConnectionState.Closed)
                        conexion.Open();

                    dt.Load(comando.ExecuteReader(CommandBehavior.CloseConnection));
                    conexion.Close();
                    //dataGridView1.DataSource = dt;
                    ThreadSafe(() => dataGridView1.DataSource = dt);

                }
            }


            //string sql = "SELECT * FROM Personal";
            //SqlConnection connection = new SqlConnection(connection_string_people);
            //SqlDataAdapter dataadapter = new SqlDataAdapter(sql, connection);
            //DataSet ds = new DataSet();
            //connection.Open();
            //dataadapter.Fill(ds, "Personal");
            //connection.Close();
            //ThreadSafe(() => dataGridView1.DataSource = ds);
            //ThreadSafe(() => dataGridView1.DataMember = "Personal");
        }

        private void ThreadSafe(MethodInvoker method)
        {
            try
            {
                if (InvokeRequired)
                    Invoke(method);
                else
                    method();
            }
            catch (ObjectDisposedException) { }
        }

    }
}
