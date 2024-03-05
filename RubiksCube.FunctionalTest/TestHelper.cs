using RubiksCube.Core.Interface;
using RubiksCube.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubiksCube.FunctionalTest
{
	public class TestHelper
	{
		public static string CubeToString(ICubeData cube)
		{
			var sb=new StringBuilder();
			for(int i=0;i<6;i++)
			{
				var face = cube.Faces[i];
				var values = string.Join("", face.Squares.Select(face => face.ToString().First()));
				sb.Append($"{i}:{values}");
				if(i<5)
					sb.Append(",");
			}
			return sb.ToString();
		}
		public static (ICubeFactory factory, Dictionary<string, IRotationCommand> commands) LoadFactoryAndCommands()
		{
			const string fileName = "Assets//rotations.json";
			Assert.IsTrue(File.Exists(fileName));

			var dataProvider = new JsonDataProvider.Model.JsonDataProvider(fileName);
			var factory = new CubeFactory(dataProvider);

			var rotationsLoader = factory.GetCubeRotationCommandLoader();
			var allCommands = rotationsLoader.LoadCommands();

			return (factory, allCommands.ToDictionary(t=>t.CommandName,t=>t));
		}
	}
}
