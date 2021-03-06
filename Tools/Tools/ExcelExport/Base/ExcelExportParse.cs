﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Tools.ExcelExport
{
    public class ExcelSheet
    {
        public string Name;                           //表名
        public string NameDes;                      //表名描述
        public string Interface;                    //需要襀实现接口
        public bool IsVert = false;                 //是否为竖表
        public string Export = string.Empty;    //导出配置 CSG
        public bool IsEncrypt = false;          //是否加密,只对客户端有效
        //数据的属性总数量
        public List<ExcelSheetTableField> Fields = new List<ExcelSheetTableField>();
        public DataTable Table;
        public string FileName;
        public string FullName => $"[{FileName}({Name})]";
        public string ConfigName => Name + "Config";
       
    }

    /// <summary>
    /// 表中的属性
    /// </summary>
    public class ExcelSheetTableField
    {
        public string Name;    //字段名
        public string Des;      //字段描述 
        public string Type;     //字段类型
        public string Export;   //导出配置 
        public string Value;    //字段值(竖表用)
        public int FieldCount;  //分列数量 (>1 分列)
    }

    public class ExcelExportParse
    {

        #region 获取所有配置表信息
        /// <summary>
        /// 获取导出表的Sheet信息
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, ExcelSheet> GetExcleSheet()
        {
            DirectoryInfo folder = new DirectoryInfo(Glob.projectSetting.RealityConfigDir);
            return GetExcleSheetForFileInfos(folder.GetFiles("*.xls*"));
        }
        /// <summary>
        /// 把excel表格数据全读一遍，存成对应数据
        /// </summary>
        /// <param name="fils"></param>
        /// <returns></returns>
        public static Dictionary<string, ExcelSheet> GetExcleSheetForFileInfos(FileInfo[] fils)
        {
            Dictionary<string, ExcelSheet> dicSheet = new Dictionary<string, ExcelSheet>();
            foreach (FileInfo file in fils)
            {
                if (file.Name.StartsWith("~$") || file.Name.StartsWith("_"))
                    continue;
                //获取单配置表中所有表
                DataSet ds = ExcelUtil.ReadExcelSheetData(file.FullName);
                ExcelSheet sheet;
                string[] sheeltInfo;
                string[] sheeltTypeInfo;
                string tableName;
                //是否为竖表
                bool isVert = false;
                foreach (DataTable dt in ds.Tables)
                {
                    #region 设置所有表格结构
                    sheeltInfo = dt.TableName.Split('#');
                    //表格名不包含'#'直接跳过
                    if (sheeltInfo.Length < 2)
                        continue;
                    sheeltTypeInfo = sheeltInfo[0].Split('_');
                    tableName = Utils.ToFirstUpper(sheeltTypeInfo[0]); //表名
                    isVert = false;
                    //表名以_v结尾的表为竖表
                    if (sheeltTypeInfo.Length > 1)
                    {
                        if (sheeltTypeInfo[1].ToLower() == "v")
                            isVert = true;    //竖表                      
                    }
                    if (!dicSheet.TryGetValue(tableName, out sheet))
                    {
                        sheet = new ExcelSheet();
                        sheet.Name = tableName;
                        sheet.NameDes = sheeltInfo[1];  //描述    
                        if (sheeltInfo.Length > 2)
                            sheet.Interface = sheeltInfo[2];  //需要继承接口

                        sheet.IsVert = isVert;
                        sheet.FileName = file.Name;
                        sheet.Table = new DataTable();
                        //设置sheet字段列
                        if (!SetSheetTable(sheet, dt))
                            return null;
                        dicSheet.Add(tableName, sheet);
                    }
                    else
                    {
                        if (sheet.IsVert)
                        {
                            Logger.LogError($"表{sheet.FullName}是竖表结构,不能做分表");
                            return null;
                        }
                        ExcelSheet nSheet = new ExcelSheet();
                        nSheet.Table = new DataTable();
                        nSheet.Name = dt.TableName;
                        //设置sheet字段列
                        if (!SetSheetTable(nSheet, dt))
                            return null;

                        //检测相同的表是否表结构相同
                        if (sheet.IsVert != isVert || !CheckSheetColumns(sheet, nSheet))
                        {
                            Logger.LogError($"表{sheet.FullName}定义的表结构不一至,请检查!!!!,目标表:{dt.TableName}");
                            return null;
                        }
                    }
                    #endregion
                    //增加表记录行
                    AddSheetTableRow(sheet, dt);
                }
            }
            return dicSheet;
        }

        /// <summary>
        /// 判断分表的字段列是否对得上
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="dtSource"></param>
        /// <returns></returns>
        private static bool CheckSheetColumns(ExcelSheet sheet, ExcelSheet nSheet)
        {
            if (sheet.Fields.Count!= nSheet.Fields.Count)
            {
                Logger.LogError($"表{sheet.FullName}和表{nSheet.Name}属性数量不匹配，不影响使用，请核对是否需要调整。");            
            }
            for (int i = 0; i < sheet.Fields.Count; i++)
            {
                if(sheet.Fields[i].Type != nSheet.Fields[i].Type)
                {
                    Logger.LogError($"表{sheet.FullName}{sheet.Fields[i].ToString()}字段类型不一至,目标表:{nSheet.Name}");
                    return false;
                }
                if (sheet.Fields[i].Name != nSheet.Fields[i].Name)
                {
                    Logger.LogError($"表{sheet.FullName}{sheet.Fields[i].ToString()}字段名不一至,目标表:{nSheet.Name}");
                    return false;
                }
            }
            
            return true;
        }
        /// <summary>
        /// 设置表初始数据结构
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="dtSource"></param>
        /// <returns></returns>
        private static bool SetSheetTable(ExcelSheet sheet, DataTable dtSource)
        {
            if (dtSource.Columns.Count > 100)
            {
                Logger.LogError(sheet.FullName + "表列数太多,请检查表格式");
                return false;
            }
            if (sheet.IsVert) //竖表结构
            {
                if (dtSource.Rows.Count < 1 && dtSource.Columns.Count < 5)
                {
                    Logger.LogError(sheet.FullName + "：表格格式不对");
                    return false;
                }
                //增加字段列
                ExcelSheetTableField field;
                int emptyRow = 0;
                for (int i = 1; i < dtSource.Rows.Count; i++)
                {
                    if (dtSource.Rows[i][0].ToString() == string.Empty) //空行判断，连接超过3行空行，后面的不再处理
                    {
                        emptyRow += 1;
                        if (emptyRow > 3) break;
                    }
                    else
                    {
                        emptyRow = 0;
                        field = new ExcelSheetTableField();
                        field.Name = dtSource.Rows[i][0].ToString();   //字段名
                        field.Export = dtSource.Rows[i][1].ToString();  //导出配置
                        field.Type = dtSource.Rows[i][2].ToString().ToLower();    //字段类型
                        field.Value = dtSource.Rows[i][3].ToString();    //字段值
                        field.Des = dtSource.Rows[i][4].ToString();     //字段描述        
                        sheet.Table.Columns.Add(field.Name, ExcelUtil.GetStringType(field.Type));
                        sheet.Fields.Add(field);
                    }
                }
                sheet.Export = dtSource.Rows[1][1].ToString().ToUpper();  //key作为表的导出配置      
                sheet.IsEncrypt = sheet.Export.IndexOf('@') != -1;
                return true;
            }
            else//横表结构
            {
                if (dtSource.Rows.Count < 4 && dtSource.Columns.Count < 2)
                {
                    Logger.LogError(sheet.FullName + "：表格格式不对");
                    return false;
                }
                //增加字段列
                ExcelSheetTableField field = null;
                //组属性
                string[] arrInfo;
                string fieldName;
                for (int i = 0; i < dtSource.Columns.Count; i++)
                {
                    arrInfo = dtSource.Rows[3][i].ToString().Split('#');
                    fieldName = arrInfo[0];
                    if (fieldName == string.Empty)
                        break;
                    if (field == null || (field.Name != fieldName))  //重新创建field字段
                    {
                        field = new ExcelSheetTableField();
                        field.Des = dtSource.Rows[0][i].ToString();     //字段描述
                        field.Export = dtSource.Rows[1][i].ToString();  //导出配置
                        field.Type = dtSource.Rows[2][i].ToString().ToLower();    //字段类型
                        field.Name = fieldName;   //字段名                                              
                        field.FieldCount = 1;
                        sheet.Fields.Add(field);
                        sheet.Table.Columns.Add(field.Name, ExcelUtil.GetStringType(field.Type));
                    }
                    else
                    {                       
                        field.Des += "\n"+dtSource.Rows[0][i].ToString();     //字段描述相加
                        field.FieldCount++;
                    }
                }
                sheet.Export = dtSource.Rows[1][0].ToString().ToUpper();  //key作为表的导出配置   
                sheet.IsEncrypt = sheet.Export.IndexOf('@') != -1;
                return true;
            }
        }
        /// <summary>
        /// 增加表格行数据
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="dtSource"></param>
        private static void AddSheetTableRow(ExcelSheet sheet, DataTable dtSource)
        {
            if (sheet.IsVert)
            {
                DataRow expRow = sheet.Table.NewRow(); //竖表只有一行记录
                ExcelSheetTableField field;
                object val;                
                for (int c = 0; c < sheet.Fields.Count; c++) //遍历数据列
                {
                    field = sheet.Fields[c];                    
                    if (field.Export == string.Empty)
                        continue;
                    val = ExcelUtil.GetObjectValue(field.Type, field.Value);
                    if (val == null)
                    {
                        Logger.LogWarning($"{sheet.FullName}表,{field.Name}字段{field.Type}类型未写转换规则，已转换为string类型");
                        val = string.Empty;
                    }
                    expRow[c] = val;
                }
                sheet.Table.Rows.Add(expRow);
            }
            else
            {
                DataRow expRow;
                ExcelSheetTableField field;
                string cellVal;
                object val;
                int cellIndex = 0;
                for (int i = 4; i < dtSource.Rows.Count; i++)
                {
                    expRow = sheet.Table.NewRow();
                    //表内改行开头为空则跳过
                    if (string.IsNullOrEmpty(dtSource.Rows[i][0].ToString()))
                        continue;
                    //用于区分导出数据的位置，计算分表数据位置。
                    cellIndex = 0;
                    for (int f = 0; f < sheet.Fields.Count; f++) //遍历数据列
                    {                       
                        field = sheet.Fields[f];
                        //如果导出配置为空则跳过
                        if (field.Export == string.Empty)
                        {
                            cellIndex++;
                            continue;
                        }
                        //否则读取表格数据
                        cellVal = dtSource.Rows[i][cellIndex].ToString();
                        cellIndex++;
                        val = ExcelUtil.GetObjectValue(field.Type, cellVal, field.FieldCount);
                        if (val == null)
                        {
                            Logger.LogWarning($"{sheet.FullName}表,{field.Name}字段{field.Type}类型未写转换规则，已转换为string类型");
                            val = string.Empty;
                        }
                        if (field.FieldCount > 1)
                        {                                     
                            for (int k = 1; k < field.FieldCount; k++)
                            {
                                ExcelUtil.AddListValue(field.Type, dtSource.Rows[i][cellIndex].ToString(), ref val);
                                cellIndex++;
                            }
                        }
                        expRow[f] = val;
                    }
                    sheet.Table.Rows.Add(expRow);
                }
            }
        }
        #endregion

        #region 配置表过滤
        /// <summary>
        /// 配置表过滤/相当于重新生成需要的表格属性。
        /// </summary>
        /// <param name="list"></param>
        public static List<ExcelSheet> ExcleSheetFilter(Dictionary<string, ExcelSheet> dicSheet,char filter)
        {
            List<ExcelSheet> list = new List<ExcelSheet>();
            ExcelSheet filterSheet;
            DataTable filterTable;
            List<string> colNames = new List<string>();  //不过滤的列名
            List<ExcelSheetTableField> filelds = new List<ExcelSheetTableField>();
            foreach (ExcelSheet sheet in dicSheet.Values)
            {
                if (sheet.Export.IndexOf(filter) != -1) //表名过滤
                {
                    colNames.Clear();
                    filelds = new List<ExcelSheetTableField>();
                    foreach (ExcelSheetTableField field in sheet.Fields)
                    {
                        if (field.Export.IndexOf(filter) != -1) //字段列过滤
                        {
                            colNames.Add(field.Name);
                            filelds.Add(field);
                        }
                    }
                    //一个新 DataTable 实例，其中包含请求的行和列。
                    //如果为 true，则返回的 DataTable 包含具有与其所有列不同的值的行。 默认值是 false。
                    //一个字符串数组，其中的一个列名称列表将包括在返回的 DataTable 中。 DataTable 包含指定的列，这些列按其在该数组中显示的顺序排列。
                    filterTable = sheet.Table.DefaultView.ToTable(false, colNames.ToArray());
                    filterSheet = new ExcelSheet();
                    filterSheet.Name = sheet.Name;
                    filterSheet.NameDes = sheet.NameDes;
                    filterSheet.IsVert = sheet.IsVert;
                    filterSheet.FileName = sheet.FileName;
                    filterSheet.IsEncrypt = sheet.IsEncrypt;
                    filterSheet.Fields = filelds;
                    filterSheet.Table = filterTable;                   
                    filterSheet.Interface = sheet.Interface;
                    list.Add(filterSheet);
                }
            }
            return list;
        }
        #endregion
    }
}
