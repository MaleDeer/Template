using CommonLibrary.Entitys;
using CommonLibrary.Helpers;
using LitJson;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace CommonLibrary.Tools
{
    /// <summary>
    /// 异步的全局过滤器
    /// </summary>
    public class GlobalActionFilterAsync : IAsyncActionFilter
    {
        /// <summary>
        /// 忽略过滤的列表
        /// </summary>
        private readonly string[] _IgnoreUrls = new string[] { };

        /// <summary>
        /// 异步的全局过滤器
        /// </summary>
        public GlobalActionFilterAsync() { }

        /// <summary>
        /// 异步的全局过滤器
        /// </summary>
        /// <param name="ignoreUrls">忽略过滤的列表</param>
        public GlobalActionFilterAsync(string[] ignoreUrls)
        {
            _IgnoreUrls = ignoreUrls;
        }

        /// <summary>
        /// 在执行前
        /// </summary>
        private void _onBeforeExecution(ActionExecutingContext context)
        {
            Logger.Info("请求开始");
            foreach (string IgnoreUrl in _IgnoreUrls)
            {
                string UpperPath = context.HttpContext.Request.Path.Value.ToUpper();
                //如果是开放的接口，让其管道短路
                if (UpperPath == IgnoreUrl.ToUpper() || UpperPath == $"/{IgnoreUrl.ToUpper()}")
                    return;
            }
            try
            {
                Logger.Info("Header信息：验证登陆");
                IHeaderDictionary Headers = context.HttpContext.Request.Headers;
                StringValues EncryptStr = Headers["EncryptStr"];
                StringValues UnionId = Headers["UnionId"];

                if (string.IsNullOrWhiteSpace(EncryptStr) || string.IsNullOrWhiteSpace(UnionId))
                {
                    Logger.Info("未登录");
                    ResultData<string> notLoginData = new ResultData<string>()
                    { Code = 300, Message = "请先登陆", Result = null };
                    context.Result = new JsonResult(notLoginData);
                    return;
                }
                else
                {
                    Logger.Info("通过登陆验证，开始验证加密认证");
                    JsonData WeChatConfig = AppConfig.Configs["PrjectConfig"]["WeChat"];
                    string EncryptSky = WeChatConfig["EncryptStr"].ToString();

                    string MD5Encrypt;
                    using (MD5 md5Hash = MD5.Create())
                    {
                        MD5Encrypt = Md5Helper.GetMd5Hash(md5Hash, EncryptSky + UnionId);
                    }

                    if (MD5Encrypt != EncryptStr)
                    {
                        Logger.Info("认证失败");
                        ResultData<string> loginFailureData = new ResultData<string>()
                        { Code = 300, Message = "认证失败", Result = null };
                        context.Result = new JsonResult(loginFailureData);
                        return;
                    }
                    Logger.Info("加密认证通过");
                }
            }
            catch (Exception e)
            {
                Logger.Error("执行动作之前失败：", e);
                ResultData<string> errorResult = new ResultData<string>()
                { Code = 400, Message = e.Message, Result = null };
                context.Result = new JsonResult(errorResult);
                return;
            }
        }

        /// <summary>
        /// 在执行后
        /// </summary>
        private void _onAfterExecution(ActionExecutedContext context)
        {
            // 强制对所有代进行即时垃圾回收
            GC.Collect();
            Logger.Info($@"【返回请求--Response--{context.HttpContext.Request.Method.ToUpper()
                }】：{context.HttpContext.Request.Scheme }://{ context.HttpContext.Request.Host
                }{context.HttpContext.Request.Path}---请求结束\n");
        }

        /// <summary>
        /// 执行异步操作
        /// </summary>
        /// <param name="context">执行上下文</param>
        /// <param name="next">执行委托</param>
        /// <returns></returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // 在执行之前
            _onBeforeExecution(context);

            var resultContext = await next();
            // 在执行之后，resultContext.Result 设置返回值
            _onAfterExecution(resultContext);
        }
    }
}
