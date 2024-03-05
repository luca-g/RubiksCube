using RubiksCube.Core.Decorator.Rotate;
using RubiksCube.JsonDataProvider.Model;

namespace RubiksCube.Core.Interface
{
	public interface ICubeFactory
	{
		ICubeData InstantiateCube();
		ICubeFaceData InstantiateFaceData(eSquareColor color);
		IRotationCommand InstantiateRotationCommand(string commandName, ICubeFaceRotation cubeFaceRotation, bool isClockwise);
		ICubeFaceRotateDecorator InstantiateCubeFaceRotateDecorator(ICubeFaceData cubeFaceData);
		ICubeRotateDecorator InstantiateCubeRotateDecorator(ICubeData cubeData);
		IRotationsDataProvider GetRotationsDataProvider();
		ICubeRotationCommandLoader GetCubeRotationCommandLoader();
	}
}
