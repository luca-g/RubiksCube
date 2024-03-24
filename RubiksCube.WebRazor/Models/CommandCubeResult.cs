using RubiksCube.Core.Interface;

namespace RubiksCube.WebRazor.Models
{
    public record CommandCubeResult(ICubeData? resultCube, string rotationString, bool blockRotations, string? error = null);
}
