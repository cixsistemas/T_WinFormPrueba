using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Embarque_Escritorio.DatagridView
{
    public class CsCargar
    {
        private string Ruta = System.Configuration.ConfigurationManager.AppSettings.Get("CadenaConexion");
        AccesoDatos.clsaccesodatos servidor = new AccesoDatos.clsaccesodatos();

        DataTable tabla;
        string __mesajeerror;
        public DataTable ObtenerData(string Opcion, string Concepto)
        {
            servidor.cadenaconexion = Ruta;
            if (servidor.abrirconexion() == true)
            {
                if (servidor.consultar("[dbo].[PaConcepto]", Opcion, Concepto).Tables.Count > 0)
                    tabla = servidor.consultar("[dbo].[PaConcepto]", Opcion, Concepto).Tables[0];
                if (tabla == null)
                {
                    servidor.cerrarconexion();
                    tabla = null;
                }
            }
            else
            {
                __mesajeerror = servidor.getMensageError();
                servidor.cerrarconexion();
                MessageBox.Show(__mesajeerror, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return (tabla);
        }
    }
}
