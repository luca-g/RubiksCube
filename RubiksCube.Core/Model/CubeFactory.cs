using RubiksCube.Core.Decorator.Rotate;
using RubiksCube.Core.Interface;
using RubiksCube.JsonDataProvider.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubiksCube.Core.Model
{
	public class CubeFactory : ICubeFactory
	{
		private readonly IRotationsDataProvider _rotationsDataProvider;
		public IRotationsDataProvider GetRotationsDataProvider()
		{
			return _rotationsDataProvider;
		}
		public CubeFactory(IRotationsDataProvider rotationsDataProvider)
		{
			_rotationsDataProvider = rotationsDataProvider;
		}
		public ICubeData InstantiateCube()
		{
			return new CubeData(this);
		}
		public ICubeFaceData InstantiateFaceData(eSquareColor color)
		{
			return new CubeFaceData(color);
		}
		public IRotationCommand InstantiateRotationCommand(string commandName, ICubeFaceRotation cubeFaceRotation, bool isClockwise)
		{
			return new CubeRotationCommand(this, commandName, cubeFaceRotation, isClockwise);
		}
		public ICubeFaceRotateDecorator InstantiateCubeFaceRotateDecorator(ICubeFaceData cubeFaceData)
		{
			return new CubeFaceRotate(cubeFaceData);
		}
		public ICubeRotateDecorator InstantiateCubeRotateDecorator(ICubeData cubeData)
		{
			return new CubeRotate(cubeData);
		}
		public ICubeRotationCommandLoader GetCubeRotationCommandLoader()
		{
			return new CubeRotationCommandLoader(this);
		}
	}
}
