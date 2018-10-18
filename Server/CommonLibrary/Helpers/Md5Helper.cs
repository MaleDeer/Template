using System;
using System.Security.Cryptography;
using System.Text;

namespace CommonLibrary.Helpers
{
    public class Md5Helper
    {
        /// <summary>
        /// 这是示例，旨在指导用户如何使用Md5Helper
        /// </summary>
        public void TestMd5Helper()
        {
            string source = "Hello World!";
            using (MD5 md5Hash = MD5.Create())
            {
                string hash = GetMd5Hash(md5Hash, source);
                Console.WriteLine("MD5哈希 " + source + " 是: " + hash + ".");
                Console.WriteLine("验证哈希……");
                if (VerifyMd5Hash(md5Hash, source, hash))
                {
                    Console.WriteLine("哈希斯是一样的。");
                }
                else
                {
                    Console.WriteLine("哈希斯是不一样的。");
                }
            }
        }

        /// <summary>
        /// 获取Md5的哈希值
        /// </summary>
        /// <param name="md5Hash"></param>
        /// <param name="input">当前值</param>
        /// <returns>哈希值</returns>
        public static string GetMd5Hash(MD5 md5Hash, string input)
        {
            //将输入字符串转换为字节数组并计算哈希值
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            //遍历散列数据的每个字节
            //并将每个字符串格式化为十六进制字符串
            for (int i = 0; i < data.Length; i++)
                sBuilder.Append(data[i].ToString("x2"));
            //返回十六进制字符串
            return sBuilder.ToString();
        }

        /// <summary>
        /// 根据字符串验证哈希值
        /// </summary>
        /// <param name="md5Hash"></param>
        /// <param name="input"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        public static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            //对输入进行散列
            string hashOfInput = GetMd5Hash(md5Hash, input);
            //创建StringComparer和比较散列
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            if (0 == comparer.Compare(hashOfInput, hash))
                return true;
            else
                return false;
        }
    }
}
