using RubiksCube.Core.Interface;
using System.Collections.Generic;
namespace RubiksCube.JsonDataProvider.Model
{
	public class CubeFaceRotation : ICubeFaceRotation
	{
		public string ClockwiseKey { get; private set; }
		public string CounterclockwiseKey { get; private set; }
		public int RotateFaceId { get; private set; }
		public IList<ISquaresCopySourceDestinationIndexes> CopySourceDestinationIndexes { get; set; }
		public CubeFaceRotation(CubeRotationJson cubeRotation)
		{
			Validate(cubeRotation);
			ClockwiseKey = cubeRotation.ClockwiseKey!;
			CounterclockwiseKey = cubeRotation.CounterclockwiseKey!;
			RotateFaceId = cubeRotation.RotateFace!.Value;
			CopySourceDestinationIndexes = InstantiateSquaresCopySourceDestinationIndexe(cubeRotation.RotateOther!);
		}

		protected static void Validate(CubeRotationJson cubeRotation)
		{
			if (cubeRotation.ClockwiseKey == null)
				throw new System.Exception("Invalid ClockwiseKey CubeRotationJson");
			if (cubeRotation.CounterclockwiseKey == null)
				throw new System.Exception("Invalid CounterclockwiseKey CubeRotationJson");
			if (cubeRotation.RotateFace == null)
				throw new System.Exception("Invalid ClockwiseKey CubeRotationJson");
			if (cubeRotation.RotateOther == null)
				throw new System.Exception("Invalid RotateOther CubeRotationJson");
		}

		protected static List<ISquaresCopySourceDestinationIndexes> 
			InstantiateSquaresCopySourceDestinationIndexe(List<string> cubeRotations)
		{
			var rv = new List <ISquaresCopySourceDestinationIndexes> ();
			foreach (var t in cubeRotations)
			{
				rv.Add(new SquaresCopySourceDestinationIndexes(t));
			}
			return rv;
		}
	}
}
