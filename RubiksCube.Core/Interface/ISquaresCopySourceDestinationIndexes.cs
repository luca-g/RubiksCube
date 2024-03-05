﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubiksCube.Core.Interface
{
	public interface ISquaresCopySourceDestinationIndexes
	{
		IFaceAndSquaresIndexes Destination { get; }
		IFaceAndSquaresIndexes Source { get; }
	}
}
