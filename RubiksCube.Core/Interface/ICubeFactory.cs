using RubiksCube.Core.Decorator.Rotate;

namespace RubiksCube.Core.Interface
{
	public interface ICubeFactory
	{
		public ICubeData InstantiateCube();
		public ICubeFaceData InstantiateFaceData(eSquareColor color);
		public IRotationCommand InstantiateRotationCommand(string commandName, ICubeFaceRotation cubeFaceRotation, bool isClockwise);
		public ICubeFaceRotateDecorator InstantiateCubeFaceRotateDecorator(ICubeFaceData cubeFaceData);
		public ICubeRotateDecorator InstantiateCubeRotateDecorator(ICubeData cubeData);
	}
}
