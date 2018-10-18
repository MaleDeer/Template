using CommonLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Entitys
{
    /// <summary>
    /// 用户的密码
    /// </summary>
    public class TUserPassWord
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 该Id为加密后的用户的密码Id
        /// </summary>
        public string EncryptId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }

        /// <summary>
        /// 密码状态
        /// </summary>
        public PassWordState State { get; set; }

        /// <summary>
        /// 用户信息
        /// </summary>
        public virtual TUser User { get; set; }
    }
}
