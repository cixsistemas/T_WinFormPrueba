using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;

namespace Embarque_Escritorio.Clases
{
  public  class CsUsuario
    {
        private readonly string Ruta = ConfigurationManager.AppSettings.Get("CadenaConeccion");
        private string Mensaje;
        private string Pagina;
        private int Respuesta;
        public int ObtenerRespuesta()
        { return Respuesta; }
        public string ObtenerMensaje()
        { return Mensaje; }

        public string ObtenerPagina()
        { return Pagina; }
        public DataTable ListarUsuarios(string Opcion, string Nombre)
        {
            DataTable dt = new DataTable();
            policia.clsaccesodatos servidor = new policia.clsaccesodatos
            {
                cadenaconexion = Ruta
            };
            try
            {
                if (servidor.abrirconexion() == true)
                {
                    dt = servidor.consultar("[dbo].[BD_Pje_Listar_Usuarios]"
                        , Opcion, Nombre).Tables[0];
                    if (dt.Rows.Count == 0)
                    {
                        //dt = null;
                        servidor.cerrarconexion();
                        Mensaje = "No hay registros para mostrar";
                        Pagina = "";
                    }
                    else
                    {
                        servidor.cerrarconexion();
                    }
                }
                else
                {
                    //dt = null;
                    servidor.cerrarconexion();
                    Mensaje = servidor.getMensageError() + " :-(";
                    Pagina = "";
                }

            }
            catch (Exception e)
            {
                servidor.cerrarconexion();
                Mensaje = servidor.getMensageError() + (e) + " :-((";
                Pagina = "";
                //throw;
                //Mensaje = "Error inesperado al intentar conectarnos con el servidor." + "f..";
                //Pagina = "../CerrarSession.aspx";
            }
            return (dt);
        }



    }
}
