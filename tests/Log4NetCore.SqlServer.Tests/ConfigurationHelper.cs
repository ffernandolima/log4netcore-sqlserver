using Microsoft.Extensions.Configuration;

namespace Log4NetCore.SqlServer.Tests
{
	public static class ConfigurationHelper
	{
		public static IConfiguration BuildConfiguration()
		{
			var configuration = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.Build();

			return configuration;
		}
	}
}
