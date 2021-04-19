using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CommonLib
{
    public static class Extension
    {
        private static readonly DateTime DateTime1970 = new DateTime(1970, 1, 1);

        /// <summary>
        /// 获取字符串全角长度，一个汉字算二个
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int GetFullLength(this string str)
        {
            if (str.Length == 0) return 0;
            ASCIIEncoding ascii = new ASCIIEncoding();
            int tempLen = 0; byte[] s = ascii.GetBytes(str);
            for (int i = 0; i < s.Length; i++)
            {
                if ((int)s[i] == 63)
                    tempLen += 2;
                else
                    tempLen += 1;
            }
            return tempLen;
        }
        private static string[] qualityColorStr = new string[] { "c3d5ec", "29f554", "41a8f8", "ff42db", "ff7d00" };
        public static string ToQualityColorStr(this string str, int quality)
        {
            return $@"<color=#{qualityColorStr[quality]}>{str}</color>";
        }
        /// <summary>
        /// 判断是否有非法字符
        /// </summary>
        /// <returns></returns>
        public static bool CheckIllegalChar(this string str)
        {
            Regex reg = new Regex("[?!@#$%\\^&*()]+");
            Match m = reg.Match(str);
            return m.Success;
        }
        /// <summary>
        /// 时间转时间戳(秒)
        /// </summary>
        /// <returns>秒</returns>
        public static int ToTimestamp(this DateTime time)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(DateTime1970);
            return (int)((time - startTime).TotalSeconds + 0.5f);
        }
        /// <summary>
        /// 时间转时间戳(毫秒)
        /// </summary>
        /// <returns>毫秒</returns>
        public static long ToTimestampMS(this DateTime time)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(DateTime1970);
            return (long)((time - startTime).TotalMilliseconds + 0.5f);
        }

        /// <summary>
        /// 本地时间与协调世界时的偏移量（秒）
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static int GetUTCOffest(this DateTime time)
        {
            return (int)TimeZone.CurrentTimeZone.GetUtcOffset(DateTime1970).TotalSeconds;
        }

        /// <summary>
        /// 时间转成指定字符串保存
        /// </summary>
        /// <returns>毫秒</returns>
        public static string ToFormatDateTime(this DateTime time)
        {
            return time.ToString("yyyy-MM-dd HH:mm:ss");
        }
        /// <summary>
        /// 指定字符串时间转成时间
        /// </summary>
        /// <param name="save"></param>
        /// <returns></returns>
        public static DateTime ToFormatDateTime(this string save)
        {
            DateTime time = DateTime.MinValue;
            DateTime.TryParseExact(save, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out time);
            return time;
        }


        /// <summary>
        /// 转换成日期数据
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static int ToDateNum(this DateTime time)
        {
            return time.Month * 100 + time.Day;
        }

        /// <summary>
        /// 时间转时间戳
        /// </summary>
        /// <returns>秒</returns>
        public static int ToTimestamp(this DateTime? time)
        {
            if (time == null) return 0;
            return ((DateTime)time).ToTimestamp();
        }


        /// <summary>
        /// 时间戳转时间
        /// </summary>
        /// <param name="timeStamp">时间戳(秒)</param>
        /// <returns>时间</returns>
        public static DateTime ToDateTime(this int timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return dtStart.AddSeconds(timeStamp);
        }

        /// <summary>
        /// 两个时间差  
        /// </summary>
        /// <param name="time"></param>
        /// <param name="time2"></param>
        /// <returns>相差天数</returns>
        public static int GetAcrossDay(DateTime? time, DateTime? time2)
        {
            int num = ((DateTime)time2).DayOfYear - ((DateTime)time).DayOfYear;
            return num;
        }

        public static DateTime? ToDateTime(string date)
        {
            if (string.IsNullOrEmpty(date))
                return null;
            if (date.Length == 19)
                return DateTime.ParseExact(date, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
            return DateTime.ParseExact(date, "yyyy-MM-dd", System.Globalization.CultureInfo.CurrentCulture);
        }
        /// <summary>
        /// 保证返回数据为范围内的数值.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="val">待测值.</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>T.</returns>
        public static T Limit<T>(this T val, T min, T max) where T : IComparable
        {
            var maxVal = (val.CompareTo(min) > 0) ? val : min;
            var minVal = (maxVal.CompareTo(max) > 0) ? max : maxVal;
            return minVal;
        }

        /// <summary>
        /// 异步执行方法1后执行方法2
        /// </summary>
        /// <param name="a1">方法1</param>
        /// <param name="a2">方法2</param>
        public static void TaskRunFunction(Action a1, Action<Task> a2)
        {
            Task.Run(() =>
            {
                try { a1(); } catch (Exception ex) { Logger.LogError(ex); }
            }).ContinueWith(a2);
        }


        ///// <summary>
        ///// MongoDB ObjectId 转成 Proto ObjectId
        ///// </summary>
        ///// <param name="objid"></param>
        ///// <returns></returns>
        //public static common.ObjectId ToProtoObjectId(this MongoDB.Bson.ObjectId objid)
        //{
        //    common.ObjectId protoObjectId = new common.ObjectId();
        //    byte[] bytes = objid.ToByteArray();
        //    byte[] b1 = new byte[4];
        //    byte[] b2 = new byte[4];
        //    byte[] b3 = new byte[4];
        //    Array.Copy(bytes, 0, b1, 0, 4);
        //    Array.Copy(bytes, 4, b2, 0, 4);
        //    Array.Copy(bytes, 8, b3, 0, 4);
        //    protoObjectId.byte1 = BitConverter.ToUInt32(b1, 0);
        //    protoObjectId.byte2 = BitConverter.ToUInt32(b2, 0);
        //    protoObjectId.byte3 = BitConverter.ToUInt32(b3, 0);
        //    return protoObjectId;
        //}

        //public static MongoDB.Bson.ObjectId ToMongoObjectId(this common.ObjectId objid)
        //{
        //    byte[] b1 = BitConverter.GetBytes(objid.byte1);
        //    byte[] b2 = BitConverter.GetBytes(objid.byte2);
        //    byte[] b3 = BitConverter.GetBytes(objid.byte3);
        //    byte[] bytes = new byte[12];
        //    Buffer.BlockCopy(b1, 0, bytes, 0, 4);
        //    Buffer.BlockCopy(b2, 0, bytes, 4, 4);
        //    Buffer.BlockCopy(b3, 0, bytes, 8, 4);
        //    return new MongoDB.Bson.ObjectId(bytes);
        //}


    }
}