﻿using RubiksCube.Core.Interface;

namespace RubiksCube.Core.Decorator.Rotate
{
    public class CubeRotate : ICubeRotateDecorator
	{
		protected readonly ICubeData _cubeData;
		public CubeRotate(ICubeData cubeData)
		{
			_cubeData = cubeData;
		}
		public void Rotate(ICubeFaceRotation cubeFaceRotation, bool isClockwise)
		{
			var face = _cubeData.Faces[cubeFaceRotation.RotateFaceId];
			RotateFace(face, isClockwise);
			RotateSquares(cubeFaceRotation, isClockwise);
		}
		protected void RotateFace(ICubeFaceData face, bool isClockwise)
		{
			var cubeFaceRotate = new CubeFaceRotate(face);
			if (isClockwise)
			{
				cubeFaceRotate.RotateClockwise();
			}
			else
			{
				cubeFaceRotate.RotateCounterClockwise();
			}
		}
		protected void RotateSquares(ICubeFaceRotation cubeFaceRotation, bool isClockwise)
		{
			var temp = new Dictionary<(int, int), eSquareColor>();
			foreach (var faceAndSquaresIndexes in cubeFaceRotation.CopySourceDestinationIndexes)
			{
				SaveTempSquares(temp, _cubeData, faceAndSquaresIndexes, isClockwise);
			}

			foreach (var faceAndSquaresIndexes in cubeFaceRotation.CopySourceDestinationIndexes)
			{
				RotateSquares(temp, _cubeData, faceAndSquaresIndexes, isClockwise);
			}
		}
		protected static void SaveTempSquares(Dictionary<(int, int), eSquareColor> temp, ICubeData cubeData, ISquaresCopySourceDestinationIndexes faceAndSquaresIndexes, bool isClockwise)
		{
			var source = isClockwise ? faceAndSquaresIndexes.Source : faceAndSquaresIndexes.Destination;
			for (int i = 0; i < source.SquareIndexes.Length; i++)
			{
				temp.Add(
					(source.FaceId, source.SquareIndexes[i]),
					cubeData.Faces[source.FaceId].Squares[source.SquareIndexes[i]]
					);
			}
		}
		protected static void RotateSquares(Dictionary<(int, int), eSquareColor> temp, ICubeData cubeData, ISquaresCopySourceDestinationIndexes faceAndSquaresIndexes, bool isClockwise)
		{
			var source = isClockwise ? faceAndSquaresIndexes.Source : faceAndSquaresIndexes.Destination;
			var destination = isClockwise ? faceAndSquaresIndexes.Destination : faceAndSquaresIndexes.Source;
			for (int i = 0; i < source.SquareIndexes.Length; i++)
			{
				cubeData.Faces[destination.FaceId].Squares[destination.SquareIndexes[i]] =
					temp[(source.FaceId, source.SquareIndexes[i])];
			}
		}
	}
}
