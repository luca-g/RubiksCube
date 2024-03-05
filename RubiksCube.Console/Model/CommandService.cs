﻿using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NLog.Config;
using RubiksCube.Core.Interface;
using RubiksCube.Core.Model;
using System.Text;

namespace RubiksCube.Console.Model
{
	public class CommandService
	{
		private readonly ILogger<CommandService> _loggerService;
		private readonly IOptions<AppConfiguration> _configuration;
		private ICubeFactory? _cubeFactory;
		private Dictionary<string, IRotationCommand>? _rotationCommands;
		public CommandService(
			ILogger<CommandService> logger,
			IOptions<AppConfiguration> configuration)
		{
			this._loggerService = logger;
			this._configuration = configuration;
		}
		public void LoadCommands()
		{
			if (this._configuration.Value.RotationFile == null)
			{
				this._loggerService.LogError("RotationFile is not set in appsettings.json");
				throw new ArgumentNullException("RotationFile");
			}
			if (!File.Exists(this._configuration.Value.RotationFile))
			{
				this._loggerService.LogError("RotationFile not found");
				throw new ArgumentNullException("RotationFile");
			}
			try
			{
				var dataProvider = new JsonDataProvider.Model.JsonDataProvider(this._configuration.Value.RotationFile);
				_cubeFactory = new CubeFactory(dataProvider);

				var rotationsLoader = _cubeFactory.GetCubeRotationCommandLoader();
				var allCommands = rotationsLoader.LoadCommands();
				_rotationCommands = allCommands.ToDictionary(t => t.CommandName, t => t);
			}
			catch (Exception ex)
			{
				this._loggerService.LogError(ex, "Error loading commands from json file");
				throw new Exception("Error loading commands from json file", ex);
			}
		}
		protected static string CubeToString(ICubeData cube)
		{
			var sb = new StringBuilder();
			for (int i = 0; i < 6; i++)
			{
				var face = cube.Faces[i];
				var values = string.Join("", face.Squares.Select(face => face.ToString().First()));
				sb.Append($"{i}:{values}");
				if (i < 5)
					sb.Append(",");
			}
			return sb.ToString();
		}
		public CommandResult ExecuteCommands(string commandsText)
		{
			try
			{
				if (_cubeFactory == null)
					LoadCommands();
				if (_cubeFactory == null)
					throw new Exception("Factory not loaded");
				if (_rotationCommands == null)
					throw new Exception("Commands not loaded");

				var cube = _cubeFactory!.InstantiateCube();
				foreach (var rotation in commandsText.Split(','))
				{
					var rotationText = rotation.Trim();
					if (_rotationCommands.TryGetValue(rotationText, out var command))
					{
						command.Execute(cube);
					}
					else
						return new CommandResult(string.Empty, $"Invalid command: {rotationText}");
				}
				var result = CubeToString(cube);
				return new CommandResult(result, null);
			}
			catch(Exception ex)
			{
				this._loggerService.LogError(ex, "Error executing command");
				return new CommandResult(string.Empty, "Error executing command");
			}
		}
	}
}