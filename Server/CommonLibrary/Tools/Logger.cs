using log4net;
using log4net.Config;
using log4net.Repository;
using System;
using System.Diagnostics;
using System.IO;

namespace CommonLibrary.Tools
{
    public class Logger
    {
        private static ILoggerRepository _Repository = LogManager.CreateRepository("NETCoreRepository");
        private static bool _IsLoad = false;
        private static ILog Init(StackTrace trace)
        {
            if (!_IsLoad)
            {
                XmlConfigurator.Configure(_Repository, new FileInfo("log4net.config"));
                _IsLoad = true;
            }
            return LogManager.GetLogger(_Repository.Name, trace.GetFrame(1).GetMethod().DeclaringType);
            //log4net.Appender.ManagedColoredConsoleAppender
        }
        public static void Info(object message, Exception exception)
        {
            //XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));
            //ILog log = LogManager.GetLogger(repository.Name, "NETCorelog4net");
            Init(new StackTrace()).Info(message, exception);
        }

        public static void Info(object message)
        {
            Init(new StackTrace()).Info(message);
        }
        public static void Error(object message, Exception exception)
        {
            Init(new StackTrace()).Error(message, exception);
        }
        public static void Error(object message)
        {
            Init(new StackTrace()).Error(message);
        }
        public static void Error(Exception e)
        {
            Init(new StackTrace()).Error("", e);
        }
        public static void Debug(object message, Exception exception)
        {
            Init(new StackTrace()).Debug(message, exception);
        }
        public static void Debug(object message)
        {
            Init(new StackTrace()).Debug(message);
        }
        public static void Warn(object message, Exception exception)
        {
            Init(new StackTrace()).Warn(message, exception);
        }
        public static void Warn(object message)
        {
            Init(new StackTrace()).Warn(message);
        }
    }
}
