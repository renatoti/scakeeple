using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Globalization;
using System.Reflection;

namespace GenericUtilities.Extensions
{
    public static class ObjectExtension
    {

        public static T GetAttributeFrom<T>(this object instance, string propertyName) where T : Attribute
        {
            var attrType = typeof(T);
            var property = instance.GetType().GetProperty(propertyName);
            return (T)property.GetCustomAttributes(attrType, false).First();
        }

        public static T GetCustomAttribute<T>(this object instance) where T : Attribute
        {
            return (T)instance.GetType().GetCustomAttributes(typeof(T), false).First();
        }

        public static String GetTableName(this object instance)
        {
            return instance.GetCustomAttribute<TableAttribute>().Name;
        }

        /// <summary>
        /// Executa um método de um determinado objeto pelo nome
        /// </summary>
        /// <param name="obj">Objeto a ser invoacod</param>
        /// <param name="methodName">Nome do Método</param>
        /// <param name="args">lista de parametros (não obrigatório)</param>
        public static Object InvokeMethodByName(this Object obj, String methodName, params Object[] args)
        {
            try
            {
                return obj.GetType().InvokeMember(methodName, BindingFlags.InvokeMethod, null, obj, args);
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                    throw new Exception(e.InnerException.Message);
                else
                    throw new Exception("InvokeMethodByName: O método chamado, não existe ou não está publico na classe ou " + e.Message);
            }
        }

        /// <summary>
        /// Executa um método genérico de um determinado objeto pelo nome
        /// </summary>
        /// <param name="obj">Objeto a ser invoacod</param>
        /// <param name="methodName">Nome do Método</param>
        /// <param name="GenericParameter">Tipo do atributo genérico da função</param>
        /// <param name="args">lista de parametros (não obrigatório)</param>
        public static Object InvokeGenericMethodByName(this Object obj, String methodName, Type GenericParameter, params Object[] args)
        {
            try
            {
                //typeof(DataService).GetMethod("GetSQLList").MakeGenericMethod(manyAttr.ClassType).Invoke(ds, new object[] { sql });
                return obj.GetType().GetMethod(methodName).MakeGenericMethod(GenericParameter).Invoke(obj, args);
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                    throw new Exception(e.InnerException.Message);
                else
                    throw new Exception("InvokeGenericMethodByName: O método chamado, não existe ou não está publico na classe ou " + e.Message);
            }
        }

        /// <summary>
        /// Executa um método de um determinado objeto pelo nome, sem disparar excessão
        /// </summary>
        /// <param name="obj">Objeto a ser invoacod</param>
        /// <param name="methodName">Nome do Método</param>
        /// <param name="args">lista de parametros (não obrigatório)</param>
        public static Object InvokeMethodByNameNoException(this Object obj, String methodName, params Object[] args)
        {
            try
            {
                return obj.InvokeMethodByName(methodName, args);
            }
            catch
            {
                return null;
            }
        }

        #region Métdos de Conversão

        /// <summary>
        /// Retorna se a string é um número decimal
        /// </summary>
        /// <param name="str">string a ser verificada</param>
        /// <returns>Se a string é um número</returns>
        public static bool IsDecimalNumber(this object obj)
        {
            bool ret = false;

            try
            {
                Convert.ToDecimal(obj);
                ret = true;
            }
            catch
            {
            }

            return ret;
        }

        /// <summary>
        /// Tenta converter o objeto em decimal
        /// </summary>        
        /// <returns></returns>
        public static decimal ConvertToDecimal(this object obj)
        {
            decimal ret = 0;

            try
            {
                ret = Convert.ToDecimal(obj);
            }
            catch
            {
            }

            return ret;
        }

        /// <summary>
        /// Tenta converter o objeto em decimal
        /// </summary>        
        /// <returns></returns>
        public static decimal ConvertToDecimalDef(this object obj, Decimal defaultValue)
        {
            decimal ret = defaultValue;

            try
            {
                ret = Convert.ToDecimal(obj);
            }
            catch
            {
            }

            return ret;
        }

        /// <summary>
        /// Tenta converter o objeto em DateTime
        /// </summary>        
        /// <returns></returns>
        public static DateTime ConvertToDateTime(this object obj)
        {
            DateTime ret = default(DateTime);

            try
            {
                CultureInfo ci = new CultureInfo("pt-BR", true);
                ci.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
                ci.DateTimeFormat.ShortTimePattern = "HH:mm:ss";
                ret = Convert.ToDateTime(obj, ci);
            }
            catch
            {
            }

            return ret;
        }

        /// <summary> 
        /// Tenta converter o objeto em DateTime
        /// </summary>
        /// <param name="defaultValue">Valor default caso a conersão não seja bem sucedida</param>
        /// <returns></returns>
        public static DateTime ConvertToDateTime(this object obj, DateTime defaultValue)
        {
            DateTime ret = defaultValue;

            try
            {
                CultureInfo ci = new CultureInfo("pt-BR", true);
                ci.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
                ci.DateTimeFormat.ShortTimePattern = "HH:mm:ss";
                ret = Convert.ToDateTime(obj, ci);
            }
            catch
            {
            }

            return ret;
        }

        /// <summary>
        /// Tenta converter o objeto em Interio
        /// </summary>        
        /// <returns></returns>
        public static Int32 ConvertToInt(this object obj)
        {
            Int32 ret = 0;
            try
            {
                ret = Convert.ToInt32(obj);
            }
            catch
            {
            }
            return ret;
        }

        /// <summary>
        /// Tenta converter o objeto em Inteiro
        /// </summary>
        /// <param name="defaultValue">Valor default caso a conversão não seja bem sucedida</param>
        /// <returns></returns>
        public static Int32 ConvertToIntDef(this object obj, Int32 defaultValue)
        {
            Int32 ret = defaultValue;
            try
            {
                ret = Convert.ToInt32(obj);
            }
            catch
            {
            }
            return ret;
        }

        public static Int64 ConvertToInt64Def(this object obj, Int64 defaultValue)
        {
            Int64 ret = defaultValue;
            try
            {
                ret = Convert.ToInt64(obj);
            }
            catch
            {
            }
            return ret;
        }
        public static Int64 ConvertToInt64(this object obj)
        {
            return ConvertToInt64Def(obj, 0);
        }

        /// <summary>
        /// Tenta converter o objeto em Double
        /// </summary>
        /// <param name="defaultValue">Valor default caso a conversão não seja bem sucedida</param>
        /// <returns></returns>
        public static Double ConvertToDoubleDef(this object obj, Double defaultValue)
        {
            Double ret = defaultValue;
            try
            {
                ret = Convert.ToDouble(obj);
            }
            catch
            {
            }
            return ret;
        }

        /// <summary>
        /// Tenta converter o objeto em Boolean
        /// </summary>
        /// <param name="defaultValue">Valor default caso a conversão não seja bem sucedida</param>
        /// <returns></returns>
        public static bool ConvertToBool(this object obj)
        {
            bool ret = false;
            try
            {
                ret = Convert.ToBoolean(obj);
            }
            catch
            {
            }
            return ret;
        }

        /// <summary> 
        /// Tenta converter o objeto em Boolean
        /// </summary>
        /// <param name="defaultValue">Valor default caso a conversão não seja bem sucedida</param>
        /// <returns></returns>
        public static bool ConvertToBoolDef(this object obj, bool defaultValue)
        {
            bool ret = defaultValue;
            try
            {
                ret = Convert.ToBoolean(obj);
            }
            catch
            {
            }
            return ret;
        }

        /// <summary>
        /// Tenta converter o objeto em String
        /// </summary>
        /// <param name="defaultValue">Valor default caso a conversão não seja bem sucedida</param>
        /// <returns></returns>
        public static String ConvertToString(this object obj)
        {
            String ret = String.Empty;
            try
            {
                ret = obj.ToString();
            }
            catch
            {
            }
            return ret;
        }

        /// <summary>
        /// Remove duplicidade de uma lista
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<T> Distinct<T>(IEnumerable<T> source)
        {
            List<T> uniques = new List<T>();
            foreach (T item in source)
            {
                if (!uniques.Contains(item)) uniques.Add(item);
            }
            return uniques;
        }

        /// <summary>
        /// Remove duplicidade de uma lista de objetos
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> knownKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (knownKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
        #endregion


        /// <summary>
        /// Pega propriedade de um derminado obejeto em função do seu nome
        /// </summary>
        /// <param name="obj">Objeto que possui a propriedadea</param>
        /// <param name="name">Nome da Propriedade</param>
        /// <returns>Propriedade</returns>
        public static PropertyInfo GetPropertyByName(this Object obj, String name)
        {
            var property = (from p in obj.GetType().GetProperties()
                            where p.Name.ToUpper().Trim() == name.ToUpper().Trim()
                            select p).FirstOrDefault<PropertyInfo>();
            return property;
        }

        /// <summary>
        /// Pega o valor de uma propriedade de um derminado obejeto em função do seu nome
        /// </summary>
        /// <param name="obj">Objeto que possui a propriedadea</param>
        /// <param name="name">Nome da Propriedade</param>
        /// <returns>Propriedade</returns>
        public static Object GetPropertyValue(this Object obj, String name)
        {
            Object value = null;

            var property = GetPropertyByName(obj, name);
            if (property != null)
                value = property.GetValue(obj, null);

            return value;
        }
    }
}
