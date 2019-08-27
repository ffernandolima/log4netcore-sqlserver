using log4net;
using Log4NetCore.SqlServer.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Log4NetCore.SqlServer.Helpers
{
	public static class AdoNetAppenderHelper
	{
		/// <summary>
		/// Sets a connection string to all AdoNetAppender instances
		/// </summary>
		/// <param name="repositoryAssembly">LoggerRepository Assembly</param>
		/// <param name="connectionString">Database connection string</param>
		/// <returns>The number of AdoNetAppender instances which were affected</returns>
		public static int SetConnectionString(Assembly repositoryAssembly, string connectionString)
		{
			if (repositoryAssembly == null)
			{
				return default;
			}

			var loggerRepository = LogManager.GetRepository(repositoryAssembly);

			if (loggerRepository == null)
			{
				return default;
			}

			var affectedAdoNetAppenders = loggerRepository.SetConnectionString(connectionString);

			return affectedAdoNetAppenders;
		}

		/// <summary>
		/// Sets a connection string to all AdoNetAppender instances from all logger repositories
		/// </summary>
		/// <param name="connectionString">Database connection string</param>
		/// <returns>A list of ValueTuple informing the LoggerRepository name and the number of AdoNetAppender instances which were affected</returns>
		public static IList<(string loggerRepositoryName, int affectedAdoNetAppenders)> SetConnectionString(string connectionString)
		{
			var result = LogManager.GetAllRepositories()
				?.Where(loggerRepository => loggerRepository != null)
				?.Select(loggerRepository => (loggerRepository.Name, loggerRepository.SetConnectionString(connectionString)))
				?.ToList();

			return result;
		}
	}
}
