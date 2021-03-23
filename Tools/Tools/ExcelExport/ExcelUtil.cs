using System.Collections.Generic;
using Excel;
using System.IO;
using System;
using System.Data;
using System.Collections;

namespace Tools
{
    public class Lang
    {
        public string key;
    }
    public class ExcelUtil
    {          
        /// <summary>
        /// 获取读取Excel中的数据
        /// </summary>
        /// <param name="filePath">excel文件路径</param>
        /// <returns></returns>
        public static DataSet ReadExcelSheetData(string filePath)
        {
            Dictionary<string, List<List<string>>> listTabs = new Dictionary<string, List<List<string>>>();
            FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            //1. Reading from a binary Excel file ('97-2003 format; *.xls)
            //IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
            //2. Reading from a OpenXml Excel file (2007 format; *.xlsx)
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);       
            return excelReader.AsDataSet();
        }
        /// <summary>
        /// 判断是否导出 true导出   false不导出
        /// </summary>
        /// <param name="name">字段名或表名</param>
        /// <param name="isServer">是否为服务器用</param>
        public static bool isExport(string name, bool isServer)
        {
            if (name == string.Empty)
                return false;
            if (name.StartsWith("n_"))
                return false;
            if (isServer && name.StartsWith("c_"))
                return false;
            if (!isServer && name.StartsWith("s_"))
                return false;
            return true;
        }
        #region 字符串分离到数组
        /// <summary>
        /// 字符串分离到对应类型数组以";"切分
        /// </summary>
        /// <typeparam name="T">数组类型</typeparam>
        /// <param name="str">源字符串</param>
        /// <param name="separator">分隔符</param>
        /// <returns></returns>
        public static List<T> SplitToArr<T>(string str, bool isSetDef = false,char separator = ';')
        {
            if (str == string.Empty || str == null)
            {
                if(isSetDef)
                    return new List<T> {default(T) };
                else
                    return new List<T> { };
            }
            string[] objarr = str.Split(new char[] { separator }, StringSplitOptions.RemoveEmptyEntries);
            List<T> tArr = new List<T>();
            for (int i = 0; i < objarr.Length; i++)
            {
                try
                {
                    if (typeof(T) == typeof(bool)) //bool值为1时也转为true
                        objarr[i] = objarr[i] == "1" ? "true" : objarr[i];
                    if (typeof(T) == typeof(int))
                    {
                        double floatV;
                        double.TryParse(objarr[i], out floatV);                       
                        tArr.Add((T)Convert.ChangeType(floatV, typeof(T)));
                    }
                    else
                        tArr.Add((T)Convert.ChangeType(objarr[i], typeof(T)));
                }
                catch (Exception e)
                {
                    tArr.Add(default(T));
                    Logger.LogError(e.Message, e.StackTrace);
                }
            }
            return tArr;
        }

        /// <summary>
        /// 物品数组分离到数组中以'_'切分
        /// </summary>
        /// <param name="itemsStr">[itemid1_num1,itemid2_num2]</param>
        /// <returns>[[itemid1,num1],[itemid2,num2]]</returns>
        public static List<int[]> SplitToItems(List<string> itemsStr)
        {
            List<int[]> items = new List<int[]>();
            foreach (string itstr in itemsStr)
            {
                List<int> iteminfo = SplitToArr<int>(itstr,false, '_');
                items.Add(iteminfo.ToArray());
            }
            return items;
        }
        #endregion
        /// <summary>
        /// 字符串类型转成Object值
        /// </summary>
        /// <param name="type">字符串类型</param>
        /// <param name="value">字符串值</param>
        /// <param name="error">错误字段信息</param>
        /// <returns></returns>
        public static object GetObjectValue(string type, string value,int fieldCount=1)
        {
            double floatV = 0;
            //是否存在分列数据
            bool isSetDef = fieldCount > 1;
            switch (type)
            {
                case "byte":
                    byte byteVal = 0;
                    byte.TryParse(value, out byteVal);
                    return byteVal;
                case "short":
                    //short shortVal = 0;
                    //short.TryParse(value, out shortVal);
                    double.TryParse(value, out floatV);
                    short shortVal = Convert.ToInt16(floatV);
                    return shortVal;
                case "int":
                    double.TryParse(value, out floatV);
                    int intVal = Convert.ToInt32(floatV);
                    //int.TryParse(value, out intVal);
                    return intVal;
                case "long":
                    long longVal = 0;
                    long.TryParse(value, out longVal);
                    return longVal;
                case "float":
                    float floatVal = 0;
                    float.TryParse(value, out floatVal);
                    return floatVal;
                case "double":
                    double doubleVal = 0;
                    double.TryParse(value, out doubleVal);
                    return doubleVal;
                case "bool":
                    if (value == "1")//bool值为1时也转为true
                        return true;
                    bool boolVal = false;
                    bool.TryParse(value, out boolVal);
                    return boolVal;
                case "date":
                    DateTime dateVal = DateTime.MinValue;
                    DateTime.TryParse(value,out dateVal);
                    return dateVal;
                case "string":
                    return value;
                case "byte[]":
                    return SplitToArr<byte>(value, isSetDef);
                case "short[]":
                    return SplitToArr<short>(value, isSetDef);
                case "int[]":
                    return SplitToArr<int>(value, isSetDef);
                case "long[]":
                    return SplitToArr<long>(value, isSetDef);
                case "float[]":
                    return SplitToArr<float>(value, isSetDef);
                case "double[]":
                    return SplitToArr<double>(value, isSetDef);                    
                case "bool[]":
                    return SplitToArr<bool>(value, isSetDef);
                case "string[]":
                    return SplitToArr<string>(value, isSetDef);
                case "list<int[]>":                    
                    return SplitToItems(SplitToArr<string>(value,isSetDef));
                case "list<list<int[]>>":
                    List<List<int[]>> list = new List<List<int[]>>();
                    list.Add(SplitToItems(SplitToArr<string>(value)));              
                    return list;
                case "lang":
                    return new Lang() { key = value };
                default:                   
                    return null;
            }
        }
        /// <summary>
        /// 追加分列数据
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <param name="obj"></param>
        public static void AddListValue(string type, string value,ref object obj)
        {
            double floatV = 0;
            switch (type)
            {
                case "byte[]":
                    byte byteVal = 0;
                    byte.TryParse(value, out byteVal);
                    (obj as List<byte>).Add(byteVal);
                    break;
                case "short[]":
                    double.TryParse(value, out floatV);
                    short shortVal = Convert.ToInt16(floatV);
                    (obj as List<short>).Add(shortVal);
                    break;
                case "int[]":
                    double.TryParse(value, out floatV);
                    int intVal = Convert.ToInt32(floatV);
                    (obj as List<int>).Add(intVal);
                    break;
                case "long[]":
                    long longVal = 0;
                    long.TryParse(value, out longVal);
                    (obj as List<long>).Add(longVal);
                    break;
                case "float[]":
                    float floatVal = 0;
                    float.TryParse(value, out floatVal);
                    (obj as List<float>).Add(floatVal);
                    break;
                case "double[]":
                    double doubleVal = 0;
                    double.TryParse(value, out doubleVal);
                    (obj as List<double>).Add(doubleVal);
                    break;
                case "bool[]":
                    bool boolVal = false;
                    if (value == "1")//bool值为1时也转为true
                        boolVal =  true;                   
                    bool.TryParse(value, out boolVal);
                    (obj as List<bool>).Add(boolVal);
                    break;
                case "string[]":
                    (obj as List<string>).Add(value);
                    break;
                case "list<int[]>":
                    (obj as List<int[]>).Add(SplitToArr<int>(value,false,'_').ToArray());
                    break;
                case "list<list<int[]>>":
                    (obj as List<List<int[]>>).Add(SplitToItems(SplitToArr<string>(value)));
                    break;
            }
        }

        /// <summary>
        /// 获取字符串类型对应的Type类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Type GetStringType(string type)
        {
            switch (type)
            {
                case "byte":
                    return typeof(byte);
                case "short":
                    return typeof(short);
                case "int":
                    return typeof(int);
                case "long":
                    return typeof(long);
                case "float":
                    return typeof(float);
                case "double":
                    return typeof(double);
                case "bool":
                    return typeof(bool);
                case "date":
                    return typeof(DateTime);
                case "lang":
                    return typeof(Lang);
                case "string":
                    return typeof(string);
                case "byte[]":
                    return typeof(List<byte>);
                case "short[]":
                    return typeof(List<short>);
                case "int[]":
                    return typeof(List<int>);
                case "long[]":
                    return typeof(List<long>);
                case "float[]":
                    return typeof(List<float>);
                case "double[]":
                    return typeof(List<double>);
                case "bool[]":
                    return typeof(List<bool>);
                case "string[]":
                    return typeof(List<string>);
                case "list<int[]>":
                    return typeof(List<int[]>);
                case "list<list<int[]>>":
                    return typeof(List<List<int[]>>);
                default:
                    return typeof(string);
            }
        }    
        /// <summary>
        /// 跟据字符串类型获取C#中的类型字符串
        /// </summary>
        /// <param name="type"></param>
        /// <param name="isNull">是否允许为null</param>
        /// <returns></returns>
        public static string GetCSStringType(string type,bool isNull = true)
        {
            switch (type)
            {
                case "date":
                    return isNull?"DateTime?": "DateTime";
                case "lang":
                    return "Lang";
                case "list<int[]>":
                    return "List<int[]>";
                case "list<list<int[]>>":
                    return "List<List<int[]>>";
                default:
                    return type;
            }
        }

        /// <summary>
        /// 跟据字符串类型获取As3中的类型字符串
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetAs3StringType(string type)
        {
            switch (type)
            {
                case "byte":                    
                case "short":                    
                case "int":
                    return "int";
                case "long":
                case "float":
                    return "Number";
                case "bool":
                    return "Boolean";
                case "date":
                    return "Date";
                case "string":
                    return "String";
                case "byte[]":                   
                case "short[]":                   
                case "int[]":
                    return "Vector.<int>";
                case "long[]":
                case "float[]":
                    return "Vector.<Number>";
                case "bool[]":
                    return "Vector.<Boolean>";
                case "string[]":
                    return "Vector.<String>";
                default:
                    return "String";
            }
        }
    }
}