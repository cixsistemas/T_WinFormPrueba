using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace Embarque_Escritorio
{
    class Clssistema
    {
        private String Ruta = System.Configuration.ConfigurationManager.AppSettings.Get("CadenaConeccion");
        policia.clsaccesodatos servidor = new policia.clsaccesodatos();
        string __mesajeerror = "";
        Utilitarios Utilitarios_ = new Utilitarios();

        public DataTable consultar_formulario_autorizacion(int Usuario, string Formulario)
        {
            Utilitarios_.tabla = null;
            policia.clsaccesodatos servidor = new policia.clsaccesodatos();
            servidor.cadenaconexion = Ruta;
            if (servidor.abrirconexiontrans() == true){
                Utilitarios_.tabla = servidor.consultar("__Consultar_Formulario_Autorizacion", Usuario, Formulario).Tables[0];
                if (Utilitarios_.tabla == null){
                    __mesajeerror = servidor.getMensageError();
                    MessageBox.Show(__mesajeerror, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    servidor.cerrarconexion();
                }else{
                    int NroFilas = Utilitarios_.tabla.Rows.Count;
                    if (NroFilas == 0){
                        //********* SE COMENTO EL DIA 31-12-2018
                        //__mesajeerror = "Ud. no tiene derecho y permisos necesarios para ingresar al sistema";
                        //MessageBox.Show(__mesajeerror, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        servidor.cerrarconexion();
                        Utilitarios_.tabla = null;
                    }
                    else
                        servidor.cerrarconexion();
                }
            }
            else{
                __mesajeerror = servidor.getMensageError();
                MessageBox.Show(__mesajeerror, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                servidor.cerrarconexion();
                Utilitarios_.tabla = null;
            }
            return (Utilitarios_.tabla);
        }

        public DataTable consultar_formulario_derecho_autorizacion(int Usuario, string Formulario, string Derecho)
        {
            Utilitarios_.tabla = null;
            policia.clsaccesodatos servidor = new policia.clsaccesodatos();
            servidor.cadenaconexion = Ruta;
            if (servidor.abrirconexion() == true){
                Utilitarios_.tabla = servidor.consultar("__consultar_formulario_derecho_autorizacion", Usuario, Formulario, Derecho).Tables[0];
                if (Utilitarios_.tabla == null){
                    __mesajeerror = servidor.getMensageError();
                    servidor.cerrarconexion();
                }else{
                    int NroFilas = Utilitarios_.tabla.Rows.Count;
                    if (NroFilas == 0){
                        //********* SE COMENTO EL DIA 31-12-2018
                        //__mesajeerror = "Ud. no tiene derecho y permisos necesarios para ingresar al sistema";
                        //MessageBox.Show(__mesajeerror, "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        servidor.cerrarconexion();
                        Utilitarios_.tabla = null;
                    }else
                        servidor.cerrarconexion();
                }
            }else{
                __mesajeerror = servidor.getMensageError();
                servidor.cerrarconexion();
                Utilitarios_.tabla = null;
            }
            return (Utilitarios_.tabla);
        }

        public DataTable consultar_datos_Usuario(int codigo)
        {
            Utilitarios_.tabla = null;
            policia.clsaccesodatos servidor = new policia.clsaccesodatos();
            servidor.cadenaconexion = Ruta;
            if (servidor.abrirconexion() == true){
                Utilitarios_.tabla = servidor.consultar("__Consultar_Usuario", codigo).Tables[0];
                if (Utilitarios_.tabla == null){
                    __mesajeerror = servidor.getMensageError();
                    servidor.cerrarconexion();
                }else{
                    int NroFilas = Utilitarios_.tabla.Rows.Count;
                    if (NroFilas == 0){
                        __mesajeerror = "No hay datos personales disponible...";
                        servidor.cerrarconexion();
                        Utilitarios_.tabla = null;
                    }else
                        servidor.cerrarconexion();
                }
            }else{
                __mesajeerror = servidor.getMensageError();
                servidor.cerrarconexion();
            }
            return (Utilitarios_.tabla);
        }


    }
}
