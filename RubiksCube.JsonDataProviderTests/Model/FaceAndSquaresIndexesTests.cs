using Microsoft.VisualStudio.TestTools.UnitTesting;
using RubiksCube.JsonDataProvider.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubiksCube.JsonDataProvider.Model.Tests
{
	[TestClass()]
	public class FaceAndSquaresIndexesTests
	{
		public static string FaceAndSquaresIndexesToString(string text)
		{
			var face = new FaceAndSquaresIndexes(text);
			return $"{face.FaceId}{string.Join("", face.SquareIndexes)}";
		}
		
		[TestMethod()]
		[DataRow("F0R0n","0012")]
		[DataRow("F1R0n", "1012")]
		[DataRow("F0R1n", "0345")]
		[DataRow("F0R2n", "0678")]
		[DataRow("F0C0n", "0036")]
		[DataRow("F0C1n", "0147")]
		[DataRow("F0C2n", "0258")]
		[DataRow("F0R0i", "0210")]
		[DataRow("F1R0i", "1210")]
		[DataRow("F0R1i", "0543")]
		[DataRow("F0R2i", "0876")]
		[DataRow("F0C0i", "0630")]
		[DataRow("F0C1i", "0741")]
		[DataRow("F0C2i", "0852")]
		public void FaceAndSquaresIndexesTest(string text, string expected)
		{
			var f2s = FaceAndSquaresIndexesToString(text);
			Assert.AreEqual(expected, f2s);
		}
	}
}