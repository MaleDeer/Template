using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLibrary.Enums
{
    /// <summary>
    /// 性别
    /// </summary>
    public enum Gender
    {
        /// <summary>
        /// 未知
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// 男
        /// </summary>
        Man = 1,

        /// <summary>
        /// 女
        /// </summary>
        Woman = 2
    }

    /// <summary>
    /// 用户的密码状态
    /// </summary>
    public enum PassWordState
    {
        /// <summary>
        /// 旧密码
        /// </summary>
        Old = 0,

        /// <summary>
        /// 新密码
        /// </summary>
        New = 1
    }
}
