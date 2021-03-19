using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data;
using System.Xml;
using System.Text.RegularExpressions;
using System.IO;
using System.Diagnostics;
using Newtonsoft.Json;
using Tools.ExcelExport;

namespace Tools
{
    public class SQLiteServer
    {
        
        private string _dbName = "";
        private SQLiteConnection connection = null;     //连接对象
        private SQLiteTransaction transaction = null;   //事务对象
        private bool _IsRunTrans = false;        //事务运行标识
        private bool _AutoCommit = false; //事务自动提交标识

        /// <summary>
        /// 新建数据库文件
        /// </summary>
        /// <param name="dbPath">数据库文件路径及名称</param>
        /// <returns>新建成功，返回true，否则返回false</returns>
        public bool CreateDB(string dbPath)
        {
            _dbName = dbPath;
            try
            {
                SQLiteConnection.CreateFile(dbPath);               
                //建立连接
                connection = new SQLiteConnection("Data Source=" + dbPath);
                connection.Open();
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError("创建Sqlite文件失败,请尝试停止Unity再试!!!!\n" + ex.Message);
                return false;
            }
        }
        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public void CloseDB()
        {
            if (connection != null && connection.State != ConnectionState.Closed)
            {
                if (_IsRunTrans && _AutoCommit)
                {
                    Logger.LogError("Commit");
                    Commit();
                }
                connection.Close();
                SQLiteConnection.ClearAllPools();
                connection.Dispose();
                transaction.Dispose();
                connection = null;
                transaction = null;
            }
        }


        public void CreateTable(string strCmd)
        {
            using (SQLiteCommand cmd = new SQLiteCommand())
            {
                cmd.Connection = connection;
                // cmd.CommandText = "CREATE TABLE " + tableName + "(Name varchar,Team varchar, Number varchar)";
                cmd.CommandText = strCmd;
                cmd.ExecuteNonQuery();
            }
        }

        public void Insert(string strCmd, SQLiteParameter[] parameters)
        {
            try
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = connection;
                    // cmd.CommandText = "CREATE TABLE " + tableName + "(Name varchar,Team varchar, Number varchar)";
                    cmd.CommandText = strCmd;
                    cmd.Parameters.AddRange(parameters);
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                Logger.LogError("插入错误,可能出现重复ID:"+ parameters[0].Value +"   "+ strCmd);
            }
        }

        public void InsertTable(DataTable table, bool isVert = false, string secondKey = null)
        {
            //Stopwatch sw = new Stopwatch();
            //sw.Start();
            string createField = string.Empty;
            string insertField = string.Empty;
            string insertParm = string.Empty;
            SQLiteParameter[] parameters = new SQLiteParameter[table.Columns.Count];
            for (int i = 0; i < table.Columns.Count; i++)
            {
                var p = table.Columns[i];
                createField += $"'{p.ColumnName}' {SqlType(p)},";
                insertField += $"'{p.ColumnName}',";
                insertParm += "@" + p.ColumnName + ",";
                parameters[i] = new SQLiteParameter("@" + p.ColumnName);
            }
            createField = createField.TrimEnd(',');
            insertField = insertField.TrimEnd(',');
            insertParm = insertParm.TrimEnd(',');
            string createCmd;
            if (!string.IsNullOrEmpty(secondKey))
                secondKey = ",'" + secondKey + "'";
            if (isVert)
                createCmd = $"create table {table.TableName} ({createField})";
            else
                createCmd = $"create table {table.TableName} ({createField}, PRIMARY KEY ('{table.Columns[0].ColumnName}'{secondKey}))";

            CreateTable(createCmd);
            string insertCmd = $"insert into {table.TableName}({insertField}) values({insertParm})";

            foreach (DataRow row in table.Rows)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                    parameters[i].Value = SqlValue(table.Columns[i], row[i]);

                Insert(insertCmd, parameters);
            }
        }

        public void InsertTable(ExcelSheet sheet)
        {
            DataTable table = sheet.Table;
            table.TableName = sheet.ConfigName;
            InsertTable(table, sheet.IsVert);
        }

        //private void sqliteTest()
        //{
        //    SQLiteHelper.CreateDB("H:/test.db");
        //    string createCmd = $"CREATE TABLE test2 (id integer, name varchar)";
        //    SQLiteHelper.CreateTable(createCmd);
        //    Stopwatch sw = new Stopwatch();
        //    sw.Start();
        //    SQLiteHelper.BeginTransaction();
        //    for (int i = 0; i < 10000; i++)
        //    {
        //        string insertCmd = "insert into test2(id,name) values(@id,@name)";
        //        List<SQLiteParameter> parameters = new List<SQLiteParameter>();
        //        parameters.Add(new SQLiteParameter("@id", i));
        //        parameters.Add(new SQLiteParameter("@name", "sssss"));
        //        SQLiteHelper.Insert(insertCmd, parameters.ToArray());
        //    }
        //    SQLiteHelper.Commit();
        //    sw.Stop();
        //    Logger.Log(sw.Elapsed.TotalSeconds);
        //}
        public static string SqlType(DataColumn p)
        {
            var clrType = p.DataType;
            if (clrType == typeof(Boolean) || clrType == typeof(Byte) || clrType == typeof(UInt16) ||
                clrType == typeof(SByte) || clrType == typeof(Int16) || clrType == typeof(Int32))
            {
                return "integer";
            }
            else if (clrType == typeof(UInt32) || clrType == typeof(Int64))
            {
                return "bigint";
            }
            else if (clrType == typeof(Single) || clrType == typeof(Double) || clrType == typeof(Decimal))
            {
                return "float";
            }
            else if (clrType == typeof(String))
            {
                return "varchar";
            }
            else if (clrType == typeof(DateTime))
            {
                return "datetime";
            }
            else if (clrType == typeof(byte[]))
            {
                return "blob";
            }
            else if (clrType == typeof(Guid))
            {
                return "varchar(36)";
            }
            //判断是否是list 或者array
            else if (clrType.FullName.Contains("System.Collections.Generic.List") || clrType.IsArray)
            {
                //                Debug.Log("数组将以json形式保存:" + clrType.Name);
                return "varchar"; //varchar(4000)
            }
            else if (clrType == typeof(Lang))
            {
                return "varchar";
            }
            else
            {
                throw new NotSupportedException("Don't know about " + clrType);
            }
        }

        public static object SqlValue(DataColumn p, object value)
        {
            var clrType = p.DataType;
            //判断是否是list 或者array
             if (clrType.FullName.Contains("System.Collections.Generic.List") || clrType.IsArray|| clrType == typeof(Lang))
            {
                var v = JsonConvert.SerializeObject(value);
                return v;
            }
            else
            {
                return value;
            }
        }

        /*
           internal static void BindParameter(Sqlite3Statement stmt, int index, object value, bool storeDateTimeAsTicks)
        {
            if (value == null)
            {
                SQLite3.BindNull(stmt, index);
            }
            else
            {
                if (value is Int32)
                {
                    SQLite3.BindInt(stmt, index, (int) value);
                }
                else if (value is String)
                {
                    SQLite3.BindText(stmt, index, (string) value, -1, NegativePointer);
                }
                else if (value is Byte || value is UInt16 || value is SByte || value is Int16)
                {
                    SQLite3.BindInt(stmt, index, Convert.ToInt32(value));
                }
                else if (value is Boolean)
                {
                    SQLite3.BindInt(stmt, index, (bool) value ? 1 : 0);
                }
                else if (value is UInt32 || value is Int64)
                {
                    SQLite3.BindInt64(stmt, index, Convert.ToInt64(value));
                }
                else if (value is Single || value is Double || value is Decimal)
                {
                    SQLite3.BindDouble(stmt, index, Convert.ToDouble(value));
                }
                else if (value is TimeSpan)
                {
                    SQLite3.BindInt64(stmt, index, ((TimeSpan) value).Ticks);
                }
                else if (value is DateTime)
                {
                    if (storeDateTimeAsTicks)
                    {
                        SQLite3.BindInt64(stmt, index, ((DateTime) value).Ticks);
                    }
                    else
                    {
                        SQLite3.BindText(stmt, index, ((DateTime) value).ToString("yyyy-MM-dd HH:mm:ss"), -1,
                            NegativePointer);
                    }
                }
                else if (value is DateTimeOffset)
                {
                    SQLite3.BindInt64(stmt, index, ((DateTimeOffset) value).UtcTicks);
#if !NETFX_CORE
                }
                else if (value.GetType().IsEnum)
                {
#else
				} else if (value.GetType().GetTypeInfo().IsEnum) {
#endif
                    SQLite3.BindInt(stmt, index, Convert.ToInt32(value));
                }
                else if (value is byte[])
                {
                    SQLite3.BindBlob(stmt, index, (byte[]) value, ((byte[]) value).Length, NegativePointer);
                }
                else if (value is Guid)
                {
                    SQLite3.BindText(stmt, index, ((Guid) value).ToString(), 72, NegativePointer);
                }
                //数组当成json串存储,文档存储
                else if (value.GetType().FullName.Contains(".List") || value.GetType().IsArray)
                {
                    var v = JsonMapper.ToJson(value);
                    SQLite3.BindText(stmt, index, v, -1, NegativePointer);
                }
                else
                {
                    throw new NotSupportedException("Cannot store type: " + value.GetType());
                }
            }
        }
         */

        /*
         string commandStr = "insert into Stars(Name,Team,Number) values(@name,@team,@number)";
    foreach (var item in stars)
    {
        if (isExist("Name", item[0].ToString()))
        {
            Console.WriteLine(item[0] + "的数据已存在！");
            continue;
        }
        else
        {
            sqlHelper.ExecuteNonQuery(commandStr, item);
            Console.WriteLine(item[0] + "的数据已保存！");
            Console.ReadKey();
        }
    }
    sqlHelper.CloseDbConn();
         
         */

        ///// <summary>
        ///// 创建表
        ///// </summary>
        ///// <param name="dbPath">指定数据库文件</param>
        ///// <param name="tableName">表名称</param>
        //public void NewTable(string dbPath, string tableName)
        //{

        //    SQLiteConnection sqliteConn = new SQLiteConnection("data source=" + dbPath);
        //    if (sqliteConn.State != System.Data.ConnectionState.Open)
        //    {
        //        sqliteConn.Open();
        //        SQLiteCommand cmd = new SQLiteCommand();
        //        cmd.Connection = sqliteConn;
        //        cmd.CommandText = "CREATE TABLE " + tableName + "(Name varchar,Team varchar, Number varchar)";
        //        cmd.ExecuteNonQuery();
        //    }
        //    sqliteConn.Close();
        //}

        /// <summary>
        /// 开始数据库事务
        /// </summary>
        public void BeginTransaction()
        {
            transaction = connection.BeginTransaction();
            _IsRunTrans = true;
        }

        /// <summary>
        /// 开始数据库事务
        /// </summary>
        /// <param name="isoLevel">事务锁级别</param>
        public void BeginTransaction(IsolationLevel isoLevel)
        {
            transaction = connection.BeginTransaction(isoLevel);
            _IsRunTrans = true;
        }

        /// <summary>
        /// 提交当前挂起的事务
        /// </summary>
        public void Commit()
        {
            if (_IsRunTrans)
            {
                transaction.Commit();
                _IsRunTrans = false;
            }
        }


    }
}

/*
  using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT name FROM sqlite_master WHERE type = 'table'";
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        using (SQLiteCommand drop = new SQLiteCommand())
                        {
                            drop.Connection = connection;
                            drop.CommandText = "drop table " + reader["name"];
                            drop.ExecuteNonQuery();
                        }
                    }
                }
 */