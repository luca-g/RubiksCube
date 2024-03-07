using RubiksCube.Core.Interface;

namespace RubiksCube.Core.Model
{
    public class CubeRotationCommandLoader : ICubeRotationCommandLoader
	{
		protected readonly ICubeFactory _cubeFactory;
		public CubeRotationCommandLoader(ICubeFactory cubeFactory)
		{
			_cubeFactory = cubeFactory;
		}
		public async Task<IList<IRotationCommand>> LoadCommandsAsync()
		{
			var rv = new List<IRotationCommand>();
			var data = _cubeFactory.GetRotationsDataProvider();
			var rotations = await data.LoadAllRotationsAsync();
			foreach (var rotation in rotations)
			{
				rv.Add(_cubeFactory.InstantiateRotationCommand(rotation.ClockwiseKey, rotation, true));
				rv.Add(_cubeFactory.InstantiateRotationCommand(rotation.CounterclockwiseKey, rotation, false));
			}
			return rv;
		}
	}
}
