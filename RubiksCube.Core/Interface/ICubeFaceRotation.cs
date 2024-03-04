namespace RubiksCube.Core.Interface
{
	public interface ICubeFaceRotation
	{
		public string ClockwiseKey { get; }
		public string CounterclockwiseKey { get; }
		public int RotateFaceId { get; }
		public List<ISquaresCopySourceDestinationIndexes> CopySourceDestinationIndexes { get; set; }
	}
}
