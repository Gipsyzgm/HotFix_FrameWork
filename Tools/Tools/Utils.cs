using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using zlib;

namespace Tools
{
    public static class Utils
    {
        /// <summary>
        /// 跟据按钮打开对应的文件夹
        /// </summary>
        /// <param name="sender"></param>
        public static void ButtonOpenDir(object sender)
        {
            Control cont = sender as Control;
            Control[] cons = cont.Parent.Controls.Find(cont.Name.Replace("Btn", "Txt"), false);
            if (cons.Length > 0)
            {
                TextBox txt = cons[0] as TextBox;
                if (txt != null)
                {
                    string path = txt.Text.Replace("/", "\\");
                    OpenDir(path);
                }
            }
        }
        public static void OpenDir(string path)
        {
            path = path.Replace("/", "\\");
            if (Directory.Exists(path))
                System.Diagnostics.Process.Start("Explorer", path);
            else
                Logger.LogError("目录不存在:" + path);
        }

        public static void OpenFile(string path)
        {
            path = path.Replace("/", "\\");
            if (File.Exists(path))
                System.Diagnostics.Process.Start(path);
            else
                Logger.LogError("文件不存在:" + path);
        }


        /// <summary>
        /// 执行单个cmd命令
        /// </summary>
        /// <param name="cmd"></param>
        public static void Cmd(string cmd)
        {
            Cmd(new List<string>() { cmd });
        }
        /// <summary>
        /// 执行多个cmd命令
        /// </summary>
        /// <param name="cmds"></param>
        public static void Cmd(List<string> cmds)
        {
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.WorkingDirectory = ".";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardError = true;
            process.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);
            process.ErrorDataReceived += new DataReceivedEventHandler(ErrorDataHandler);
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            for (int i = 0; i < cmds.Count; i++)
                process.StandardInput.WriteLine(cmds[i]); // Cmd 命令
            //string strResult = process.StandardError.ReadToEnd().Trim();
            process.StandardInput.WriteLine("exit");

            process.WaitForExit();
        }

        private static void OutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            if (!String.IsNullOrEmpty(outLine.Data))
            {
                Logger.Log(outLine.Data);
            }
        }
        private static void ErrorDataHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            if (!String.IsNullOrEmpty(outLine.Data))
            {
                Logger.LogError(outLine.Data);
            }
        }


        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="path">保存路径</param>
        /// <param name="content">文件内容</param>
        /// <param name="iscover">存在是否进行覆盖,默认true</param>
        public static void SaveFile(string path, Object content, bool iscover = true, bool isLog = true)
        {
            FileInfo info = new FileInfo(path);
            if (!iscover && info.Exists) //不覆盖
            {
                if (isLog)
                    Logger.LogWarning($"文件已存在，不进行覆盖操作!! {path}");
                return;
            }

            CheckCreateDirectory(info.DirectoryName);
            FileStream fs = new FileStream(path, FileMode.Create);

            if (content is MemoryStream)
            {
                BinaryWriter w = new BinaryWriter(fs);
                w.Write(((MemoryStream)content).ToArray());
                w.Close();
            }
            else if (content is byte[])
            {
                BinaryWriter w = new BinaryWriter(fs);
                w.Write((byte[])content);
                w.Close();
            }
            else
            {
                StreamWriter sWriter = new StreamWriter(fs, Encoding.GetEncoding("UTF-8"));
                sWriter.WriteLine(content);
                sWriter.Flush();
                sWriter.Close();
            }

            fs.Close();
            if (isLog)
                Logger.Log($"成功生成文件 {path}");
        }


        /// <summary>
        /// 判断文件夹是否存在，不存在则创建一个
        /// </summary>
        /// <param name="path">文件夹路径</param>
        public static void CheckCreateDirectory(string path)
        {
            if (!Directory.Exists(path))//如果不存在就创建文件夹
            {
                Directory.CreateDirectory(path);
            }
        }
        /// <summary>
        /// 删除目录
        /// </summary>
        /// <param name="path"></param>
        public static void DeleteDirectory(string path)
        {
            if (Directory.Exists(path))
                Directory.Delete(path, true);
        }


        /// <summary>
        /// 重置目录有文件全部删除,文件夹不存在创建
        /// </summary>
        /// <param name="path"></param>
        public static void ResetDirectory(string path)
        {
            try
            {
                if (Directory.Exists(path))
                    Directory.Delete(path, true);
                System.Threading.Thread.Sleep(10);
                Directory.CreateDirectory(path);
            }
            catch { }
        }

        /// <summary>
        /// 首字大写
        /// </summary>
        public static string ToFirstUpper(string str)
        {
            return str.Substring(0, 1).ToUpper() + str.Substring(1);
        }
        /// <summary>
        /// 首字小写
        /// </summary>
        public static string ToFirstLower(string str)
        {
            return str.Substring(0, 1).ToLower() + str.Substring(1);
        }

        /// <summary>
        /// 压缩文件字节
        /// </summary>
        /// <param name="inBytes"></param>
        /// <returns></returns>
        public static MemoryStream Compress(byte[] inBytes)
        {
            MemoryStream outStream = new MemoryStream();
            using (MemoryStream intStream = new MemoryStream(inBytes))
            {
                using (GZipStream Compress = new GZipStream(outStream, CompressionMode.Compress))
                {
                    intStream.CopyTo(Compress);
                }
            }
            return outStream;
        }
        /// <summary>
        /// 解压字节
        /// </summary>
        /// <param name="inStream"></param>
        /// <returns></returns>
        public static byte[] Decompress(MemoryStream inStream)
        {
            byte[] result = null;
            MemoryStream compressedStream = new MemoryStream(inStream.ToArray());
            using (MemoryStream outStream = new MemoryStream())
            {
                using (GZipStream Decompress = new GZipStream(compressedStream, CompressionMode.Decompress))
                {
                    Decompress.CopyTo(outStream);
                    result = outStream.ToArray();
                }
            }
            return result;
        }

        //===================================
        /// <summary>
        /// 字符串压缩成Base64
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string CompressString(string str)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(str);
            byte[] cbytes = CompressBytes(bytes);
            return Convert.ToBase64String(cbytes);
        }

        public static byte[] CompressBytes(string str)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(str);
            return CompressBytes(bytes);
        }

        /// <summary>
        /// 压缩字节数组
        /// </summary>
        /// <param name="sourceByte">需要被压缩的字节数组</param>
        /// <returns>压缩后的字节数组</returns>
        public static byte[] CompressBytes(byte[] sourceByte)
        {
            MemoryStream inputStream = new MemoryStream(sourceByte);
            Stream outStream = compressStream(inputStream);
            byte[] outPutByteArray = new byte[outStream.Length];
            outStream.Position = 0;
            outStream.Read(outPutByteArray, 0, outPutByteArray.Length);
            outStream.Close();
            inputStream.Close();
            return outPutByteArray;
        }
        /// <summary>
        /// 解压缩字节数组
        /// </summary>
        /// <param name="sourceByte">需要被解压缩的字节数组</param>
        /// <returns>解压后的字节数组</returns>
        public static byte[] DecompressBytes(byte[] sourceByte)
        {
            MemoryStream inputStream = new MemoryStream(sourceByte);
            Stream outputStream = deCompressStream(inputStream);
            byte[] outputBytes = new byte[outputStream.Length];
            outputStream.Position = 0;
            outputStream.Read(outputBytes, 0, outputBytes.Length);
            outputStream.Close();
            inputStream.Close();
            return outputBytes;
        }
        /// <summary>
        /// 压缩流
        /// </summary>
        /// <param name="sourceStream">需要被压缩的流</param>
        /// <returns>压缩后的流</returns>
        private static Stream compressStream(Stream sourceStream)
        {
            MemoryStream streamOut = new MemoryStream();
            ZOutputStream streamZOut = new ZOutputStream(streamOut, zlibConst.Z_DEFAULT_COMPRESSION);//, zlibConst.Z_DEFAULT_COMPRESSION
            CopyStream(sourceStream, streamZOut);
            streamZOut.finish();
            return streamOut;
        }
        /// <summary>
        /// 解压缩流
        /// </summary>
        /// <param name="sourceStream">需要被解压缩的流</param>
        /// <returns>解压后的流</returns>
        private static Stream deCompressStream(Stream sourceStream)
        {
            MemoryStream outStream = new MemoryStream();
            ZOutputStream outZStream = new ZOutputStream(outStream);
            CopyStream(sourceStream, outZStream);
            outZStream.finish();
            return outStream;
        }
        /// <summary>
        /// 复制流
        /// </summary>
        /// <param name="input">原始流</param>
        /// <param name="output">目标流</param>
        public static void CopyStream(System.IO.Stream input, System.IO.Stream output)
        {
            byte[] buffer = new byte[2000];
            int len;
            while ((len = input.Read(buffer, 0, 2000)) > 0)
            {
                output.Write(buffer, 0, len);
            }
            output.Flush();
        }



        /// <summary>
        /// 扩展方法,转成真实路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ToReality(this string path)
        {
            return path.Replace("$ServerDir$", Glob.projectSetting.RealityServerDir).Replace("$ClientDir$", Glob.projectSetting.RealityClientDir).Replace("$ClientHotDir$", Glob.projectSetting.RealityClientHotDir).Replace("$GMServerDir$", Glob.projectSetting.RealityGMServerDir).Replace("$ProjectDir$", Glob.projectSetting.ProjectDir);
        }


        private static byte[] Keys = { 0x45, 0xDC, 0x37, 0xFB, 0xBC, 0xAB, 0xCD, 0xEF };
        private static byte[] encryKeys = { 0xAB, 0x33, 0x37, 0x5C, 0xBB, 0x75, 0xC3, 0xAB };

        /// <summary>
        /// DES加密字符串
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <param name="encryptKey">加密密钥,要求为8位</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串 </returns>
        public static string EncryptDES(string encryptString)
        {
            try
            {
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();//实例化数据加密标准
                MemoryStream mStream = new MemoryStream();//实例化内存流
                //将数据流链接到加密转换的流
                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(encryKeys, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch
            {
                return encryptString;
            }
        }

        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        public static string DecryptDES(string decryptString)
        {
            try
            {
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(encryKeys, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return decryptString;
            }
        }


        //static  int BufferSize = 1;
        // /// <summary>
        // /// 压缩字节数组
        // /// </summary>
        // /// <param name="sourceBytes">源字节数组</param>
        // /// <param name="compressionLevel">压缩等级</param>
        // /// <param name="password">密码</param>
        // /// <returns>压缩后的字节数组</returns>
        // public static byte[] CompressBytes(byte[] sourceBytes, string password = null)
        // {
        //     byte[] result = new byte[] { };

        //     if (sourceBytes.Length > 0)
        //     {
        //         try
        //         {
        //             using (MemoryStream tempStream = new MemoryStream())
        //             {
        //                 using (MemoryStream readStream = new MemoryStream(sourceBytes))
        //                 {
        //                     using (ZipOutputStream zipStream = new ZipOutputStream(tempStream))
        //                     {
        //                         zipStream.Password = password;//设置密码
        //                        // zipStream.SetLevel(CheckCompressionLevel(compressionLevel));//设置压缩等级
        //                         ZipEntry zipEntry = new ZipEntry("ZipBytes");
        //                         zipEntry.DateTime = DateTime.Now;
        //                         zipEntry.Size = sourceBytes.Length;
        //                         zipStream.PutNextEntry(zipEntry);
        //                         int readLength = 0;
        //                         byte[] buffer = new byte[BufferSize];

        //                         do
        //                         {
        //                             readLength = readStream.Read(buffer, 0, BufferSize);
        //                             zipStream.Write(buffer, 0, readLength);
        //                         } while (readLength == BufferSize);

        //                         readStream.Close();
        //                         zipStream.Flush();
        //                         zipStream.Finish();
        //                         result = tempStream.ToArray();
        //                         zipStream.Close();
        //                     }
        //                 }
        //             }
        //         }
        //         catch (System.Exception ex)
        //         {
        //             throw new Exception("压缩字节数组发生错误", ex);
        //         }
        //     }

        //     return result;
        // }

        // /// <summary>
        // /// 解压字节数组
        // /// </summary>
        // /// <param name="sourceBytes">源字节数组</param>
        // /// <param name="password">密码</param>
        // /// <returns>解压后的字节数组</returns>
        // public static byte[] DecompressBytes(byte[] sourceBytes, string password = null)
        // {
        //     byte[] result = new byte[] { };

        //     if (sourceBytes.Length > 0)
        //     {
        //         try
        //         {
        //             using (MemoryStream tempStream = new MemoryStream(sourceBytes))
        //             {
        //                 using (MemoryStream writeStream = new MemoryStream())
        //                 {
        //                     using (ZipInputStream zipStream = new ZipInputStream(tempStream))
        //                     {
        //                         zipStream.Password = password;
        //                         ZipEntry zipEntry = zipStream.GetNextEntry();

        //                         if (zipEntry != null)
        //                         {
        //                             byte[] buffer = new byte[BufferSize];
        //                             int readLength = 0;

        //                             do
        //                             {
        //                                 readLength = zipStream.Read(buffer, 0, BufferSize);
        //                                 writeStream.Write(buffer, 0, readLength);
        //                             } while (readLength == BufferSize);

        //                             writeStream.Flush();
        //                             result = writeStream.ToArray();
        //                             writeStream.Close();
        //                         }
        //                         zipStream.Close();
        //                     }
        //                 }
        //             }
        //         }
        //         catch (System.Exception ex)
        //         {
        //             throw new Exception("解压字节数组发生错误", ex);
        //         }
        //     }
        //     return result;
        // }


    }
}
