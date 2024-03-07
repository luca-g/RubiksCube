using RubiksCube.Core.Interface;

namespace RubiksCube.JsonDataProvider.Model
{
	public interface IRotationsDataProvider
	{
		Task<IList<ICubeFaceRotation>> LoadAllRotationsAsync();
	}
}