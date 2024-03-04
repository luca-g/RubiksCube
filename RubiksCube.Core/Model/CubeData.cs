
using RubiksCube.Core.Interface;

namespace RubiksCube.Core.Model
{
    public class CubeData : ICubeData
    {
		private readonly ICubeFactory _factory;

		public ICubeFaceData[] Faces { get; private set; }
        protected static ICubeFaceData[] Instantiate(ICubeFactory factory)
        {
            var faces = new ICubeFaceData[6];
			faces[0] = factory.InstantiateFaceData(eSquareColor.White);
			faces[1] = factory.InstantiateFaceData(eSquareColor.Orange);
			faces[2] = factory.InstantiateFaceData(eSquareColor.Green);
			faces[3] = factory.InstantiateFaceData(eSquareColor.Red);
			faces[4] = factory.InstantiateFaceData(eSquareColor.Blue);
			faces[5] = factory.InstantiateFaceData(eSquareColor.Yellow);
            return faces;
		}
		public CubeData(ICubeFactory factory)
		{
			this._factory = factory;
			this.Faces = Instantiate(factory);
		}
		public void Reset()
		{
			this.Faces = Instantiate(_factory);
		}
    }
}
