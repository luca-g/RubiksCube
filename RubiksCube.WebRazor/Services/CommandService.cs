using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NLog.Config;
using RubiksCube.Core.Interface;
using RubiksCube.WebRazor.Models;
using System.Text;

namespace RubiksCube.WebRazor.Services
{
	public class CommandService
	{
		public const int MaxRotations = 10;
		private readonly ILogger<CommandService> _loggerService;
		private readonly ICubeFactory _cubeFactory;
		private Dictionary<string, IRotationCommand>? _rotationCommands;
		public CommandService(
			ILogger<CommandService> logger,
			ICubeFactory cubeFactory)
		{
			this._loggerService = logger;
			this._cubeFactory = cubeFactory;
		}
		public async Task LoadCommandsAsync()
		{
			try
			{
				var rotationsLoader = _cubeFactory.GetCubeRotationCommandLoader();
				var allCommands = await rotationsLoader.LoadCommandsAsync();
				_rotationCommands = allCommands.ToDictionary(t => t.CommandName, t => t);
			}
			catch (Exception ex)
			{
				this._loggerService.LogError(ex, "Error loading commands from json file");
				throw new Exception("Error loading commands from json file", ex);
			}
		}
		public async Task<CommandCubeResult> ExecuteCommandsAsync(string commandsText)
		{
			try
			{
				if (_rotationCommands == null)
				{
					await LoadCommandsAsync();
					if (_rotationCommands == null)
						throw new Exception("Commands not loaded");
				}

				var cube = _cubeFactory!.InstantiateCube();
				var rotations = commandsText.Replace("1", "'").Split(',');
                foreach (var rotation in rotations.Take(MaxRotations))
				{
					var rotationText = rotation.Trim();
					if (_rotationCommands.TryGetValue(rotationText, out var command))
					{
						command.Execute(cube);
					}
					else
						return new CommandCubeResult(cube, "", false, $"Invalid command: {rotationText}");
				}
				if(rotations.Length > MaxRotations)
				{
					commandsText = string.Join(",", rotations.Take(MaxRotations)).Replace("'","1");					
				}
				return new CommandCubeResult(cube, commandsText, rotations.Length >= MaxRotations, null);
			}
			catch(Exception ex)
			{
				this._loggerService.LogError(ex, "Error executing command");
				return new CommandCubeResult(null, "", false, "Error executing command");
			}
		}
	}
}
