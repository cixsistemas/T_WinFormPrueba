using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Embarque_Escritorio.HojaRuta.Others
{
    public class Response<T>
    {
        //public bool EsCorrecto { get; set; }
        //public bool Estado { get; set; }
        //public string Mensaje { get; set; }
        //public T Valor { set; get; }

        //public Response(bool esCorrecto, T valor, string mensaje, bool estado);
       
        public bool EsCorrecto { get; set; }
        public bool Estado { get; set; }
        public string Mensaje { get; set; }
        public T Valor { get; set; }

        public Response(bool esCorrecto)
        {
            this.EsCorrecto = esCorrecto;
        }

        public Response(bool esCorrecto, T valor)
              : this(esCorrecto)
        {
            this.Valor = valor;
        }

        public Response(bool esCorrecto, T valor, string mensaje)
          : this(esCorrecto)
        {
            this.Valor = valor;
            this.Mensaje = mensaje;
        }

        public Response(bool esCorrecto, T valor, string mensaje, bool estado)
          : this(esCorrecto)
        {
            this.Valor = valor;
            this.Mensaje = mensaje;
            this.Estado = estado;
        }

    }
}
