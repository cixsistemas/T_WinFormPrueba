using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Embarque_Escritorio.HojaRuta.Entities
{
   public class TrabajadoresEntity
    {
        public int EmpresaId { get; set; }

        public string EmpTipo { get; set; }

        public string EmpCodigo { get; set; }

        public string Paterno { get; set; }

        public string Materno { get; set; }

        public string Nombres { get; set; }

        public string DNI { get; set; }

        public string EmailPersonal { get; set; }

        public string EmailCorporativo { get; set; }

        public DateTime FecNacimiento { get; set; }

        public DateTime FecIngreso { get; set; }

        public DateTime FecCese { get; set; }

        public DateTime FecTerminoContrato { get; set; }

        public bool TomarComoChofer { get; set; }

        public bool TomarComoTripulante { get; set; }

        public int UsrRegistro { get; set; }

        public DateTime FechaRegistro { get; set; }

        public string HoraRegistro { get; set; }

        public bool Estado { get; set; }

        public string EstadoPlanilla { get; set; }

        public DateTime FeciniVacaciones { get; set; }

        public DateTime FecFinVacaciones { get; set; }
    }
}
