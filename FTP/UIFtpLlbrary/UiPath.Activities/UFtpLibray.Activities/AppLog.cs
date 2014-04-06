using NLog;
using NLog.Config;
using System;
using System.IO;
using System.Reflection;
namespace FtpActivities
{
	public class AppLog
	{
		private static Logger AppLogger;
		private static AppLog instance = new AppLog();
		public string LogsFolder = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "UiPath\\Logs");
		public static AppLog Instance
		{
			get
			{
				return AppLog.instance;
			}
		}
		private AppLog()
		{
			string directoryName = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
			LogManager.Configuration = new XmlLoggingConfiguration(System.IO.Path.Combine(directoryName, "NLog.config"), true);
			AppLog.AppLogger = LogManager.GetCurrentClassLogger();
		}
		public void Error(System.Exception ex)
		{
			if (!LogManager.IsLoggingEnabled())
			{
				return;
			}
			AppLog.AppLogger.ErrorException(ex.Message, ex);
		}
		public void Info(string message)
		{
			if (!LogManager.IsLoggingEnabled())
			{
				return;
			}
			AppLog.AppLogger.Info(message);
		}
		public void Trace(object obj)
		{
			if (!LogManager.IsLoggingEnabled())
			{
				return;
			}
			AppLog.AppLogger.Trace(obj);
		}
		public void Fatal(string message)
		{
			if (!LogManager.IsLoggingEnabled())
			{
				return;
			}
			AppLog.AppLogger.Fatal(message);
		}
		public void Warn(string message)
		{
			if (!LogManager.IsLoggingEnabled())
			{
				return;
			}
			AppLog.AppLogger.Warn(message);
		}
		public void Debug(string message)
		{
			if (!LogManager.IsLoggingEnabled())
			{
				return;
			}
			AppLog.AppLogger.Debug(message);
		}
	}
}
