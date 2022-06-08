using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Embarque_Escritorio
{
    class Encriptar
    {
       
            private string patron_busqueda = "ABCDEFGHIJKLMNÑOPQRSTVWXYZabcdefghijklmnñopqrstvwxyz1234567890";
            private string Patron_encripta = "ABCDEFGHIJKLMNÑOPQRSTVWXYZabcdefghijklmnñopqrstvwxyz1234567890";
            // Los patrones están aquí sin terminar por falta de espacio.

            public string EncriptarCadena(string cadena)
            {
                int idx;
                string result = "";

                for (idx = 0; idx <= cadena.Length - 1; idx++)
                    result += EncriptarCaracter(cadena.Substring(idx, 1), cadena.Length, idx);
                return result;
            }

            private string EncriptarCaracter(string caracter, int variable, int a_indice)
            {
                int indice;

                if (patron_busqueda.IndexOf(caracter) != -1)
                {
                    indice = (patron_busqueda.IndexOf(caracter) + variable + a_indice) % patron_busqueda.Length;
                    return Patron_encripta.Substring(indice, 1);
                    //return;
                }

                return caracter;
            }

            public string DesEncriptarCadena(string cadena)
            {
                int idx;
                string result = "";

                for (idx = 0; idx <= cadena.Length - 1; idx++)
                    result += DesEncriptarCaracter(cadena.Substring(idx, 1), cadena.Length, idx);
                return result;
            }

            private string DesEncriptarCaracter(string caracter, int variable, int a_indice)
            {
                int indice;

                if (Patron_encripta.IndexOf(caracter) != -1)
                {
                    if ((Patron_encripta.IndexOf(caracter) - variable - a_indice) > 0)
                        indice = (Patron_encripta.IndexOf(caracter) - variable - a_indice) % Patron_encripta.Length;
                    else
                        // La línea está cortada por falta de espacio
                        indice = (patron_busqueda.Length) + ((Patron_encripta.IndexOf(caracter) - variable - a_indice) % Patron_encripta.Length);
                    indice = indice % Patron_encripta.Length;
                    return patron_busqueda.Substring(indice, 1);
                }
                else
                    return caracter;
            }

    }
}
