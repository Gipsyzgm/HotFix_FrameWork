using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Tools
{
    /// <summary>
    /// CDkey配置枚举
    /// </summary>
    public enum CDKeyFieldName
    {
        Id,
        UseCount,
        Name,
        PFId,
        Num,
        EndTime,
        Items,
        Des
    }

    public class CDKeyHelper
    {
        private static List<DataRow> cdKeyRows = new List<DataRow>();
        /// <summary>
        /// 创建服务端Net相关文件
        /// </summary>
        /// <param name="DBPath">DB配置文件目录</param>
        /// <param name="serverPath">服务器根目录</param>
        /// <param name="callback">执行完回调</param>
        public static List<string> SetCDKeyExcel(string path)
        {
            List<string> cdkList = new List<string>();
            DataSet ds = ExcelUtil.ReadExcelSheetData(path);            
            DataTable dtData = ds.Tables[0];
            if (dtData.TableName.IndexOf("CDKey") == -1)
                return cdkList;
            cdKeyRows = new List<DataRow>();
            for (int r = 4; r < dtData.Rows.Count; r++) //遍历数据行
            {
                if (string.IsNullOrEmpty(dtData.Rows[r][0].ToString()))
                    continue;
                cdKeyRows.Add(dtData.Rows[r]);
            }
          
            for (int i = 0; i < cdKeyRows.Count; i++)
            {
                string str = $"[{cdKeyRows[i][(int)CDKeyFieldName.Id].ToString()}] {cdKeyRows[i][(int)CDKeyFieldName.Name].ToString()}";
                cdkList.Add(str);
            }
            return cdkList;
        }
        /// <summary>
        /// 获取字段值
        /// </summary>
        /// <param name="row"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetRowFieldValue(int row, CDKeyFieldName name)
        {
            string rtn = string.Empty;
            if (row < cdKeyRows.Count)
            {
                try
                {
                    rtn = cdKeyRows[row][(int)name].ToString();
                } catch
                { }
            }
            return rtn;
        }
        /// <summary>
        /// 保存导出CDKey
        /// </summary>
        /// <param name="path"></param>
        /// <param name="name"></param>
        /// <param name="list"></param>

        public static void SaveCDKeyFile(string path,string name, ConcurrentBag<string> list)
        {
            StringBuilder sb = new StringBuilder();
            foreach(string str in list)
                sb.AppendLine(str);
            name = name + ".txt";
            string savePath = Path.Combine(path, name);
            Utils.SaveFile(savePath, sb.ToString().TrimEnd('\n'));
        }
    }
}
