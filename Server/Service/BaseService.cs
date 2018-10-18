using CommonLibrary.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    /// <summary>
    /// Service层基类
    /// </summary>
    public class BaseService
    {
        /// <summary>
        /// 抛出异常
        /// </summary>
        /// <param name="errorMsg">异常消息</param>
        /// <returns></returns>
        protected Exception ThrowException(string errorMsg = "")
        {
            Logger.Error(errorMsg);
            return new Exception(errorMsg);
        }

        /// <summary>
        /// 抛出异常
        /// </summary>
        /// <param name="e">异常</param>
        /// <param name="errorMsg">异常消息</param>
        /// <returns></returns>
        public Exception ThrowException(Exception e, string errorMsg = "")
        {
            Logger.Error(e);
            return e;
        }
    }
}
