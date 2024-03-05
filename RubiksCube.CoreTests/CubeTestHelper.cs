using Moq;
using RubiksCube.Core.Interface;
using RubiksCube.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubiksCube.CoreTests
{
	public class CubeTestHelper
	{
		public static ICubeData GetTestCube()
		{
			Mock<ICubeFactory> factoryMock = new();
			factoryMock.Setup(factory => factory.InstantiateFaceData(It.IsAny<eSquareColor>()))
				.Returns((eSquareColor color) => new CubeFaceData(color));
			return new CubeData(factoryMock.Object);
		}
		public class FaceAndSquaresIndexes : IFaceAndSquaresIndexes
		{
			public int FaceId { get; private set; }
			public int[] SquareIndexes { get; private set; }
			public FaceAndSquaresIndexes(string indexesString)
			{
				var values = indexesString.Split(',');
				var intValues = values.Select(value => int.Parse(value));
				FaceId = intValues.First();
				SquareIndexes = intValues.Skip(1).ToArray();
			}
		}
		public static IFaceAndSquaresIndexes GetFaceAndSquaresIndexes(string indexesString)
		{
			return new FaceAndSquaresIndexes(indexesString);
		}
		public class SquaresCopySourceDestinationIndexes : ISquaresCopySourceDestinationIndexes
		{
			public IFaceAndSquaresIndexes Destination { get; private set; }

			public IFaceAndSquaresIndexes Source { get; private set; }
			public SquaresCopySourceDestinationIndexes(string copyIndexesString)
			{
				var parts = copyIndexesString.Split('=');
				Destination = GetFaceAndSquaresIndexes(parts[0]);
				Source = GetFaceAndSquaresIndexes(parts[1]);
			}
		}
		public static ISquaresCopySourceDestinationIndexes GetSquaresCopySourceDestinationIndexes(string indexesString)
		{
			return new SquaresCopySourceDestinationIndexes(indexesString);
		}
	}
}
