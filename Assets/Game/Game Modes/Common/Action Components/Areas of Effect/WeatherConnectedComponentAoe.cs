using UnityEngine;
using HexesOfMortvell.Core.Actions;
using HexesOfMortvell.Core.Grid;
using HexesOfMortvell.Core.Grid.Regions;
using System.Collections.Generic;
using System.Linq;

namespace HexesOfMortvell.GameModes.Common
{
	public class WeatherConnectedComponentAoe : ActionAoe
	{
		public GameObject expandingWeather;
		public bool alwaysIncludeTargets = true;

		public override IEnumerable<BoardCell> GetAoe(
			IEnumerable<BoardCell> targets)
		{
			return targets.SelectMany(
				target => target.WeatherConnectedComponent(
					expandingWeather,
					alwaysIncludeSeed: this.alwaysIncludeTargets));
		}
	}
}
