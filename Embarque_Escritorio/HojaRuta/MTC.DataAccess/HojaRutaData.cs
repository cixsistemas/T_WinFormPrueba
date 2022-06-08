using Embarque_Escritorio.HojaRuta.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Embarque_Escritorio.Clases;
using Embarque_Escritorio.HojaRuta.Utility;
using System.Configuration;

namespace Embarque_Escritorio.HojaRuta.Capa_Datos
{
    public class _HojaRutaData
    {
        CsSeguridad _CsSeguridad = new CsSeguridad();
        private static string ConexionRutaBDAlertas = ConfigurationManager.AppSettings.Get("CadenaConeccionBDAlertas");

        public List<EmpresaEntity> ListEmpresa(int EmpresaId)
        {

            //servidor.CadenaConexion = CadenaConexion;
            List<EmpresaEntity> lista = new List<EmpresaEntity>();
            SqlConnection conexion = null;
            try
            {
                using (conexion = new SqlConnection(ConexionRutaBDAlertas))
                {
                    using (SqlCommand comando = new SqlCommand("[dbo].[Usp_Extraer_MTC_Empresa]", conexion))
                    {
                        comando.CommandType = System.Data.CommandType.StoredProcedure;
                        comando.Parameters.Add(new SqlParameter("@EmpresaId", EmpresaId));
                        //comando.Parameters.Add(new SqlParameter("@Nombre", Nombre));
                        conexion.Open();
                        SqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            EmpresaEntity entidad = new EmpresaEntity();
                            entidad.RUC = Convert.ToString(reader["RUC"]);
                            //entidad.RUC = Convert.ToString("20304030636");
                            entidad.USR_MTC = _CsSeguridad.Desencripta(Convert.ToString(reader["USR_MTC"]), "JLMSECURITY");
                            entidad.CLAVE_MTC = _CsSeguridad.Desencripta(Convert.ToString(reader["CLAVE_MTC"]), "JLMSECURITY");
                            entidad.PARTIDA_MTC = _CsSeguridad.Desencripta(Convert.ToString(reader["PARTIDA_MTC"]), "JLMSECURITY");
                            entidad.user_reniec = _CsSeguridad.Desencripta(Convert.ToString(reader["user_reniec"]), "JLMSECURITY");
                            lista.Add(entidad);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
                //throw;
            }
            finally
            {
                conexion.Close();
            }
            return lista;
        }

        public List<TrabajadoresEntity> ObtenerChofer(string Codi_Chofer)
        {

            //servidor.CadenaConexion = CadenaConexion;
            List<TrabajadoresEntity> lista = new List<TrabajadoresEntity>();
            SqlConnection conexion = null;
            try
            {
                using (conexion = new SqlConnection(ConexionRutaBDAlertas))
                {
                    using (SqlCommand comando = new SqlCommand("[dbo].[scwsp_BuscarChofer]", conexion))
                    {
                        comando.CommandType = System.Data.CommandType.StoredProcedure;
                        comando.Parameters.Add(new SqlParameter("@Codi_Chofer", Codi_Chofer));
                        conexion.Open();
                        SqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            TrabajadoresEntity entidad = new TrabajadoresEntity();
                            entidad.EmpCodigo = Convert.ToString(reader["EmpCodigo"]);
                            entidad.DNI = Convert.ToString(reader["Dni_Chofer"]);
                            entidad.Nombres = Convert.ToString(reader["nomb_Chofer"]);
                            lista.Add(entidad);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
                //throw;
            }
            finally
            {
                conexion.Close();
            }
            return lista;
        }


        public TrabajadoresEntity ObtenerEmpleado(string EmpCodigo)
        {
            //servidor.CadenaConexion = CadenaConexion;
            List<TrabajadoresEntity> lista = new List<TrabajadoresEntity>();
            SqlConnection conexion = null;
            try
            {
                using (conexion = new SqlConnection(ConexionRutaBDAlertas))
                {
                    if (conexion.State != ConnectionState.Open)
                        conexion.Open();
                    using (SqlCommand comando = new SqlCommand("[dbo].[Usp_Tb_Trabajadores_Dni]", conexion))
                    {
                        comando.CommandType = System.Data.CommandType.StoredProcedure;
                        comando.Parameters.Add(new SqlParameter("@codigo", EmpCodigo));
                        //conexion.Open();
                        //SqlDataReader reader = comando.ExecuteReader();
                        using (SqlDataReader dr = comando.ExecuteReader())
                        {
                            if (dr.Read())
                                return new TrabajadoresEntity()
                                {
                                    DNI = DataReader.GetStringValue(dr, "dni")
                                };
                            dr.Close();
                        }
                        comando.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                //throw;
            }
            finally
            {
                conexion.Close();
            }
            return null;
        }

        public string ObtenerEmpleado2(string EmpCodigo)
        {
            //servidor.CadenaConexion = CadenaConexion;
            List<TrabajadoresEntity> lista = new List<TrabajadoresEntity>();
            SqlConnection conexion = null;
            string DNI = "";
            try
            {
                using (conexion = new SqlConnection(ConexionRutaBDAlertas))
                {
                    if (conexion.State != ConnectionState.Open)
                        conexion.Open();
                    using (SqlCommand comando = new SqlCommand("[dbo].[Usp_Tb_Trabajadores_Dni]", conexion))
                    {
                        comando.CommandType = System.Data.CommandType.StoredProcedure;
                        comando.Parameters.Add(new SqlParameter("@codigo", EmpCodigo));
                        //conexion.Open();
                        //SqlDataReader reader = comando.ExecuteReader();
                        using (SqlDataReader dr = comando.ExecuteReader())
                        {
                            if (dr.Read())
                                //return new TrabajadoresEntity()
                                //{
                                DNI = Convert.ToString(dr["dni"]);
                            //};
                            //dr.Close();
                        }
                        comando.Dispose();
                        //while (reader.Read())
                        //{
                        //    TrabajadoresEntity entidad = new TrabajadoresEntity();
                        //    //entidad.EmpCodigo = Convert.ToString(reader["EmpCodigo"]);
                        //    entidad.DNI = Convert.ToString(reader["dni"]);
                        //    //entidad.Nombres = Convert.ToString(reader["nomb_Chofer"]);
                        //    lista.Add(entidad);
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                //throw;
            }
            finally
            {
                conexion.Close();
            }
            return DNI;
        }


        //====================================================================
        //**** FINALIZAR HOJA DE RUTA
        #region FINALIZAR HOJA DE RUTA
        public List<HojaRutaEntity> LitsFinalizarHojaRuta(int EmpresaId, string FechaInicio, string FechaFinal
            , string Placa, string NroHojaRuta, int CODI_SUCURSAL, int Codi_Ruta, int Estado)
        {

            //servidor.CadenaConexion = CadenaConexion;
            List<HojaRutaEntity> lista = new List<HojaRutaEntity>();
            SqlConnection conexion = null;
            try
            {
                using (conexion = new SqlConnection(ConexionRutaBDAlertas))
                {
                    if (conexion.State != ConnectionState.Open)
                        conexion.Open();
                    using (SqlCommand comando = new SqlCommand("[dbo].[Usp_tb_hojarutaCodiProg_New2]", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        ////comando.Parameters.Add("@BASE_PASAJE", SqlDbType.VarChar, 10).Value = BASE_PASAJE != null ? BASE_PASAJE : (object)DBNull.Value;
                        comando.Parameters.Add("@Codi_Empresa", SqlDbType.Int).Value = EmpresaId > 0 ? EmpresaId : (object)0;
                        comando.Parameters.Add("@Fecha", SqlDbType.VarChar, 10).Value = FechaInicio;
                        comando.Parameters.Add("@Fecha2", SqlDbType.VarChar, 10).Value = FechaFinal;
                        comando.Parameters.Add("@VehiculoId", SqlDbType.VarChar, 15).Value = Placa != null ? Placa : (object)DBNull.Value;
                        comando.Parameters.Add("@Nro_HojaRuta", SqlDbType.VarChar, 15).Value = NroHojaRuta != null ? NroHojaRuta : (object)DBNull.Value;
                        comando.Parameters.Add("@CODI_SUCURSAL", SqlDbType.Int).Value = CODI_SUCURSAL > 0 ? CODI_SUCURSAL : (object)0;
                        comando.Parameters.Add("@Codi_Ruta", SqlDbType.Int).Value = Codi_Ruta > 0 ? Codi_Ruta : (object)0;
                        comando.Parameters.Add("@Estado", SqlDbType.Int).Value = Estado > 0 ? Estado : (object)0;
                        SqlDataReader reader = comando.ExecuteReader();
                        while (reader.Read())
                        {
                            HojaRutaEntity entidad = new HojaRutaEntity();
                            entidad.IdHojaRuta = Convert.ToInt32(reader["IdHojaRuta"]);
                            //entidad.Codi_Empresa = Convert.ToInt32(reader["Codi_Empresa"]);
                            entidad.HojaRuta = Convert.ToString(reader["Nro_HojaRuta"]);
                            entidad.Nro_Ruta = Convert.ToString(reader["Nro_Ruta"]);
                            entidad.Programacion = Convert.ToInt32(reader["Codi_Programacion"]);
                            entidad.OrigenDes = Convert.ToString(reader["Origen"]);
                            entidad.DestinoDes = Convert.ToString(reader["Destino"]);
                            entidad.Fech_programacion = DataReader.GetDateTimeValue(reader, "Fech_programacion");
                            entidad.hora_programacion = DataReader.GetStringValue(reader, "hora_programacion");
                            entidad.Fecha_arriboStr = Convert.ToString(reader["Fecha_arribo"]);
                            entidad.hora_arribo = Convert.ToString(reader["hora_arribo"]);
                            entidad.VehiculoId = Convert.ToString(reader["VehiculoId"]);
                            entidad.IdOrigen = Convert.ToString(reader["IdOrigen"]);
                            entidad.IdDestino = Convert.ToString(reader["IdDestino"]);
                            entidad.FechaOrigenStr = Convert.ToString(reader["FechaOrigen"]);
                            entidad.horaOrigen = Convert.ToString(reader["horaOrigen"]);
                            entidad.FechaLlegadaStr = Convert.ToString(reader["FechaLlegada"]);
                            entidad.HoraLlegada = Convert.ToString(reader["HoraLlegada"]);
                            entidad.FechaFin = Convert.ToDateTime(reader["FechaFin"]);
                            entidad.FechaFinStr = Convert.ToString(reader["FechaFin"]);
                            entidad.HoraFin = Convert.ToString(reader["HoraFin"]);
                            entidad.St_Ok_manifiesto = Convert.ToBoolean(reader["St_Ok_manifiesto"]);
                            entidad.St_Ok_Finalizado = Convert.ToBoolean(reader["St_Ok_Finalizado"]);
                            entidad.usr_manifiesto = Convert.ToInt32(reader["usr_manifiesto"]);
                            //entidad.usr_envia = Convert.ToInt32(reader["usr_envia"]);
                            entidad.usr_envia = DataReader.GetIntValue(reader, "usr_envia");
                            //entidad.usr_finaliza = Convert.ToInt32(reader["usr_finaliza"]);
                            entidad.usr_finaliza = DataReader.GetIntValue(reader, "usr_finaliza");
                            entidad.Fecha_SysStr = Convert.ToString(reader["Fecha_Sys"]);
                            entidad.Hora_Sys = Convert.ToString(reader["Hora_Sys"]);
                            entidad.St_Ok_Cancelado = DataReader.GetBooleanValue(reader, "St_Ok_Cancelado");
                            //entidad.St_Ok_Cancelado = Convert.ToBoolean(reader["St_Ok_Cancelado"]);
                            entidad.Usr_cancelado = DataReader.GetIntValue(reader, "Usr_cancelado");
                            //entidad.Usr_cancelado = Convert.ToInt32(reader["Usr_cancelado"]);
                            //entidad.Fecha_cancel = Convert.ToDateTime(reader["Fecha_cancel"]);
                            entidad.Fecha_cancel = DataReader.GetDateTimeValue(reader, "Fecha_cancel");
                            entidad.Codi_Sucursal = DataReader.GetSmallIntValue(reader, "Codi_Sucursal");
                            //entidad.Codi_Sucursal = Convert.ToInt32(reader["Codi_Sucursal"]);
                            entidad.Codi_ruta = DataReader.GetSmallIntValue(reader, "Codi_ruta");
                            //entidad.Codi_ruta = Convert.ToInt32(reader["Codi_ruta"]);
                            
                            //entidad.Fech_programacion = Convert.ToDateTime(reader["Fech_programacion"]);
                            //entidad.hora_programacion = Convert.ToString(reader["hora_programacion"]);
                           
                           
                            lista.Add(entidad);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                //throw;
            }
            finally
            {
                conexion.Close();
            }
            return lista;
        }

        public bool FinalizaHojaRuta(string Nro_HojaRuta, DateTime FechaFin,string HoraFin, int UsrFinaliza)
        {
            bool flag = false;
            SqlConnection conexion = null;
            using (conexion = new SqlConnection(ConexionRutaBDAlertas))
            {
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();
                using (SqlCommand sqlCommand = new SqlCommand("Usp_Tb_HojaRuta_Finalizar", conexion))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@NRO_HOJARUTA", SqlDbType.VarChar, 15).Value = Nro_HojaRuta;
                    sqlCommand.Parameters.Add("@FechaFin", SqlDbType.DateTime).Value = FechaFin;
                    sqlCommand.Parameters.Add("@HoraFin", SqlDbType.VarChar, 10).Value = HoraFin;
                    sqlCommand.Parameters.Add("@usr_finaliza", SqlDbType.Int).Value = UsrFinaliza;
                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.Dispose();
                    flag = true;
                }
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
            return flag;
        }
        #endregion
        //====================================================================

        //public static List<EmpresaEntity> ListEmpresa2(int EmpresaId)
        //{
        //    List<EmpresaEntity> empresaEntityList = new List<EmpresaEntity>();
        //    using (SqlConnection connection = GetConnection.BDALERTAS())
        //    {
        //        if (connection.State != ConnectionState.Open)
        //            connection.Open();
        //        using (SqlCommand sqlCommand = new SqlCommand("Usp_Extraer_MTC_Empresa", connection))
        //        {
        //            sqlCommand.CommandType = CommandType.StoredProcedure;
        //            sqlCommand.Parameters.Add("@EmpresaId", SqlDbType.Int).Value = (object)EmpresaId;
        //            using (SqlDataReader dr = sqlCommand.ExecuteReader())
        //            {
        //                while (dr.Read())
        //                    empresaEntityList.Add(new EmpresaEntity()
        //                    {
        //                        RUC = DataReader.GetStringValue((IDataReader)dr, "RUC"),
        //                        USR_MTC = _CsSeguridad.Desencripta(DataReader.GetStringValue((IDataReader)dr, "USR_MTC"), "JLMSECURITY"),
        //                        CLAVE_MTC = _CsSeguridad.Desencripta(DataReader.GetStringValue((IDataReader)dr, "CLAVE_MTC"), "JLMSECURITY"),
        //                        PARTIDA_MTC = _CsSeguridad.Desencripta(DataReader.GetStringValue((IDataReader)dr, "PARTIDA_MTC"), "JLMSECURITY"),
        //                        user_reniec = _CsSeguridad.Desencripta(DataReader.GetStringValue((IDataReader)dr, "user_reniec"), "JLMSECURITY")
        //                    });
        //                dr.Close();
        //            }
        //            sqlCommand.Dispose();
        //        }
        //        if (connection.State == ConnectionState.Open)
        //            connection.Close();
        //    }
        //    return empresaEntityList;
        //}
    }
}
