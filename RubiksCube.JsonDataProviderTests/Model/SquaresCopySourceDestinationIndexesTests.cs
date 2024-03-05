using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace RubiksCube.JsonDataProvider.Model.Tests
{
	[TestClass()]
	public class SquaresCopySourceDestinationIndexesTests
	{
		public static string SquaresCopySourceDestinationIndexesToString(string text)
		{
			var parts = text.Split('=');
			return
				FaceAndSquaresIndexesTests.FaceAndSquaresIndexesToString(parts[0])
				+ "="
				+ FaceAndSquaresIndexesTests.FaceAndSquaresIndexesToString(parts[1]);
		}

		[TestMethod()]
		[DataRow("F0R2n=F1C1i", "0678=1741")]
		[DataRow("F0R2n=F1C1n", "0678=1147")]
		public void SquaresCopySourceDestinationIndexesTest(string text, string expected)
		{
			var indexes = SquaresCopySourceDestinationIndexesToString(text);
			Assert.AreEqual(expected, indexes);
		}
	}
}