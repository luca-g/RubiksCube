using Microsoft.VisualStudio.TestTools.UnitTesting;
using RubiksCube.Core.Interface;
using RubiksCube.CoreTests;
using System.Collections.Generic;
namespace RubiksCube.Core.Decorator.Rotate.Tests
{
	[TestClass()]
	public class CubeRotateTests
	{
		public class TestClass : CubeRotate
		{
			public ICubeData GetCubeData => _cubeData;
			public TestClass(ICubeData cubeData) : base(cubeData)
			{
			}
			public static void _SaveTempSquares(Dictionary<(int, int), eSquareColor> temp, ICubeData cubeData, ISquaresCopySourceDestinationIndexes faceAndSquaresIndexes, bool isClockwise)
			{
				CubeRotate.SaveTempSquares(temp, cubeData, faceAndSquaresIndexes, isClockwise);
			}
			public static void _RotateSquares(Dictionary<(int, int), eSquareColor> temp, ICubeData cubeData, ISquaresCopySourceDestinationIndexes faceAndSquaresIndexes, bool isClockwise)
			{
				CubeRotate.RotateSquares(temp, cubeData, faceAndSquaresIndexes, isClockwise);
			}
		}

		[TestMethod()]
		public void RotateTestClockwise()
		{
			var testClass = new TestClass(CubeTestHelper.GetTestCube());
			var facesIntexes = CubeTestHelper.GetSquaresCopySourceDestinationIndexes("0,0,1,2=1,0,1,2");
			var cube = testClass.GetCubeData;
			cube.Faces[1].Squares[0] = (eSquareColor)2;
			cube.Faces[1].Squares[1] = (eSquareColor)3;
			cube.Faces[1].Squares[2] = (eSquareColor)4;
			var temp = new Dictionary<(int, int), eSquareColor>();
			TestClass._SaveTempSquares(temp, testClass.GetCubeData, facesIntexes, true);
			TestClass._RotateSquares(temp, testClass.GetCubeData, facesIntexes, true);
			Assert.AreEqual(cube.Faces[0].Squares[0], cube.Faces[1].Squares[0]);
			Assert.AreEqual(cube.Faces[0].Squares[1], cube.Faces[1].Squares[1]);
			Assert.AreEqual(cube.Faces[0].Squares[2], cube.Faces[1].Squares[2]);
		}
		[TestMethod()]
		public void RotateTestCounterClockwise()
		{
			var testClass = new TestClass(CubeTestHelper.GetTestCube());
			var facesIntexes = CubeTestHelper.GetSquaresCopySourceDestinationIndexes("0,0,1,2=1,0,1,2");
			var cube = testClass.GetCubeData;
			cube.Faces[1].Squares[0] = (eSquareColor)2;
			cube.Faces[1].Squares[1] = (eSquareColor)3;
			cube.Faces[1].Squares[2] = (eSquareColor)4;
			var temp = new Dictionary<(int, int), eSquareColor>();
			TestClass._SaveTempSquares(temp, testClass.GetCubeData, facesIntexes, false);
			TestClass._RotateSquares(temp, testClass.GetCubeData, facesIntexes, false);
			Assert.AreEqual(cube.Faces[0].Squares[2], cube.Faces[1].Squares[0]);
			Assert.AreEqual(cube.Faces[0].Squares[1], cube.Faces[1].Squares[1]);
			Assert.AreEqual(cube.Faces[0].Squares[0], cube.Faces[1].Squares[2]);
		}
	}
}