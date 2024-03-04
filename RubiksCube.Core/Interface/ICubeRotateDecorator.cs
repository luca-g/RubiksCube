namespace RubiksCube.Core.Interface
{
    public interface ICubeRotateDecorator
    {
        void Rotate(ICubeFaceRotation cubeFaceRotation, bool isClockwise);
    }
}