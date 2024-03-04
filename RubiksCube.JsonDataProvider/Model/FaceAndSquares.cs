using RubiksCube.Core.Interface;

namespace RubiksCube.JsonDataProvider.Model
{
	public class FaceAndSquares : IFaceAndSquaresIndexes
	{
		public int FaceId { get; private set; }
		public int[] SquareIndexes { get; private set; }

		public FaceAndSquares(string faceRowColumn)
		{
			Validate(faceRowColumn);
			(FaceId, SquareIndexes) = Parse(faceRowColumn);
		}
		protected static (int, int[]) Parse(string faceRowColumn)
		{
			var faceID = faceRowColumn[1] - '0';
			var isRow = faceRowColumn[2] == 'R';
			var rowColumnID = faceRowColumn[3] - '0';
			var firstIndex = isRow ? rowColumnID * 3 : rowColumnID;
			var indices = new List<int>();
			for (int i = 0; i < 3; i++)
			{
				indices.Add(isRow ? firstIndex + i : i * 3 + rowColumnID);
			}
			var isInverted = faceRowColumn[4] == 'i';
			if (isInverted)
				indices.Reverse();

			var aIndices = indices.ToArray();
			return (faceID, aIndices);
		}
		protected static void Validate(string faceRowColumn)
		{
			if (faceRowColumn.Length != 4)
			{
				throw new ArgumentException($"Invalid faceRowColumn length {faceRowColumn}");
			}
			if (!faceRowColumn.StartsWith("F"))
			{
				throw new ArgumentException($"faceRowColumn should start with F {faceRowColumn}");
			}
			var faceID = faceRowColumn[1];
			if (!(faceID >= '0' && faceID <= '5'))
			{
				throw new ArgumentException($"Invalid face ID for faceRowColumn {faceRowColumn}");
			}
			var rowColumn = faceRowColumn[2];
			if (!(rowColumn == 'R' || rowColumn == 'C'))
			{
				throw new ArgumentException($"Expected row or column for faceRowColumn {faceRowColumn}");
			}
			var rowColumnID = faceRowColumn[3];
			if (!(rowColumnID >= '0' && rowColumnID <= '2'))
			{
				throw new ArgumentException($"Invalid row or column for faceRowColumn {faceRowColumn}");
			}
			var inverted = faceRowColumn[4];
			if (!(inverted == 'n' || inverted == 'i'))
			{
				throw new ArgumentException($"Expected faceRowColumn ending with 'i' or 'n' for {faceRowColumn}");
			}
		}
	}
}