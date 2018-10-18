using CommonLibrary.Entitys;
using LitJson;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;

namespace CommonLibrary.Tools
{
    public class GlobalActionFilter: IActionFilter
    {
        /// <summary>
        /// 忽略过滤的列表
        /// </summary>
        private readonly string[] _IgnoreUrls = new string[] { };

        /// <summary>
        /// 全局过滤器
        /// </summary>
        public GlobalActionFilter() { }

        /// <summary>
        /// 全局过滤器
        /// </summary>
        /// <param name="ignoreUrls">忽略过滤的列表</param>
        public GlobalActionFilter(string[] ignoreUrls)
        {
            _IgnoreUrls = ignoreUrls;
        }

        /// <summary>
        /// 在执行动作之前，在模型绑定完成之后调用
        /// Called before the action executes, after model binding is complete.
        /// </summary>
        /// <param name="context">执行上下文</param>
        public void OnActionExecuting(ActionExecutingContext context)
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
        /// 在执行操作之后，在操作结果之前调用
        /// Called after the action executes, before the action result.
        /// </summary>
        /// <param name="context">执行上下文</param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // 强制对所有代进行即时垃圾回收
            GC.Collect();
            Logger.Info($@"【返回请求--Response--{context.HttpContext.Request.Method.ToUpper()
                }】：{context.HttpContext.Request.Scheme }://{ context.HttpContext.Request.Host
                }{context.HttpContext.Request.Path}---请求结束\n");
        }
    }
}
