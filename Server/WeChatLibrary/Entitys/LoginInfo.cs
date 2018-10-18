using System;
using System.Collections.Generic;
using System.Text;

namespace WeChatLibrary.Entitys
{
    public class LoginInfo
    {
        /// <summary>
        /// 用户信息
        /// </summary>
        public EncryptedData UserInfo { get; set; }
        /// <summary>
        /// 加密字符串
        /// </summary>
        public string EncryptStr { get; set; }
    }
}
