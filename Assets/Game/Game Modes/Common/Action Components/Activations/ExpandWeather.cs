using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using HexesOfMortvell.Core.Actions;
using HexesOfMortvell.Core.Grid;

namespace HexesOfMortvell.GameModes.Common
{
	public class ExpandWeather : ActionActivation
	{
		public override void Perform(
			BoardCellContent actor,
			IEnumerable<BoardCell> targets,
			IEnumerable<BoardCell> aoe)
		{
			var origin = targets.First();
			foreach (var cell in aoe)
				cell.ChangeWeather(origin.Weather.gameObject);
		}

		public override void Cleanup(IEnumerable<BoardCell> aoe) {}
	}
}