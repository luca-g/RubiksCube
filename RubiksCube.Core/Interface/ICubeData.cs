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
		ICubeFaceData[] Faces { get; }
		void Reset();
	}
}
