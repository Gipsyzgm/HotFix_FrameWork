using System.IO;
using System.Net.Sockets;

namespace Telepathy
{

    public static class NetworkStreamExtensions
    {
        // .Read如果远程关闭了连接，则返回“ 0”，但如果我们自愿关闭了自己的连接，则抛出IOException。
        //让我们添加一个在两种情况下都返回'0'的ReadSafely方法，因此我们不必担心异常，因为断开连接就是断开连接...
        public static int ReadSafely(this NetworkStream stream, byte[] buffer, int offset, int size)
        {
            try
            {
                return stream.Read(buffer, offset, size);
            }
            catch (IOException)
            {
                return 0;
            }
        }

        // 辅助函数读取精确的“ n”个字节
        // -> 默认值.Read最多读取'n'个字节。 该函数正好读取“ n”个字节
        // -> 这一直阻塞，直到收到“ n”个字节
        // -> 如果断开连接，则立即返回false
        public static bool ReadExactly(this NetworkStream stream, byte[] buffer, int amount)
        {
            // TCP缓冲区中可能没有足够的字节用于.Read一次读取全部数量，因此我们需要继续尝试直到拥有所有字节（阻塞）
            // 注意：这只是一个快速阅读的快速版本：
            //     for (int i = 0; i < amount; ++i)
            //         if (stream.Read(buffer, i, 1) == 0)
            //             return false;
            //     return true;
            int bytesRead = 0;
            while (bytesRead < amount)
            {
                // 使用“安全”读取扩展名最多读取“剩余”字节
                int remaining = amount - bytesRead;
                int result = stream.ReadSafely(buffer, bytesRead, remaining);

                //如果断开连接，读取返回0
                if (result == 0)
                    return false;

                //否则添加到读取的字节
                bytesRead += result;
            }
            return true;
        }
    }
}