// Decompiled with JetBrains decompiler
// Type: SeguridadJelaf.Seguridad
// Assembly: SeguridadJelaf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 664FB724-E747-41A9-A08F-1F361CA38E43
// Assembly location: D:\Web\SiscomWeb\ServiceMTC\bin\SeguridadJelaf.dll

using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Embarque_Escritorio.Clases
{
    public class CsSeguridad
    {
        private const string clave = "JLM_2010/*SEGURIDAD";
        //int num1 = 0;
        private string Mensaje;
        public string ObtenerMensaje()
        { return Mensaje; }
        public string Encripta(string Unacadena, string Unallave)
        {
            long num1 = 0;
            int[,] Llave = new int[checked((int)num1 + 1), checked((int)num1 + 1)];
            string str = "";
            if (Operators.CompareString(Unacadena, "", false) != 0 & Operators.CompareString(Unallave, "", false) != 0)
            {
                if (GeneraLlave(Unallave, ref Llave))
                {
                    long num2 = (long)Strings.Len(Unacadena);
                    long Start = 1;
                    while (Start <= num2)
                    {
                        str += Conversions.ToString(Strings.Chr(Llave[checked((int)unchecked(Start % (long)checked(Strings.Len(Unallave) - 1))), Strings.Asc(Strings.Mid(Unacadena, checked((int)Start), 1))]));
                        checked { ++Start; }
                    }
                }
                else
                {
                    int num3 = (int)Interaction.MsgBox((object)"La llave debe tener al menos dos caracteres.");
                }
            }
            return str;
        }


        public string Desencripta(string UnaCadena, string UnaLlave)
        {
            int num1 = 0;
            string str1;
            int num2 = 0;
            try
            {
                ProjectData.ClearProjectError();
                num1 = 2;
                long num3 = 0;
                int[,] Llave = new int[checked((int)num3 + 1), checked((int)num3 + 1)];
                string str2 = "";
                if (Operators.CompareString(UnaCadena, "", false) != 0)
                {
                    if (GeneraLlave(UnaLlave, ref Llave))
                    {
                        long num4 = (long)Strings.Len(UnaCadena);
                        long Start = 1;
                        while (Start <= num4)
                        {
                            int CharCode = checked(Strings.Asc(Strings.Mid(UnaCadena, (int)Start, 1)) - Llave[(int)unchecked(Start % (long)checked(Strings.Len(UnaLlave) - 1)), 0]);
                            str2 = CharCode <= 0 ? str2 + Conversions.ToString(Strings.Chr(checked(CharCode + (int)byte.MaxValue))) : str2 + Conversions.ToString(Strings.Chr(CharCode));
                            checked { ++Start; }
                        }
                    }
                    else
                    {
                        int num5 = (int)Interaction.MsgBox((object)"La llave debe tener al menos dos caracteres.");
                    }
                }
                str1 = str2;
                goto label_14;
            //label_10:
                num2 = -1;
                switch (num1)
                {
                    case 2:
                        int num6 = (int)Interaction.MsgBox((object)(Conversions.ToString(Information.Err().Number) + " - " + Information.Err().Description), MsgBoxStyle.Critical, (object)"Error al Desencriptar");
                        Information.Err().Clear();
                        goto label_14;
                }
            }
            catch (Exception ex) when (ex is Exception & num1 != 0 & num2 == 0)
            {
                ProjectData.SetProjectError(ex);
                Mensaje = ex.Message.ToString();
                //goto label_10;
            }
            throw ProjectData.CreateProjectError(-2146828237);
        label_14:
            if (num2 != 0)
                ProjectData.ClearProjectError();
            return str1;
        }

        public bool GeneraLlave(string UnaLlave, ref int[,] Llave)
        {
            bool flag;
            try
            {
                if (Strings.Len(UnaLlave) > 1)
                {
                    Llave = new int[checked(Strings.Len(UnaLlave) - 1 + 1), (int)byte.MaxValue];
                    int num1 = Strings.Len(UnaLlave);
                    int Start = 1;
                    while (Start <= num1)
                    {
                        Llave[checked(Start - 1), 0] = Strings.Asc(Strings.Mid(UnaLlave, Start, 1));
                        checked { ++Start; }
                    }
                    int num2 = checked(Strings.Len(UnaLlave) - 1);
                    int index1 = 0;
                    while (index1 <= num2)
                    {
                        int index2 = 1;
                        do
                        {
                            int num3 = checked(Llave[index1, index2 - 1] + 1);
                            if (num3 == 256)
                                num3 = 1;
                            Llave[index1, index2] = num3;
                            checked { ++index2; }
                        }
                        while (index2 <= 254);
                        checked { ++index1; }
                    }
                    flag = true;
                }
                else
                    flag = false;
            }
            catch (Exception ex)
            {
                ProjectData.SetProjectError(ex);
                int num = (int)Interaction.MsgBox((object)Information.Err().Description);
                flag = false;
                ProjectData.ClearProjectError();
            }
            return flag;
        }
    }

}
