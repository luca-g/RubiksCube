using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RubiksCube.Console.Model;

namespace RubiksCube.Console
{
	public class ConsoleService : IHostedService
	{
		private readonly ILogger<ConsoleService> _loggerService;
		private readonly CommandService _commandService;
		private readonly ManualService _manualService;
		private readonly IOptions<AppConfiguration> _configuration;
		public ConsoleService(
			ILogger<ConsoleService> logger,
			CommandService commandService,
			ManualService manualService,
			IOptions<AppConfiguration> configuration)
		{
			this._loggerService = logger;
			this._commandService = commandService;
			this._manualService = manualService;
			this._configuration = configuration;
		}
		public async Task StartAsync(CancellationToken cancellationToken)
		{
			try
			{
				await _commandService.LoadCommandsAsync();
			}
			catch(Exception ex)
			{
				this._loggerService.LogError(ex,"Error loading commands from json file");
				throw new Exception("Error loading commands from json file",ex);
			}
			await this.ReadCommandsAsync(cancellationToken);
		}
		protected async Task DisplayStringForCommandAsync(string command)
		{
			var result = await _commandService.ExecuteCommandsAsync(command);
			if (result.error != null)
			{
				_loggerService.LogError(result.error);
				System.Console.WriteLine(result.error);
			}
			else
			{
				_loggerService.LogInformation(result.result);
				System.Console.WriteLine(result.result);
			}
		}
		public async Task ReadCommandsAsync(CancellationToken cancellationToken)
		{
			try
			{
				_manualService.ShowManualOnConsole();
				if (_configuration.Value.DefaultRotation != null)
				{
					System.Console.WriteLine($"Example: {_configuration.Value.DefaultRotation}");
					await DisplayStringForCommandAsync(_configuration.Value.DefaultRotation);
				}
				System.Console.WriteLine("");
				while (!cancellationToken.IsCancellationRequested)
				{
					try
					{
						System.Console.WriteLine("Enter a rotation string");
						_loggerService.LogInformation("Waiting for rotation");
						string? command = System.Console.ReadLine();
						if (command != null)
						{
							_loggerService.LogInformation($"Rotation entered: {command}");
							await DisplayStringForCommandAsync(command);
						}
					}
					catch (Exception ex)
					{
						if (cancellationToken.IsCancellationRequested)
							return;
						_loggerService.LogError(ex, "Error");
						System.Console.WriteLine("Unexpected error");
					}
				}
			}
			catch (Exception ex)
			{
				if (cancellationToken.IsCancellationRequested)
					return;
				_loggerService.LogError(ex, "Error");
				System.Console.WriteLine("Unexpected error");
			}
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
