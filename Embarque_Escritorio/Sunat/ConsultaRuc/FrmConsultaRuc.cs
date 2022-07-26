using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Embarque_Escritorio.Sunat.ConsultaRuc
{
    public partial class FrmConsultaRuc : Form
    {
        public FrmConsultaRuc()
        {
            InitializeComponent();
        }
        private string path;
        private List<rucViewModel> rucViewModelList;

        CsSunat _LSunat = new CsSunat();

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //busqueda_masiva();

            if (MessageBox.Show("¿Desea buscar?", "Sistema"
           , MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                List<rucViewModel> list2 = new List<rucViewModel>();

                int num = 1;

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    

                    if (backgroundWorker1.CancellationPending == true)
                    {
                        e.Cancel = true;
                        break;
                    }

                    string ruc = row.Cells["NumeroRuc"].Value.ToString();

                    try
                    {
                        Thread.Sleep(500);
                        dynamic respuesta = _LSunat.GetTipoCambio("https://api.apis.net.pe/v1/ruc?numero=" + ruc);

                        rucViewModel oRUc = new rucViewModel();

                        oRUc.NumeroRuc = respuesta.numeroDocumento.ToString();
                        oRUc.RazonSocial = respuesta.nombre.ToString();
                        oRUc.Estado = respuesta.estado.ToString();
                        oRUc.Direccion = respuesta.direccion.ToString();
                        oRUc.Distrito = respuesta.distrito.ToString();
                        oRUc.Ubigeo = respuesta.ubigeo.ToString();
                        list2.Add(oRUc);
                    }
                    catch
                    {
                        //MessageBox.Show("EL RUC: " + ruc + " no es valido", "Error al leer RUC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        rucViewModel oRUc = new rucViewModel();
                        oRUc.NumeroRuc = ruc;
                        oRUc.RazonSocial = "";
                        list2.Add(oRUc);
                    }

                    backgroundWorker1.ReportProgress(num);

                    num++;

                }

                //asignar(list2);
                this.rucViewModelList = list2;
            }
            else
            {
                MessageBox.Show("Elije el archivo que deseas subir", "Error al leer archivo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pbrTrabajo.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dataGridView1.DataSource = this.rucViewModelList;

            if (e.Cancelled == true)
            {
                MessageBox.Show("Proceso Cancelado");
            }
            else if (e.Error != null)
            {
                MessageBox.Show("Ocurrio un error en el proceso");
            }
            else
            {
                MessageBox.Show("Termino proceso");
            }
        }

        private void FrmConsultaRuc_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Aqui va el codigo para leer el archivo

                //lblArchivo.Text = openFileDialog1.FileName;

                path = openFileDialog1.FileName;

                leer_archivo_excel();
            }
        }

        private void leer_archivo_excel()
        {
            if (path == null)
                MessageBox.Show("Elije el archivo que deseas subir", "Error al leer archivo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                try
                {
                    SLDocument sl = new SLDocument(path);

                    int iRow = 2;
                    List<rucViewModel> list = new List<rucViewModel>();

                    while (!string.IsNullOrEmpty(sl.GetCellValueAsString(iRow, 1)))
                    {
                        rucViewModel oRUc = new rucViewModel();
                        oRUc.NumeroRuc = sl.GetCellValueAsString(iRow, 1);
                        oRUc.RazonSocial = sl.GetCellValueAsString(iRow, 2);

                        list.Add(oRUc);

                        iRow++;
                    }

                    dataGridView1.DataSource = list;

                    pbrTrabajo.Maximum = dataGridView1.Rows.Count;

                }
                catch
                {
                    MessageBox.Show("Cierra el archivo que deseas leer o elije un archivo valido", "Error al leer archivo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // comenzamos el progreso 

            if (backgroundWorker1.IsBusy != true)
            {
                backgroundWorker1.RunWorkerAsync();
            }
            // aqui iteramos el dataGridView y ponemos la razon social correspondiente a cada ruc

            //busqueda_masiva();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
        }
    }
}
