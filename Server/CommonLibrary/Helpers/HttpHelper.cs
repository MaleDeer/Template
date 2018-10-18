using System;
using System.IO;
using System.Net;
using System.Text;

namespace CommonLibrary.Helpers
{
    public class HttpHelper
    {
        /// <summary>
        /// Http请求
        /// </summary>
        /// <param name="url">请求的URL</param>
        /// <param name="encoding">编码类型</param>
        /// <param name="method">Get/Post</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public static string Request(string url, Encoding encoding = null, string method = "GET", string parameters = null)
        {
            try
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                if (encoding == null)
                {
                    encoding = Encoding.GetEncoding("UTF-8");
                }
                HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(url);
                Request.Method = method;
                Request.Timeout = 10000;
                Request.ContentType = "application/x-www-form-urlencoded";
                Request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.104 Safari/537.36 Core/1.53.4620.400 QQBrowser/9.7.13014.400";
                //Request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident / 7.0; rv: 11.0) like Gecko";
                Request.Proxy = null;
                if (method.ToUpper() == "POST" && parameters != null)
                {
                    byte[] data = encoding.GetBytes(parameters);
                    using (Stream stream = Request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }
                }
                HttpWebResponse response = (HttpWebResponse)Request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream resStream = response.GetResponseStream();
                    StreamReader streamReader = new StreamReader(resStream, encoding);
                    string content = streamReader.ReadToEnd();
                    streamReader.Close();
                    resStream.Close();
                    return content;
                }
                else
                {
                    throw new Exception($"请求失败!,url:{url}\r\nres:{JsonHelper.ObjToJsonString(response)}");
                }
            }
            catch (Exception e)
            {
                throw new Exception($"请求失败!,url:{url}\r\nres:{e.Message}");
            }
        }
    }
}
