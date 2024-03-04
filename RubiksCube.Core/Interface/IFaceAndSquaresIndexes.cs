namespace RubiksCube.Core.Interface
{
	public interface IFaceAndSquaresIndexes
	{
		public int FaceId { get; }
		public int[] SquareIndexes { get; }
	}
}
