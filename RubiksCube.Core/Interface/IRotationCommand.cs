namespace RubiksCube.Core.Interface
{
	public interface IRotationCommand
	{
		public ICubeFaceRotation CubeFaceRotation { get; }
		public bool IsClockwise { get; }
		public void Execute(ICubeData cubeData);
	}
}
