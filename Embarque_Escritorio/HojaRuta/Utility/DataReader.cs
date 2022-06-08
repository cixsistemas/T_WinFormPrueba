using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Embarque_Escritorio.HojaRuta.Utility
{
   public class DataReader
    {
        public static object GetObjectValue(IDataReader dr, string column)
        {
            try
            {
                object obj = dr[column];
                return obj == DBNull.Value ? (object)null : obj;
            }
            catch
            {
                return (object)null;
            }
        }

        public static string GetStringValue(IDataReader dr, string column)
        {
            try
            {
                object objectValue = DataReader.GetObjectValue(dr, column);
                return objectValue == null ? string.Empty : objectValue.ToString().Trim();
            }
            catch
            {
                return string.Empty;
            }
        }

        public static float GetRealValue(IDataReader dr, string column)
        {
            try
            {
                return Convert.ToSingle(DataReader.GetObjectValue(dr, column));
            }
            catch
            {
                return 0.0f;
            }
        }

        public static double GetFloatValue(IDataReader dr, string column)
        {
            try
            {
                return Convert.ToDouble(DataReader.GetObjectValue(dr, column));
            }
            catch
            {
                return 0.0;
            }
        }

        public static Decimal GetDecimalValue(IDataReader dr, string column)
        {
            try
            {
                object objectValue = DataReader.GetObjectValue(dr, column);
                return objectValue == null ? Convert.ToDecimal(0) : Convert.ToDecimal(objectValue);
            }
            catch
            {
                return Convert.ToDecimal(0);
            }
        }

        public static long GetBigIntValue(IDataReader dr, string column)
        {
            try
            {
                object objectValue = DataReader.GetObjectValue(dr, column);
                return objectValue == null ? Convert.ToInt64(0) : Convert.ToInt64(objectValue);
            }
            catch
            {
                return Convert.ToInt64(0);
            }
        }

        public static int GetIntValue(IDataReader dr, string column)
        {
            try
            {
                object objectValue = DataReader.GetObjectValue(dr, column);
                return objectValue == null ? Convert.ToInt32(0) : Convert.ToInt32(objectValue);
            }
            catch
            {
                return Convert.ToInt32(0);
            }
        }

        public static short GetSmallIntValue(IDataReader dr, string column)
        {
            try
            {
                object objectValue = DataReader.GetObjectValue(dr, column);
                return objectValue == null ? Convert.ToInt16(0) : Convert.ToInt16(objectValue);
            }
            catch
            {
                return Convert.ToInt16(0);
            }
        }

        public static byte GetTinyIntValue(IDataReader dr, string column)
        {
            try
            {
                object objectValue = DataReader.GetObjectValue(dr, column);
                return objectValue == null ? Convert.ToByte(0) : Convert.ToByte(objectValue);
            }
            catch
            {
                return Convert.ToByte(0);
            }
        }

        public static DateTime? GetDateTimeValue(IDataReader dr, string column)
        {
            try
            {
                object obj = dr[column];
                return obj == DBNull.Value || obj == null ? new DateTime?() : new DateTime?(Convert.ToDateTime(obj));
            }
            catch
            {
                return new DateTime?();
            }
        }

        public static bool GetBooleanValue(IDataReader dr, string column)
        {
            try
            {
                object objectValue = DataReader.GetObjectValue(dr, column);
                return objectValue != null && Convert.ToBoolean(objectValue);
            }
            catch
            {
                return false;
            }
        }

        public static long GetLongValue(IDataReader dr, string column)
        {
            try
            {
                object objectValue = DataReader.GetObjectValue(dr, column);
                return objectValue == null ? Convert.ToInt64(0) : Convert.ToInt64(objectValue);
            }
            catch
            {
                return Convert.ToInt64(0);
            }
        }

        public static short GetShortValue(IDataReader dr, string column)
        {
            try
            {
                object objectValue = DataReader.GetObjectValue(dr, column);
                return objectValue == null ? Convert.ToInt16(0) : Convert.ToInt16(objectValue);
            }
            catch
            {
                return Convert.ToInt16(0);
            }
        }
    }
}
