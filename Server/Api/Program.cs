using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CommonLibrary.Tools;
using LitJson;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Api
{
    /// <summary>
    /// 程序
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 程序入口    
        /// </summary>
        /// <param name="args">参数</param>
        public static void Main(string[] args)
        {
            //指定配置文件
            AppConfig.Init(Directory.GetCurrentDirectory() + "/appsettings.json");
            //CreateWebHostBuilder(args).Build().Run();
            BuildWebHost(args).Run();
        }

        /// <summary>
        /// 创建Web主机
        /// </summary>
        /// <param name="args">参数</param>
        /// <returns></returns>
        public static IWebHost BuildWebHost(string[] args)
        {
            JsonData PrjectConfig = AppConfig.Configs["PrjectConfig"];
            if (PrjectConfig["AppProtHttp"] == null || PrjectConfig["AppProtHttps"] == null ||
                PrjectConfig["AppProtHttp"].IsInt == false || PrjectConfig["AppProtHttps"].IsInt == false)
                Logger.Error("端口配置错误,必须为数字");
            int ProtHttp = (int)PrjectConfig["AppProtHttp"];
            int ProtHttps = (int)PrjectConfig["AppProtHttps"];
            string AppUrl = $"https://*:{ProtHttps};http://*:{ProtHttp}";
            Logger.Info("程序启动.");
            IWebHost host = WebHost.CreateDefaultBuilder(args).UseUrls(AppUrl)
               .UseStartup<Startup>()
               .Build();
            Logger.Info($"监听端口Http: {ProtHttp};监听端口Https: {ProtHttps}");
            return host;
        }
    }
}
