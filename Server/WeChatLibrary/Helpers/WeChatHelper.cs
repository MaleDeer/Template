using CommonLibrary.Helpers;
using CommonLibrary.Tools;
using LitJson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using WeChatLibrary.Entitys;

namespace WeChatLibrary.Helpers
{
    public class WeChatHelper
    {
        /// <summary>
        /// 微信小程序的AppId
        /// </summary>
        private readonly string _AppId;
        /// <summary>
        /// 微笑小程序的AppSecret
        /// </summary>
        private readonly string _AppSecret;

        /// <summary>
        /// 构造函数
        /// </summary>
        public WeChatHelper() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="appId">应用程序的AppId</param>
        /// <param name="appSecret">应用程序的AppSecret</param>
        public WeChatHelper(string appId, string appSecret)
        {
            _AppId = appId;
            _AppSecret = appSecret;
        }

        /// <summary>
        /// 根据微信小程序平台提供的签名验证算法验证用户发来的数据是否有效
        /// </summary>
        /// <param name="rawData">公开的用户资料</param>
        /// <param name="signature">公开资料携带的签名信息</param>
        /// <param name="sessionKey">服务端获取的SessionKey</param>
        /// <returns></returns>
        public bool VaildateUserInfo(string rawData, string signature, string sessionKey)
        {
            //创建SHA1签名类
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            //编码用于SHA1验证的源数据
            byte[] source = Encoding.UTF8.GetBytes(rawData + sessionKey);
            //生成签名
            byte[] target = sha1.ComputeHash(source);
            //转化为string类型，注意此处转化后是中间带短横杠的大写字母，需要剔除横杠转小写字母
            string result = BitConverter.ToString(target).Replace("-", "").ToLower();
            //比对，输出验证结果
            return signature == result;
        }

        /// <summary>
        /// 登录凭证校验
        /// </summary>
        /// <param name="code">临时登录凭证code</param>
        /// <returns></returns>
        private UserKey _getWxProof(string code)
        {
            try
            {
                JsonData WeChatConfig = AppConfig.Configs["PrjectConfig"]["WeChat"];
                string ApiUrl = $"https://api.weixin.qq.com/sns/jscode2session?appid={WeChatConfig["AppID"]}&secret={WeChatConfig["AppSecret"]}&js_code={code}&grant_type={WeChatConfig["GrantType"]}";
                //请求失败重试，三次后放弃
                string GetResult = "";
                for (int i = 0; i < 3; i++)
                {
                    try
                    {
                        GetResult = HttpHelper.Request(ApiUrl);
                        if (!string.IsNullOrEmpty(GetResult))
                            break;
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
                UserKey Result = JsonHelper.ParseFormJson<UserKey>(GetResult);
                if (Result != null)
                {
                    return Result;
                }
                throw new Exception("获取用户信息失败");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 获取用户标识
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public UserKey GetWxUserIdentity(string code)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(code))
                    throw new Exception("缺少参数code");
                return _getWxProof(code);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 解密微信字符 encryptedData
        /// </summary>
        /// <param name="encryptedData">对称解密的目标密文</param>
        /// <param name="iv">对称解密算法初始向量</param>
        /// <param name="sessionKey">对称解密秘钥</param>
        /// <returns></returns>
        public EncryptedData DecryptEncryptedData(string encryptedData, string sessionKey, string iv)
        {
            //格式化待处理字符串
            byte[] byte_encryptedData = Convert.FromBase64String(encryptedData);
            byte[] byte_iv = Convert.FromBase64String(iv);
            byte[] byte_sessionKey = Convert.FromBase64String(sessionKey);

            //创建解密器生成工具事例
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider
            {
                Mode = CipherMode.CBC,
                BlockSize = 128,
                Padding = PaddingMode.PKCS7,
                IV = byte_iv,
                Key = byte_sessionKey
            };

            //根据设置好的数据生成解密器事例
            ICryptoTransform transform = aes.CreateDecryptor();
            //解密
            byte[] final = transform.TransformFinalBlock(byte_encryptedData, 0, byte_encryptedData.Length);
            //生成结果
            string result = Encoding.UTF8.GetString(final);
            //反序列化结果，生成用户信息实例
            return JsonConvert.DeserializeObject<EncryptedData>(result);
        }
    }
}
