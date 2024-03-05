using RubiksCube.Core.Interface;

namespace RubiksCube.JsonDataProvider.Model
{
	public class SquaresCopySourceDestinationIndexes : ISquaresCopySourceDestinationIndexes
	{
		public IFaceAndSquaresIndexes Destination { get; private set; }

		public IFaceAndSquaresIndexes Source { get; private set; }
		public SquaresCopySourceDestinationIndexes(string copyIndexesString)
		{
			(Destination, Source) = DecodeCopyIndexesString(copyIndexesString);
		}
		protected static (IFaceAndSquaresIndexes, IFaceAndSquaresIndexes) DecodeCopyIndexesString(string copyIndexesString)
		{
			if (!copyIndexesString.Contains('='))
			{
				throw new Exception($"Invalid copy indexes string {copyIndexesString}");
			}
			var parts = copyIndexesString.Split('=');
			if (parts.Length != 2)
			{
				throw new Exception($"Invalid copy indexes string {copyIndexesString}");
			}
			var destination = new FaceAndSquaresIndexes(parts[0]);
			var source = new FaceAndSquaresIndexes(parts[1]);
			return (destination, source);
		}
	}
}
