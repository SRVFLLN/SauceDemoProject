using NLog;
using NLog.Config;
using NLog.Targets;

namespace SauceDemoProject
{
    public static class Logger
    {
        public static void LoadConfiguration(string path = null) 
        {
            if (path != null)
            {
                try
                {
                    LogManager.LoadConfiguration(path);
                }
                catch
                {
                    LogManager.Configuration = GetConfiguration();
                }
            }
            else 
            {
                LogManager.Configuration = GetConfiguration();
            }
        }
        private static LoggingConfiguration GetConfiguration()
        {
            var layout = "${date:format=yyyy-MM-dd HH\\:mm\\:ss} ${level:uppercase=true} - ${message}";
            var config = new LoggingConfiguration();
            config.AddRule(LogLevel.Info, LogLevel.Fatal, new ConsoleTarget("logconsole")
            {
                Layout = layout
            });
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, new FileTarget("logfile")
            {
                FileName = "../../../Log/log.log",
                Layout = layout
            });
            return config;
        }

        public static void Info(string message, string arg = null)
        {
            if (arg == null)
                LogManager.GetLogger("*").Info(message);
            else
                LogManager.GetLogger("*").Info(message, arg);
        }

        public static void Error(string message, string arg = null)
        {
            if (arg == null)
                LogManager.GetLogger("*").Error(message);
            else
                LogManager.GetLogger("*").Error(message, arg);
        }
    }
}