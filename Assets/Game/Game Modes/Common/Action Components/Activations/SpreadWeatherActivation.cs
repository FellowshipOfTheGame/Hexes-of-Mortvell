using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using HexesOfMortvell.Core.Actions;
using HexesOfMortvell.Core.Grid;

namespace HexesOfMortvell.GameModes.Common
{
	public class SpreadWeatherActivation : ActionActivation
	{
		public override void Perform(
			BoardCellContent actor,
			IEnumerable<BoardCell> targets,
			IEnumerable<BoardCell> aoe)
		{
			var weatherOrigin = targets.FirstOrDefault();
			var weather = weatherOrigin?.Weather;
			if (weather == null)
				return;
			foreach (var cell in aoe)
				weather.ApplyTo(cell);
		}

		public override void Cleanup(IEnumerable<BoardCell> aoe) {}
	}
}
