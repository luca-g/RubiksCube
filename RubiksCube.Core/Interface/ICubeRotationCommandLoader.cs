namespace RubiksCube.Core.Interface
{
    public interface ICubeRotationCommandLoader
    {
        IList<IRotationCommand> LoadCommands();
    }
}