using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Embarque_Escritorio.Sunat.TipoCambio
{
    public partial class TipoCambio : Form
    {
        public TipoCambio()
        {
            InitializeComponent();
        }
        DBApi _DBApi = new DBApi();
        private void TipoCambio_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string dateFormatted = Convert.ToString(dateTimePicker1.Value.ToString("yyyy-MM-dd"));

            MessageBox.Show(dateFormatted);
            dynamic respuesta = _DBApi.Get("https://api.apis.net.pe/v1/tipo-cambio-sunat?fecha=" + dateFormatted);
            //GetItems();
            txtCompra.Text = respuesta.compra.ToString();
            txtVenta.Text = respuesta.venta.ToString();
        }


       

        private static void GetItems()
        {
            var url = $"https://api.apis.net.pe/v1/tipo-cambio-sunat?fecha=2021-06-23";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return;
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();
                            // Do something with responseBody
                            Console.WriteLine(responseBody);
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                ex.Message.ToString();
                // Handle error
            }
        }

       
    }
}
