using RubiksCube.Core.Interface;
namespace RubiksCube.JsonDataProvider.Model
{
	public class JsonDataProvider : IRotationsDataProvider
	{
		private readonly string _commandsFile;
		public JsonDataProvider(string commandsFile)
		{
			_commandsFile = commandsFile;
		}
		public async Task<IList<ICubeFaceRotation>> LoadAllRotationsAsync()
		{
			var rotations = await LoadCubeRotationsAsync(_commandsFile);
			return LoadICubeFaceRotations(rotations);
		}
		protected async Task<List<CubeRotationJson>> LoadCubeRotationsAsync(string commandsFile)
		{
			if (!File.Exists(commandsFile))
				throw new Exception($"File {commandsFile} not found");
			var json = await System.IO.File.ReadAllTextAsync(commandsFile);
			var root = Newtonsoft.Json.JsonConvert.DeserializeObject<CubeRotationJsonRoot>(json);
			if (root?.Rotations == null)
				throw new Exception($"Error loading cube rotations from file {commandsFile}");
			return root.Rotations;
		}
		protected static List<ICubeFaceRotation> LoadICubeFaceRotations(List<CubeRotationJson> cubeRotations)
		{
			var rv = new List<ICubeFaceRotation>();
			return cubeRotations.Select(t => (ICubeFaceRotation)new CubeFaceRotation(t)).ToList();
		}
	}
}
