using CommonLibrary.Enums;

namespace WeChatLibrary.Entitys
{
    public class EncryptedData
    {
        public string openId { get; set; }
        public string nickName { get; set; }
        public Gender gender { get; set; }
        public string language { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string country { get; set; }
        public string avatarUrl { get; set; }
        public string unionId { get; set; }
        public WaterMark watermark { get; set; }
    }

    public class WaterMark
    {
        public string appid { get; set; }
        public long timestamp { get; set; }
    }
}
