using Microsoft.VisualStudio.TestTools.UnitTesting;
using RubiksCube.Core.Interface;
using RubiksCube.Core.Model;

namespace RubiksCube.Core.Decorator.Rotate.Tests
{
	[TestClass()]
	public class CubeFaceRotateTests
	{
		[TestMethod()]
		public void RotateClockwiseTest()
		{
			var face = new CubeFaceData(eSquareColor.White);
			face.Squares[0] = (eSquareColor)1;
			face.Squares[1] = (eSquareColor)2;
			face.Squares[2] = (eSquareColor)3;
			var rotate = new CubeFaceRotate(face);
			rotate.RotateClockwise();
			Assert.AreEqual(face.Squares[2], (eSquareColor)1);
			Assert.AreEqual(face.Squares[5], (eSquareColor)2);
			Assert.AreEqual(face.Squares[8], (eSquareColor)3);
		}
		[TestMethod()]
		public void RotateClockwiseTest2()
		{
			var face = new CubeFaceData(eSquareColor.White);
			face.Squares[3] = (eSquareColor)1;
			face.Squares[4] = (eSquareColor)2;
			face.Squares[5] = (eSquareColor)3;
			var rotate = new CubeFaceRotate(face);
			rotate.RotateClockwise();
			Assert.AreEqual(face.Squares[1], (eSquareColor)1);
			Assert.AreEqual(face.Squares[4], (eSquareColor)2);
			Assert.AreEqual(face.Squares[7], (eSquareColor)3);
		}
		[TestMethod()]
		public void RotateClockwiseTest3()
		{
			var face = new CubeFaceData(eSquareColor.White);
			face.Squares[2] = (eSquareColor)1;
			face.Squares[5] = (eSquareColor)2;
			face.Squares[8] = (eSquareColor)3;
			var rotate = new CubeFaceRotate(face);
			rotate.RotateClockwise();
			Assert.AreEqual(face.Squares[8], (eSquareColor)1);
			Assert.AreEqual(face.Squares[7], (eSquareColor)2);
			Assert.AreEqual(face.Squares[6], (eSquareColor)3);
		}
		[TestMethod]
		public void RotateCounterClockwiseTest()
		{
			var face = new CubeFaceData(eSquareColor.White);
			face.Squares[0] = (eSquareColor)1;
			face.Squares[1] = (eSquareColor)2;
			face.Squares[2] = (eSquareColor)3;
			var rotate = new CubeFaceRotate(face);
			rotate.RotateCounterClockwise();
			Assert.AreEqual(face.Squares[6], (eSquareColor)1);
			Assert.AreEqual(face.Squares[3], (eSquareColor)2);
			Assert.AreEqual(face.Squares[0], (eSquareColor)3);
		}
	}
}