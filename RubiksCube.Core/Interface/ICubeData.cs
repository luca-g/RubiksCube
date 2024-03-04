using RubiksCube.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubiksCube.Core.Interface
{
	public interface ICubeData
	{
		public ICubeFaceData[] Faces { get; }
		public void Reset();
	}
}
