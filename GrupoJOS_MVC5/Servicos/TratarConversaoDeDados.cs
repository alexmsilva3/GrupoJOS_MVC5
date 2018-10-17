using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GrupoJOS_MVC5.Servicos
{
    public class TratarConversaoDeDados
    {
        public static string TrataString(object valor)
        {
            try
            {
                return valor.ToString();
            }
            catch
            {
                return "";
            }
        }
        public static long TrataLong(object valor)
        {
            try
            {
                return Convert.ToInt64(valor);
            }
            catch (Exception)
            {

                return 0;
            }
        }
        public static decimal TrataDecimal(object valor)
        {
            try
            {
                return Convert.ToDecimal(valor);
            }
            catch
            {
                return 0;
            }
        }

        public static double TrataDouble(object valor)
        {
            try
            {
                return Convert.ToDouble(valor.ToString().Replace('.', ','));
            }
            catch
            {
                return 0;
            }
        }

        public static int TrataInt(object valor)
        {
            try
            {
                return Convert.ToInt32(valor);
            }
            catch
            {
                return 0;
            }
        }

        public static DateTime TrataDateTime(object valor)
        {
            try
            {

                return Convert.ToDateTime(valor);
            }
            catch
            {
                return DateTime.MinValue;
            }
        }



        public static bool TrataBit(object valor)
        {
            try
            {
                if (valor.ToString() == "0" || valor.ToString().ToLower() == "false")
                    return false;
                else if (valor.ToString() == "1" || valor.ToString().ToLower() == "true")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public static double ConverteParaDouble(string valor)
        {
            return double.Parse(valor.Replace('.', ','));
        }

        public static double ConverteParaDouble(decimal valor)
        {
            return Convert.ToDouble(valor);
        }
    }
}