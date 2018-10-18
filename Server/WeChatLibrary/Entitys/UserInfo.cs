using CommonLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace WeChatLibrary.Entitys
{
    public class UserInfo
    {
        public string avatarUrl { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public Gender gender { get; set; }
        public string language { get; set; }
        public string nickName { get; set; }
        public string province { get; set; }
    }
}
