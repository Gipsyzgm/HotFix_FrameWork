using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.ProtoExport
{
    public class ProtoUtils
    {
        public static string GetProtoCFile(bool isILRProtobuff = false)
        {
            string protoc = "3rdLib/protoc.exe";
            if (isILRProtobuff)
                protoc  = "3rdLib/protoc_ilr.exe";
            return Path.Combine(Environment.CurrentDirectory, protoc);
        }
    }
}
