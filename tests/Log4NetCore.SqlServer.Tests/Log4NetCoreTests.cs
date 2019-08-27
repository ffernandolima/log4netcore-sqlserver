using log4net;
using Log4NetCore.SqlServer.Appenders;
using Log4NetCore.SqlServer.Extensions;
using Log4NetCore.SqlServer.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Reflection;

namespace Log4NetCore.SqlServer.Tests
{
	[TestClass]
	public class Log4NetCoreTests
	{
		private static readonly Assembly _RepositoryAssembly = typeof(Log4NetCoreTests).Assembly;

		private string _connectionString;

		[TestInitialize]
		public void TestInitialize()
		{
			Log4NetHelper.ConfigureLog4Net();

			_connectionString = ConfigurationHelper
				.BuildConfiguration()
				.GetConnectionString("log4netCoreDb");
		}

		[TestMethod]
		public void AdoNetAppenderHelperTest()
		{
			var results = AdoNetAppenderHelper.SetConnectionString(_connectionString);

			Assert.IsTrue((results?.Any()).GetValueOrDefault());

			var logger = LogManager.GetLogger(_RepositoryAssembly, "Log4NetCore.SqlServer.Tests.AdoNetLogger");
		}

		[TestMethod]
		public void AdoNetAppenderHelperTestWithRepositoryAssembly()
		{
			var affectedAdoNetAppenders = AdoNetAppenderHelper.SetConnectionString(_RepositoryAssembly, _connectionString);

			Assert.IsTrue(affectedAdoNetAppenders > 0);

			var logger = LogManager.GetLogger(_RepositoryAssembly, "Log4NetCore.SqlServer.Tests.AdoNetLogger");
		}

		[TestMethod]
		public void LoggerRepositoryExtensionsTest()
		{
			var loggerRepository = LogManager.GetRepository(_RepositoryAssembly);

			var affectedAdoNetAppenders = loggerRepository.SetConnectionString(_connectionString);

			Assert.IsTrue(affectedAdoNetAppenders > 0);

			var logger = LogManager.GetLogger(_RepositoryAssembly, "Log4NetCore.SqlServer.Tests.AdoNetLogger");
		}

		[TestMethod]
		public void AdoNetAppenderExtensionsTest()
		{
			var adoNetAppender = new AdoNetAppender();

			adoNetAppender.SetConnectionString(_connectionString);

			Assert.IsTrue(!string.IsNullOrWhiteSpace(adoNetAppender.ConnectionString));
		}
	}
}
