using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericUtilities.Extensions
{   
    public static class StringExtensions
    {       
        public static Byte[] ConvertToByteArray(this String str)
        {
            return str.ConvertToByteArray<UnicodeEncoding>();
        }

        public static Byte[] ConvertToByteArray<T>(this String str) where T : Encoding, new()
        {
            T encoding = new T();
            return encoding.GetBytes(str);
        }

        public static String ConvertByteArrayToString(this Byte[] array)
        {
            return ConvertByteArrayToString<UnicodeEncoding>(array);
        }

        public static String ConvertByteArrayToString<T>(this Byte[] array) where T : Encoding, new()
        {
            if (array != null && array.Length > 0)
            {
                T enc = new T();
                return enc.GetString(array);
            }
            else
                return String.Empty;
        }

        public static Stream ConvertToStream(this String str)
        {
            MemoryStream mem = new MemoryStream();
            StreamWriter stWriter = new StreamWriter(mem);
            stWriter.Write(str);
            stWriter.Flush();
            mem.Position = 0;

            return mem;
        }

        /// <summary>
        /// String para decimal fazendo o tratamento da cultura corrente.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Decimal GetDecimal(this string str)
        {
            decimal number;
            try
            {
                number = decimal.Parse(str.Replace(",", "."), CultureInfo.InvariantCulture);
                return number;
            }
            // All attempts to parse the string have failed; rethrow the exception.
            catch (FormatException e)
            {
                throw new FormatException(String.Format("Unable to parse '{0}'.", str), e);
            }
        }

        public static Decimal GetDecimalDef(this string str, decimal defValue)
        {
            decimal dec = defValue;
            try
            {
                dec = GetDecimal(str);
            }
            catch { dec = defValue; }

            return dec;
        }
    }
}
