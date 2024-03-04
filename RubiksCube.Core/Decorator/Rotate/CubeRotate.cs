using RubiksCube.Core.Interface;

namespace RubiksCube.Core.Decorator.Rotate
{
    public class CubeRotate : ICubeRotateDecorator
	{
		private readonly ICubeData _cubeData;
		public CubeRotate(ICubeData cubeData)
		{
			_cubeData = cubeData;
		}
		public void Rotate(ICubeFaceRotation cubeFaceRotation, bool isClockwise)
		{
			var face = _cubeData.Faces[cubeFaceRotation.RotateFaceId];
			var cubeFaceRotate = new CubeFaceRotate(face);
			if (isClockwise)
			{
				cubeFaceRotate.RotateClockwise();
			}
			else
			{
				cubeFaceRotate.RotateCounterClockwise();
			}
			foreach (var faceAndSquaresIndexes in cubeFaceRotation.CopySourceDestinationIndexes)
			{
				var source = isClockwise ? faceAndSquaresIndexes.Source : faceAndSquaresIndexes.Destination;
				var destination = isClockwise ? faceAndSquaresIndexes.Destination : faceAndSquaresIndexes.Source;
				var temp = new Dictionary<(int, int), eSquareColor>();
				for (int i = 0; i < source.SquareIndexes.Length; i++)
				{
					temp.Add(
						(source.FaceId, source.SquareIndexes[i]),
						_cubeData.Faces[source.FaceId].Squares[source.SquareIndexes[i]]
						);
				}
				for (int i = 0; i < source.SquareIndexes.Length; i++)
				{
					_cubeData.Faces[destination.FaceId].Squares[destination.SquareIndexes[i]] =
						temp[(source.FaceId, source.SquareIndexes[i])];
				}
			}
		}
	}
}
