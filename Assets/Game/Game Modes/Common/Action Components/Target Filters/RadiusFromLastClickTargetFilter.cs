using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using HexesOfMortvell.Core.Actions;
using HexesOfMortvell.Core.Grid;
using HexesOfMortvell.Core.Grid.Regions;
using HexesOfMortvell.Core.Units;

namespace HexesOfMortvell.GameModes.Common
{
	public class RadiusFromLastClickTargetFilter : ActionTargetFilter
	{
		public int radius;
		public bool containUnit;
		public bool containWeather;

		public override IEnumerable<BoardCell> ValidTargets(
			BoardCellContent actor,
			IEnumerable<BoardCell> partialTargets)
		{
			var possibleTargets =
				GetRadiusFromLastClick(actor, partialTargets, radius);
			var validTargets = new List<BoardCell>();
			if (!this.containUnit && !this.containWeather) {
				return possibleTargets;
			}
			else {
				foreach (var cell in possibleTargets) {
					if ((this.containWeather && (cell.GetWeather() != null)) ||
							(this.containUnit && (cell.GetContent() != null) &&
							(cell.GetContent().GetComponent<Unit>() != null))) {
						validTargets.Add(cell);
					}
				}
				return validTargets;
			}
		}

		public IEnumerable<BoardCell> GetRadiusFromLastClick(
			BoardCellContent actor,
			IEnumerable<BoardCell> partialTargets,
			int radius = 0)
		{
			var lastTarget = partialTargets.LastOrDefault();
			var radiusCenter = lastTarget ?? actor.Cell;
			return radiusCenter.Neighborhood(radius);
		}
	}
}
