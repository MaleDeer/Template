using CommonLibrary.Entitys;
using CommonLibrary.Tools;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    /// <summary>
    /// 控制器基类
    /// </summary>
    public class BaseController : ControllerBase
    {
        // ******************
        // 创建 -- POST
        // 读取 -- GET
        // 整体更新 -- PUT
        // 局部更新 -- PATCH
        // 删除 -- DELETE
        // ******************

        /// <summary>
        /// 数据保护
        /// </summary>
        protected readonly IDataProtector _protector;

        /// <summary>
        /// 提供数据保护
        /// </summary>
        /// <param name="provider">数据保护提供者</param>
        public BaseController(IDataProtectionProvider provider)
        {
            _protector = provider.CreateProtector("2TXFY-G2K6J-7C4D8-MG3MV");
        }

        /// <summary>
        /// 正确返回（同步无数据）
        /// </summary>
        /// <returns></returns>
        protected ResultData<string> OutData()
        {
            return new ResultData<string>() { Code = 200, Message = "", Result = "OK" };
        }

        /// <summary>
        /// 正确返回（异步无数据）
        /// </summary>
        /// <returns></returns>
        protected async Task<ResultData<string>> OutDataAsync()
        {
            ResultData<string> resultData = new ResultData<string>()
            { Code = 200, Message = "", Result = "OK" };
            return await Task.FromResult(resultData);
        }

        /// <summary>
        /// 正确返回（同步有数据）
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="Data">数据</param>
        /// <returns></returns>custom
        protected ResultData<T> OutData<T>(T Data)
        {
            return new ResultData<T>() { Code = 200, Message = "", Result = Data };
        }

        /// <summary>
        /// 正确返回（异步有数据）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        protected async Task<ResultData<T>> OutDataAsync<T>(T data)
        {
            ResultData<T> resultData = new ResultData<T>()
            { Code = 200, Message = "", Result = data };
            return await Task.FromResult(resultData);
        }

        /// <summary>
        /// 错误返回（同步）
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="message">消息</param>
        /// <returns></returns>
        protected ResultData<T> OutError<T>(string message)
        {
            Logger.Error("OutError:" + message);
            return new ResultData<T>() { Code = 400, Message = message, Result = default(T) };
        }

        /// <summary>
        /// 错误返回（异步）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <returns></returns>
        protected async Task<ResultData<T>> OutErrorAsync<T>(string message)
        {
            Logger.Error("OutError:" + message);
            ResultData<T> resultData = new ResultData<T>()
            { Code = 400, Message = message, Result = default(T) };
            return await Task.FromResult(resultData);
        }
    }
}
