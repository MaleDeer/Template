using CommonLibrary.Enums;
using System;
using System.Collections.Generic;

namespace Model.Entitys
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class TUser
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 该Id为加密后的用户Id
        /// </summary>
        public string EncryptId { get; set; }

        /// <summary>
        /// 微信用户唯一标识
        /// </summary>
        public string OpenId { get; set; }

        /// <summary>
        /// 用户在微信开放平台的唯一标识符
        /// </summary>
        public string UnionId { get; set; }

        /// <summary>
        /// 小程序唯一标识
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 账户
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 省
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 故乡
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string AvatarUrl { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 用户的密码
        /// </summary>
        public virtual ICollection<TUserPassWord> UserPassWords { get; set; }
    }
}
