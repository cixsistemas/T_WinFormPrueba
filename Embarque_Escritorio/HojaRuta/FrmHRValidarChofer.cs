using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Embarque_Escritorio.ServiceReference2;
using System.IO;
using System.Xml;
using System.Net;
using Embarque_Escritorio.HojaRuta;
using Embarque_Escritorio.Clases;
using Embarque_Escritorio.HojaRuta.Others;
using Embarque_Escritorio.HojaRuta.Entities;
using Embarque_Escritorio.HojaRuta.Capa_Datos;

namespace Embarque_Escritorio.BDPasajes
{
    public partial class FrmHojaRuta1 : Form
    {
        public FrmHojaRuta1()
        {
            InitializeComponent();
        }
        CsCredenciales _CsCredenciales = new CsCredenciales();
        WSServiciosHRSoapClient client = new WSServiciosHRSoapClient();
        ///WSServiciosHRSoap sopa = new WSServiciosHRSoap();
        CsConductor _CsConductor = new CsConductor();
        ////ServiceReference2.Conductor conductor = new ServiceReference2.Conductor();
        ServiceReference2.Errores errores = new ServiceReference2.Errores();

        private WSServiciosHRSoapClient _ServiceMTCHR = new WSServiciosHRSoapClient();
        _HojaRutaData _HojaRutaData = new _HojaRutaData();
        HojaRutaLogic _HojaRutaLogic = new HojaRutaLogic();
        MensajesEntity _MensajesEntity = new MensajesEntity();

        //getConductorResponseBody g = new getConductorResponseBody();
        //CheckMovilSoapClient ws = new CheckMovilSoapClient();
        //private ServiceReference1 objWorkManager = new ServiceReference1();
        private void FrmHojaRuta1_Load(object sender, EventArgs e)
        {
            //g1.ClientCredentials.UserName.UserName = "";
            //g.getConductorResult
        }
        //public class Conductor
        //{

        //    public string nombre { get; set; }
        //    public string dni { get; set; }
        //}
        //client.getConductor cond = new client.getConductor();

        //Conductor _Conductor = new Conductor();
        private string xmlrespuesta;

        private void btnConectar_Click(object sender, EventArgs e)
        {

            ////Seguridad seguridad = new Seguridad();
            ////Conductor oConductor = new Conductor();

            ////seguridad.Ruc = _CsCredenciales.ruc;
            ////seguridad.Usuario = _CsCredenciales.usuario;
            ////seguridad.Clave = _CsCredenciales.clave;
            ////seguridad.Partida = _CsCredenciales.partida;

            ////_CsConductor.tipoDoc = "L.E.";
            ////_CsConductor.dni = "43986031";

            ////////conductor.TpoDocumento = _CsConductor.tipoDoc;
            ////////conductor.NroDocumento = _CsConductor.dni;
            ////////////conductor.Seguridad.Ruc = _CsCredenciales.ruc;

            //////////client.getConductor(conductor);
            //// string codigo = errores.Code;

            ////MessageBox.Show(codigo);

            //////_Conductor.nombre = "juna";
            //////_Conductor.dni = "43986031";



            try
            {
                bool rpta;

                _HojaRutaLogic.ValidarChoferMTC(1, "C0064");
                ////Response < T > = _HojaRutaLogic.ValidarChoferMTC(1, "C0064");

                //=======================================================
                //SEGUNDA FORMA DE HACERLO
                rpta = _HojaRutaLogic.ValidarChofer2(1, txtCodigoChofer.Text.Trim());
                if (rpta == true)
                {
                    __mensajeVale.Text = (_HojaRutaLogic.MensajeBD() + " :D").ToUpper();
                    //MessageBox.Show(_HojaRutaLogic.MensajeBD() + " :D");
                }
                else
                {
                    __mensajeVale.Text = (_HojaRutaLogic.MensajeBD() + " :(").ToUpper();
                    //MessageBox.Show(_HojaRutaLogic.MensajeBD() + " :(");
                }
                //=======================================================

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + " :-0");
                //throw;
            }


            ////InvokeSevice(); //FUNCIONAAAAA

        }

       
        private HttpWebRequest CreateSoapRequest()
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("https://wshr.mtc.gob.pe/WSServiciosHR.asmx?op=getConductor");
            webRequest.ContentType = "text/xml; charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }



        public void InvokeSevice()

        {
            string rpta = "";
            //Llamando al método CreateSOAPWebRequest
            HttpWebRequest request = CreateSoapRequest();
            XmlDocument SOAPReqBody = new XmlDocument();
            //Solicitud de cuerpo SOAP
            SOAPReqBody.LoadXml(@"<?xml version=""1.0"" encoding=""utf-8""?>
            <soap12:Envelope xmlns:soap12=""http://www.w3.org/2003/05/soap-envelope"">
            <soap12:Body>
            <getConductor xmlns=""http://wshr.mtc.gob.pe/"">
              <oConductor>
                <Seguridad>
                  <Ruc>"+ _CsCredenciales.ruc + @"</Ruc>
                  <Usuario>"+ _CsCredenciales.usuario + @" </Usuario>
                  <Clave>" +_CsCredenciales.clave + @"</Clave>
                  <Partida>"+ _CsCredenciales.partida + @"</Partida>
                </Seguridad>
                <TpoDocumento>L.E.</TpoDocumento>
                <NroDocumento>"+ comboBox1.Text + @"</NroDocumento>
              </oConductor>
            </getConductor>
            </soap12:Body>
            </soap12:Envelope>");

            using (Stream stream = request.GetRequestStream())
            {
                SOAPReqBody.Save(stream);
            }

            //Obteniendo Resspuesta de request
            using (WebResponse Serviceres = request.GetResponse())
            {
                using (StreamReader rd = new StreamReader(Serviceres.GetResponseStream()))

                {
                    //leyendo stream
                    var ServiceResult = rd.ReadToEnd();
                    rpta = ServiceResult.ToString();
                    rd.Close();
                }

            }
           //rpta =   client.getConductor..rpta();
            MessageBox.Show(rpta);

        }


        private DataSet xmlResponse(string NumeroReferencia)
        {
            DataSet ds = new DataSet();
            try
            {
                string xml;
                xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>  <soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:bce=\"http://j2ee.netbeans.org/wsdl/BpelBceSGI/BceInformacionOperacion\">" +
                       "<soapenv:Header/>" +
                       "   <soapenv:Body>" +
                       "      <bce:BceInformacionOperacionOperation>" +
                       "         <numReferenciaSgiMef>" + NumeroReferencia + "</numReferenciaSgiMef>" +
                       "      </bce:BceInformacionOperacionOperation>" +
                       "   </soapenv:Body>" +
                       "</soapenv:Envelope>";

                string urlconfig = "https://wshr.mtc.gob.pe/WSServiciosHR.asmx";

                string url = urlconfig;
                ////string responsestring = "";
                HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(url);
                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] buffer = encoding.GetBytes(xml);
                string response;
                myReq.AllowWriteStreamBuffering = false;
                myReq.Method = "POST";
                myReq.ContentType = "text/xml; charset=UTF-8";
                myReq.ContentLength = buffer.Length;
                myReq.Headers.Add("SOAPAction", "");
                ServicePointManager.ServerCertificateValidationCallback = ((sender1, certificate, chain, sslPolicyErrors) => true);

                using (Stream post = myReq.GetRequestStream())
                {
                    post.Write(buffer, 0, buffer.Length);
                }

                HttpWebResponse myResponse = (HttpWebResponse)myReq.GetResponse();

                Stream responsedata = myResponse.GetResponseStream();
                StreamReader responsereader = new StreamReader(responsedata);
                response = responsereader.ReadToEnd();

                ds.ReadXml(new XmlTextReader(new StringReader(response)));
                myResponse.Close();
                responsereader.Close();
                responsedata.Close();
                return ds;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                ds.Dispose();
            }
        }

    }
}
