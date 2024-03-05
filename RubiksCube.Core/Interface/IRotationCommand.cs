namespace RubiksCube.Core.Interface
{
	public interface IRotationCommand
	{
		string CommandName { get; }
		ICubeFaceRotation CubeFaceRotation { get; }
		bool IsClockwise { get; }
		void Execute(ICubeData cubeData);
	}
}
