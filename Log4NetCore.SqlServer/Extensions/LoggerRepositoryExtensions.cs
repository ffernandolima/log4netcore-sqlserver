using log4net.Repository;
using Log4NetCore.SqlServer.Appenders;
using System.Linq;

namespace Log4NetCore.SqlServer.Extensions
{
	public static class LoggerRepositoryExtensions
	{
		/// <summary>
		/// Sets a connection string to all AdoNetAppender instances
		/// </summary>
		/// <param name="loggerRepository">LoggerRepository instance</param>
		/// <param name="connectionString">Database connection string</param>
		/// <returns>The number of AdoNetAppender instances which were affected</returns>
		public static int SetConnectionString(this ILoggerRepository loggerRepository, string connectionString)
		{
			if (loggerRepository == null)
			{
				return default;
			}

			var adoNetAppenders = loggerRepository.GetAppenders()
				?.Where(appender => appender is AdoNetAppender)
				?.Cast<AdoNetAppender>()
				?.Where(adoNetAppender => adoNetAppender != null)
				?.ToArray();

			if ((adoNetAppenders?.Any()).GetValueOrDefault())
			{
				for (int idx = 0; idx < adoNetAppenders.Length; idx++)
				{
					var adoNetAppender = adoNetAppenders[idx];
					adoNetAppender.SetConnectionString(connectionString);
				}
			}

			var affectedAdoNetAppenders = (adoNetAppenders?.Length).GetValueOrDefault();

			return affectedAdoNetAppenders;
		}
	}
}
