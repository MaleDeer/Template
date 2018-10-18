using LitJson;
using System;
using System.IO;

namespace CommonLibrary.Tools
{
    public class AppConfig
    {
        private static string _FilePath { get; set; }
        public static JsonData Configs = null;

        /// <summary>
        /// 配置初始化
        /// </summary>
        /// <param name="filePath"></param>
        public static void Init(string filePath)
        {
            try
            {
                _FilePath = filePath;
                using (StreamReader sr = new StreamReader(_FilePath))
                {
                    try
                    {
                        Configs = JsonMapper.ToObject(sr);
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static void Update()
        {
            Init(_FilePath);
        }
    }
}
