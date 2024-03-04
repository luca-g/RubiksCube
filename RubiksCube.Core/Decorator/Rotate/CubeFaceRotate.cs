using RubiksCube.Core.Interface;

namespace RubiksCube.Core.Decorator.Rotate
{
    public class CubeFaceRotate : ICubeFaceRotateDecorator
	{
		private readonly ICubeFaceData _cubeFaceData;
		public CubeFaceRotate(ICubeFaceData cubeFaceData)
		{
			_cubeFaceData = cubeFaceData;
		}
		public void RotateClockwise()
		{
			eSquareColor[] temp = new eSquareColor[9];
			_cubeFaceData.Squares.CopyTo(temp, 0);
			_cubeFaceData.Squares[0] = temp[6];
			_cubeFaceData.Squares[1] = temp[3];
			_cubeFaceData.Squares[2] = temp[0];
			_cubeFaceData.Squares[3] = temp[7];
			_cubeFaceData.Squares[5] = temp[1];
			_cubeFaceData.Squares[6] = temp[8];
			_cubeFaceData.Squares[7] = temp[5];
			_cubeFaceData.Squares[8] = temp[2];
		}
		public void RotateCounterClockwise()
		{
			eSquareColor[] temp = new eSquareColor[9];
			_cubeFaceData.Squares.CopyTo(temp, 0);
			_cubeFaceData.Squares[0] = temp[2];
			_cubeFaceData.Squares[1] = temp[5];
			_cubeFaceData.Squares[2] = temp[8];
			_cubeFaceData.Squares[3] = temp[1];
			_cubeFaceData.Squares[5] = temp[7];
			_cubeFaceData.Squares[6] = temp[0];
			_cubeFaceData.Squares[7] = temp[3];
			_cubeFaceData.Squares[8] = temp[6];
		}
	}
}
