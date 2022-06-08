using Embarque_Escritorio.HojaRuta.Entities;
using Embarque_Escritorio.HojaRuta.Others;
using Embarque_Escritorio.ServiceReference2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Embarque_Escritorio.HojaRuta.Capa_Datos;
using System.Globalization;

namespace Embarque_Escritorio.HojaRuta
{
    public class HojaRutaLogic
    {
        Seguridad seguridad = new Seguridad();
        Conductor oConductor = new Conductor();
        private static WSServiciosHRSoapClient _ServiceMTCHR = new WSServiciosHRSoapClient();
        _HojaRutaData __HojaRutaData = new _HojaRutaData();
        string MensajeLogic = "";
        //===========================================================================
        private int Respuesta;
        public int ObtenerRespuesta()
        { return Respuesta; }

        private string Mensaje;
        public string MensajeBD()
        { return Mensaje; }
        //===========================================================================

        //====================================================================
        //**** VALIDAR CHOFER EN MTC
        #region VALIDAR CHOFER EN MTC
        public Response<bool> ValidarChoferMTC(int EmpresaId, string EmpCodigo)
        {
            try
            {
                if (EmpresaId < 0)
                    return new Response<bool>(false, false, "CÓDIGO DE LA EMPRESA INCORRECTA.", false);
                if (string.IsNullOrEmpty(EmpCodigo))
                    return new Response<bool>(false, false, "CÓDIGO DEL EMPLEADO INCORRECTO.", false);
                List<EmpresaEntity> empresaEntityList2 = __HojaRutaData.ListEmpresa(EmpresaId);
                TrabajadoresEntity trabajadoresEntity = __HojaRutaData.ObtenerEmpleado(EmpCodigo);
                if (empresaEntityList2.Count <= 0)
                    return new Response<bool>(false, false, "LA EMPRESA NO EXISTE.", false);

                foreach (EmpresaEntity empresaEntity in empresaEntityList2)
                {
                    seguridad.Ruc = empresaEntity.RUC;
                    seguridad.Usuario = empresaEntity.USR_MTC;
                    seguridad.Clave = empresaEntity.CLAVE_MTC;
                    seguridad.Partida = empresaEntity.PARTIDA_MTC;
                }
                if (trabajadoresEntity == null)
                    return new Response<bool>(false, false, "EL CONDUCTOR NO EXISTE.", false);

                if (!string.IsNullOrEmpty(trabajadoresEntity.DNI))
                {
                    oConductor.Seguridad = seguridad;
                    oConductor.TpoDocumento = trabajadoresEntity.DNI.Length > 8 ? "P.T.P." : "L.E.";
                    oConductor.NroDocumento = trabajadoresEntity.DNI;
                }

                ResultConductor conductor = _ServiceMTCHR.getConductor(oConductor);
                return conductor.Return ? new Response<bool>(true, true, "PROCESO EXITOSO", true) :
                    new Response<bool>(false, false, conductor.Errores.Where
                    (x => x.Info != null || x.Info != string.Empty).FirstOrDefault().Info, false);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ValidarChofer2(int EmpresaId, string EmpCodigo)
        {
            bool Rpta = false;
            //MensajesEntity _MensajesEntity = new MensajesEntity();

            try
            {
                List<EmpresaEntity> empresaEntityList2 = __HojaRutaData.ListEmpresa(EmpresaId);
                string trabajadoresEntity2 = __HojaRutaData.ObtenerEmpleado2(EmpCodigo);
                TrabajadoresEntity trabajadoresEntity = __HojaRutaData.ObtenerEmpleado(EmpCodigo);
                foreach (EmpresaEntity empresaEntity in empresaEntityList2)
                {
                    seguridad.Ruc = empresaEntity.RUC;
                    seguridad.Usuario = empresaEntity.USR_MTC;
                    seguridad.Clave = empresaEntity.CLAVE_MTC;
                    seguridad.Partida = empresaEntity.PARTIDA_MTC;
                }

                //if (trabajadoresEntity == null)
                //{
                //    Mensaje = "EL CONDUCTOR NO EXISTE.";
                //    Rpta = false;
                //}

                if (trabajadoresEntity2 == "")
                {
                    Mensaje = "EL CONDUCTOR NO EXISTE.";
                    ////_MensajesEntity.MensajeBD(MensajeLogic);
                    ////_MensajesEntity.Mensaje = MensajeLogic;
                    //_MensajesEntity.Mensaje = "EL CONDUCTOR NO EXISTE.";
                    //return;
                    Rpta = false;
                }

                else
                {
                    oConductor.Seguridad = seguridad;
                    //oConductor.TpoDocumento = "L.E.";
                    //oConductor.NroDocumento = trabajadoresEntity2;
                    oConductor.TpoDocumento = trabajadoresEntity.DNI.Length > 8 ? "P.T.P." : "L.E.";
                    oConductor.NroDocumento = trabajadoresEntity.DNI;


                    ResultConductor conductor = _ServiceMTCHR.getConductor(oConductor);
                    Rpta = conductor.Return;
                    if (Rpta == true)
                    {
                        Mensaje = "PROCESO EXITOSO";
                    }
                    else
                    {
                        Mensaje = conductor.Errores.Where(x => x.Info != null || x.Info != string.Empty).FirstOrDefault().Info;
                    }
                }

                ////if (!string.IsNullOrEmpty(trabajadoresEntity.DNI))
                ////{

                ////}


                //_MensajesEntity.Mensaje = "EL CONDUCTOR EXISTE.";

                ////MensajeLogic = "EL CONDUCTOR EXISTE.";
                //_MensajesEntity.MensajeBD(MensajeLogic);
                //_MensajesEntity.Mensaje = MensajeLogic;

                //return;

                //Rpta = true;

            }
            catch (Exception)
            {

                throw;
            }

            return Rpta;
        }
        #endregion
        //====================================================================

        //====================================================================
        //**** FINALIZAR HOJA DE RUTA
        #region FINALIZAR HOJA DE RUTA
        //LISTAR
        public List<HojaRutaEntity> LitsFinalizarHojaRuta(int EmpresaId, string FechaInicio, string FechaFinal
            , string Placa, string NroHojaRuta, int CODI_SUCURSAL, int Codi_Ruta, int Estado)
        {
            _HojaRutaData con = new _HojaRutaData();
            List<HojaRutaEntity> lista = new List<HojaRutaEntity>();
            lista = con.LitsFinalizarHojaRuta(EmpresaId, FechaInicio, FechaFinal
                , Placa, NroHojaRuta, CODI_SUCURSAL, Codi_Ruta, Estado);
            return lista;
        }

        //VALIDAR
        public bool ValidarFinalizarMTC(string FecLlegada, string HorLlegada)
        {
            bool valida = true;
            try
            {
                //if (FecLlegada == "")
                //{
                //    valida = false;
                //    Mensaje = "FECHA LLEGADA INCORRECTA.";
                //}
                //if (HorLlegada == "")
                //{
                //    valida = false;
                //    Mensaje = "HORA DE LLEGADA INCORRECTA.";
                //}

                DateTime now = DateTime.Now;
                DateTime dateTime1 = Convert.ToDateTime(FecLlegada);
                double num1 = Convert.ToDouble(HorLlegada.Substring(0, 2));
                double num2 = Convert.ToDouble(HorLlegada.Substring(3, 2));
                DateTime dateTime2 = dateTime1.AddHours(num1).AddMinutes(num2);
                dateTime2 = dateTime2.AddMinutes(-30.0);
                if (dateTime2 > now)
                {
                    valida = false;
                    Mensaje = "Improcedente, usted puede finalizar la hoja de ruta a partir del "
                        + dateTime2.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) + " "
                        + dateTime2.ToString("hh:mm tt", CultureInfo.InvariantCulture);
                    //return dateTime2 > now ? new Response<bool>(false, false, "Improcedente, usted puede finalizar la hoja de ruta a partir del " + dateTime2.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) + " " + dateTime2.ToString("hh:mm tt", CultureInfo.InvariantCulture), false) : new Response<bool>(true, true, "CORRECTO", true);
                    
                }
               
            }
            catch (Exception ex)
            {
                Mensaje = ex.Message.ToString();
                //return new Response<bool>(false, false, Functions.MessageError(ex), false);
            }
            return valida;
        }

        public bool FinalizarHojaRuta(int EmpresaId, string NroHojaRuta, string FecLlegada,
      string HorLlegada, int UsrFinaliza)
        {
            bool Rpta = false;
            try
            {
                List<EmpresaEntity> empresaEntityList2 = __HojaRutaData.ListEmpresa(EmpresaId);
                Seguridad seguridad = new Seguridad();
                Finalizar oFinalizar = new Finalizar();

                //================================================================
                //VALIDAR VARIABLES
                if (string.IsNullOrEmpty(NroHojaRuta))
                {
                    Mensaje = "EL CONDUCTOR NO EXISTE.";
                    Rpta = false;
                }
                if (string.IsNullOrEmpty(FecLlegada))
                {
                    Mensaje = "FECHA LLEGADA INCORRECTA.";
                    Rpta = false;
                }
                if (string.IsNullOrEmpty(HorLlegada))
                {
                    Mensaje = "HORA LLEGADA INCORRECTA.";
                    Rpta = false;
                }
                if (UsrFinaliza < 0)
                {
                    Mensaje = "USUARIO INCORRECTO.";
                    Rpta = false;
                }
                if (empresaEntityList2.Count <= 0)
                {
                    Mensaje = "LA EMPRESA NO EXISTE.";
                    Rpta = false;
                }                    
                //================================================================

                foreach (EmpresaEntity empresaEntity in empresaEntityList2)
                {
                    seguridad.Ruc = empresaEntity.RUC;
                    seguridad.Usuario = empresaEntity.USR_MTC;
                    seguridad.Clave = empresaEntity.CLAVE_MTC;
                    seguridad.Partida = empresaEntity.PARTIDA_MTC;
                }

                //IGUALANDO VARIABLES 
                oFinalizar.Code = NroHojaRuta;
                oFinalizar.FecLlegada = FecLlegada;
                oFinalizar.HorLlegada = Convert.ToDateTime(HorLlegada).ToString("HH:mm", CultureInfo.InvariantCulture);
                oFinalizar.Seguridad = seguridad;

                ResultFinalizar finalizar = _ServiceMTCHR.setFinalizar(oFinalizar);
                Rpta = finalizar.Return;
                if (Rpta == true)
                {
                    Mensaje = "PROCESO EXITOSO";
                    __HojaRutaData.FinalizaHojaRuta(NroHojaRuta, Convert.ToDateTime(FecLlegada), HorLlegada, UsrFinaliza);
                }
                else
                {
                    Mensaje = finalizar.Errores.Where(x => x.Info != null || x.Info != string.Empty).FirstOrDefault().Info
                        + " Codigo de error: "
                        + finalizar.Errores.Where(x => x.Code != null || x.Code != string.Empty).FirstOrDefault().Code;
                }

                //return _ServiceMTCHR.setFinalizar(oFinalizar).Return ? 
                //    (__HojaRutaData.FinalizaHojaRuta(NroHojaRuta, Convert.ToDateTime(FecLlegada), HorLlegada, UsrFinaliza) ? 
                //    new Response<bool>(true, true, "PROCESO EXITOSO", true) : 
                //    new Response<bool>(false, false, "NO SE ACTUALIZO EL NUMERO DE RUTA.", false)) 
                //    : (__HojaRutaData.FinalizaHojaRuta(NroHojaRuta, Convert.ToDateTime(FecLlegada), HorLlegada, UsrFinaliza) ? 
                //    new Response<bool>(true, true, "PROCESO EXITOSO", true)
                //    : new Response<bool>(false, false, "NO SE ACTUALIZO EL NUMERO DE RUTA.", false));
            }
            catch (Exception)
            {

                throw;
            }

            return Rpta;
        }
        #endregion
        //====================================================================
    }
}
