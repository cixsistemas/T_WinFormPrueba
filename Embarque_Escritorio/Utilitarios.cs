using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace Embarque_Escritorio
{
    public class Utilitarios{
        public DataTable tabla = null;
        public DataTable _tabla_sistema = null/* TODO Change to default(_) if this is not a reference type */;
        public DataTable _tabla = null/* TODO Change to default(_) if this is not a reference type */;
      
        public int indice = -1;
        private String Ruta = System.Configuration.ConfigurationManager.AppSettings.Get("CadenaConeccion");
        policia.clsaccesodatos servidor = new policia.clsaccesodatos();
        ////public static char ChrW(int CharCode);

        //DataTable tabla;
        public string __mesajeerror = "";

       
        public void saltar_Flechas(System.Windows.Forms.KeyEventArgs e)
        {
            // PASAR DE UN CONTROL A OTRO CON FLECHAS
            if (e.KeyData == Keys.Down)
                SendKeys.Send("{TAB}");
            if (e.KeyData == Keys.Up)
                SendKeys.Send("+{TAB}");
        }

        public void saltar_ENTER(System.Windows.Forms.KeyPressEventArgs e)
        {
            //// PASAR DE UN CONTROL A OTRO CON ENTER
            //char MyKeyChr = (char)e.KeyCode;
            //if (e.KeyChar == ChrW(Keys.Enter))
            //{
            //    e.Handled = true;
            //    SendKeys.Send("{TAB}");
            //}
        }


        public void cargacombobox(object _combo, string _ruta, string _procedimientoalmacenado, string _campo, string _valor, params object[] y)
        {
            //policia.clsaccesodatos servidor = new policia.clsaccesodatos();
            //servidor.cadenaconexion = _ruta;
            //if (servidor.abrirconexion() == true)
            //{
            //    switch (y.Length)
            //    {
            //        case 1:
            //            {
            //                tabla = servidor.consultar(_procedimientoalmacenado, y[0]).Tables[0];
            //                break;
            //            }
            //    }
            //    if (tabla == null)
            //        servidor.cerrarconexion();
            //    else
            //    {
            //        int NroFilas = tabla.Rows.Count;
            //        if (NroFilas == 0)
            //            servidor.cerrarconexion();
            //        else
            //        {
            //            _combo.DataSource = tabla;
            //            _combo.DisplayMember = _campo;
            //            _combo.ValueMember = _valor;
            //            servidor.cerrarconexion();
            //        }
            //    }
            //}
            //else
            //{
            //    __mesajeerror = servidor.getMensageError();
            //    servidor.cerrarconexion();
            //}
        }

    }


}
