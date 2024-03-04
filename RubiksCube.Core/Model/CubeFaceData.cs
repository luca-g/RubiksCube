
using RubiksCube.Core.Interface;

namespace RubiksCube.Core.Model
{
    public class CubeFaceData : ICubeFaceData
    {
        public eSquareColor StartColor;
        public eSquareColor[] Squares { get; private set; }
        public CubeFaceData(eSquareColor color)
        {
            StartColor = color;
            Squares = Instantiate(color);
        }
        protected static eSquareColor[] Instantiate(eSquareColor color)
        {
            var squares = new eSquareColor[9];
			for (int i = 0; i < 9; i++)
			{
				squares[i] = color;
			}
            return squares;
		}
    }
}
