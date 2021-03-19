using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.ExcelExport;

namespace Tools
{
    public class CSVUtils
    {
        public static string SerializeCSV(ExcelSheet sheet)
        {
            StringBuilder csv = new StringBuilder();
            StringBuilder row = new StringBuilder();
            string rowString = string.Empty;
            for (var i = 0; i < sheet.Table.Rows.Count; i++)
            {
                row.Clear();               
                for (var j = 0; j < sheet.Fields.Count; j++)
                {
                    row.Append(getSheetFieldValue(sheet.Fields[j], sheet.Table.Rows[i][j]));                  
                    if (j < sheet.Fields.Count - 1)
                        row.Append("∴");
                }
                rowString = row.ToString().Replace("\n", "\\n");
                if (i < sheet.Table.Rows.Count-1)
                    csv.AppendLine(rowString);
                else
                    csv.Append(rowString);
            }
            return csv.ToString();
        }

        protected static string getSheetFieldValue(ExcelSheetTableField field,object value)
        {
            switch (field.Type)
            {
                case "byte":                   
                case "short":                  
                case "int":                   
                case "long":                   
                case "float":                   
                case "double":                 
                case "bool":
                case "string":                    
                    return value.ToString();
                case "date":
                    return value.ToString();
                case "byte[]":
                    return arrayToString(value as List<byte>);
                case "short[]":
                    return arrayToString(value as List<short>);
                case "int[]":
                    return arrayToString(value as List<int>);
                case "long[]":
                    return arrayToString(value as List<long>);
                case "float[]":
                    return arrayToString(value as List<float>);
                case "double[]":
                    return arrayToString(value as List<double>);
                case "bool[]":
                    return arrayToString(value as List<bool>);
                case "string[]":
                    return arrayToString(value as List<string>);
                case "list<int[]>":
                    return arrayToString(value as List<int[]>);
                // return SplitToItems(SplitToArr<string>(value));
                case "list<list<int[]>>":
                    //List<List<int[]>> list = new List<List<int[]>>();
                    //list.Add(SplitToItems(SplitToArr<string>(value)));
                    return arrayToString(value as List<List<int[]>>);
                   // return list;
                case "lang":
                    return (value as Lang).key;
                    //return new Lang() { key = value };
                default:
                    return value.ToString();
            }
        }

        private static string arrayToString<T>(List<T> list,char separator = ';')
        {
            string str = string.Empty;
            for (int i = 0; i < list.Count; i++)
            {
                str += list[i].ToString();
                if (i < list.Count - 1)
                    str += separator;
            }
            return str;
        }

        private static string arrayToString<T>(T[] list, char separator = ';')
        {
            string str = string.Empty;
            for (int i = 0; i < list.Length; i++)
            {
                str += list[i].ToString();
                if (i < list.Length - 1)
                    str += separator;
            }
            return str;
        }

        private static string arrayToString(List<int[]> list)
        {
            string str = string.Empty;
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < list[i].Length; j++)
                {
                    str += list[i][j].ToString();
                    if (j < list[i].Length - 1)
                        str += "_";
                }
                if (i < list.Count - 1)
                    str += ";";
            }
            return str;
        }

        private static string arrayToString(List<List<int[]>> list)
        {
            string str = string.Empty;
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < list[i].Count; j++)
                {
                    for (int x = 0; x < list[i][j].Length; x++)
                    {
                        str += list[i][j][x].ToString();
                        if (x < list[i][j].Length - 1)
                            str += "_";
                    }
                    if (j < list[i].Count - 1)
                        str += ";";
                }
                if (i < list.Count - 1)
                    str += "#";
            }
            return str;
        }


        public static string GetExportFunction(ExcelSheetTableField field,int index)
        {
            switch (field.Type)
            {               
                case "byte":                   
                case "short":                    
                case "int":                    
                case "long":
                case "float":
                case "double":                   
                case "bool":
                    return $"            {field.Name} = {field.Type}.Parse(args[{index}]);";
                case "string":
                    return $"            {field.Name} = args[{index}];";
                case "date":
                    return $"            {field.Name} = DateTime.Parse(args[{index}]);";
                case "byte[]":                   
                case "short[]":
                case "int[]":
                case "long[]":
                case "float[]":
                case "double[]":
                case "bool[]":
                case "string[]":
                    return $"            {field.Name} = CSF.ConfigUtils.GetArray_{field.Type.Replace("[]","")}(args[{index}]);";
                case "list<int[]>":
                    return $"            {field.Name} = CSF.ConfigUtils.GetListArray(args[{index}]);";
                case "list<list<int[]>>":
                    return $"            {field.Name} = CSF.ConfigUtils.GetListListArray(args[{index}]);";
                case "lang":
                    return $"            {field.Name} = new Lang(args[{index}]);";
                //return new Lang() { key = value };
                default:
                    return "Error";
            }
        }
    }
}
