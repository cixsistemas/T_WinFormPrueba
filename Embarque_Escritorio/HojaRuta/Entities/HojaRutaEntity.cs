using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Embarque_Escritorio.HojaRuta.Entities
{
    public class HojaRutaEntity
    {
        public int IdHojaRuta { get; set; }
        //public int Codi_Empresa { get; set; }
        public string HojaRuta { get; set; }
        public string Nro_Ruta { get; set; }
        public int Programacion { get; set; }
        public string OrigenDes { get; set; }
        public string DestinoDes { get; set; }
        public string VehiculoId { get; set; }
        public DateTime? Fech_programacion { get; set; }
        public string hora_programacion { get; set; }
        public string Fecha_arriboStr { get; set; }
        public string hora_arribo { get; set; }
        public string IdOrigen { get; set; }
        public string IdDestino { get; set; }
        public DateTime? FechaOrigen { get; set; }
        public string FechaOrigenStr { get; set; }
        public string horaOrigen { get; set; }
        public DateTime? FechaLlegada { get; set; }
        public string FechaLlegadaStr { get; set; }
        public string HoraLlegada { get; set; }
        public DateTime? FechaFin { get; set; }
        public string FechaFinStr { get; set; }
        public string HoraFin { get; set; }
        public bool St_Ok_manifiesto { get; set; }
        public bool St_Ok_Enviado { get; set; }
        public bool St_Ok_Finalizado { get; set; }
        public int usr_manifiesto { get; set; }
        public int usr_envia { get; set; }
        public int usr_finaliza { get; set; }
        public DateTime? Fecha_Sys { get; set; }
        public string Fecha_SysStr { get; set; }
        public string Hora_Sys { get; set; }
        public bool St_Ok_Cancelado { get; set; }
        public int Usr_cancelado { get; set; }
        public DateTime? Fecha_cancel { get; set; }
        
        public string RutaDes { get; set; }
        public int Codi_Sucursal { get; set; }
        public int Codi_ruta { get; set; }
      
        public DateTime? Fecha_arribo { get; set; }
       
    }
}
