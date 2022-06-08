using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Embarque_Escritorio.HojaRuta.Entities
{
  public  class EmpresaEntity
    {
        public int EmpresaId { get; set; }

        public string Empresa { get; set; }

        public string RUC { get; set; }

        public int UsrRegistro { get; set; }

        public DateTime FechaRegistro { get; set; }

        public string HoraRegistro { get; set; }

        public bool Estado { get; set; }

        public string USR_MTC { get; set; }

        public string CLAVE_MTC { get; set; }

        public string PARTIDA_MTC { get; set; }

        public string user_reniec { get; set; }
    }
}
