using System;
using System.Collections.Generic;
using System.Text;

namespace WeChatLibrary.Entitys
{
    public class FullUserInfo
    {
        public string encryptedData { get; set; }
        public string errMsg { get; set; }
        public string iv { get; set; }
        public string rawData { get; set; }
        public string signature { get; set; }
        public UserInfo userInfo { get; set; }
    }
}
