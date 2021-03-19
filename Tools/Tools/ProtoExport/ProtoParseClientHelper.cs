using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Tools
{
    public class Package
    {
        public string name; //包名
        public List<string> comment = new List<string>();  //包注释
    }
    public class ProtoMessage
    {
        public string name;     //Message名称
        public List<string> comment = new List<string>();  //注释信息
        public List<ProtoMessageField> fields = new List<ProtoMessageField>(); //字段信息
        public List<string> import = new List<string>();    //引用了其它Proto
        public void AddImport(string im) { if (!import.Contains(im)) import.Add(im); }
    }
    public class ProtoMessageField
    {
        public string type;             //字段类型
        public string name;           //字段名
        public string comment;      //字段注释
        public string defaultValue; //默认值
    }

    public class ProtoEnum
    {
        public string name; //枚举名称
        public List<string> comment = new List<string>();  //注释信息
        public List<ProtoEnumItme> items = new List<ProtoEnumItme>();  //注释信息
    }
    public class ProtoEnumItme
    {        
        public string name;           //枚举名
        public string value;           //枚举值
        public string comment;      //枚举注释
    }

    public class ProtoParseClientHelper
    {
        #region 生成客户端带注释的Proto文件
        /// <summary>
        /// 生成客户端带注释的Proto文件
        /// </summary>
        /// <param name="protoFile">proto文件</param>
        /// <param name="savePath">保存路径</param>
        public static void GenerateProtoClinetAs(FileInfo protoFile, string savePath)
        {
            List<string[]> configList = new List<string[]>();
            string className = Utils.ToFirstUpper(protoFile.Name.Split('.')[0]) + "Msg";
            string saveFilePath = Path.Combine(savePath, className + ".as");
            StreamReader sr = File.OpenText(protoFile.FullName);
            string line = "";           
            List<string> comment = new List<string>();

            Package pack = new Package();

            List<ProtoMessage> msgList = new List<ProtoMessage>(); //所有消息结构

            List<ProtoEnum> enumList = new List<ProtoEnum>();   //所有枚举结构

            ProtoMessage currMsg = null;    //当前解析的消息
            ProtoEnum currEnum = null;    //当前解析的消息
            while ((line = sr.ReadLine()) != null)
            {
                //line = Regex.Replace(line, "[\t, ]", "");  //去tab,去空格              
                line = line.TrimStart();
                if (line == string.Empty)
                    continue;
                if (line.StartsWith("{")|| line.StartsWith("syntax"))
                {
                    continue;
                }
                else if (line.StartsWith("}"))
                {
                    currMsg = null;
                    currEnum = null;
                    comment.Clear();
                }
                else if (line.StartsWith("//"))  //注释行
                {
                    comment.Add(line.Replace("//",""));
                }
                if (line.StartsWith("package ")) //包
                {
                    pack.name = line.Replace("package ", "").Trim().TrimEnd(';');
                    pack.comment = new List<string>(comment.ToArray());
                    comment.Clear();
                }
                else if (line.StartsWith("message ")) //消息
                {
                    currMsg = new ProtoMessage();
                    currMsg.comment = new List<string>(comment.ToArray()); ;
                    comment.Clear();
                    currMsg.name = line.Replace("message ", "").Trim();
                    msgList.Add(currMsg);
                }               
                else if (currMsg != null) //解释字段
                {         
                    ProtoMessageField field = new ProtoMessageField();
                    string[] fileInfos = line.Replace("\t"," ").Split(' ');
                    int commsplitIndex = line.IndexOf("//");
                    if (commsplitIndex == 0)
                        continue;
                    if (commsplitIndex > 0) //字段描述
                        field.comment = line.Substring(commsplitIndex + 2);

                    int notEmptyIndex = 0;
                    for (int i = 0; i < fileInfos.Length && notEmptyIndex < 3; i++)
                    {
                        if (fileInfos[i] != string.Empty)
                        {                          
                            if (notEmptyIndex == 0) //第1个是类型
                            {
                                string[] typeinfos = fileInfos[i].Split('.');
                                string type;
                                string import = string.Empty;
                                if (typeinfos.Length > 1)
                                {
                                    import = typeinfos[0];
                                    type = typeinfos[1];
                                }
                                else
                                    type = typeinfos[0];

                                if(type== "repeated")
                                    continue;

                                bool isRtnDef = false;
                                if (line.StartsWith("repeated"))
                                {
                                    field.type = $"Vector.<{getFieldType(type, out isRtnDef)}>";
                                    field.defaultValue = $"new Vector.<{getFieldType(type, out isRtnDef)}>()";
                                }
                                else
                                {
                                    field.type = getFieldType(type, out isRtnDef);
                                    if (field.type == "String")
                                        field.defaultValue = @"""""";
                                    else if (field.type == "Object")
                                        field.comment += "  [Long类型 .toNumber()转Number]";
                                }                                
                                //类型是否引用了其它Proto
                                if (import != string.Empty && isRtnDef)
                                    currMsg.AddImport(import + "." + field.type);
                            }                              
                            else if (notEmptyIndex == 1) //第2个是字段名
                                field.name = fileInfos[i].Split('=')[0];
                            notEmptyIndex++;
                        }
                    }
                    
                    if (field.type.ToLower().StartsWith("enum_"))
                    {
                        field.comment += " 枚举:" + field.type;
                        field.type = "int";   //客户端没有定义枚举的方法,转成int型                       
                    }
                    currMsg.fields.Add(field);
                }                
                else if (currEnum != null)
                {
                    string[] infos = line.Split('=');
                    if (infos.Length < 2)
                        continue;
                    ProtoEnumItme item = new ProtoEnumItme();
                    item.name = infos[0].Trim();
                    string[] vcinfos = infos[1].Split(';');
                    if (vcinfos.Length < 1)
                        continue;
                    item.value = vcinfos[0].Trim();
                    if (vcinfos.Length > 1)
                        item.comment = vcinfos[1].Trim().TrimStart('/');
                    currEnum.items.Add(item);
                }
                else if (line.StartsWith("enum ")) //枚举
                {
                    currEnum = new ProtoEnum();
                    currEnum.comment = new List<string>(comment.ToArray()); ;
                    comment.Clear();
                    currEnum.name = line.Replace("enum ", "").Trim();
                    enumList.Add(currEnum);
                }
            }
            sr.Close();
            for (int i = 0; i < msgList.Count; i++)
                generateMessageClass(msgList[i], pack, savePath);
            for (int i = 0; i < enumList.Count; i++)
                generateEnumClass(enumList[i], pack, savePath);
            
        }
        /// <summary>
        /// 生成message对应的Class文件
        /// </summary>
        private static void generateMessageClass(ProtoMessage msg, Package pack, string savePath )
        {
            string saveFilePath = Path.Combine(savePath, pack.name, msg.name + ".as");
            StringBuilder msgField = new StringBuilder(); //字段   
            string fname = string.Empty;
            ProtoMessageField field;
            for (int i = 0; i < msg.fields.Count; i++)
            {
                field = msg.fields[i];
                string defVal = field.defaultValue == null ? "" : " = " + field.defaultValue;
                msgField.AppendLine($@"		/**{field.comment}*/");                
                msgField.AppendLine($@"		public var {field.name}:{field.type}{defVal};");
            }
            StringBuilder sbImport = new StringBuilder(); //引用   
            for (int i = 0; i < msg.import.Count; i++)
            {
                msgField.AppendLine($@"		import CNet.Protos.{msg.import[i]};");
            }

            string str = $@"/**工具生成不要修改*/
package CNet.Protos.{pack.name}
{{
    import CNet.Net;
{sbImport}
{getComment(pack.comment, "	", msg.comment)}
	public class {msg.name}
	{{
{msgField}	}}
}}";
            Utils.SaveFile(saveFilePath, str, true);
        }
        /// <summary>
        /// 生成Enum对应的Class文件
        /// </summary>
        private static void generateEnumClass(ProtoEnum enu, Package pack, string savePath)
        {
            string saveFilePath = Path.Combine(savePath, pack.name, enu.name + ".as");
            StringBuilder msgField = new StringBuilder(); //字段   
            string fname = string.Empty;
            ProtoEnumItme field;
            for (int i = 0; i < enu.items.Count; i++)
            {
                field = enu.items[i];
                if(i==0)
                    msgField.AppendLine($@"		/**{field.comment} 值为:{field.value}或null*/");
                else
                    msgField.AppendLine($@"		/**{field.comment} 值为:{field.value}*/");
                msgField.AppendLine($@"		public static const {field.name}:int =  {field.value};");
            }
            string str = $@"/**工具生成不要修改*/
package CNet.Protos.{pack.name}
{{
    import CNet.Net;
{getComment(pack.comment, "	", enu.comment)}
	public final class {enu.name}
	{{
{msgField}	}}
}}";
            Utils.SaveFile(saveFilePath, str, true);
        }

        #endregion
        /// <summary>
        /// 生成多行注释
        /// </summary>
        /// <param name="comments">注释内容</param>
        /// <param name="blank">前面加的空格</param>
        /// <returns></returns>
        private static string getComment(List<string> comments, string blank = "",List<string> addComments =null)
        {
            if (comments.Count == 0)
                return string.Empty;

            string rtn = blank + "/**\r\n";
            for (int i = 0; i < comments.Count; i++)
                rtn += blank +" * "+ comments[i] +"</br>\r\n";     

            if (addComments != null)
            {
                for (int i = 0; i < addComments.Count; i++)
                    rtn += blank + " * " + addComments[i] + "</br>\r\n";
            }
            rtn += blank + " */";
            return rtn;
        }


      
        /// <summary>
        /// 获取字段对应as类型
        /// isDefault 是否以默认方式返回的，默认方式返回的是使用了自定义结构
        /// </summary>
        private static string getFieldType(string fieldType,out bool isDefault)
        {
            isDefault = false;
            switch (fieldType)
            {
                case "double":
                case "float":
                    return "Number";
                case "int64":
                case "uint64":
                case "sint64":
                case "fixed64":
                case "sfixed64":
                    return "Object";
                case "int32":
                case "sint32":
                case "fixed32":
                case "sfixed32":
                    return "int";
                case "uint32":
                    return "uint";
                case "bool":
                    return "Boolean";
                case "string":
                    return "String";
                case "bytes":
                    return "ArrayBuffer";
                default:
                    isDefault = true;
                    return fieldType;
            }
        }

    }
   
}


#region MyRegion
//            //开始生成文件内容
//         StringBuilder msgField = new StringBuilder(); //消息带注释定义字符串           
//            StringBuilder msgFieldValue = new StringBuilder(); //消息赋值
//            string fname = string.Empty;
//            for (int i = 0; i < msgList.Count; i++)
//            {
//                fname = msgList[i].name;
//                msgField.AppendLine(getMsgComment(msgList[i], "		"));
//                msgField.AppendLine("		public var " + fname + ":*");
//                msgFieldValue.AppendLine("			" + fname + " = msg." + fname);
//            }


//            //开始生成文件内容
//            StringBuilder enumField = new StringBuilder(); //枚举定义
//            StringBuilder enumClass = new StringBuilder(); //枚举类
//            for (int i = 0; i <enumList.Count; i++)
//            {
//                if (enumField.Length == 0)
//                {
//                    enumField.AppendLine();
//                    enumField.AppendLine("		//==========以下为枚举==========");
//                }
//                fname = enumList[i].name;
//                enumField.AppendLine(getComment(enumList[i].comment, "		"));
//                enumField.AppendLine($"		public var {fname}:Enum_{fname} = new Enum_{fname}();");
//                enumClass.AppendLine(getEnumClass(enumList[i]));
//            }


//            string str = $@"/**工具生成不要修改*/
//package CNet.Protos
//{{
//{getComment(packComm,"	")}
//	public class {className}
//	{{
//{msgField}{enumField}
//		public function {className}(msg:*)
//		{{
//{msgFieldValue}		}}
//	}}
//}}
//{enumClass}";

//            Utils.SaveFile(saveFilePath, str,true);


///// <summary>
///// 获取枚举类
///// </summary>
///// <param name="penum"></param>
///// <returns></returns>
//private static string getEnumClass(ProtoEnum penum)
//{
//    StringBuilder enumItem = new StringBuilder(); //枚举定义
//    ProtoEnumItme item;
//    for (int i = 0; i < penum.items.Count; i++)
//    {
//        item = penum.items[i];
//        enumItem.AppendLine("	/**" + item.comment + "*/");
//        enumItem.AppendLine("	public const " + item.name + ":int = " + item.value + "; ");
//    }
//    string str = $@"class Enum_{penum.name}
//{{
//{enumItem}}}";
//    return str;
//}
#endregion