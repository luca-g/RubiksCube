using RubiksCube.Core.Interface;

namespace RubiksCube.JsonDataProvider.Model
{
	public interface IRotationsDataProvider
	{
		IList<ICubeFaceRotation> LoadAllRotations();
	}
}