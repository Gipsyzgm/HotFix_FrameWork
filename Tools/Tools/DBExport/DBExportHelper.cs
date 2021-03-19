using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Tools
{
    public class DBExportHelper
    {

        //private const string Server_DBTableClass_FilePath = @"GameServer\XGame\DBMgr\Table";                    //生成表存放目录
        //private const string Server_DBWriteTable_FilePath = @"GameServer\XGame\DBMgr\DBWriteTable.cs";     //数据写入table转换文件目录
        //private const string Server_TableName_FilePath = @"GameServer\XGame\DBMgr\TableName.cs";

        /// <summary>
        /// 创建服务端Net相关文件
        /// </summary>
        /// <param name="DBPath">DB配置文件目录</param>
        /// <param name="serverPath">服务器根目录</param>
        /// <param name="callback">执行完回调</param>
        public static void CreateServerDBClassFile(string DBPath, string serverPath, Action callback = null)
        {
            //Utils.DeleteDirectory(Path.Combine(serverPath, Server_DBTableClass_FilePath));
            Utils.DeleteDirectory(Glob.codeOutSetting.ServerDB.OutDBTableClassDir.ToReality());

            DataSet ds = ExcelUtil.ReadExcelSheetData(DBPath);
            List<string[]> tabNames = new List<string[]>();
            foreach (DataTable dt in ds.Tables)
            {
                string[] tableInfo = null;
                List<string[]> fieldInfo = null; //[字段名，字段类型，字段描述]
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string cellFirst = dt.Rows[i][0].ToString().Trim(); //每行的第一格
                    if (cellFirst == string.Empty|| cellFirst.StartsWith("#"))
                        continue;
                    if (cellFirst.StartsWith("t_")) //一个新的表
                    {
                        if (tableInfo != null) //解析上一个表
                        {
                            parseTabelInfo(tableInfo, fieldInfo, serverPath);
                            tabNames.Add(tableInfo);
                        }
                        bool isSId = cellFirst.EndsWith("$");
                        if(isSId)
                            cellFirst = cellFirst.TrimEnd('$');
                        string[] infos = cellFirst.Split('#');
                        tableInfo = new string[4];
                        tableInfo[0] = infos[0].Replace("t_", "");    //表名
                        tableInfo[1] = dt.Rows[i][1].ToString();       //表描述 
                        if (infos.Length == 2)
                            tableInfo[2] = infos[1];
                        else
                            tableInfo[2] = string.Empty;
                        tableInfo[3] = isSId?"1":string.Empty;

                        fieldInfo = new List<string[]>();
                        continue;
                    }
                    if (tableInfo != null)
                    {
                        fieldInfo.Add(new string[] { dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString() });
                    } 
                }
                //单个Sheet读完，解析最后一个表
                if (tableInfo != null)
                {
                    parseTabelInfo(tableInfo, fieldInfo, serverPath);
                    tabNames.Add(tableInfo);
                }          
            }
            createDBWriteTableFile(serverPath, tabNames);
            createTableNameFile(serverPath, tabNames);
            createTableDBIndexFile(serverPath, tabNames);
            if (callback != null)
                callback();
        }      

        /// <summary>
        /// 解析Table信息并生成cs结构文件
        /// </summary>
        /// <param name="tableInfo"></param>
        /// <param name="fieldInfo"></param>
        /// <param name="serverPath"></param>
        private static void parseTabelInfo(string[] tableInfo, List<string[]> fieldInfo, string serverPath)
        {
            //string saveFilePath = Path.Combine(serverPath, Server_DBTableClass_FilePath, tableInfo[0] + ".cs");
            string saveFilePath = Path.Combine(Glob.codeOutSetting.ServerDB.OutDBTableClassDir.ToReality(), tableInfo[0] + ".cs");

            StringBuilder fieldStrs = new StringBuilder();
            string fieldStr = string.Empty;
            bool isUseSID = tableInfo[3] == "1";
            for (int i = 0; i < fieldInfo.Count; i++)
            {
                fieldStr = string.Empty;
                if (fieldInfo[i][0] == "id")
                {
                    if (isUseSID)
                    {
                        fieldStr = $@"override {ExcelUtil.GetCSStringType(fieldInfo[i][1])} id
        {{
            get {{ return base.id; }}
            protected set
            {{
                lock (o)
                {{
                    if (base.id == ObjectId.Empty)
                    {{
                        base.id = value;
                        if(!objShortIdList.TryGetValue(base.id,out _shortId))                        
                        {{
                            _shortId = ++_identityShortId;
                            shortObjidList.Add(_identityShortId, base.id);
                            objShortIdList.Add(base.id, _identityShortId);
                        }}
                    }}
                }}
            }}
        }}";
                    }

                }
                else
                    fieldStr = $"{ExcelUtil.GetCSStringType(fieldInfo[i][1])} {fieldInfo[i][0]} {{ get; set; }}";

                string dataTimeField = "";
                switch (fieldInfo[i][1].ToLower())
                {
                    case "date":
                        dataTimeField = "\r\n        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]";
                        break;
                    default: 
                        if(fieldInfo[i][1].ToLower().StartsWith("dictionary"))
                            dataTimeField = "\r\n        [BsonDictionaryOptions(MongoDB.Bson.Serialization.Options.DictionaryRepresentation.ArrayOfArrays)]";
                        break;
                }

                if (fieldStr != string.Empty)
                {
                    //生成属性字段
                    string field = $@"        /// <summary>
        /// {fieldInfo[i][2].Replace("\n", "\r\n        /// ")}
        /// </summary>{dataTimeField}
        public {fieldStr}";
                    fieldStrs.AppendLine(field);
                }
            }
            string str = string.Empty;
            if (isUseSID)
            {
                str = $@"using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Collections.Generic;
using System;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace GameLib.Comm.DBMgr
{{    
    [BsonIgnoreExtraElements]
    public class {tableInfo[0]} : BaseTable
    {{
        private static readonly object o = new object();
        static int _identityShortId = 0;
        private static Dictionary<int, ObjectId> shortObjidList = new Dictionary<int, ObjectId>();
        private static Dictionary<ObjectId, int> objShortIdList = new Dictionary<ObjectId, int>();
        /// <summary>{tableInfo[1]}   isCreadId 是否创建ObjectId/自己创建都要设为true</summary>
        public {tableInfo[0]}(bool isCreadId = false) : base(isCreadId){{}}
        
        /// <summary>{tableInfo[1]}   oid指定ObjectId</summary>
        public {tableInfo[0]}(ObjectId oid) : base(oid){{}}

{fieldStrs.ToString()}
        /// <summary>
        /// 简短Id转ObjectId
        /// </summary>
        /// <param name=""shortid"">简短Id</param>
        /// <returns></returns>
        public static ObjectId ToObjectId(int shortid)
        {{
            ObjectId oid = ObjectId.Empty;
            shortObjidList.TryGetValue(shortid, out oid);
            return oid;
        }}
        /// <summary>
        /// ObjectId转简短Id
        /// </summary>
        /// <param name=""oid"">ObjectId</param>
        /// <returns></returns>
        public static int ToShortId(ObjectId oid)
        {{
            int shortid = 0;
            objShortIdList.TryGetValue(oid, out shortid);
            return shortid;
        }}
    }}
}}";
            }
            else
            {
                str = $@"using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Collections.Generic;
using System;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace GameLib.Comm.DBMgr
{{    
    [BsonIgnoreExtraElements]
    public class {tableInfo[0]} : BaseTable
    {{
        /// <summary>{tableInfo[1]}   isCreadId 是否创建ObjectId/自己创建都要设为true</summary>
        public {tableInfo[0]}(bool isCreadId = false) : base(isCreadId){{}}
        
        /// <summary>{tableInfo[1]}   oid指定ObjectId</summary>
        public {tableInfo[0]}(ObjectId oid) : base(oid){{}}

{fieldStrs.ToString()}       
    }}
}}";
            }
            Utils.SaveFile(saveFilePath, str, true);
        }

        /// <summary>
        /// 生成服务端DBWriteTable.cs文件
        /// </summary>
        /// <param name="serverPath"></param>
        /// <param name="tabNames"></param>
        private static void createDBWriteTableFile(string serverPath, List<string[]> tabNames)
        {
            //string saveFilePath = Path.Combine(serverPath, Server_DBWriteTable_FilePath);
            string saveFilePath = Glob.codeOutSetting.ServerDB.OutDBWriteTableFile.ToReality();

            StringBuilder sbTable = new StringBuilder();
            string strtab = string.Empty;
            for (int i = 0; i < tabNames.Count; i++)
            {
                sbTable.AppendLine($@"                case ""{tabNames[i][0]}""://{tabNames[i][1]}
                    MongoDBHelper.Instance.{{0}}(data as {tabNames[i][0]});
                    break;");

            }

            string str = $@"/// <summary>
/// 工具生成，不要修改
/// 数据库写入操作,跟据表名转换成表对象
/// </summary>
namespace GameLib.Comm.DBMgr
{{
    public partial class DBWrite
    {{
        private void insertDB(ITable data)
        {{
            switch (data.GetType().Name)
            {{
{string.Format(sbTable.ToString(), "Insert")}            }}           
        }}
      
        private void updateDB(ITable data)
        {{
            switch (data.GetType().Name)
            {{
{string.Format(sbTable.ToString(), "Update")}            }}
        }}
      
        private void deleteDB(ITable data)
        {{
            switch (data.GetType().Name)
            {{
{string.Format(sbTable.ToString(), "Delete")}            }}
        }}
    }}
}}";

            Utils.SaveFile(saveFilePath, str, true);
        }

        /// <summary>
        /// 生成服务端TableName.cs文件
        /// </summary>
        /// <param name="serverPath"></param>
        /// <param name="tabNames"></param>
        private static void createTableNameFile(string serverPath, List<string[]> tabNames)
        {
            //string saveFilePath = Path.Combine(serverPath, Server_TableName_FilePath);
            string saveFilePath = Glob.codeOutSetting.ServerDB.OutDBTableNameFile.ToReality();

            StringBuilder sbTable = new StringBuilder();
            string strtab = string.Empty;
            for (int i = 0; i < tabNames.Count; i++)
            {
                sbTable.AppendLine($@"            tableNameDic.Add(""{tabNames[i][0]}"", ""{tabNames[i][1]}"");");

            }

            string str = $@"using System.Collections.Generic;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace GameLib.Comm.DBMgr
{{
    public class TableName
    {{
        private Dictionary<string, string> tableNameDic = new Dictionary<string, string>();
        private static readonly TableName instance = new TableName();
        public static TableName Instance => instance;

        private TableName()
        {{
{sbTable}        }}
        /// <summary>获取有中文名字</summary>
        public string GetName(string name)
        {{
            if (tableNameDic.ContainsKey(name))
                return tableNameDic[name];
            return string.Empty;
        }}
    }}
}}";
            Utils.SaveFile(saveFilePath, str, true);
        }


        /// <summary>
        /// 生成服务端TableDBIndex.cs文件
        /// </summary>
        /// <param name="serverPath"></param>
        /// <param name="tabNames"></param>
        private static void createTableDBIndexFile(string serverPath, List<string[]> tabNames)
        {
            string saveFilePath = Glob.codeOutSetting.ServerDB.OutDBTableDBIndexFile.ToReality();

            StringBuilder sbTable = new StringBuilder();
            string strtab = string.Empty;
            for (int i = 0; i < tabNames.Count; i++)
            {
                if(tabNames[i][2]!=string.Empty)
                    sbTable.AppendLine($@"            tableIndexs.Add(typeof({tabNames[i][0]}), {tabNames[i][2]});");
            }

            string str = $@"using System;
using System.Collections.Generic;
/// <summary>
/// 工具生成，不要修改
/// </summary>
namespace GameLib.Comm.DBMgr
{{
    public class TableDBIndex
    {{
        private Dictionary<Type, int> tableIndexs = new Dictionary<Type, int>();
        private static readonly TableDBIndex instance = new TableDBIndex();
        public static TableDBIndex Instance => instance;

        private TableDBIndex()
        {{
{sbTable}        }}
        /// <summary>
        /// 跟据类型获取DB对应的库索引
        /// </summary>
        public int GetDB(Type type)
        {{
            int index = 0;
            tableIndexs.TryGetValue(type, out index);
            return index;
        }}      
    }}
}}";
            Utils.SaveFile(saveFilePath, str, true);
        }


    }
}
