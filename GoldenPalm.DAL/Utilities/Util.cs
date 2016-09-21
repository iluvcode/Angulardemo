using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

// This project shared by 
namespace GoldenPalm.DAL
{


    public static class Util
    {


        public static double getPercentage(double Qty, double Total)
        {
            return (Total == 0) ? 0 : Qty * 100 / Total;
        }


        public static string EncryptBase64(String inputString)
        {
            return System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(inputString));


        }

        public static DateTime? ToDateTime(String inputString)
        {
            if (string.IsNullOrEmpty(inputString))
                return null;
            else
                return Convert.ToDateTime(inputString);


        }
        
        public static string getAltValue(string curValue, string value1, string value2)
        {

            if (curValue != value1)
            {
                return value1;
            }
            else
            {
                return value2;
            }

        }


        public static Dictionary<string, string> ToDictionary<T>(List<T> cd, string valueField, string textField)
        {

            return cd.ToDictionary(x => x.GetType().GetProperty(valueField).GetValue(x, null).ToString(), x => x.GetType().GetProperty(textField).GetValue(x, null).ToString());


        }

        public static List<ListColumn> ToListColumn<T>(List<T> cd, string valueField, string textField)
        {

            List<ListColumn> tmp = cd.Select(x => new ListColumn
           {
               text = x.GetType().GetProperty(textField).GetValue(x, null).ToString(),
               value = x.GetType().GetProperty(valueField).GetValue(x, null).ToString()
           }).ToList();

            return tmp;


        }

        public static List<ListColumn> ToListColumnEnc<T>(List<T> cd, string valueField, string textField)
        {

            List<ListColumn> tmp = cd.Select(x => new ListColumn
            {
                text =  x.GetType().GetProperty(textField).GetValue(x, null).ToString(),
                value = UtilWeb.Encrypt(x.GetType().GetProperty(valueField).GetValue(x, null).ToString())
            }).ToList();

            return tmp;


        }

        public static int? ToNullableInt32(this string s)
        {
            int i;
            if (Int32.TryParse(s, out i)) return i;
            return null;
        }

        public static MemoryStream CSV2Stream(this string csvData)
        {
            MemoryStream output = new MemoryStream();
            StreamWriter stringWriter = new StreamWriter(output, System.Text.Encoding.UTF8);

            stringWriter.Write(csvData);

            stringWriter.Flush();
            output.Position = 0;
            return output;
        }


                      

       
    }


    public static class IEnumerableToCSV
    {
        public static string ToCsv<T>(this IEnumerable<T> items)
           where T : class
        {
            StringBuilder csvBuilder = new StringBuilder();
            PropertyInfo[] properties = typeof(T).GetProperties();

            csvBuilder.AppendLine(GetCsvHeaderSorted(properties));

            foreach (T item in items)
            {
                IEnumerable<string> valuesSorted = properties
                .Select(x => new
                {
                    Value = x.GetValue(item, null),
                    Attribute = (CsvColumnAttribute)Attribute.GetCustomAttribute(x, typeof(CsvColumnAttribute), false)
                })
                .Where(x => x.Attribute != null && x.Attribute.Export)
                .OrderBy(x => x.Attribute.Order)
                .Select(x => GetPropertyValueAsString(x.Value));

                string line = String.Join(",", valuesSorted);

                csvBuilder.AppendLine(line);
            }
            return csvBuilder.ToString();
        }


        private static string GetCsvHeaderSorted(PropertyInfo[] propertyInfos)
        {
            IEnumerable<string> headersSorted = propertyInfos
                .Select(x => (CsvColumnAttribute)Attribute.GetCustomAttribute(x, typeof(CsvColumnAttribute), false))
                .Where(x => x != null && x.Export)
                .OrderBy(x => x.Order)
                .Select(x => x.Name);
            return String.Join(",", headersSorted);
        }


        private static string GetPropertyValueAsString(object propertyValue)
        {
            string propertyValueString;

            if (propertyValue == null)
                propertyValueString = "";
            else if (propertyValue is DateTime)
                propertyValueString = ((DateTime)propertyValue).ToString("MM/dd/yyyy");
            else if (propertyValue is int)
                propertyValueString = propertyValue.ToString();
            else if (propertyValue is float)
                propertyValueString = ((float)propertyValue).ToString("#.####"); // format as you need it
            else if (propertyValue is double)
                propertyValueString = ((double)propertyValue).ToString("#.####"); // format as you need it
            else // treat as a string
                propertyValueString = @"""" + propertyValue.ToString().Replace(@"""", @"""""") + @""""; // quotes with 2 quotes

            return propertyValueString;
        }


        private static string ToCsvValue<T>(this T item)
        {
            if (item == null) return "\"\"";

            if (item is string)
            {
                return string.Format("\"{0}\"", item.ToString().Replace("\"", "\\\""));
            }
            double dummy;
            if (double.TryParse(item.ToString(), out dummy))
            {
                return string.Format("{0}", item);
            }
            return string.Format("\"{0}\"", item);
        }



      



    }

}
