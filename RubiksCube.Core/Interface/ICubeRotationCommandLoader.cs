namespace RubiksCube.Core.Interface
{
    public interface ICubeRotationCommandLoader
    {
		Task<IList<IRotationCommand>> LoadCommandsAsync();
    }
}