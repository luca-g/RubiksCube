using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubiksCube.Core.Interface
{
	public interface IRotationCommand
	{
		public ICubeFaceRotation CubeFaceRotation { get; }
		public bool IsClockwise { get; }
		public void Execute(ICubeData cubeData);
	}
}
