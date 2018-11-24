using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexesOfMortvell.Core.Grid
{
	public class WeatheredCellCount
	{
		public string weatherType;
		public int count;

		public WeatheredCellCount(string weatherType, int count)
		{
			this.weatherType = weatherType;
			this.count = count;
		}
	}
}
