using Microsoft.VisualStudio.TestTools.UnitTesting;
using RubiksCube.JsonDataProvider.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubiksCube.JsonDataProvider.Model.Tests
{
	[TestClass()]
	public class JsonDataProviderTests
	{
		[TestMethod()]
		public void LoadAllRotationsTest()
		{
			const string fileName = "Assets//rotations.json";
			Assert.IsTrue(File.Exists(fileName));
			var dataProvider = new JsonDataProvider(fileName);
			var rotations = dataProvider.LoadAllRotations();
			Assert.IsTrue(rotations.Any());
			Assert.IsTrue(rotations.Count == 6);
		}
	}
}