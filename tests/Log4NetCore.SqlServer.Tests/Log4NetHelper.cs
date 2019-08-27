using log4net;
using log4net.Config;
using System;
using System.IO;

namespace Log4NetCore.SqlServer.Tests
{
	public static class Log4NetHelper
	{
		public static void ConfigureLog4Net()
		{
			var type = typeof(Log4NetHelper);
			var assembly = type.Assembly;

			XmlConfigurator.Configure(
				LogManager.GetRepository(assembly),
				new FileInfo($@"{AppDomain.CurrentDomain.BaseDirectory}log4net.config")
			);
		}
	}
}
