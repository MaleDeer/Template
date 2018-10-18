using System;
using System.Collections.Generic;
using System.Text;

namespace WeChatLibrary.Entitys
{
    /// <summary>
    /// 微信用户信息
    /// </summary>
    public class UserKey
    {
        /// <summary>
        /// 用户唯一标识
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 会话密钥
        /// </summary>
        public string session_key { get; set; }
        /// <summary>
        /// 用户在开放平台的唯一标识符
        /// </summary>
        public string unionid { get; set; }
    }
}
