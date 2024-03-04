// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
using Newtonsoft.Json;

namespace RubiksCube.JsonDataProvider.Model
{
	public class CubeRotationJsonRoot
	{
		[JsonProperty("rotations")]
		public List<CubeRotationJson>? Rotations { get; set; }
	}
}