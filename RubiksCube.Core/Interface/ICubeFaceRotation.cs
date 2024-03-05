namespace RubiksCube.Core.Interface
{
	public interface ICubeFaceRotation
	{
		public string ClockwiseKey { get; }
		public string CounterclockwiseKey { get; }
		public int RotateFaceId { get; }
		public IList<ISquaresCopySourceDestinationIndexes> CopySourceDestinationIndexes { get; set; }
	}
}
