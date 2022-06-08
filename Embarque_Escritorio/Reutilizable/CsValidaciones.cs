using System.Windows.Forms;
using System;
using System.Globalization;

namespace Presentacion.Reutilizable
{
    public class CsValidaciones
    {


        //La siguiente funcion nos permite verificar si el usuario esta ingresando numeros
        public void SoloNumeros(KeyPressEventArgs e)
        {
            //Preguntamos si el caracter ingresado es numerico y esta comprendido entre 0 y 9. Si no es asi se bloque el caracter ingresado
            if (!(((int)e.KeyChar >= 47 && (int)e.KeyChar <= 57) || ((int)e.KeyChar == 8)))
            {
                e.Handled = true;
            }
        }

        ////INGRESAR SOLO DECIMALES
        //public void SoloDecimales(KeyPressEventArgs e, bool Punto, string Texto)
        //{
        //    if ((int)e.KeyChar == 8) ;
        //    else if ((int)e.KeyChar == 46)
        //    {
        //        if (Punto == false)
        //            e.Handled = true;
        //        else
        //        {
        //            for (int i = 0; i < Texto.Length; i++)
        //            {
        //                if (Texto.Substring(i).IndexOf(".") > 0)
        //                {
        //                    e.Handled = true;
        //                }
        //            }
        //        }
        //    }
        //    else if (!((int)e.KeyChar >= 48 && (int)e.KeyChar <= 57))
        //    {
        //        e.Handled = true;
        //    }
        //}


        //La siguiente funcion nos permite verificar si el usuario esta ingresando letras
        public static void SoloLetras(KeyPressEventArgs e)
        {
            if (!(((int)e.KeyChar >= 97 && (int)e.KeyChar <= 122) || ((int)e.KeyChar >= 'A' && (int)e.KeyChar <= 'Z') || ((int)e.KeyChar == 32) || ((int)e.KeyChar == 8)))
            {
                e.Handled = true;
            }
        }


        public bool VerificaExistenciaDetalle(int Id, string NombreColumna, DataGridView _grilla)
        {
            bool ok = false;
            for (int i = 0; i < _grilla.Rows.Count; i++)
            {
                if (Convert.ToInt32(_grilla.Rows[i].Cells[NombreColumna].Value) == Id)
                {
                    ok = true;
                    break;
                }
            }
            return ok;
        }

        

        public bool VerificaExistenciaDetalleEditar(int Id, string NombreColumna, DataGridView _grilla, int FilaEditar)
        {
            bool ok = false;
            for (int i = 0; i < _grilla.Rows.Count; i++)
            {
                //filaeditar--> ESTA VARIABLE REPRESENTA POSICION QUE SE VA A EDITAR
                if (i != FilaEditar)
                {
                    if (Convert.ToInt32(_grilla.Rows[i].Cells[NombreColumna].Value) == Id)
                    {
                        ok = true;
                        break;
                    }
                }
            }
            return ok;
        }

        public bool VerificaExistenciaDetalleEgreso(int Id, string NombreColumna, decimal Precio, string NombreColumnaPrecio
            , DataGridView _grilla)
        {
            bool ok = false;
            for (int i = 0; i < _grilla.Rows.Count; i++)
            {
                if (Convert.ToInt32(_grilla.Rows[i].Cells[NombreColumna].Value) == Id
                    & Convert.ToDecimal(_grilla.Rows[i].Cells[NombreColumnaPrecio].Value) == Precio)
                {
                    ok = true;
                    break;
                }
            }
            return ok;
        }

        public bool VerificaExistenciaDetalleEditarEgreso(int Id, string NombreColumna, decimal Precio, string NombreColumnaPrecio
            , DataGridView _grilla, int FilaEditar)
        {
            bool ok = false;
            for (int i = 0; i < _grilla.Rows.Count; i++)
            {
                //filaeditar--> ESTA VARIABLE REPRESENTA POSICION QUE SE VA A EDITAR
                if (i != FilaEditar)
                {
                    if (Convert.ToInt32(_grilla.Rows[i].Cells[NombreColumna].Value) == Id
                        & Convert.ToDecimal(_grilla.Rows[i].Cells[NombreColumnaPrecio].Value) == Precio)
                    {
                        ok = true;
                        break;
                    }
                }
            }
            return ok;
        }

        public bool VerificaFilasDetalle(DataGridView _grilla)
        {
            bool ok = false;
            if (_grilla.Rows.Count == 0)
            {
                ok = true;
            }
            return ok;
        }

        //========================================================================
        public bool check_fila_grilla(DataGridView _grilla)
        {
            bool ok = false;
            for (int i = 0; i < _grilla.Rows.Count; i++)
            {
                if (Convert.ToBoolean((_grilla.Rows[i].Cells["Seleccionar"].Value)) == true)
                {
                    ok = true;
                    break;
                }
            }
            return ok;
        }
        //========================================================================

       
        //========================================================================


        //========================================================================
        //========================================================================
        //========================================================================
        //========================================================================
        //========================================================================
        //========================================================================
        //========================================================================

    }
}
