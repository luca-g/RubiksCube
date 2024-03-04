using RubiksCube.Core.Decorator.Rotate;
using RubiksCube.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubiksCube.Core.Interface
{
	public interface ICubeFactory
	{
		public ICubeData InstantiateCube();
		public ICubeFaceData InstantiateFaceData(eSquareColor color);
		public IRotationCommand InstantiateRotationCommand(string commandName, ICubeFaceRotation cubeFaceRotation, bool isClockwise);
		public ICubeFaceRotateDecorator InstantiateCubeFaceRotateDecorator(ICubeFaceData cubeFaceData);
		public ICubeRotateDecorator InstantiateCubeRotateDecorator(ICubeData cubeData);
	}
}
