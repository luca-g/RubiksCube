using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RubiksCube.Core.Interface;
using RubiksCube.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubiksCube.Console.Model
{
	public class ManualService
	{
		private readonly ILogger<ManualService> _loggerService;
		private readonly IOptions<AppConfiguration> _configuration;
		private string? _manualText;
		public ManualService(
			ILogger<ManualService> logger,
			IOptions<AppConfiguration> configuration)
		{
			this._loggerService = logger;
			this._configuration = configuration;
		}
		public void LoadManual()
		{
			if (this._configuration.Value.ManualFile == null)
			{
				this._loggerService.LogError("ManualFile is not set in appsettings.json");
				throw new ArgumentNullException("ManualFile");
			}
			if (!File.Exists(this._configuration.Value.ManualFile))
			{
				this._loggerService.LogError("ManualFile not found");
				throw new ArgumentNullException("ManualFile");
			}
			try
			{
				_manualText = File.ReadAllText(_configuration.Value.ManualFile);
			}
			catch (Exception ex)
			{
				this._loggerService.LogError(ex, "Error loading manual file");
				throw new Exception("Error loading manual file", ex);
			}
		}
		public void ShowManualOnConsole()
		{
			if (this._manualText == null)
				LoadManual();
			System.Console.WriteLine(_manualText);
		}
	}
}
