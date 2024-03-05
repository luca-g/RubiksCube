namespace RubiksCube.Core.Interface
{
	public interface ICubeFaceRotation
	{
		string ClockwiseKey { get; }
		string CounterclockwiseKey { get; }
		int RotateFaceId { get; }
		IList<ISquaresCopySourceDestinationIndexes> CopySourceDestinationIndexes { get; set; }
	}
}
