using RubiksCube.Core.Decorator.Rotate;
using RubiksCube.Core.Interface;

namespace RubiksCube.Core.Model
{
	public class CubeRotationCommand : IRotationCommand
	{
		private readonly ICubeFactory _cubeFactory;
		public string CommandName { get; private set; }
		public ICubeFaceRotation CubeFaceRotation { get; private set; }

		public bool IsClockwise { get; private set; }
		public CubeRotationCommand(ICubeFactory cubeFactory, string commandName, ICubeFaceRotation cubeFaceRotation, bool isClockwise)
		{
			_cubeFactory = cubeFactory;
			CommandName = commandName;
			CubeFaceRotation = cubeFaceRotation;
			IsClockwise = isClockwise;
		}
		public void Execute(ICubeData cubeData)
		{
			var rotate = _cubeFactory.InstantiateCubeRotateDecorator(cubeData);
			rotate.Rotate(CubeFaceRotation, IsClockwise);
		}
	}
}
