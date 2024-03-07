using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NLog.Extensions.Logging;
using RubiksCube.Console.Model;
using RubiksCube.Core.Interface;
using RubiksCube.JsonDataProvider.Model;
using System.Reflection.Metadata;
using System.Runtime;

namespace RubiksCube.Console
{
	internal class Program
	{
		static async Task Main()
		{
			var logger = NLog.LogManager.GetCurrentClassLogger();
			logger.Info("Starting");
			try
			{
				var host = new HostBuilder()
					 .ConfigureAppConfiguration((configApp) =>
					 {
						 configApp.SetBasePath(System.IO.Directory.GetCurrentDirectory());
						 configApp.AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);
					 })
					 .ConfigureServices((hostContext, services) =>
					 {
						 services.Configure<AppConfiguration>(hostContext.Configuration.GetSection("RubiksCube"));
						 services.AddHostedService<ConsoleService>();
						 services.AddLogging(loggingBuilder =>
						 {
							 loggingBuilder.ClearProviders();
							 loggingBuilder.AddConfiguration(hostContext.Configuration.GetSection("Logging"));
							 loggingBuilder.AddNLog();
						 });
						 services.AddTransient<CommandService>();
						 services.AddTransient<ManualService>();
						 services.AddTransient<IRotationsDataProvider, JsonDataProvider.Model.JsonDataProvider>(
							 t=>
							 {
								 var configuration = t.GetService<IOptions<AppConfiguration>>();
								 if (configuration?.Value.RotationFile == null)
								 {
									 throw new ArgumentNullException("RotationFile is not set in appsettings.json");
								 }
								 if (!File.Exists(configuration.Value.RotationFile))
								 {
									 throw new ArgumentNullException("RotationFile not found");
								 }
								 return new JsonDataProvider.Model.JsonDataProvider(configuration.Value.RotationFile);
							 }
							 );
						 services.AddTransient<ICubeFactory>(t =>
						 {
							 var dataProvider = t.GetService<IRotationsDataProvider>();
							 return new RubiksCube.Core.Model.CubeFactory(dataProvider!);
						 });
					 })
					 .Build();
				
				await host.RunAsync();
			}
			catch (Exception ex)
			{
				logger.Error(ex);
			}
			finally
			{
				logger.Info("Exiting program");
				NLog.LogManager.Shutdown();
			}

		}
	}
}
