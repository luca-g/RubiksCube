using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using RubiksCube.Core.Model;

namespace RubiksCube.FunctionalTest
{
	[TestClass]
	public class RotationsTest
	{
		[TestMethod]
		public void TestStartingCube()
		{
			var (factory, commands) = TestHelper.LoadFactoryAndCommands();
			var cube = factory.InstantiateCube();
			var faces = TestHelper.CubeToString(cube);
			Assert.AreEqual(
				"0:WWWWWWWWW,1:OOOOOOOOO,2:GGGGGGGGG,3:RRRRRRRRR,4:BBBBBBBBB,5:YYYYYYYYY",
				faces);
		}
		[TestMethod]
		[DataRow("F", "0:WWWWWWOOO,1:OOYOOYOOY,2:GGGGGGGGG,3:WRRWRRWRR,4:BBBBBBBBB,5:RRRYYYYYY")]
		[DataRow("F,R'", "0:WWBWWBOOB,1:OOYOOYOOY,2:GGWGGWGGO,3:RRRRRRWWW,4:YBBYBBRBB,5:RRGYYGYYG")]
		[DataRow("F,R',U", "0:OWWOWWBBB,1:GGWOOYOOY,2:RRRGGWGGO,3:YBBRRRWWW,4:OOYYBBRBB,5:RRGYYGYYG")]
		[DataRow("F,R',U,B'", "0:OOGOWWBBB,1:YGWYOYGOY,2:RRRGGWGGO,3:YBORRWWWW,4:YBBOBBOYR,5:RRGYYGWRB")]
		[DataRow("F,R',U,B',L", "0:ROGBWWBBB,1:GYYOOGYYW,2:ORROGWBGO,3:YBORRWWWW,4:YBWOBYOYR,5:RRGGYGGRB")]
		[DataRow("F,R',U,B',L,D'", "0:ROGBWWBBB,1:GYYOOGBGO,2:ORROGWWWW,3:YBORRWOYR,4:YBWOBYYYW,5:GGBRYRRGG")]
		[DataRow("F,R',U,B',L,D',D", "0:ROGBWWBBB,1:GYYOOGYYW,2:ORROGWBGO,3:YBORRWWWW,4:YBWOBYOYR,5:RRGGYGGRB")]
		[DataRow("F,R',U,B',L,D',D,L'", "0:OOGOWWBBB,1:YGWYOYGOY,2:RRRGGWGGO,3:YBORRWWWW,4:YBBOBBOYR,5:RRGYYGWRB")]
		[DataRow("F,R',U,B',L,D',D,L',B", "0:OWWOWWBBB,1:GGWOOYOOY,2:RRRGGWGGO,3:YBBRRRWWW,4:OOYYBBRBB,5:RRGYYGYYG")]
		[DataRow("F,R',U,B',L,D',D,L',B,U'", "0:WWBWWBOOB,1:OOYOOYOOY,2:GGWGGWGGO,3:RRRRRRWWW,4:YBBYBBRBB,5:RRGYYGYYG")]
		[DataRow("F,R',U,B',L,D',D,L',B,U',R", "0:WWWWWWOOO,1:OOYOOYOOY,2:GGGGGGGGG,3:WRRWRRWRR,4:BBBBBBBBB,5:RRRYYYYYY")]
		public void TestRotations(string commandsText, string expected)
		{
			var (factory, commands) = TestHelper.LoadFactoryAndCommands();
			var cube = factory.InstantiateCube();
			foreach (var rotation in commandsText.Split(','))
			{
				var command = commands[rotation];
				command.Execute(cube);
			}
			var faces = TestHelper.CubeToString(cube);
			Assert.AreEqual(
				expected,
				faces);
		}
	}
}