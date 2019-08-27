# Log4NetCore.Sqlserver

log4net AdoNetAppender for .NET Core.

https://logging.apache.org/log4net/ has no support for AdoNetAppender yet, so this project solves this problem.

P.S: Only SQL Server database provider supported.

# Status

# Getting Started

- Appender configuration 

In your log4net.config, please add these configurations:

```XML

		<appender name="AdoNetAppender" type="Log4NetCore.SqlServer.Appenders.AdoNetAppender, Log4NetCore.SqlServer">
			<bufferSize value="1" />
			<connectionType value="System.Data.SqlClient.SqlConnection,System.Data.SqlClient,Version=4.0.0.0,Culture=neutral,PublicKeyToken=b77a5c561934e089" />
			<connectionStringName value="[YOUR CONNECTION STRING NAME]" /> <!-- Example: log4netCoreDb -->
			<connectionStringFile value="[YOUR JSON CONFIGURATION FILE]" /> <!-- Example: appsettings.json -->
			<commandText value="INSERT INTO Log ([Date],[Level],[Logger],[Message]) VALUES (@date, @level, @logger, @message)" />
			<parameter>
				<parameterName value="@date" />
				<dbType value="DateTime" />
				<layout type="log4net.Layout.RawTimeStampLayout" />
			</parameter>
			<parameter>
				<parameterName value="@level" />
				<dbType value="String" />
				<size value="50" />
				<layout type="log4net.Layout.PatternLayout">
					<conversionPattern value="%level" />
				</layout>
			</parameter>
			<parameter>
				<parameterName value="@logger" />
				<dbType value="String" />
				<size value="100" />
				<layout type="log4net.Layout.PatternLayout">
					<conversionPattern value="%logger" />
				</layout>
			</parameter>
			<parameter>
				<parameterName value="@message" />
				<dbType value="String" />
				<size value="8000" />
				<layout type="log4net.Layout.PatternLayout">
					<conversionPattern value="%message" />
				</layout>
			</parameter>
		</appender>
		
```

Please pay attention to the property named type, it shall use the qualified assembly "Log4NetCore.SqlServer.Appenders.AdoNetAppender, Log4NetCore.SqlServer". Also, you need to provide your connection string name and the file which contains it through the properties "connectionStringName" and "connectionStringFile, so this app will find your connection string automatically.

- Set database connection manually (through code)

Some extensions were created in order to help you with it:

```C#

// Configure log4net
var assembly = [YOUR REPOSITORY ASSEMBLY];

XmlConfigurator.Configure(
	LogManager.GetRepository(assembly),
	new FileInfo($@"{AppDomain.CurrentDomain.BaseDirectory}log4net.config")
);

// Configure IConfiguration
var configuration = new ConfigurationBuilder()
	.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
	.Build();
	
// Get ConnectionString
var connectionString = configuration.GetConnectionString("log4netCoreDb");

// Configure Log4NetCore.SqlServer

int affectedAdoNetAppenders = 0;

// All repositories
var results = AdoNetAppenderHelper.SetConnectionString(connectionString);

// Specfic repository from the assembly
affectedAdoNetAppenders = AdoNetAppenderHelper.SetConnectionString(assembly, connectionString);

// Specfic repository
var loggerRepository = LogManager.GetRepository(assembly);

affectedAdoNetAppenders = loggerRepository.SetConnectionString(_connectionString);

// Specific appender
var adoNetAppender = new AdoNetAppender();

adoNetAppender.SetConnectionString(_connectionString);

```   

# Support / Contributing
If you want to help with the project, feel free to open pull requests and submit issues. 

# Donate

If you would like to show your support for this project, then please feel free to buy me a coffee.

<a href="https://www.buymeacoffee.com/fernandolima" target="_blank"><img src="https://www.buymeacoffee.com/assets/img/custom_images/white_img.png" alt="Buy Me A Coffee" style="height: auto !important;width: auto !important;" ></a>
