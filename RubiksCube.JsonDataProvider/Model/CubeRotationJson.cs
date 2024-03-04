// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

using Newtonsoft.Json;

namespace RubiksCube.JsonDataProvider.Model
{

	public class CubeRotationJson
	{
		[JsonProperty("clockwiseKey")]
		public string? ClockwiseKey { get; set; }

		[JsonProperty("counterclockwiseKey")]
		public string? CounterclockwiseKey { get; set; }

		[JsonProperty("rotateFace")]
		public int? RotateFace { get; set; }

		[JsonProperty("rotateOther")]
		public List<string>? RotateOther { get; set; }
	}
}